# Change Log
All notable changes to this project will be documented in this file.
 
The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## v1.2.1
### Fixed
- API after Wait returned wrong interface
 
## v1.2.0
### Changed
- WorkflowExceptions now contain all Steps as message
- StackTrace in WorkflowExceptions is now at the root of the Run to minimize the output
- Optimized the output of a WorkflowException

### Added
- Setups for workflows can be created in any flavour
- Set description of workflow directly in the WorkflowSetup
- WorkflowContext stores all log messages

### Changed
- Workflowdescription is now presented prominent in output

## v1.1.0
### Added
- Setup for workflows containing a description that gets output
  
### Changed
- Breaking: Refactor Static StartWith methods from WorkflowBuilder to Workflow
 
## v1.0.1
### Added
- Add multiple Assertions to Verificationstep

## v1.0.0
### Added
 Initial release

 
