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
                    CreatFileGuesses();
                    Game();
                    break;
                case 2:
                    AdminMenu();
                    break;
                case 3:
                    break;
                case 0:
                    Console.WriteLine("" +
                        "Please enter a valid number");
                    Menu();
                    break;
                default:
                    break;
            }


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
        static void CreateFile()
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine("DOG");
                streamWriter.WriteLine("DUCK");
                streamWriter.WriteLine("CAT");
                streamWriter.WriteLine("CHICKEN");
            }

        }
        static void ReadFile(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string[] readWords = File.ReadAllLines(path);

                foreach (string readWord in readWords)
                {
                    Console.WriteLine(readWord);
                }
                Console.ReadLine();
                //Menu();
            }

        }
        static void AppendToFile(string path)
        {
            Console.WriteLine("Enter a word");
            string userInput = Console.ReadLine();

            try
            {
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine(userInput);

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            //Menu();
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
