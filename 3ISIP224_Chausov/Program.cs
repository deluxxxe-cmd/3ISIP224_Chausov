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
            products.Add(new Product(1001, "Хлеб", Category.Продукты, 60, 20));
            products.Add(new Product(1002, "Футболка", Category.Одежда, 1500, 10));
            products.Add(new Product(1003, "Наушники", Category.Электроника, 3000, 5));
            products.Add(new Product(1004, "Молоко", Category.Продукты, 90, 15));
            products.Add(new Product(1005, "Клавиатура", Category.Электроника, 2500, 0));

            nextCode = 1006;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("========== МАГАЗИН ==========");
                Console.WriteLine("1. Показать все товары");
                Console.WriteLine("2. Добавить товар");
                Console.WriteLine("3. Удалить товар");
                Console.WriteLine("4. Заказать поставку");
                Console.WriteLine("5. Продать товар");
                Console.WriteLine("6. Поиск товара");
                Console.WriteLine("0. Выход");
                Console.WriteLine("==============================");

                Console.Write("Выберите команду: ");
                string command = Console.ReadLine();

                try
                {
                    switch (command)
                    {
                        case "1":
                            ShowAllProducts();
                            break;

                        case "2":
                            AddProduct();
                            break;

                        case "3":
                            DeleteProduct();
                            break;

                        case "4":
                            SupplyProduct();
                            break;

                        case "5":
                            SellProduct();
                            break;

                        case "6":
                            SearchProduct();
                            break;

                        case "0":
                            Console.WriteLine("Программа завершена.");
                            return;

                        default:
                            Console.WriteLine("Такой команды нет.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        // Показать все товары
        static void ShowAllProducts()
        {
            Console.WriteLine();
            Console.WriteLine("========== ВСЕ ТОВАРЫ ==========");

            if (products.Count == 0)
            {
                Console.WriteLine("Товаров нет.");
                return;
            }

            foreach (Product product in products)
            {
                product.Show();
            }
        }

        // Добавить товар
        static void AddProduct()
        {
            Console.WriteLine();
            Console.WriteLine("========== ДОБАВЛЕНИЕ ТОВАРА ==========");

            Console.Write("Введите название: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Название товара не может быть пустым.");
            }

            Console.WriteLine("Выберите категорию:");

            Console.WriteLine("1. Продукты");
            Console.WriteLine("2. Одежда");
            Console.WriteLine("3. Электроника");

            Console.Write("Категория: ");
            string categoryInput = Console.ReadLine();

            Category category;

            if (categoryInput == "1")
            {
                category = Category.Продукты;
            }
            else if (categoryInput == "2")
            {
                category = Category.Одежда;
            }
            else if (categoryInput == "3")
            {
                category = Category.Электроника;
            }
            else
            {
                throw new Exception("Неверная категория.");
            }

            Console.Write("Введите цену: ");
            string priceInput = Console.ReadLine();

            double price;

            if (!double.TryParse(priceInput, out price))
            {
                throw new Exception("Цена должна быть числом.");
            }

            if (price < 0)
            {
                throw new Exception("Цена не может быть отрицательной.");
            }

            Console.Write("Введите количество: ");
            string quantityInput = Console.ReadLine();

            int quantity;

            if (!int.TryParse(quantityInput, out quantity))
            {
                throw new Exception("Количество должно быть целым числом.");
            }

            if (quantity < 0)
            {
                throw new Exception("Количество не может быть отрицательным.");
            }

            Product product = new Product(
                nextCode,
                name,
                category,
                price,
                quantity
            );

            products.Add(product);

            Console.WriteLine($"Товар успешно добавлен.");
            Console.WriteLine($"Код нового товара: {nextCode}");

            nextCode++;
        }

        // Удалить товар
        static void DeleteProduct()
        {
            Console.WriteLine();
            Console.WriteLine("========== УДАЛЕНИЕ ТОВАРА ==========");

            Console.Write("Введите код товара: ");
            string input = Console.ReadLine();

            int code;

            if (!int.TryParse(input, out code))
            {
                throw new Exception("Код должен быть числом.");
            }

            Product product = FindByCode(code);

            if (product == null)
            {
                Console.WriteLine("Товар с таким кодом не найден.");
                return;
            }

            products.Remove(product);

            Console.WriteLine("Товар успешно удалён.");
        }

        // Заказать поставку
        static void SupplyProduct()
        {
            Console.WriteLine();
            Console.WriteLine("========== ПОСТАВКА ==========");

            Console.Write("Введите код товара: ");
            string codeInput = Console.ReadLine();

            int code;

            if (!int.TryParse(codeInput, out code))
            {
                throw new Exception("Код должен быть числом.");
            }

            Product product = FindByCode(code);

            if (product == null)
            {
                Console.WriteLine("Товар не найден.");
                return;
            }

            Console.Write("Введите количество товара для поставки: ");
            string quantityInput = Console.ReadLine();

            int quantity;

            if (!int.TryParse(quantityInput, out quantity))
            {
                throw new Exception("Количество должно быть целым числом.");
            }

            if (quantity <= 0)
            {
                throw new Exception("Количество поставляемого товара должно быть больше нуля.");
            }

            product.Quantity += quantity;

            Console.WriteLine("Поставка выполнена.");
            Console.WriteLine($"Новое количество товара: {product.Quantity}");
        }

        // Продать товар
        static void SellProduct()
        {
            Console.WriteLine();
            Console.WriteLine("========== ПРОДАЖА ==========");

            Console.Write("Введите код товара: ");
            string codeInput = Console.ReadLine();

            int code;

            if (!int.TryParse(codeInput, out code))
            {
                throw new Exception("Код должен быть числом.");
            }

            Product product = FindByCode(code);

            if (product == null)
            {
                Console.WriteLine("Товар не найден.");
                return;
            }

            Console.WriteLine($"Товар: {product.Name}");
            Console.WriteLine($"На складе: {product.Quantity}");

            Console.Write("Введите количество для продажи: ");
            string quantityInput = Console.ReadLine();

            int quantity;

            if (!int.TryParse(quantityInput, out quantity))
            {
                throw new Exception("Количество должно быть целым числом.");
            }

            if (quantity <= 0)
            {
                throw new Exception("Количество для продажи должно быть больше нуля.");
            }

            if (quantity > product.Quantity)
            {
                throw new Exception("Недостаточно товара на складе.");
            }

            product.Quantity -= quantity;

            double totalPrice = quantity * product.Price;

            Console.WriteLine("Продажа выполнена.");
            Console.WriteLine($"Продано: {quantity} шт.");
            Console.WriteLine($"Сумма: {totalPrice} руб.");
            Console.WriteLine($"Осталось на складе: {product.Quantity}");
        }

        // Поиск товара
        static void SearchProduct()
        {
            Console.WriteLine();
            Console.WriteLine("========== ПОИСК ==========");

            Console.WriteLine("1. По коду");
            Console.WriteLine("2. По названию");
            Console.WriteLine("3. По категории");

            Console.Write("Выберите способ поиска: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Введите код: ");
                string input = Console.ReadLine();

                int code;

                if (!int.TryParse(input, out code))
                {
                    throw new Exception("Код должен быть числом.");
                }

                Product product = FindByCode(code);

                if (product == null)
                {
                    Console.WriteLine("Товар не найден.");
                }
                else
                {
                    product.Show();
                }
            }
            else if (choice == "2")
            {
                Console.Write("Введите название: ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new Exception("Название не может быть пустым.");
                }

                bool found = false;

                foreach (Product product in products)
                {
                    if (product.Name.ToLower() == name.ToLower())
                    {
                        product.Show();
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Товар не найден.");
                }
            }
            else if (choice == "3")
            {
                Console.WriteLine("1. Продукты");
                Console.WriteLine("2. Одежда");
                Console.WriteLine("3. Электроника");

                Console.Write("Выберите категорию: ");
                string categoryInput = Console.ReadLine();

                Category category;

                if (categoryInput == "1")
                {
                    category = Category.Продукты;
                }
                else if (categoryInput == "2")
                {
                    category = Category.Одежда;
                }
                else if (categoryInput == "3")
                {
                    category = Category.Электроника;
                }
                else
                {
                    throw new Exception("Неверная категория.");
                }

                bool found = false;

                foreach (Product product in products)
                {
                    if (product.Category == category)
                    {
                        product.Show();
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("В этой категории товаров нет.");
                }
            }
            else
            {
                Console.WriteLine("Неверный вариант поиска.");
            }
        }

        // Поиск товара по коду
        static Product FindByCode(int code)
        {
            foreach (Product product in products)
            {
                if (product.Code == code)
                {
                    return product;
                }
            }

            return null;
        }
    }
}
