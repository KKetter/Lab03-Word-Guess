using System;
using System.IO;

namespace Lab03_Word_Guess
{
    class Program
    {
        public static string guessPath = "../../../../guessFile";
        public static string path = "../../../../wordFile.txt";
        public static bool isCorrect = false;
        public static string guessThisWord;
        static void Main(string[] args)
        {
            CreateFile();
            Menu();
        }

        public static void Menu()
        {
            Console.WriteLine("Hello! Welcome to Word Guess Game");
            Console.WriteLine("Please select a menu option");
            Console.WriteLine("1: Play Word Guess Game");
            Console.WriteLine("2: Admin Menu");
            Console.WriteLine("3: Exit");
            string userInput = Console.ReadLine();
            int pick = PickHandler(userInput);

            switch (pick)
            {
                case 1:
                    CreateFile();
                    CreateFileGuesses();
                    //Game();
                    break;
                case 2:
                    AdminMenu();
                    break;
                case 3:
                    Environment.Exit(pick);
                    break;
                case 0:
                    Console.WriteLine("" + "Please enter a valid number");
                    Menu();
                    break;
                default:
                    break;
            }


        }
        public static void AdminMenu()
        {
            Console.WriteLine("Welcome to admin functions");
            Console.WriteLine("1. View Word Bank");
            Console.WriteLine("2. Edit Word Bank");
            Console.WriteLine("3. Remove a word from Word Bank");
            Console.WriteLine("4. Return to Main Menu");

            string userInput = Console.ReadLine();
            int pick = PickHandler(userInput);

            switch (pick)
            {
                case 1:
                    ViewWords();
                    break;
                case 2:
                    AddAWord();
                    break;
                case 3:
                    RemoveOneWord();
                    break;
                case 4:
                    Menu();
                    break;
                default:
                    break;
            }
        }

        // admin menu functions - application of CRUD
        /// <summary>
        /// Display all words in the word file
        /// </summary>
        public static void ViewWords()
        {
            ReadFile(path);
            Console.WriteLine();
            AdminMenu();
        }
        /// <summary>
        /// Prompts user for word to add to bank then converts to uppercase before adding to file
        /// </summary>
        public static void AddAWord()
        {
            ReadFile(path);
            Console.WriteLine("What word would like to add to the game?");
            string addedWord = Console.ReadLine();
            string addedWordUpper = addedWord.ToUpper();
            AppendToFile(path, addedWord);
            Console.WriteLine();
            AdminMenu();
        }
       /// <summary>
       /// prompts user to delete word from bank, checks word against file, deletes and re-writes file w/o selected word
       /// </summary>
        public static void RemoveOneWord()
        {
            string[] words = File.ReadAllLines(path);
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine("What word would you like to remove?");
            string deletedWord = Console.ReadLine();
            string deletedWordUpper = deletedWord.ToUpper();
            string[] newWords = new string[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                if (deletedWord != words[i])
                {
                    newWords[i] = words[i];
                }
                if (newWords == words)
                {
                    Console.WriteLine("Not in the word bank!");
                    AdminMenu();
                }
            }
            DeleteFile(path);
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                foreach (string word in newWords)
                {
                    streamWriter.WriteLine(word);
                }
            }
            Console.WriteLine("Your word bank is now:");
            ViewWords();
            AdminMenu();
        }

        public static int PickHandler(string userInput)
        {
            int pick;
            try
            {
                int check = Convert.ToInt32(userInput);
            }
            catch (FormatException)
            {
                pick = 0;
                return pick;
                throw;
            }
            pick = Convert.ToInt32(userInput);
            return pick;
        }
        public static void CreateFileGuesses()
        {
            using (StreamWriter streamWriter = new StreamWriter(guessPath))
            {
                streamWriter.WriteLine("Guesses");
            }
        }

        // CRUD functions
        public static void CreateFile()
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine("Karn");
                streamWriter.WriteLine("Tezzeret");
                streamWriter.WriteLine("Bolas");
                streamWriter.WriteLine("Chandra");
            }

        }
        public static string[] ReadFile(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string[] readWords = File.ReadAllLines(path);
                foreach (string readWord in readWords)
                {
                    Console.WriteLine(readWord);
                }
                return readWords;
                
            }

        }
        public static string[] AppendToFile(string path, string input)
        {
            string inputUpper = input.ToUpper();

            using (StreamWriter streamWriter = File.AppendText(path))
            {
                streamWriter.WriteLine(inputUpper);
            }
            return ReadFile(path);
        }
        static void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
