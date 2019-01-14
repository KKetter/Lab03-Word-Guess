using System;
using System.IO;

namespace Lab03_Word_Guess
{
    class Program
    {
        public static string guessPath = "../../../../guessFile.txt";
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
                    Game();
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
            Console.WriteLine("2. Add a word to Word Bank");
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

        // admin menu functions
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
        /// <summary>
        /// converts pick strings into integers, contains exception for wrong menu inputs
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
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

        /// <summary>
        /// selects a random word from the words file to begin the game
        /// </summary>
        public static void Game()
        {
            guessThisWord = RandomWord(path);
            while(isCorrect == false)
            {
                Console.WriteLine(String.Join(' ', WordChecker(guessThisWord)));
                ReadFile(guessPath);
                GuessConversion(GuessPrompt(), guessThisWord);
                Console.WriteLine();
            }
            Console.WriteLine(String.Join(' ', WordChecker(guessThisWord)));
            Console.WriteLine("You Won!");
            isCorrect = false;
            DeleteFile(guessPath);
            DeleteFile(path);
            Menu();
        }
        /// <summary>
        /// Selects a random word from the words file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string RandomWord(string path)
        {
            string[] words = File.ReadAllLines(path);
            int wordsLength = words.Length;
            Random r = new Random();
            int whichWord = r.Next(wordsLength);
            string word = words[whichWord];
            return word;
        }
        /// <summary>
        /// Creates blanks ie word form for guessing, checks guesses against targeted random word, for correct guesses fills in word for correspoding blank
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static char[] WordChecker(string word)
        {
            int wordLength = word.Length;
            char[] wordForm = new char[wordLength];
            for (int i = 0; i < wordLength; i++)
            {
                wordForm[i] = '_';
            }

            string[] guessedLetters = File.ReadAllLines(guessPath);
            char[] guessedLettersChar = new char[guessedLetters.Length];
            for (int i = 1; i < guessedLetters.Length; i++)
            {
                guessedLettersChar[i - 1] = Convert.ToChar(guessedLetters[i]);
            };
            for (int i = 0; i < guessedLetters.Length; i++)
            {
                for (int j = 0; j < word.Length; j++)
                {
                    if(word[j] == guessedLettersChar[i])
                    {
                        wordForm[j] = word[j];
                    }
                }
            }
            return wordForm;
        }
        /// <summary>
        /// converts guess from string to char and handles conversion exceptions, these guesses are appended to a guess file, lastly value is checked for "correct-ness"
        /// </summary>
        /// <param name="letterInput"></param>
        /// <param name="word"></param>
        public static void GuessConversion(string letterInput, string word)
        {
            char letter;
            bool canConvert = false;
            while (canConvert == false)
            {
                try
                {
                    letter = Convert.ToChar(letterInput);
                }
                catch (Exception)
                {

                    Console.WriteLine("Only enter one letter to guess!");
                    letterInput = GuessPrompt();
                }
                canConvert = true;
            }
            letter = Convert.ToChar(letterInput);
            AppendToFile(guessPath, letterInput);
            char[] checkedWord = WordChecker(word);
            string checkedWordStr = new string(checkedWord);
            if (checkedWordStr == word)
            {
                isCorrect = true;
            }
        }
        /// <summary>
        /// Prompts user to guess
        /// </summary>
        /// <returns></returns>
        public static string GuessPrompt()
        {
            Console.Write("Guess a letter: ");
            string choice = Console.ReadLine();
            string choiceStr = choice.ToUpper();
            return choiceStr;
        }

        public static void CreateFileGuesses()
        {
            using (StreamWriter streamWriter = new StreamWriter(guessPath))
            {
                streamWriter.WriteLine("Guesses");
            }
        }
        public static void CreateFile()
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine("KARN");
                streamWriter.WriteLine("TEZZERET");
                streamWriter.WriteLine("GARRUK");
                streamWriter.WriteLine("CHANDRA");
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
