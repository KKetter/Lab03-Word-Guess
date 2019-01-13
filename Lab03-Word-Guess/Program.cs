using System;
using System.IO;

namespace Lab03_Word_Guess
{
    class Program
    {
        public static string path = "../../../../testfile.txt";
        static void Main(string[] args)
        {
            //
            
            CreateFile(path);
            //AppendToFile(path);
            //ReadFile(path);
            //DeleteFile(path);
            // 
            Console.ReadLine();
            Menu();
            
           

        }

        static void CreateFile(string path)
        {
            // Using statement
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    try
                    {
                        streamWriter.WriteLine("This is a test file");
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (IOException e)
            {

                Console.Write(e);
                throw;
            }
            catch (NotSupportedException e)
            {
                Console.Write(e);
                throw;
            }
            catch (Exception e)
            {
                Console.Write(e);
                throw;
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
            }
        }
        static void AppendToFile(string path)
        {
            Console.WriteLine("Enter a word");
            string userInput = Console.ReadLine();

            try
            {
                using(StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine(userInput);

                }
            }
            catch (Exception)
            {

                throw;
            }
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

        private static void Menu()
        {
            Console.WriteLine("Hello! Welcome to Word Guess Game");
            Console.WriteLine("Please select a menu option");
            Console.WriteLine("1: Play Word Guess Game");
            Console.WriteLine("2: View the word bank");
            Console.WriteLine("3: Add a word");
            Console.WriteLine("4: Delete a word");
            Console.WriteLine("5: Exit");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    //Game();
                    break;
                case "2":
                    ReadFile(path);
                    break;
                case "3":
                    AppendToFile(path);
                    break;
                case "4":
                    Console.WriteLine(": ");
                    break;
                case "5":
                    Console.Clear();
                    break;
            }

        }
    }
}
