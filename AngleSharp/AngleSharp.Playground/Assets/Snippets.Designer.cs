﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleInteraction.Assets {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Snippets {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Snippets() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ConsoleInteraction.Assets.Snippets", typeof(Snippets).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;p&gt;
        /// &lt;svg&gt;
        ///  &lt;metadata&gt;
        ///   &lt;!-- this is invalid --&gt;
        ///   &lt;cdr:license xmlns:cdr=&quot;http://www.example.com/cdr/metadata&quot; name=&quot;MIT&quot; /&gt;
        ///  &lt;/metadata&gt;
        /// &lt;/svg&gt;
        ///&lt;/p&gt;.
        /// </summary>
        internal static string Invalid {
            get {
                return ResourceManager.GetString("Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;head&gt;&lt;/head&gt;
        ///&lt;title&gt;hi&lt;/title&gt;
        ///&lt;p&gt;&lt;/p&gt;
        ///&lt;strong&gt;hi&lt;/strong&gt;.
        /// </summary>
        internal static string NormalClosedP {
            get {
                return ResourceManager.GetString("NormalClosedP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;head&gt;&lt;/head&gt;
        ///&lt;title&gt;hi&lt;/title&gt;
        ///&lt;p/&gt;
        ///&lt;strong&gt;hi&lt;/strong&gt;.
        /// </summary>
        internal static string SelfClosedP {
            get {
                return ResourceManager.GetString("SelfClosedP", resourceCulture);
            }
        }
    }
}
