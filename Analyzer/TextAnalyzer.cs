    
namespace Analyzer
{
    using System.Collections.Generic;

    using Analyzer.Accumulator;
    using Analyzer.AutofacModules;

    using Autofac;

    /// <summary>
    /// Represents the functionality to group, count and aggregate words within text
    /// </summary>
    public class TextAnalyzer
    {
        /// <summary>
        /// Private static readonly Aotofac ContainerBuilder assigned in the static constructor
        /// </summary>
        private static readonly IContainer Container;
       
        /*
         * Private static constructor to ensure that the class cannot be marked as [beforefieldinit], and therefore 
         * have its initializer invoked BEFORE the static IContainer is referenced.
         */

        /// <summary>
        /// Initializes static members of the <see cref="TextAnalyzer"/> class
        /// </summary>
        static TextAnalyzer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacModule());
            Container = builder.Build();
        }

        /*
         *  A more extensive implementation COULD provide the functionality to check the FileInfo.Length property and make a 
         *  decision of the algorithm to use based on either this value or a user entered Enum. Alternatively we could have
         *  used MEF to MultiImport algorithms so they can be changed independently, and even dynamically loaded at run time.
         *  
         * Known limitations include:
         * Not including integers, or words including integers in them, as whole words.
        */

        /// <summary>
        /// Method to count the unique words in the "sentence" parameter. 
        /// </summary>
        /// <param name="sentence"> The string from which words will be grouped and counted </param>
        /// <param name="treatAsFilePath"> Boolean parameter specifying whether the string should be treated as a file path </param>
        /// <returns> A Dictionary where each <see cref="KeyValuePair{TKey, TValue}"/> contains a unique word and the number of occurences </returns>
        public Dictionary<string, int> AccumulateWords(string sentence, bool treatAsFilePath = false)
        {
            return treatAsFilePath 
                ? Container.Resolve<IAccumulator>().AccumulateFromFile(sentence) 
                : Container.Resolve<IAccumulator>().AccumulateFromString(sentence);
        }
    }
}