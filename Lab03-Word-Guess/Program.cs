using System;
using System.IO;

namespace Lab03_Word_Guess
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../testfile.txt";
            //CreateFile(path);
            AppendToFile(path);
            ReadFile(path);
            DeleteFile(path);
            Console.ReadLine();

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
            try
            {
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.WriteLine(lines[i]);
                }
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        static void AppendToFile(string path)
        {
            try
            {
                using(StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine("This is a new NEW line to be added");
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
    }
}
