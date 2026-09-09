using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _3ISIP224_Chausov
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите кол-во операции(2 - 40) ");
            int n = int.Parse(Console.ReadLine());
            while (n < 2 || n > 40)
            {
                Console.Write("Введите число (от 0 до 40)");
                n = Convert.ToInt32(Console.ReadLine());
            }

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
            int choise = -1;
            while(choise != 0)
            {
                Console.WriteLine("\n Меню ");
                Console.WriteLine("1. Вывод данных");
                Console.WriteLine("2. Статистика");
                Console.WriteLine("3. Сортировка по цене");
                Console.WriteLine("4. Конвертация валюты");
                Console.WriteLine("5. Поиск по названию");
                Console.WriteLine("0. Выход");

                Console.Write("Выберите пункт: ");
                choise = int.Parse(Console.ReadLine());
                if (choise == 1)
                {
                    Console.WriteLine("\n Все расходы:");
                    for (int i = 0; i < n; i++)
                    {
                        Console.WriteLine(names[i] + " - " + money[i] + "руб.");
                    }
                }
                else if (choise == 2)
                {
                    double sum = 0;
                    double max = money[0];
                    double min = money[0];

                    for (int i = 0; i < n; i++)
                    {
                        sum = sum + money[i];
                        if (money[i] > max)
                        {
                            max = money[i];
                        }
                        if (money[i] < min)
                        {
                            min = money[i];
                        }
                    }
                    double a = sum / n;
                    Console.WriteLine("Сумма: " + sum + "руб");
                    Console.WriteLine("Среднее " + a + "руб");
                    Console.WriteLine("Максимальное " + max + "руб");
                    Console.WriteLine("Минимальное " + min + "руб");
                }
                else if (choise == 3) {
                    for (int i = 0; i < n - 1; i++)
                    {
                        for (int j = 0; j < n - 1 - i; j++)
                        {
                            if (money[j] > money[j + 1])
                            {
                                double temp = money[j];
                                money[j] = money[j + 1];
                                money[j + 1] = temp;

                                string temName = names[j];
                                names[j] = names[j + 1];
                                names[j + 1] = temName;
                            }
                        }
                    }
                    Console.WriteLine("Расходы по возрастанию: ");
                    for(int i = 0; i < n; i++)
                    {
                        Console.WriteLine(names[i] + " - " + money[i] + " руб.");
                    }
                }
                else if (choise == 4)
                {
                    Console.Write("Выберите курс валют");
                    double kurs = double.Parse(Console.ReadLine());

                    double sum = 0;
                    for(int i = 0; i < n; i++)
                    {
                        sum = sum + money[i];
                    }
                    Console.WriteLine("Сумма валют: " + sum / kurs);
                }
                else if (choise == 5)
                {
                    Console.WriteLine("Введите название для поиска: ");
                    string search = Console.ReadLine();

                    bool found = false;
                    for (int i = 0; i < n; i++)
                    {
                        if (names[i].ToLower().Contains(search.ToLower()))
                        {
                            Console.WriteLine(names[i] + " - " + money[i] + " руб.");
                            found = true;
                        }
                    }
                    if (found == false)
                    {
                        Console.WriteLine("Расход не найден");
                    }
                }
            }
        }
    }
}
