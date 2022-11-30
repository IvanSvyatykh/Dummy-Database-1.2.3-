using Database;
using System;
using System.Collections.Generic;

namespace Dummy_Database
{
    public class WriterData
    {
        static void WriteTable(Person people, Book book, int legthOfLine, int maxAuthor, int maxBookName, int maxReader, ref int Y)
        {
            for (int i = 0; i < 1; i++)
            {
                EndLine(legthOfLine);
                Y++;
                CreateLine(book.Name, book.AuthorName, people.ReaderName, maxAuthor, maxBookName, maxReader, Y);
                Console.WriteLine();
                Y++;
            }
        }
        public static void WriteData(List<PersonsBook> personsBooks, List<Book> books, List<Person> people, int legthOfLine, int maxAuthor, int maxBookName, int maxReader)
        {
            int Y = 0;
            for (int i = 0; i < 1; i++)
            {              
                EndLine(legthOfLine);
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
                bookId = personsBooks[i].BookId;

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
                WriteTable(people[personId], books[bookId], legthOfLine, maxAuthor, maxBookName, maxReader, ref Y);
            }
            EndLine(legthOfLine);
        }
        static void CreateLine(string bookName, string author, string readerName, int maxAuthor, int maxBookName, int maxReader, int Y)
        {
            //Метод делает "линию" из данных переданных для вывода
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
        static void EndLine(int lengthOfLine)
        {
            //Метод создает линию для разделения данных
            int separatorsCapacity = 5;
            int lengthOfdate = 0;
            for (int i = 1; i < (separatorsCapacity + lengthOfLine + lengthOfdate * 2); i++)
            {
                Console.Write("-");
            }

            Console.Write("\n");
        }
    }
}
