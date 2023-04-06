using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace WordCounter
{
	internal class WordCounter
	{
		private static readonly string _punctuationSymbolsInWord = "'-_";
		public static Dictionary<string, int> CountWords(string path)
		{
			using (var txtReader = File.OpenText(path))
			{
				Dictionary<string, int> wordsDict = new();
				char currentChar;
				while (true)
				{
					do
					{
						if (txtReader.Peek() == -1)
							return wordsDict;

						currentChar = (char)txtReader.Read();
					} while (IsWordSymbol(currentChar) == false);

					StringBuilder currentWordBuilder = new();
					do
					{
						currentWordBuilder.Append(currentChar);

						if (txtReader.Peek() == -1)
							break;

						currentChar = (char)txtReader.Read();
					} while (IsWordSymbol(currentChar));

					string currentWord = currentWordBuilder.ToString().ToLower();

					if (currentWord.All(ch => _punctuationSymbolsInWord.Contains(ch)) == false)
						wordsDict[currentWord] = wordsDict.TryGetValue(currentWord, out int counter) ? ++counter : 1;
				}
			}
			
		}
		private static bool IsWordSymbol(char ch)
		{
			return char.IsLetterOrDigit(ch) || _punctuationSymbolsInWord.Contains(ch);
		}
	}
}
