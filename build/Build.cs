using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.SonarScanner;
using Nuke.Common.Utilities.Collections;
using Octokit;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.SonarScanner.SonarScannerTasks;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;

    [Parameter("Version to be injected in the Build")]
    public string Version { get; set; } = $"1.2.1";

    [Parameter("The Buildnumber provided by the CI")]
    public int BuildNo = 2;

    [Parameter("Is RC Version")]
    public bool IsRc = false;

    [Parameter("URL of the SonarQube Server")]
    public string SonarServer = "";

    [Parameter("Login Token of the SonarQube Server")]
    public string SonarToken = "";

    AbsolutePath TestsDirectory => RootDirectory / "Tests";

    AbsolutePath ArtifactsDirectory => RootDirectory / "Artifacts";

    AbsolutePath DeployPath => (AbsolutePath)"C:" / "Projects" / "NuGet Store";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetVersion($"{Version}.{BuildNo}")
                .SetAssemblyVersion($"{Version}.{BuildNo}")
                .SetFileVersion(Version)
                .SetInformationalVersion($"{Version}.{BuildNo}")
                .AddProperty("PackageVersion", PackageVersion)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetNoBuild(true)
                .EnableNoRestore());
        });

    Target Release => _ => _
        //.DependsOn(Clean)
        //.DependsOn(Compile)
        .DependsOn(Test)
        .Executes(() =>
        {
            // copy to artifacts folder
            foreach (var file in Directory.GetFiles(RootDirectory, $"*.{PackageVersion}.nupkg", SearchOption.AllDirectories))
            {
                CopyFile(file, ArtifactsDirectory / Path.GetFileName(file), FileExistsPolicy.Overwrite);
            }

            foreach (var file in Directory.GetFiles(RootDirectory, $"*.{PackageVersion}.snupkg", SearchOption.AllDirectories))
            {
                CopyFile(file, ArtifactsDirectory / Path.GetFileName(file), FileExistsPolicy.Overwrite);
            }
        });

    Target Deploy => _ => _
        .DependsOn(Release)
        .Executes(() =>
        {
            // copy to local store
            foreach (var file in Directory.GetFiles(ArtifactsDirectory, $"*.{PackageVersion}.nupkg", SearchOption.AllDirectories))
            {
                CopyFile(file, DeployPath / Path.GetFileName(file), FileExistsPolicy.Overwrite);
            }

            foreach (var file in Directory.GetFiles(ArtifactsDirectory, $"*.{PackageVersion}.snupkg", SearchOption.AllDirectories))
            {
                CopyFile(file, DeployPath / Path.GetFileName(file), FileExistsPolicy.Overwrite);
            }
        });

    Target Sonar => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            Serilog.Log.Write(Serilog.Events.LogEventLevel.Information, "start sonar");

            SonarScannerBegin(s => s
                .SetProjectKey("WickedFlame_WorkflowValidation")
                .SetName("WorkflowValidation")
                .SetOrganization("wickedflame")
                .SetFramework("net5.0")
                .SetServer(SonarServer)
                .SetLogin(SonarToken)
                .SetOpenCoverPaths("src/Tests/**/coverage.opencover.xml"));

            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration.Debug)
                .EnableNoRestore());

            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration.Debug)
                .SetNoBuild(true)
                .EnableNoRestore()
                .EnableCollectCoverage()
                .SetFramework("net6.0")
                .SetCoverletOutputFormat(CoverletOutputFormat.opencover));

            Serilog.Log.Write(Serilog.Events.LogEventLevel.Information, "end sonar");

            SonarScannerEnd(s => s
                .SetFramework("net5.0")
                .SetLogin(SonarToken));
        });

    string PackageVersion
        => IsRc ? BuildNo < 10 ? $"{Version}-RC0{BuildNo}" : $"{Version}-RC{BuildNo}" : Version;

}
