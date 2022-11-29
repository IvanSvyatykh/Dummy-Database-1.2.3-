using System;
using System.Collections.Generic;
using System.IO;
using Database;

namespace Dummy_Database
{
    public class CsvParser
    {
        public static List<Person> CsvParserForPerson(string path, List<Person> people, ref int maxReader, List<string> listForPersonId)
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
                else if (path.Contains("\\Person.csv") && splitted.Length != 2)
                {
                    Console.WriteLine("Количество данных в каждой строке файла person.csv должно быть равно 2, проверьте коррекность ввода");
                    Console.WriteLine($"Ошибка в файле {path}  в {count} строке");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                count++;
            }
            return people;
        }
        public static List<Book> CsvParserForBook(string path, List<Book> books, ref int maxAuthor, ref int maxBookName, List<string> listForBookId)
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
                else if (path.Contains("\\Book.csv") && splitted.Length != 6)
                {
                    Console.WriteLine("Количество данных в каждой строке файла Book.csv должно быть равно 6, проверьте коррекность ввода");
                    Console.WriteLine($"Ошибка в файле {path}  в {count} строке");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                count++;
            }

            return books;
        }
        public static List<PersonsBook> CsvParserForPersonsBook(string path, List<PersonsBook> personsBooks, ref List<string> listForBookId, ref List<string> listForPersonId)
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

            return personsBooks;
        }
    }
}
