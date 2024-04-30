using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лаба_8_отредактированная
{
    abstract class Task
    {
        public Task(string text) { }
        protected abstract void ParseText(string text);
    }

    class Task_2 : Task
    {
        private string originalText;
        private string decryptedText;

        public Task_2(string text) : base(text)
        {
            ParseText(text);
        }

        protected override void ParseText(string text)
        {
            List<string> elements = new List<string>();
            string currentElement = "";

            foreach (char c in text)
            {
                if (char.IsLetterOrDigit(c))
                {
                    currentElement += c;
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentElement))
                    {
                        elements.Add(ReverseString(currentElement));
                        currentElement = "";
                    }
                    elements.Add(c.ToString());
                }
            }

            if (!string.IsNullOrEmpty(currentElement))
            {
                elements.Add(ReverseString(currentElement));
            }

            string parsedText = string.Concat(elements);

            decryptedText = Decrypt(parsedText);
        }

        public string Decrypt(string decryptedMessage)
        {
            List<string> elements = new List<string>();
            string currentElement = "";

            foreach (char c in decryptedMessage)
            {
                if (char.IsLetterOrDigit(c))
                {
                    currentElement += c;
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentElement))
                    {
                        elements.Add(ReverseString(currentElement));
                        currentElement = "";
                    }
                    elements.Add(c.ToString());
                }
            }

            if (!string.IsNullOrEmpty(currentElement))
            {
                elements.Add(ReverseString(currentElement));
            }

            return string.Concat(elements);
        }

        private string ReverseString(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public override string ToString()
        {
            return decryptedText;
        }
    }




    class Task_4 : Task
    {
        protected string text;
        int complexity;
        public Task_4(string text) : base(text)
        {
            this.text = text;

        }


        public int GetComplexity()
        {
            int wordCount = text.Split(new char[] { ' ', '.', ',', ':', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
            int punctuationCount = text.Split(new char[] { '.', ',', ':', ';', '!', '?' }).Length - 1;
            return complexity = wordCount + punctuationCount;
        }

        protected override void ParseText(string text)
        {

        }

    }


    class Task_6 : Task
    {
        private int[] syllablesCount = new int[10];
        protected string text;

        public Task_6(string text) : base(text)
        {
            ParseText(text);
        }

        protected override void ParseText(string text)
        {
            string[] words = text.Split(new char[] { ' ', '.', ',', ':', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                int syllables = CountSyllables(word);
                if (syllables <= 9)
                {
                    syllablesCount[syllables]++;
                }
                else
                {
                    syllablesCount[9]++;
                }
            }
        }

        private int CountSyllables(string word)
        {
            int syllables = 0;
            string vowels = "аеёиоуыэюяaeiouy";

            for (int i = 0; i < word.Length; i++)
            {
                if (vowels.Contains(Char.ToLower(word[i])))
                {

                    if (i == 0 || !vowels.Contains(Char.ToLower(word[i - 1])))
                    {
                        syllables++;
                    }
                }
            }
            return syllables;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 1; i < syllablesCount.Length; i++)
            {
                if (syllablesCount[i] > 0)
                {
                    result += $"Слов с {i} слогами: {syllablesCount[i]}\n";
                }
            }
            return result;
        }
    }


    class Task_8 : Task
    {
        private string formattedText;

        public Task_8(string text) : base(text)
        {
            ParseText(text);
        }

        protected override void ParseText(string text)
        {
            var Words = text.Split(' ');
            var Lines = new List<string>();
            var newLine = new StringBuilder();
            int maxLineWidth = 50;

            foreach (var Word in Words)
            {
                if (newLine.Length + Word.Length + 1 > maxLineWidth)
                {
                    string[] wordsInLine = newLine.ToString().TrimEnd().Split(' ');
                    int numGaps = wordsInLine.Length - 1;
                    if (numGaps > 0)
                    {
                        int totalSpacesToAdd = maxLineWidth - newLine.Length + numGaps;
                        int spacesBetweenWords = totalSpacesToAdd / numGaps;
                        int extraSpaces = totalSpacesToAdd % numGaps;

                        StringBuilder newLineWithSpaces = new StringBuilder();
                        for (int i = 0; i < wordsInLine.Length - 1; i++)
                        {
                            newLineWithSpaces.Append(wordsInLine[i]);
                            newLineWithSpaces.Append(' ', spacesBetweenWords);
                            if (extraSpaces > 0)
                            {
                                newLineWithSpaces.Append(' ');
                                extraSpaces--;
                            }
                        }
                        newLineWithSpaces.Append(wordsInLine[wordsInLine.Length - 1]);
                        Lines.Add(newLineWithSpaces.ToString());
                    }
                    else
                    {
                        Lines.Add(newLine.ToString().PadRight(maxLineWidth));
                    }
                    newLine.Clear();
                }
                newLine.Append(Word).Append(' ');
            }
            Lines.Add(newLine.ToString().TrimEnd().PadRight(maxLineWidth));
            formattedText = string.Join("\n", Lines);
        }

        public override string ToString()
        {
            return formattedText;
        }
    }


    class Task_9 : Task
    {
        private string formattedText;
        private Dictionary<string, char> codeTable;

        public Task_9(string text) : base(text)
        {
            codeTable = GetCodeTable(text);
            ParseText(text);
        }

        public override string ToString()
        {
            return formattedText;
        }

        protected override void ParseText(string text)
        {
            string compressedText = text;
            foreach (var code in codeTable)
            {
                compressedText = compressedText.Replace(code.Key, code.Value.ToString());
            }



            Console.WriteLine("\nТаблица для шифровки:");
            foreach (var code in codeTable)
            {
                Console.WriteLine($"{code.Key}: {code.Value}");
            }

            formattedText = compressedText;
        }

        public Dictionary<string, char> GetCodeTable()
        {
            return codeTable;
        }

        private Dictionary<string, char> GetCodeTable(string text)
        {
            Dictionary<string, char> codes = new Dictionary<string, char>();

            int count = 0;
            char uniqueCode = '1';

            for (int i = 0; i < text.Length - 1 && count < 50; i++)
            {
                string sequence = text.Substring(i, 2);
                if (text.Split(new string[] { sequence }, StringSplitOptions.None).Length - 1 > 1)
                {
                    if (!codes.ContainsKey(sequence))
                    {
                        codes.Add(sequence, uniqueCode);
                        uniqueCode++;
                        count++;
                    }
                }
            }


            return codes;
        }

        private bool IsAllRussian(string sequence)
        {
            foreach (char c in sequence)
            {
                if (!IsRussian(c))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsRussian(char c)
        {
            return (c >= 'А' && c <= 'я') && c != 'ё' && c != 'Ё';
        }
    }

    class Task_10 : Task
    {
        private string decodedText;
        private Task_9 task9;

        public Task_10(Task_9 task9) : base(task9.ToString())
        {
            this.task9 = task9;
            this.decodedText = DecodeText(task9.ToString(), task9.GetCodeTable());
            ParseText(decodedText);
        }

        private string DecodeText(string encodedText, Dictionary<string, char> codeTable)
        {
            var reversedCodeTable = codeTable.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

            var sb = new StringBuilder();

            foreach (char c in encodedText)
            {
                if (reversedCodeTable.ContainsKey(c))
                {
                    sb.Append(reversedCodeTable[c]);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return decodedText;
        }

        protected override void ParseText(string text)
        {

        }
    }


    class Program
    {
        public static void Main()
        {
            string text = "Двигатель самолета - это сложная инженерная конструкция, обеспечивающая подъем, управление и движение в воздухе. Он состоит из множества компонентов, каждый из которых играет важную роль в общей работе механизма. Внутреннее устройство двигателя включает в себя компрессор, камеру сгорания, турбину и системы управления и охлаждения. Принцип работы основан на воздушно-топливной смеси, которая подвергается сжатию, воспламенению и расширению, обеспечивая движение воздушного судна.";

            Task_2 task2 = new Task_2(text);
            Console.WriteLine("Номер 2:");
            Console.WriteLine("Зашифрованный текст:");
            Console.WriteLine(task2.Decrypt(task2.ToString()));
            Console.WriteLine("Дешифрованный текст:");
            Console.WriteLine(task2);

            Console.WriteLine();

            Task_4 task4 = new Task_4(text);
            Console.WriteLine("Номер 4:");
            Console.WriteLine(task4.GetComplexity());

            Console.WriteLine();

            Task_6 task6 = new Task_6(text);
            Console.WriteLine("Номер 6:");
            Console.WriteLine(task6);

            Console.WriteLine();

            Task_8 task8 = new Task_8(text);
            Console.WriteLine("Номер 8:");
            Console.WriteLine(task8);

            Console.WriteLine();

            Task_9 task9 = new Task_9(text);
            Console.WriteLine("Номер 9:");
            Console.WriteLine(task9);

            Console.WriteLine();

            Task_10 task10 = new Task_10(task9);
            Console.WriteLine("Номер 10:");
            Console.WriteLine(task10);

        }
    }
}
