using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declare variables
            string rowText;
            int rowNum;
            string columnText;
            int columnNum;
            int answerNum;

            Console.WriteLine("Please enter the row and column for the Pascal Triangle value or type \"Exit\" to quit");

            // Loop until break
            while (true)
            {
                // Reset variables
                rowText = "";
                rowNum = 0;
                columnText = "";
                columnNum = 0;
                answerNum = 0;

                // Enter row
                Console.Write("Row:");
                rowText = Console.ReadLine();

                // Check row
                if (String.Equals("Exit", rowText, StringComparison.OrdinalIgnoreCase) == true)
                {
                    break;
                }
                else if(!(int.TryParse(rowText, out rowNum)))
                {
                    Console.WriteLine("Please enter an integer value");
                    continue;
                }

                // Enter column
                Console.Write("Column:");
                columnText = Console.ReadLine();

                // Check column
                if (string.Equals("Exit", columnText, StringComparison.OrdinalIgnoreCase) == true)
                {
                    break;
                }
                else if (!(int.TryParse(columnText, out columnNum)))
                {
                    Console.WriteLine("Please enter an integer value");
                    Console.WriteLine("");
                    continue;
                }
                else if (columnNum - rowNum > 1)
                {
                    Console.WriteLine("Column number is too large");
                    Console.WriteLine("");
                    continue;
                }

                answerNum = GetPascalValue(rowNum, columnNum);

                Console.WriteLine("Answer: {0}", answerNum);
                Console.WriteLine("");
            }

        }

        public static int GetPascalValue(int rowNum, int ColumnNum)
        {
            // Top row, first and last column
            if ((rowNum + 1) == 1 || (ColumnNum) == 1 || (rowNum + 1) == ColumnNum)
            {
                return 1;
            }
            else
            {
                return GetPascalValue(rowNum - 1, ColumnNum - 1) + GetPascalValue(rowNum - 1, ColumnNum);
            }
        }
    }
}
