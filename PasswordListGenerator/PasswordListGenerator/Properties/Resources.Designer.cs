﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PasswordListGenerator.Properties {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
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
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
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
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
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
        ///   Ищет локализованную строку, похожую на {
        ///	&quot;A&quot;: {
        ///		&quot;cyrillic&quot;: &quot;А&quot;,
        ///		&quot;mad-leet&quot;: [
        ///			&quot;4&quot;,
        ///			&quot;/\\&quot;,
        ///			&quot;@&quot;,
        ///			&quot;/-\\&quot;,
        ///			&quot;^&quot;,
        ///			&quot;aye&quot;,
        ///			&quot;(L&quot;,
        ///			&quot;Д&quot;
        ///		],
        ///		&quot;good-leet&quot;: [
        ///			&quot;4&quot;,
        ///			&quot;/\\&quot;,
        ///			&quot;@&quot;,
        ///			&quot;/-\\&quot;
        ///		],
        ///		&quot;pronunciation&quot;: &quot;a&quot;
        ///	},
        ///	&quot;B&quot;: {
        ///		&quot;cyrillic&quot;: &quot;В&quot;,
        ///		&quot;mad-leet&quot;: [
        ///			&quot;I3&quot;,
        ///			&quot;8&quot;,
        ///			&quot;13&quot;,
        ///			&quot;|3&quot;,
        ///			&quot;ß&quot;,
        ///			&quot;!3&quot;,
        ///			&quot;(3&quot;,
        ///			&quot;/3&quot;,
        ///			&quot;)3&quot;,
        ///			&quot;|-]&quot;,
        ///			&quot;j3&quot;,
        ///			&quot;6&quot;
        ///		],
        ///		&quot;good-leet&quot;: [
        ///			&quot;|3&quot;,
        ///			&quot;8&quot;
        ///		],
        ///		&quot;pronunciation&quot;: &quot;bee&quot;
        ///	},
        ///	&quot;C&quot;: {
        ///		&quot;cyrillic&quot;: &quot;С&quot;,
        ///		&quot;mad-leet&quot;:  [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string EnglishLeetDict {
            get {
                return ResourceManager.GetString("EnglishLeetDict", resourceCulture);
            }
        }
    }
}