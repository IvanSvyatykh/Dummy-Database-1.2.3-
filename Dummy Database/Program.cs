using System;
using System.Collections.Generic;
using System.IO;
using Dummy_Database;

namespace Database
{
    public class Programm
    {
        static void Main()
        {
            string NameProject = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;//Берем общий путь для всех файлов, непривязанный к компьютеру
            string personPath = NameProject + "\\Person.csv";
            string booksPath = NameProject + "\\Book.csv";
            string personsBookPath = NameProject + "\\PersonsBooks.csv";

            int maxAuthor = -1;
            int maxReader = -1;
            int maxBookName = -1;

            List<Person> people = new List<Person>();
            List<Book> books = new List<Book>();
            List<PersonsBook> personsBooks = new List<PersonsBook>();

            List<string> listForBookId = new List<string>();
            List<string> listForPersonId = new List<string>();

            personsBooks = CsvParser.CsvParsePersonsBook(personsBookPath, personsBooks, ref listForBookId, ref listForPersonId);
            people = CsvParser.CsvParsePerson(personPath, people, ref maxReader, listForPersonId);
            books = CsvParser.CsvParseBook(booksPath, books, ref maxAuthor, ref maxBookName, listForBookId); 

            int leghtOfLine = maxAuthor + maxReader + maxBookName;

            WriterData.WriteData(personsBooks, books, people, leghtOfLine, maxAuthor, maxBookName, maxReader);
        }
    }
}