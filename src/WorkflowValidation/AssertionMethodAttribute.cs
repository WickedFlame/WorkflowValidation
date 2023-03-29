using System;

namespace WorkflowValidation
{
    /// <summary>
    /// Used to mark Methods as assertion methods.
    /// This is used to tell Sonar scans that the method is used to throw assertions
    /// </summary>
    public class AssertionMethodAttribute : Attribute
    {
    }
}
