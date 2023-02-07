using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorerConsole
{
    internal class Explorer
    {
        public static void start()
        {
            Console.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            Console.SetCursorPosition(Console.WindowWidth / 2, 0);
            Console.WriteLine("Выберите диск");
            string delimeter = new string('-', Console.WindowWidth);
            Console.WriteLine(delimeter);
            Console.SetCursorPosition(2, 2);

            foreach (DriveInfo drive in drives)
            {
                Console.Write(drive.Name);
                Console.SetCursorPosition(30, Console.GetCursorPosition().Top);
                Console.Write($"Всего {drive.TotalSize / (1024 * 1024 * 1024)} ГБ");
                Console.SetCursorPosition(60, Console.GetCursorPosition().Top);
                Console.Write($"Свободно {drive.TotalFreeSpace / (1024 * 1024 * 1024)} ГБ");
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top + 1);
            }
        }

        public static string choose_drive()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            int y = 0;
            string drive_path = "";
            bool flag = true;
            Arrow arrow = new Arrow(1 + drives.Length, 2);

            while (flag)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        arrow.MoveDown(Console.GetCursorPosition().Top);
                        break;
                    case ConsoleKey.UpArrow:
                        arrow.MoveUp(Console.GetCursorPosition().Top);
                        break;
                    case ConsoleKey.Enter:
                        drive_path = drives[Console.GetCursorPosition().Top - 2].RootDirectory.ToString();
                        flag = false;
                        break;
                }
            }
            return drive_path;
        }
        private static void print_directory(string path)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2, 0);
            Console.WriteLine($"Папка {Path.GetFileName(path)}");
            string delimeter = new string('-', Console.WindowWidth);
            Console.WriteLine(delimeter);
            Console.SetCursorPosition(10, 3);
            Console.Write("Название");
            Console.SetCursorPosition(40, 3);
            Console.Write("Дата создания");
            Console.SetCursorPosition(70, 3);
            Console.Write("Тип");
            Console.SetCursorPosition(Console.WindowWidth - 26, 4);
            Console.Write("| F1 - Создать папку");
            Console.SetCursorPosition(Console.WindowWidth - 26, 5);
            Console.Write("| F2 - Создать файл");
            Console.SetCursorPosition(Console.WindowWidth - 26, 6);
            Console.Write("| F3 - Удалить");
            Console.SetCursorPosition(Console.WindowWidth - 26, 7);
            delimeter = new string('-', 25);
            Console.WriteLine($"|{delimeter}");
            Console.SetCursorPosition(Console.WindowWidth - 26, 8);
            Console.Write("|");
            Console.SetCursorPosition(0, 4);

            string[] items = Directory.GetDirectories(path).Union(Directory.GetFiles(path)).ToArray();

            if (items != null)
            {
                Console.Write("->");
            }
            try
            {
                foreach (string item in items)
                {
                    FileAttributes attr = File.GetAttributes(item);
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Console.Write($"{Path.GetFileName(item)}");
                        Console.SetCursorPosition(2, Console.GetCursorPosition().Top + 1);
                    }
                    else
                    {
                        Console.Write($"{Path.GetFileName(item)}");
                        Console.SetCursorPosition(38, Console.GetCursorPosition().Top);
                        Console.Write($"{File.GetCreationTime(item)}");
                        Console.SetCursorPosition(70, Console.GetCursorPosition().Top);
                        Console.Write($"{Path.GetExtension(item)}");
                        Console.SetCursorPosition(2, Console.GetCursorPosition().Top + 1);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void choose_items(string path)
        {
            print_directory(path);

            string[] items = Directory.GetDirectories(path).Union(Directory.GetFiles(path)).ToArray();

            int y = 0;
            bool flag = true;
            string newpath = "";
            Arrow arrow = new Arrow(3 + items.Length, 4);

            Console.SetCursorPosition(0, 4);
            while (flag)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        arrow.MoveDown(Console.GetCursorPosition().Top);
                        break;
                    case ConsoleKey.UpArrow:
                        arrow.MoveUp(Console.GetCursorPosition().Top);
                        break;
                    case ConsoleKey.Escape:
                        if (path.Split('\\').Length > 1)
                        {
                            try
                            {
                                newpath = Directory.GetParent(path).ToString();

                            }
                            catch (NullReferenceException)
                            {

                            }
                        }

                        flag = false;
                        break;
                    case ConsoleKey.F1:
                        Console.SetCursorPosition(Console.WindowWidth - 24, 8);
                        Console.Write("Название папки: ");
                        Directory.CreateDirectory(path + Console.ReadLine());
                        Console.SetCursorPosition(Console.WindowWidth - 24, 8);
                        Console.Write(new string(' ', 24));
                        newpath = path;
                        flag = false;
                        break;
                    case ConsoleKey.F2:
                        Console.SetCursorPosition(Console.WindowWidth - 24, 8);
                        Console.Write("Название файла: ");
                        File.Create(path + "\\" + Console.ReadLine());
                        Console.SetCursorPosition(Console.WindowWidth - 24, 8);
                        Console.Write(new string(' ', 24));
                        newpath = path;
                        flag = false;
                        break;
                    case ConsoleKey.F3:
                        FileAttributes attr1 = File.GetAttributes(items[Console.GetCursorPosition().Top - 4]);
                        if ((attr1 & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            Directory.Delete(items[Console.GetCursorPosition().Top - 4]);
                        }
                        else
                        {
                            File.Delete(items[Console.GetCursorPosition().Top - 4]);
                        }
                        newpath = path;
                        flag = false;
                        break;
                    case ConsoleKey.Enter:
                        try
                        {
                            FileAttributes attr = File.GetAttributes(items[Console.GetCursorPosition().Top - 4]);
                            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                            {
                                newpath = items[Console.GetCursorPosition().Top - 4];
                                flag = false;
                            }
                            else
                            {
                                var fileToOpen = items[Console.GetCursorPosition().Top - 4];
                                var process = new Process();
                                process.StartInfo = new ProcessStartInfo()
                                {
                                    UseShellExecute = true,
                                    FileName = fileToOpen
                                };

                                process.Start();
                                try
                                {
                                    process.WaitForExit();
                                }
                                catch (InvalidOperationException e)
                                {

                                }
                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {

                        }
                        break;
                }
            }
            if (newpath != "")
            {
                choose_items(newpath);
            }
        }
    }
}
