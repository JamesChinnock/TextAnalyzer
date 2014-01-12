
namespace Analyzer.Algorithms
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a node in an ordered tree data structure (Trie)
    /// </summary>
    public class TrieNode : IComparable<TrieNode>
    {
        /// <summary>
        /// Private object to allow locking Monitor code to lock on something other than 'this'
        /// </summary>
        private readonly object _lockableObject = new object();

        /// <summary>
        /// The current character within the text
        /// </summary>
        private readonly char _character;

        /// <summary>
        /// The parent node from which this node originated
        /// </summary>
        private readonly TrieNode _parentNode;

        /// <summary>
        /// Concurrent dictionary of characters and child nodes
        /// </summary>
        private readonly ConcurrentDictionary<char, TrieNode> _childNodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrieNode"/> class
        /// </summary>
        /// <param name="parentNode"> The <see cref="TrieNode"/> instance that is a Parent to this node </param>
        /// <param name="character"> The character this node represents </param>
        public TrieNode(TrieNode parentNode, char character)
        {
            this._parentNode = parentNode;
            this._character = character;
            this.WordCount = 0;
            this._childNodes = new ConcurrentDictionary<char, TrieNode>();
        }

        /// <summary>
        /// Gets the WordCount property
        /// </summary>
        public int WordCount { get; private set; }

        /// <summary>
        /// Method to recursively add a child node if the char is a letter or digit and isn't already in the child nodes.
        /// The WordCount is then incremented
        /// </summary>
        /// <param name="word"> The current word </param>
        /// <param name="index"> The index of the char currently being examined within the word </param>
        public void AddWord(string word, int index = 0)
        {
            if (index < word.Length)
            {
                var key = word[index];
                if (char.IsLetterOrDigit(key) || char.IsSymbol(key))
                {
                    if (!this._childNodes.ContainsKey(key))
                    {
                        this._childNodes.TryAdd(key, new TrieNode(this, key));
                    }

                    this._childNodes[key].AddWord(word, index + 1);
                }
                else
                {
                    this.AddWord(word, index + 1);
                }
            }
            else
            {
                if (this._parentNode != null)
                {
                    lock (this._lockableObject)
                    {
                        this.WordCount++;
                    }
                }
            }
        }

        /// <summary>
        /// REturns the most commonly recuring words
        /// </summary>
        /// <param name="nodes"> Enumerable collection of nodes to be populated with results </param>
        /// <param name="distinctWordCount"> Count of distinct words </param>
        public void GetMostCommon(ref List<TrieNode> nodes, ref int distinctWordCount)
        {
            if (this.WordCount > 0)
            {
                distinctWordCount++;
            }

            if (this.WordCount > nodes[0].WordCount)
            {
                nodes[0] = this;
                nodes.Sort();
            }

            foreach (var key in this._childNodes.Keys)
            {
                this._childNodes[key].GetMostCommon(ref nodes, ref distinctWordCount);
            }
        }

        /// <summary>
        /// Private method to count complete words in node and sub nodes
        /// </summary>
        /// <param name="numOfWords"> Number of words so far </param>
        public void CountWords(ref int numOfWords)
        {
            if (this.WordCount > 0)
            {
                numOfWords++;
            }

            foreach (var key in this._childNodes.Keys)
            {
                this._childNodes[key].CountWords(ref numOfWords);
            }
        }

        /// <summary>
        /// Overridden ToString method
        /// </summary>
        /// <returns> The string representation of the TrieNode </returns>
        public override string ToString()
        {
            if (this._parentNode == null)
            {
                return string.Empty;
            }

            return this._parentNode.ToString() + this._character;    
        }

        /// <summary>
        /// Method to compare one <see cref="TrieNode"/> to another
        /// </summary>
        /// <param name="other"> The comparand </param>
        /// <returns> 
        /// Less than zero: Current instance precedes the parameter object
        /// Zero: Current instance occurs in the same position as the parameter object
        /// More than zero: Current instance follows the parameter object
        /// </returns>
        public int CompareTo(TrieNode other)
        {
            return this.WordCount.CompareTo(other.WordCount);
        }
    }
}