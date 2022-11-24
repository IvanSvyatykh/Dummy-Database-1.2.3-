using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Database;
using static System.Net.Mime.MediaTypeNames;

namespace Database
{
    public class Programm
    {
        static void CreateLine(string bookName, string author, string readerName, int maxAuthor, int maxBookName, int maxReader, int Y)
        {
            int X = 0;
            Console.SetCursorPosition(X, Y);
            Console.Write($"|{bookName}");
            X = X + maxBookName + 1;
            Console.SetCursorPosition(X, Y);
            Console.Write($"|{author}");
            X = X + maxAuthor + 1;
            Console.SetCursorPosition(X, Y);
            Console.Write($"|{readerName}");
            X = X + maxReader + 1;
            Console.SetCursorPosition(X, Y);
            Console.Write("|");
        }
        public static void Separator(int lengthOfLine)
        {
            int separatorsCapacity = 5;
            int lengthOfdate = 0;
            for (int i = 1; i < (separatorsCapacity + lengthOfLine + lengthOfdate * 2); i++)
            {
                Console.Write("-");
            }

            Console.Write("\n");
        }
        static void CsvParserForPerson(string path, List<Person> people, ref int maxReader, List<string> listForPersonId)
        {
            int count = 0;
            foreach (string line in File.ReadLines(path))
            {
                if (count == 0)
                {
                    count++;
                    continue;
                }

                string[] splitted = line.Split(";");

                if (path.Contains("\\Person.csv") && splitted.Length == 2 && listForPersonId.Contains(splitted[0]))
                {
                    Person person = new Person();
                    try
                    {
                        person.Id = int.Parse(splitted[0]);
                    }
                    catch
                    {
                        Console.WriteLine("Id номер должен быть целым числом, проверьте корректность файла");
                        Console.WriteLine($"Ошибка в файле {path}  в {count} строке");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    person.ReaderName = splitted[1];
                    maxReader = Math.Max(maxReader, splitted[1].Length);
                    people.Add(person);
                }
                else if (path.Contains("\\Person.csv") && splitted.Length!=2)
                {
                    Console.WriteLine("Количество данных в каждой строке файла person.csv должно быть равно 2, проверьте коррекность ввода");
                    Console.WriteLine($"Ошибка в файле {path}  в {count} строке");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                count++;
            }
        }
        static void CsvParserForBook(string path, List<Book> books, ref int maxAuthor, ref int maxBookName, List<string> listForBookId)
        {
            int count = 0;
            foreach (string line in File.ReadLines(path))
            {
                if (count == 0)
                {
                    count++;
                    continue;
                }

                string[] splitted = line.Split(";");

                if (path.Contains("\\Book.csv") && splitted.Length == 6 && listForBookId.Contains(splitted[0]))
                {
                    Book book = new Book();
                    try
                    {
                        book.Id = int.Parse(splitted[0]);
                    }
                    catch
                    {
                        Console.WriteLine("Id номер должен быть целым числом, проверьте корректность файла");
                        Console.WriteLine($"Ошибка в файле {path} в  {count} строке");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    book.Name = splitted[1];
                    maxBookName = Math.Max(maxBookName, splitted[1].Length);
                    book.AuthorName = splitted[2];
                    maxAuthor = Math.Max(maxAuthor, splitted[2].Length);
                    try
                    {
                        book.YearOfPublication = int.Parse(splitted[3]);
                    }
                    catch
                    {
                        Console.WriteLine("Год публикации должен быть целым числом, проверьте корректность файла");
                        Console.WriteLine($"Ошибка в файле {path}  в {count} строке");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    book.Case = int.Parse(splitted[4]);
                    book.Shelf = int.Parse(splitted[5]);
                    books.Add(book);
                }
                else if (path.Contains("\\Book.csv") && splitted.Length!=6)
                {
                    Console.WriteLine("Количество данных в каждой строке файла Book.csv должно быть равно 6, проверьте коррекность ввода");
                    Console.WriteLine($"Ошибка в файле {path}  в {count} строке");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                count++;
            }
        }
        static void CsvParserForPersonsBook(string path, List<PersonsBook> personsBooks, ref List<string> listForBookId, ref List<string> listForPersonId)
        {
            int count = 0;

            foreach (string line in File.ReadLines(path))
            {
                if (count == 0)
                {
                    count++;
                    continue;
                }
     
                string[] splitted = line.Split(";");

                if (path.Contains("\\PersonsBooks.csv") && splitted.Length == 4)
                {
                    PersonsBook data = new PersonsBook();
                    try
                    {
                        data.BookId = int.Parse(splitted[0]);
                        listForBookId.Add(splitted[0]);
                    }
                    catch
                    {
                        Console.WriteLine("Id номер книги должен быть целым числом, проверьте корректность файла");
                        Console.WriteLine($"Ошибка в файле {path} в  {count} строке");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    try
                    {
                        data.PersonId = int.Parse(splitted[1]);
                        listForPersonId.Add(splitted[1]);
                    }
                    catch
                    {
                        Console.WriteLine("Id номер человека должен быть целым числом, проверьте корректность файла");
                        Console.WriteLine($"Ошибка в файле {path} в  {count} строке");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    data.DateOfGetting = splitted[2];
                    data.DateOfReturn = splitted[3];
                    personsBooks.Add(data);
                    count++;
                }
            }
        }
        static void WriteTableForSurvey(Person people, Book book, int legthOfLine, int maxAuthor, int maxBookName, int maxReader , ref int Y)
        {        
            for (int i = 0; i < 1; i++)
            {
                Separator(legthOfLine);
                Y++;
                CreateLine(book.Name, book.AuthorName, people.ReaderName, maxAuthor, maxBookName, maxReader, Y);
                Console.WriteLine();
                Y++;
            }
        }
        static void WriteData(List<PersonsBook> personsBooks, List<Book> books , List<Person> people , int legthOfLine, int maxAuthor, int maxBookName, int maxReader)
        {
            int Y = 0;
            for (int i = 0; i < 1; i++)
            {
                Separator(legthOfLine);
                Y++;
                CreateLine("Название", "Автор", "Имя читателя", maxAuthor, maxBookName, maxReader, Y);
                Console.WriteLine();
                Y++;
            }

            for (int i = 0; i < personsBooks.Count; i++)
            {
                int personId;
                int bookId;

                personId = personsBooks[i].PersonId;
                bookId=personsBooks[i].BookId;
                
                for (int j = 0; j < people.Count; j++)
                {
                    if (people[j].Id == personId)
                    {
                        personId = j;
                        break;
                    }
                }

                for (int j = 0; j < books.Count; j++)
                {
                    if (books[j].Id == bookId)
                    {
                        bookId = j;
                        break;
                    }
                }
                WriteTableForSurvey(people[personId], books[bookId],legthOfLine,maxAuthor,maxBookName, maxReader , ref Y);
            }
            Separator(legthOfLine);
        }
        static void Main()
        {
            string NameProject = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
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

            CsvParserForPersonsBook(personsBookPath, personsBooks, ref listForBookId, ref listForPersonId);
            CsvParserForPerson(personPath, people, ref maxReader , listForPersonId);
            CsvParserForBook(booksPath, books, ref maxAuthor, ref maxBookName , listForBookId);

            int leghtOfLine = maxAuthor + maxReader + maxBookName;

            WriteData(personsBooks, books, people, leghtOfLine, maxAuthor, maxBookName, maxReader);
        }
    }
}