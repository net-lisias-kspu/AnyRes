using System.Reflection;
using System.Runtime.CompilerServices;

// Information about this assembly is defined by the following attributes.
// Change them to the values specific to your project.

[assembly: AssemblyTitle ("AnyRes /L Unleashed")]
[assembly: AssemblyDescription ("A simple KSP mod to change to any resolution.")]
[assembly: AssemblyConfiguration ("")]
[assembly: AssemblyCompany (AnyRes.LegalMamboJambo.Company)]
[assembly: AssemblyProduct (AnyRes.LegalMamboJambo.Product)]
[assembly: AssemblyCopyright (AnyRes.LegalMamboJambo.Copyright)]
[assembly: AssemblyTrademark (AnyRes.LegalMamboJambo.Trademark)]
[assembly: AssemblyCulture ("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

//[assembly: AssemblyVersion ("1.0.*")]

// The following attributes are used to specify the signing key for the assembly,
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

[assembly: AssemblyVersion(AnyRes.Version.Number)]
[assembly: KSPAssemblyDependency("KSPe", 2, 4)]
[assembly: KSPAssemblyDependency("KSPe.UI", 2, 4)]
