using System.Text;
using CommandLine;
using CommandLine.Text;
using PasswordListGenerator.Properties;

namespace PasswordListGenerator.Substitutions
{
    public class SubstituteSubOptions : EncodingSubOptions
    {
        [ValueOption(0)]
        public string SourceWord { get; set; }

        [Option('d', "dict", Required = false, HelpText = "Dict to subs symbols (Must contain array of methods with arrays for every symbol)")]
        public string DictFilename { get; set; }

        [Option('m', "method", Required = false, HelpText = "Method from dictionary to substitute letters in word")]
        public string Method { get; set; }

        [Option("ignore-case", Required = false, HelpText = "Ignore letter case in source word")]
        public bool IsIgnoreCase { get; set; }

        [Option('o', "out-file", Required = false, HelpText = "File to write output substitutions")]
        public string OutFilename { get; set; }

        [Option('i', Required = false, HelpText = "Use std input to get words for substitution")]
        public bool IsUseStdInput { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var helpStringBuilder = new StringBuilder(HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current)));
            helpStringBuilder.AppendLine();
            helpStringBuilder.AppendLine(Resources.additionalUsage);
            helpStringBuilder.Append(Resources.AdditionalSubsDictUsage);
            helpStringBuilder.Append(Resources.AdditionalSubsMethodUsage);
            helpStringBuilder.Append(Resources.AdditionalSubs_i_Usages);
            return helpStringBuilder.ToString();
        }
    }
}
