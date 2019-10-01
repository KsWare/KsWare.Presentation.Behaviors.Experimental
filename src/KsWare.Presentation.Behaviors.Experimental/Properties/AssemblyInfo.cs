using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;


[assembly: AssemblyTitle("KsWare.Presentation.Behaviors.Experimental")]
[assembly: AssemblyDescription("KsWare.Presentation.Behaviors.Experimental")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("KsWare")]
[assembly: AssemblyProduct("KsWare.Presentation.Behaviors.Experimental")]
[assembly: AssemblyCopyright("Copyright © 2019 by KsWare. All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]


[assembly: ComVisible(false)]

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]
//[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

[assembly: AssemblyVersion("0.0.0.0")]
[assembly: AssemblyFileVersion("0.0.0.0")]
[assembly: AssemblyInformationalVersion("0.0.0.0")]

[assembly: XmlnsDefinition(KsWare.Presentation.Behaviors.Experimental.AssemblyInfo.XmlNamespace, "KsWare.Presentation.Behaviors.Experimental")]
[assembly: XmlnsPrefix(KsWare.Presentation.Behaviors.Experimental.AssemblyInfo.XmlNamespace, "ksv")]

//[assembly: InternalsVisibleTo("KsWare.Presentation.Behaviors.Experimental.Tests, PublicKey=$PublicKey$")]

// namespace must equal to assembly name
// ReSharper disable once CheckNamespace
namespace KsWare.Presentation.Behaviors.Experimental
{
	public static class AssemblyInfo
	{

		public static Assembly Assembly => Assembly.GetExecutingAssembly();

		public const string XmlNamespace = "http://ksware.de/Presentation/ViewFramework";

		public const string RootNamespace = "KsWare.Presentation.Behaviors.Experimental";

	}
}