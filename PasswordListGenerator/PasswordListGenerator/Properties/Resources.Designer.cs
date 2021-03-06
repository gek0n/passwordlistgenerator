﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PasswordListGenerator.Properties {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PasswordListGenerator.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to --delimiter
        ///		Use this option to specify delimiter, which should be paste between 
        ///		elements in combined string. By default it has &lt;space&gt; value.
        ///		Example:
        ///			PasswordListGenerator comb -i textFile.txt --delimiter a
        ///
        ///		&lt;-*textFile content*-&gt;
        ///			0
        ///			1
        ///			2
        ///
        ///		Output:
        ///			0a1
        ///			0a2
        ///			1a0
        ///			1a2
        ///			2a0
        ///			2a1
        ///
        ///		If you want to use not alphanumeric symbols as delimiter, then you 
        ///		better should use &quot;&quot; (double quotes) to specify the symbols.
        ///		Example:
        ///			PasswordListGenerator comb -i  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AdditionalComb_delimiter_Usage {
            get {
                return ResourceManager.GetString("AdditionalComb_delimiter_Usage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -m, --max-length
        ///		Use this options to specify maximum number of source words in result 
        ///		combinations. It has 2 by default.
        ///		Example:
        ///			PasswordListGenerator comb -i textFile.txt -m 3
        ///
        ///		&lt;-*textFile content*-&gt;
        ///			0
        ///			1
        ///			2
        ///
        ///		Output:
        ///			0 1 2
        ///			0 2 1
        ///			1 0 2
        ///			1 2 0
        ///			2 0 1
        ///			2 1 0
        ///
        ///		If set max-length value more than count of source words, then you need 
        ///		to set &quot;repetitions&quot; option, else error will be occured.
        ///
        ///		Example:
        ///			PasswordListGenerator comb -i textFile.txt -m  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AdditionalComb_m_Usage {
            get {
                return ResourceManager.GetString("AdditionalComb_m_Usage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to --prefix
        ///		Usi this option if you want specify prefix for every combined string. 
        ///		By default it has not value.
        ///		Example:
        ///			PasswordListGenerator comb -i textFile.txt --prefix a
        ///
        ///		&lt;-*textFile content*-&gt;
        ///			0
        ///			1
        ///			2
        ///
        ///		Output:
        ///			a0 1
        ///			a0 2
        ///			a1 0
        ///			a1 2
        ///			a2 0
        ///			a2 1
        ///
        ///		If you want to use not alphanumeric symbols as prefix, then you better 
        ///		should use &quot;&quot; (double quotes) to specify the symbols.
        ///		Example:
        ///			PasswordListGenerator comb -i textFile.txt --prefix=&quot;&lt;|&gt;&quot;
        ///.
        /// </summary>
        internal static string AdditionalComb_prefix_Usage {
            get {
                return ResourceManager.GetString("AdditionalComb_prefix_Usage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -r, --repetition
        ///		Use this option if you want to include variants of combination, that 
        ///		contains repetitions of source symbols.
        ///		Example:
        ///			PasswordListGenerator comb -i textFile.txt -r
        ///
        ///		&lt;-*textFile content*-&gt;
        ///			0
        ///			1
        ///			2
        ///
        ///		Output:
        ///			0 0
        ///			0 1
        ///			0 2
        ///			1 0
        ///			1 1
        ///			1 2
        ///			2 0
        ///			2 1
        ///			2 2
        ///.
        /// </summary>
        internal static string AdditionalComb_r_Usage {
            get {
                return ResourceManager.GetString("AdditionalComb_r_Usage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to --suffix
        ///		Usi this option if you want specify suffix for every combined string. 
        ///		By default it has not value.
        ///		Example:
        ///			PasswordListGenerator comb -i textFile.txt --suffix a
        ///
        ///		&lt;-*textFile content*-&gt;
        ///			0
        ///			1
        ///			2
        ///
        ///		Output:
        ///			0 1a
        ///			0 2a
        ///			1 0a
        ///			1 2a
        ///			2 0a
        ///			2 1a
        ///
        ///		If you want to use not alphanumeric symbols as suffix, then you better 
        ///		should use &quot;&quot; (double quotes) to specify the symbols.
        ///		Example:
        ///			PasswordListGenerator comb -i textFile.txt --suffix=&quot;&lt;|&gt;&quot;
        ///.
        /// </summary>
        internal static string AdditionalComb_suffix_Usage {
            get {
                return ResourceManager.GetString("AdditionalComb_suffix_Usage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -i
        ///		Use this option if you want to substitute many words typed form keyboard 
        ///		or loaded from input stream. For example, if you type:
        ///
        ///			PasswordListGenerator subs -i
        ///
        ///		You will see folowing:
        ///		Selected method is GoodLeet
        ///		
        ///		Now you can type any word or symbol and substitutions will appear in the 
        ///		console:
        ///		&lt;Q&gt; (typed)
        ///		Q
        ///		(,)
        ///		&lt;B&gt; (typed)
        ///		B
        ///		|3
        ///		8
        ///		&lt;q&gt; (typed)
        ///		[ERROR]: The symbol &quot;q&quot; is not in the dictionary. Please specify other 
        ///		dictionary or use ignore-case optio [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AdditionalSubs_i_Usages {
            get {
                return ResourceManager.GetString("AdditionalSubs_i_Usages", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -d, --dict
        ///		Use this option to specify file, that contains json data in folowing 
        ///		format:
        ///		{
        ///			&quot;method_1&quot;: {
        ///				&quot;A&quot;: [
        ///					&quot;a&quot;,
        ///					&quot;/-\&quot;,
        ///				],
        ///				&quot;1&quot;: [
        ///					&quot;|&quot;,
        ///					&quot;i&quot;,
        ///					&quot;I&quot;
        ///				]
        ///				...
        ///			},
        ///			&quot;method_2&quot;: {
        ///				&quot;A&quot;: [...],
        ///				...
        ///			},
        ///			...
        ///		}
        ///
        ///		Now if you type: 
        ///		PasswordListGenerator subs -d dictFilename.json -m method_1 A
        ///
        ///		You will see folowing text in console:
        ///		A
        ///		a
        ///		/-\
        ///		It&apos;s all possible substitutions for symbol &quot;A&quot; in method &quot;method_1&quot; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AdditionalSubsDictUsage {
            get {
                return ResourceManager.GetString("AdditionalSubsDictUsage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -m, --method
        ///		Use this option to specify method, that containing dictionaty of symbols
        ///		with all possible substitutions	for each. If you specify file with user 
        ///		dictionary, then you must use methods, written in this file 
        ///		(see -d option). If method does not specified, default method will be 
        ///		used. First method in dictionary will be used as default. In default 
        ///		file &quot;GoodLeet&quot; method is used by default.
        ///.
        /// </summary>
        internal static string AdditionalSubsMethodUsage {
            get {
                return ResourceManager.GetString("AdditionalSubsMethodUsage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Additional information for options:.
        /// </summary>
        internal static string additionalUsage {
            get {
                return ResourceManager.GetString("additionalUsage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error using {0} encoding. Fallback to utf-8.
        /// </summary>
        internal static string encodingFallback {
            get {
                return ResourceManager.GetString("encodingFallback", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///	&quot;GoodLeet&quot;: {
        ///		&quot;A&quot;: [
        ///			&quot;4&quot;,
        ///			&quot;/\\&quot;,
        ///			&quot;@&quot;,
        ///			&quot;/-\\&quot;
        ///		],
        ///		&quot;B&quot;: [
        ///			&quot;|3&quot;,
        ///			&quot;8&quot;
        ///		],
        ///		&quot;C&quot;: [
        ///			&quot;[&quot;,
        ///			&quot;{&quot;,
        ///			&quot;(&quot;,
        ///			&quot;&lt;&quot;
        ///		],
        ///		&quot;D&quot;: [
        ///			&quot;|)&quot;,
        ///			&quot;|}&quot;,
        ///			&quot;|]&quot;,
        ///			&quot;|&gt;&quot;
        ///		],
        ///		&quot;E&quot;: [
        ///			&quot;3&quot;
        ///		],
        ///		&quot;F&quot;: [
        ///			&quot;|=&quot;,
        ///			&quot;/=&quot;
        ///		],
        ///		&quot;G&quot;: [
        ///			&quot;9&quot;,
        ///			&quot;6&quot;
        ///		],
        ///		&quot;H&quot;: [
        ///			&quot;/-/&quot;,
        ///			&quot;]-[&quot;,
        ///			&quot;|-|&quot;,
        ///			&quot;\\-\\&quot;,
        ///			&quot;}-{&quot;,
        ///			&quot;)-(&quot;,
        ///			&quot;!-!&quot;,
        ///			&quot;/~/&quot;,
        ///			&quot;]~[&quot;,
        ///			&quot;|~|&quot;,
        ///			&quot;\\~\\&quot;,
        ///			&quot;}~{&quot;,
        ///			&quot;)~(&quot;
        ///		],
        ///		&quot;I&quot;: [
        ///			&quot;|&quot;,
        ///			&quot;1&quot;,
        ///			&quot;!&quot;
        ///		],
        ///	 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string EnglishLeetDict {
            get {
                return ResourceManager.GetString("EnglishLeetDict", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to In filename is invalid.
        /// </summary>
        internal static string inFilenameIsInvalid {
            get {
                return ResourceManager.GetString("inFilenameIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] LICENSE {
            get {
                object obj = ResourceManager.GetObject("LICENSE", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dictionary file is not specified. Default dictionary will be used.
        /// </summary>
        internal static string loggerWarnSubstituteDictionaryNotSpecifiedMessage {
            get {
                return ResourceManager.GetString("loggerWarnSubstituteDictionaryNotSpecifiedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Max length is invalid.
        /// </summary>
        internal static string maxLengthNotInRangeCombineExceptionMessage {
            get {
                return ResourceManager.GetString("maxLengthNotInRangeCombineExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to It must be more than 1 not empty entries splitted with {new line} in the file.
        /// </summary>
        internal static string notEnoughEntriesCombineExceptionMessage {
            get {
                return ResourceManager.GetString("notEnoughEntriesCombineExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Out filename is invalid.
        /// </summary>
        internal static string outFilenameIsInvalid {
            get {
                return ResourceManager.GetString("outFilenameIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Max length is more than count of the words and repetitions not allowed.
        /// </summary>
        internal static string repetitionDisallowedCombineExceptionMessage {
            get {
                return ResourceManager.GetString("repetitionDisallowedCombineExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to See help for more information.
        /// </summary>
        internal static string seeHelpForMoreInfo {
            get {
                return ResourceManager.GetString("seeHelpForMoreInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nothing for substitute. Specify word or use \&quot;-i\&quot; option.
        /// </summary>
        internal static string sourceWordSubstituteExceptionMessage {
            get {
                return ResourceManager.GetString("sourceWordSubstituteExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Json scheme is invalid. Please check your file.
        /// </summary>
        internal static string validateJsonSubstituteExceptionMessage {
            get {
                return ResourceManager.GetString("validateJsonSubstituteExceptionMessage", resourceCulture);
            }
        }
    }
}
