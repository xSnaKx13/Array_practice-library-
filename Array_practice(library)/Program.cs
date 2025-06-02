using System;
using System.Linq;


namespace Array_practice_library_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library();
        }

        static void Library()
        {
            bool libraryIsWorking = true;
            Random rand = new Random();

            int rows = rand.Next(3, 5);
            int cols = rand.Next(3, 5);
            string[,] library = new string[rows, cols];
            string[] choiseBook = new string[0];

            string[] authors = { "Война и мир", "Преступление и наказание", "Евгений Онегин", "Анна Каренина",
        "Мастер и Маргарита", "Братья Карамазовы", "Идиот", "Отцы и дети", "Мёртвые души", "Герой нашего времени",
        "Тихий Дон", "Доктор Живаго", "1984", "Скотный двор", "Улисс", "Великий Гэтсби", "Над пропастью во ржи",
        "Три товарища", "Маленький принц", "Гарри Поттер и философский камень", "Властелин колец", "Хоббит",
        "Алиса в Стране чудес", "Алиса в Зазеркалье", "О дивный новый мир", "451 градус по Фаренгейту", "Лолита",
        "Сто лет одиночества", "Портрет Дориана Грея", "Фауст", "Илиада", "Одиссея", "Дон Кихот", "Гамлет",
        "Ромео и Джульетта", "Моби Дик", "Граф Монте-Кристо", "Три мушкетёра", "Двадцать тысяч лье под водой",
        "Приключения Шерлока Холмса" };

            string[] tempLibrary = new string[rows * cols];
            for (int i = 0; i < tempLibrary.Length; i++)
            {
                int randomIndex = rand.Next(authors.Length);
                tempLibrary[i] = authors[randomIndex];
            }

            string[] uniqueBooks = tempLibrary.Distinct().ToArray();

            while (uniqueBooks.Length < rows * cols)
            {
                int randomIndex = rand.Next(authors.Length);
                string newBook = authors[randomIndex];
                if (!uniqueBooks.Contains(newBook))
                {
                    uniqueBooks = uniqueBooks.Append(newBook).ToArray();
                }
            }

            int index = 0;
            for (int i = 0; i < library.GetLength(0); i++)
            {
                for (int j = 0; j < library.GetLength(1); j++)
                {
                    library[i, j] = uniqueBooks[index++];
                }
            }

            while (libraryIsWorking)
            {
                Console.Clear();
                for (int i = 0; i < library.GetLength(0); i++)
                {
                    for (int j = 0; j < library.GetLength(1); j++)
                    {
                        if (library[i, j].StartsWith("Книга взята"))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(library[i, j] + " | ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(library[i, j] + " | ");
                        }
                    }
                    Console.WriteLine("\n");
                }

                Console.WriteLine("1 - Взять книгу\n2 - Вернуть книгу\n3 - Выйти из программы");
                int actionWithABook = int.Parse(Console.ReadLine());

                switch (actionWithABook)
                {
                    case 1:
                        Console.Write("Введите книгу, которую хотите забрать: ");
                        string bookChoice = Console.ReadLine().ToLower();

                        bool bookFound = false;

                        for (int i = 0; i < library.GetLength(0); i++)
                        {
                            for (int j = 0; j < library.GetLength(1); j++)
                            {
                                if (bookChoice == library[i, j].ToLower())
                                {
                                    Array.Resize(ref choiseBook, choiseBook.Length + 1);
                                    choiseBook[choiseBook.Length - 1] = library[i, j];
                                    library[i, j] = $"Книга взята ({library[i, j]})";
                                    bookFound = true; 
                                }
                            }
                        }

                        if (!bookFound)
                        {
                            Console.WriteLine("Такой книги нет.");
                        }
                        else
                        {
                            Console.WriteLine("Книга успешно взята!");
                        }
                        break;

                    case 2:
                        Console.Write("Введите книгу, которую хотите вернуть: ");
                        string returnBook = Console.ReadLine().ToLower();

                        bool returnFound = false;

                        for (int i = 0; i < choiseBook.Length; i++)
                        {
                            if (choiseBook[i].ToLower().Contains(returnBook))
                            {
                                for (int row = 0; row < library.GetLength(0); row++)
                                {
                                    for (int col = 0; col < library.GetLength(1); col++)
                                    {
                                        if (library[row, col].ToLower().Contains(returnBook))
                                        {
                                            library[row, col] = library[row, col].Replace("Книга взята (", "").Replace(")", "");
                                            break;
                                        }
                                    }
                                }

                                for (int k = i; k < choiseBook.Length - 1; k++)
                                {
                                    choiseBook[k] = choiseBook[k + 1];
                                }
                                Array.Resize(ref choiseBook, choiseBook.Length - 1);

                                returnFound = true;
                                break;
                            }
                        }

                        if (!returnFound)
                        {
                            Console.WriteLine("Такой книги нет в списке взятых.");
                        }
                        else
                        {
                            Console.WriteLine("Книга успешно возвращена!");
                        }
                        break;

                    case 3:
                        libraryIsWorking = false;
                        Console.WriteLine("Выход из программы...");
                        break;

                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }

                Console.WriteLine("\nСписок взятых книг:");
                foreach (var book in choiseBook)
                {
                    Console.WriteLine(book);
                }

                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
            }
        }
    }
}
