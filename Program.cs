using System;
using System.Diagnostics;
using System.IO;

namespace ExplorerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Explorer.start();
                Console.SetCursorPosition(0, 2);
                Console.Write("->");
                string drive_path = Explorer.choose_drive();

                Explorer.choose_items(drive_path);
            }
        }
    }
}