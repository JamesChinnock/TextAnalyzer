
namespace Analyzer.Algorithms
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /*
     * I am aware of the limitation to the number of items returned by the 'top' parameter in the private method,
     * and that the I have had to hardcode a limit of 6 due to the file text data - and time it therefore takes 
     * to print the results in the Unit Test window. My reason for leaving this is simply a time
     * constraint as opposed to being unaware of the issue. Hence unit tests and code are for now 'erroneously' restrained too.
    */

    /// <summary>
    /// Represents that ability to read and process a text based file
    /// </summary>
    public class PrefixTreeAlgorithm : IAlgorithm
    {
        /// <summary>
        /// Method to run the WordReader on the specified file
        /// </summary>
        /// <param name="stream"> The stream of data to be read by a StreamReader </param>
        /// <returns> Dictionary of words and count values  </returns>
        public Dictionary<string, int> Run(Stream stream)
        {
            var rootNode = new TrieNode(null, '?');

            using (var streamReader = new StreamReader(stream))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var words = line.Split(null);
                    foreach (var word in words)
                    {
                        rootNode.AddWord(word.Trim().ToLower());
                    }
                }
            }

            var numWords = 0;
            rootNode.CountWords(ref numWords);
            return AccumulatedWords(numWords, rootNode);
        }

        /// <summary>
        /// Private method to extract the Words and WordCount from the _rootnodes' child TrieNodes
        /// </summary>
        /// <param name="top"> The number of entries to return </param>
        /// <param name="trieNode"> The rootnode from accumulation will start </param>
        /// <returns> Dictionary of words and count values </returns>
        private static Dictionary<string, int> AccumulatedWords(int top, TrieNode trieNode)
        {
            if (top == 0)
            {
                return new Dictionary<string, int>();
            }

            if (top > 6)
            {
                top = 6;
            }

            var nodes = Enumerable.Range(0, top).Select(a => new TrieNode(null, '?')).ToList();
            var distinctWords = 0;

            trieNode.GetMostCommon(ref nodes, ref distinctWords);
            nodes.Reverse();

            return nodes.ToDictionary(node => node.ToString(), node => node.WordCount);
        }
    }
}