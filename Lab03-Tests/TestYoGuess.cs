using System;
using Xunit;
using Lab03_Word_Guess;

namespace Lab03_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void AddAWord()
        {
            string path = "../../..hangman.txt";
            string word = "DUCK";
            string[] index = Program.AppendToFile(path, word);
            Assert.Contains("DUCK", index);
            Program.DeleteFile(path);
        }
        [Fact]
        public void ReadFile()
        {
            string path = "../../..hangman.txt";
            string word = "DUCK";
            string[] index = Program.AppendToFile(path, word);
            string[] index2 = Program.ReadFile(path);
            Assert.Contains("DUCK", index2);
            Program.DeleteFile(path);
        }
        [Fact]
        public void DeleteWordWorks()
        {
            string path = "../../..hangman.txt";
            string word = "DUCK";
            string[] index = Program.AppendToFile(path, word);

            string[] wordsInFile = Program.ReadFile(path);
            string userDelete = "DUCK";

            Program.RemoveOneWord(userDelete, path);

            string[] newFile = Program.ReadFile(path);

            Assert.DoesNotContain(userDelete, newFile);
            Program.DeleteFile(path);
        }
        [Fact]
        public void ContainsWordWorks()
        {
            string word = "DUCK";
            string letterInput = "U";
            Program.GuessConversion(letterInput, word);
            char[] guessArray = Program.WordChecker(word);
            char letter = Convert.ToChar(letterInput);
            Assert.Contains(letter, guessArray);
            Program.DeleteFile("../../../../guessFile.txt");
        }
    }
}
