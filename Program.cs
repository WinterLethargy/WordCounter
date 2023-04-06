using System.IO;

namespace WordCounter
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var enterFilePath = GetEnterFilePath();

			var wordDict = WordCounter.CountWords(enterFilePath);

			var exitPath = enterFilePath.Replace(".txt", "_WordCount.txt");

			WriteWordCount(wordDict, exitPath);
		}
		private static void WriteWordCount(Dictionary<string, int> wordDict, string exitPath)
		{
			using (var writer = File.CreateText(exitPath))
			{
				var maxWordLength = wordDict.Keys.Max(w => w.Length);
				var indent = 2;
				foreach (var kvp in wordDict.OrderByDescending(kvp => kvp.Value))
				{
					writer.Write(kvp.Key);

					var spaceCount = maxWordLength + indent - kvp.Key.Length;

					writer.Write(new string(' ', spaceCount));
					writer.WriteLine(kvp.Value);
				}
			}
		}
		private static string GetEnterFilePath()
		{
			string enterFilePath;
			do
			{
				enterFilePath = RequestEnterFilePath();

				if (Path.HasExtension(enterFilePath) == false)
				{
					enterFilePath += ".txt";
				}

				if (IsCorrectFile(enterFilePath))
				{
					break;
				}
				else
				{
					ShowErrorMessage();
				}
			} while (true);

			return enterFilePath;
		}
		private static string RequestEnterFilePath()
		{
				Console.WriteLine("Введите имя txt файла, если он в одной папке с запущенным приложением, или полный путь:");
				Console.Write("> ");

				var path = Console.ReadLine();
				Console.WriteLine();

				return path;
		}
		private static void ShowErrorMessage()
		{
			Console.WriteLine("Неподходящий файл! Должен быть указан существующий txt файл.");
			Console.WriteLine();
		}
		private static bool IsCorrectFile(string path)
		{
			if (File.Exists(path) && Path.GetExtension(path) == ".txt")
			{
				return true;
			}

			return false;
		}
	}
}
