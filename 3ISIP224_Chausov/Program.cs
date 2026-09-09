using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP224_Chausov
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество операции (от 0 до 40)");
            int n = Convert.ToInt32(Console.ReadLine());

            string[] names = new string[n];
            double[] money = new double[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Расходы " + (i + 1));
                Console.Write("Название товара или услуг: ");
                names[i] = Console.ReadLine();
                Console.Write("Кол-во денег: ");
                money[i] = double.Parse(Console.ReadLine());
            }
            Console.WriteLine("\n Все расходы:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(names[i] + " - " + money[i] + "руб.");
            }
            double sum = 0;
            double max = money[0];
            double min = money[0];

            for(int i = 0; i < n; i++)
            {
                sum = sum + money[i];
                if (money[i] > max)
                {
                    max = money[i];
                }
                if (money[i] < min) {
                    min = money[i];
                }
            }
            double a = sum / n;
            Console.WriteLine("Сумма: " + sum + "руб");
            Console.WriteLine("Среднее " + a + "руб");
            Console.WriteLine("Максимальное " + max + "руб");
            Console.WriteLine("Минимальное " + min + "руб");
        }
    }
}
