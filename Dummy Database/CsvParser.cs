using System;
using System.Collections.Generic;
using System.IO;
using Database;

namespace Dummy_Database
{
    public class CsvParser
    {
        
        public static List<Person> CsvParsePerson(string path, List<Person> people, ref int maxReader, List<string> listForPersonId)
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
                    if (int.TryParse(splitted[0], out int num))
                    {
                        person.Id = num;
                    }
                    else
                    {
                        Console.WriteLine("Id номер должен быть целым числом, проверьте корректность файла");
                        Console.WriteLine();
                        Console.WriteLine($"Ошибка в файле {path}  в {count} строке");
                        Console.ReadKey();
                        throw new ArgumentException("Id номер должен быть целым числом, проверьте корректность файла");
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
                    throw new ArgumentException("Количество данных в каждой строке файла person.csv должно быть равно 2, проверьте коррекность ввода");
                }
                count++;
            }
            return people;
        }
        public static List<Book> CsvParseBook(string path, List<Book> books, ref int maxAuthor, ref int maxBookName, List<string> listForBookId)
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
                    if (int.TryParse(splitted[0], out int num))
                    {
                        book.Id = num;
                    }
                    else
                    {
                        Console.WriteLine("Id номер должен быть целым числом, проверьте корректность файла");
                        Console.WriteLine();
                        Console.WriteLine($"Ошибка в файле {path}  в {count} строке");
                        Console.ReadKey();
                        throw new ArgumentException("Id номер должен быть целым числом, проверьте корректность файла");
                    }
                    book.Name = splitted[1];
                    maxBookName = Math.Max(maxBookName, splitted[1].Length);
                    book.AuthorName = splitted[2];
                    maxAuthor = Math.Max(maxAuthor, splitted[2].Length);
                    if (int.TryParse(splitted[3], out num))
                    {
                        book.YearOfPublication = num;
                    }
                    else
                    {
                        Console.WriteLine("Год публикации должен быть целым числом, проверьте корректность файла");
                        Console.WriteLine();
                        Console.WriteLine($"Ошибка в файле {path}  в {count} строке");
                        Console.ReadKey();
                        throw new ArgumentException("Год публикации должен быть целым числом, проверьте корректность файла");
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
                    throw new Exception("Количество данных в каждой строке файла Book.csv должно быть равно 6, проверьте коррекность ввода");
                }
                count++;
            }

            return books;
        }
        public static List<PersonsBook> CsvParsePersonsBook(string path, List<PersonsBook> personsBooks, ref List<string> listForBookId, ref List<string> listForPersonId)
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
                    if (int.TryParse(splitted[0], out int num))
                    {
                        data.BookId = num;
                        listForBookId.Add(splitted[0]);
                    }
                    else
                    {
                        Console.WriteLine("Id номер книги должен быть целым числом, проверьте корректность файла");
                        Console.WriteLine($"Ошибка в файле {path} в  {count} строке");
                        Console.ReadKey();
                        throw new ArgumentException("Id номер книги должен быть целым числом, проверьте корректность файла");
                    }
                    if (int.TryParse(splitted[1], out num))
                    {
                        data.PersonId = num;
                        listForPersonId.Add(splitted[1]);
                    }
                    else
                    {
                        Console.WriteLine("Id номер человека должен быть целым числом, проверьте корректность файла");
                        Console.WriteLine($"Ошибка в файле {path} в  {count} строке");
                        Console.ReadKey();
                        throw new Exception("Id номер человека должен быть целым числом, проверьте корректность файла");
                    }
                    data.DateOfGetting = DateTime.Parse(splitted[2]);
                    data.DateOfReturn = DateTime.Parse(splitted[3]);
                    if(data.DateOfGetting>data.DateOfReturn)
                    {
                        Console.WriteLine("Ошибка дата взяти книги не может быть позже даты возвращения");
                        Console.WriteLine($"Ошибка в файле {path} в  {count} строке");
                        Console.ReadKey();
                        throw new ArgumentException("Ошибка дата взяти книги не может быть позже даты возвращения");
                    }
                    personsBooks.Add(data);
                    count++;
                }
            }
            return personsBooks;
        }
    }
}
