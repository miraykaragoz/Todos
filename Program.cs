namespace todos
{
    class Program
    {
        public class Todo
        {
            public string? Task { get; set; }
            public bool IsComplete { get; set; }
            public DateTime Deadline { get; set; }
        }

        static List<Todo> todos = new List<Todo>();

        static void SaveTxt()
        {
            string[] todosTxt = new string[todos.Count];

            for (int i = 0; i < todos.Count; i++)
            {
                string todoTxt = $"{todos[i].Task}|{todos[i].IsComplete}|{todos[i].Deadline}";
                todosTxt[i] = todoTxt;
            }

            using StreamWriter writer = new StreamWriter("todo.txt");
            writer.Write(string.Join('\n', todosTxt));
        }

        static void ReturnMenu()
        {
            Console.WriteLine("\nMenüye dönmek için bir tuşa basın");
            Console.ReadKey(true);
            ShowMenu();
        }

        static void ReadTxt()
        {
            using StreamReader reader = new StreamReader("todo.txt");

            string todoTxt;

            while ((todoTxt = reader.ReadLine()) != null)
            {
                string[] todoStr = todoTxt.Split('|');
                todos.Add(new Todo()
                {
                    Task = todoStr[0],
                    IsComplete = bool.Parse(todoStr[1]),
                    Deadline = DateTime.Parse(todoStr[2])
                });
            }
        }

        static void ShowMenu(bool isFirstOpening = false)
        {
            Console.Clear();
            if (isFirstOpening)
            {
                Console.WriteLine("Hoş Geldiniz!");
            }
            Console.WriteLine("1. Yapılacak İşleri Listele");
            Console.WriteLine("2. Yeni İş Ekle");
            Console.WriteLine("3. İş Tamamla");
            Console.WriteLine("4. İş Listesini Temizle");
            Console.WriteLine("5. Çıkış");

            Console.Write("Seçiminiz: ");
            char inputUserChoice = Console.ReadKey().KeyChar;

            switch (inputUserChoice)
            {
                case '1':
                    ListTodos();
                    break;
                case '2':
                    AddTodo();
                    break;
                case '3':
                    CompleteTask();
                    break;
                case '4':
                    RemoveTodos();
                    break;
                case '5':
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nHatalı Seçim Yaptınız!");
                    Console.ResetColor();
                    ReturnMenu();
                    break;
            }
        }

        static void ListTodos()
        {
            Console.Clear();
            Console.WriteLine("1. Tüm işleri listele");
            Console.WriteLine("2. Bugün bitmesi gereken işleri listele");
            Console.WriteLine("3. İşleri tek tek listele");
            Console.WriteLine("4. Aktif işleri listele");
            Console.WriteLine("5. Biten işleri listele");
            Console.WriteLine("6. Ana menüye dön");
            Console.Write("Seçiminiz: ");
            string inputChoice = Console.ReadLine();

            Console.Clear();

            if (todos.Count == 0)
            {
                Console.WriteLine("Listelenecek iş bulunamadı.");
                ReturnMenu();
            }
            if (inputChoice == "1")
            {
                for (int i = 0; i < todos.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {todos[i].Task}|{todos[i].IsComplete}|{todos[i].Deadline.ToString("dd-MM-yyyy")}");
                    Console.WriteLine("---------------------");
                }

                ReturnMenu();
            }
            else if (inputChoice == "2")
            {
                foreach (var todo in todos)
                {
                    if (todo.Deadline.Date == DateTime.Today)
                    {
                        Console.WriteLine($"{todo.Task}|{todo.IsComplete}|{todo.Deadline.ToString("dd-MM-yyyy")}");
                        Console.WriteLine("---------------------");
                    }
                }

                ReturnMenu();
            }
            else if (inputChoice == "3")
            {
                for (int i = 0; i < todos.Count; i++)
                {
                    Console.Clear();
                    Console.WriteLine($"{i + 1} - {todos[i].Task}|{todos[i].IsComplete}|{todos[i].Deadline.ToString("dd-MM-yyyy")}");
                    Console.WriteLine("---------------------");
                    Console.WriteLine("1. Sonraki || 2. Düzenle || 3. Sil ");
                    string inputListChoice = Console.ReadLine();
                    
                    if (inputListChoice == "1")
                    {
                        
                    }
                    else if (inputListChoice == "2")
                    {
                        Console.Clear();
                        Console.Write("Yeni iş giriniz: ");
                        string newTask = Console.ReadLine();
                        Console.Clear();
                        Console.Write("İşin kaç dakika sonra bitmesi gerekiyor?: ");
                        int newDeadline = int.Parse(Console.ReadLine());
                        todos[i].Task = newTask;
                        todos[i].Deadline = DateTime.Now.AddMinutes(newDeadline);
                        SaveTxt();
                        ReturnMenu();
                    }
                    else if (inputListChoice == "3")
                    {
                        Console.WriteLine("\nKaydı silmek istediğinize emin misiniz? (E/H)");
                        string inputDelete = Console.ReadLine();

                        if (inputDelete == "E" || inputDelete == "e")
                        {
                            todos.RemoveAt(i);
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Kayıt Silindi!");
                            Console.ResetColor();
                            SaveTxt();
                        }
                        else
                        {
                            Console.WriteLine("Kayıt silinmedi.");
                        }
                    }                   
                }

                ReturnMenu();
            }
            else if (inputChoice == "4")
            {
                for (int i = 0; i < todos.Count; i++)
                {
                    if (todos[i].IsComplete == false)
                    {
                        Console.WriteLine($"{i + 1} - {todos[i].Task}|{todos[i].IsComplete}|{todos[i].Deadline.ToString("dd-MM-yyyy HH:mm")}");
                        Console.WriteLine("---------------------");
                    }
                }

                ReturnMenu();
            }
            else if (inputChoice == "5")
            {
                for (int i = 0; i < todos.Count; i++)
                {
                    if (todos[i].IsComplete == true)
                    {
                        Console.WriteLine($"{i + 1} - {todos[i].Task}|{todos[i].IsComplete}|{todos[i].Deadline.ToString("dd-MM-yyyy HH:mm")}");
                        Console.WriteLine("---------------------");
                    }
                }

                ReturnMenu();
            }
            else if (inputChoice == "6")
            {
                ReturnMenu();
            }
        }

        static void AddTodo()
        {
            Console.Clear();
            Console.WriteLine("Eklemek istediğiniz işi yazınız: ");
            string inputAddTodo = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("İşin bitmesi gereken tarihi yazınız. (dd-MM-yyyy HH:MM:SS): ");
            DateTime inputDeadline = DateTime.Parse(Console.ReadLine());

            todos.Add(new Todo
            {
                Task = inputAddTodo,
                IsComplete = false,
                Deadline = inputDeadline
            });

            SaveTxt();
            ReturnMenu();
        }

        static void RemoveTodos()
        {
            Console.WriteLine("\nTüm işleri temizlemek istediğinize emin misiniz? (E/H)");
            string inputDelete = Console.ReadLine();

            if (inputDelete == "E" || inputDelete == "e")
            {
                todos.Clear();
                Console.Clear();
                Console.WriteLine("Tüm işler silindi.");
                SaveTxt();
                ReturnMenu();
            }
            else
            {
                Console.WriteLine("İşler silinmedi.");
                ReturnMenu();
            }
        }

        static void CompleteTask()
        {
            Console.Clear();
            for (int i = 0; i < todos.Count; i++)
            {
                Console.Clear();
                Console.WriteLine($"{i + 1} - {todos[i].Task}|{todos[i].IsComplete}|{todos[i].Deadline.ToString("dd-MM-yyyy HH:mm")}");
                Console.WriteLine("---------------------");
                Console.WriteLine("1. Sonraki || 2. Tamamla || 3. Ana Menü ");
                string inputListChoose = Console.ReadLine();

                if (inputListChoose == "1")
                {
                    break;
                }
                else if (inputListChoose == "2")
                {
                    Console.Clear();
                    Console.WriteLine($"{todos[i].Task} bitirdiğinize emin misiniz? (E/H) ");
                    string inputComplete = Console.ReadLine();

                    if (inputComplete == "E" || inputComplete == "e")
                    {
                        Console.WriteLine($"{todos[i].Task} tamalandı olarak kaydedildi.");
                        todos[i].IsComplete = true;
                        SaveTxt();
                    }
                    else if (inputComplete == "H" || inputComplete == "h")
                    {
                        Console.WriteLine($"{todos[i].Task} tamamlanamadı.");
                    }

                    ReturnMenu();
                }
                else if (inputListChoose == "3")
                {
                    ReturnMenu();
                }
            }
        }        

        static void Main(string[] args)
        {
            ReadTxt();
            ShowMenu(true);
        }
    }
}


