using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorerConsole
{
    internal class Arrow
    {
        public int max;
        public int min;

        public Arrow(int max, int min)
        {
            this.max = max;
            this.min = min;
        }


        private void ClearArrow(int position)
        {
            Console.SetCursorPosition(0, position);
            Console.Write("  ");
        }

        private void CreateArrow(int position) 
        {
            Console.SetCursorPosition(0, position);
            Console.Write("->");
        }

        public void MoveDown(int currentPosition)
        {
            if (currentPosition + 1 <= max)
            {
                ClearArrow(currentPosition);
                CreateArrow(currentPosition + 1);
            }
        }

        public void MoveUp(int currentPosition)
        {
            if (currentPosition - 1 >= min)
            {
                ClearArrow(currentPosition);
                CreateArrow(currentPosition - 1);
            }
        }
    }
}
