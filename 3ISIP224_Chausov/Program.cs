using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP224_Chausov
{
    class Weapon
    {
        public string Name { get; private set; }
        public int Damage { get; private set; }

        public Weapon(string name, int damage)
        {
            Name = name;
            Damage = damage;
        }

        public override string ToString()
        {
            return Name + " (урон: " + Damage + ")";
        }
    }

    class Armor
    {
        public string Name { get; private set; }
        public int Defense { get; private set; }

        public Armor(string name, int defense)
        {
            Name = name;
            Defense = defense;
        }

        public override string ToString()
        {
            return Name + " (защита: " + Defense + ")";
        }
    }

    class Player
    {
        public int MaxHp { get; private set; } = 100;
        public int Hp { get; private set; } = 100;

        public Weapon Weapon { get; private set; }
        public Armor Armor { get; private set; }

        public bool Frozen { get; set; }

        public Player()
        {
            Weapon = new Weapon("Ржавый меч", 15);
            Armor = new Armor("Старая одежда", 5);
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                damage = 0;

            Hp -= damage;

            if (Hp < 0)
                Hp = 0;
        }

        public void Heal()
        {
            Hp = MaxHp;
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (weapon != null)
                Weapon = weapon;
        }

        public void EquipArmor(Armor armor)
        {
            if (armor != null)
                Armor = armor;
        }

        public void ShowInfo()
        {
            Console.WriteLine("\nЗдоровье: " + Hp + "/" + MaxHp);
            Console.WriteLine("Оружие: " + Weapon);
            Console.WriteLine("Доспехи: " + Armor);
        }
    }

    class Enemy
    {
        public string Name { get; private set; }
        public int Hp { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }

        public int CritChance { get; private set; }
        public int FreezeChance { get; private set; }
        public bool IgnoreArmor { get; private set; }

        public Enemy(string name, int hp, int attack, int defense)
        {
            Name = name;
            Hp = hp;
            Attack = attack;
            Defense = defense;
        }

        public void SetCritChance(int chance)
        {
            CritChance = Math.Max(0, Math.Min(100, chance));
        }

        public void SetFreezeChance(int chance)
        {
            FreezeChance = Math.Max(0, Math.Min(100, chance));
        }

        public void SetIgnoreArmor(bool value)
        {
            IgnoreArmor = value;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                damage = 0;

            Hp -= damage;

            if (Hp < 0)
                Hp = 0;
        }

        public bool IsAlive()
        {
            return Hp > 0;
        }

        public virtual void AttackPlayer(Player player, Random random)
        {
            int damage = Attack;

            if (random.Next(100) < CritChance)
            {
                damage *= 2;
                Console.WriteLine("Критический удар!");
            }

            if (random.Next(100) < FreezeChance)
            {
                player.Frozen = true;
                Console.WriteLine("Игрок заморожен!");
            }

            Game.ResolveEnemyAttack(player, this, damage, random);
        }
    }

    class Goblin : Enemy
    {
        public Goblin() : base("Гоблин", 35, 12, 3)
        {
            SetCritChance(20);
        }
    }

    class Skeleton : Enemy
    {
        public Skeleton() : base("Скелет", 45, 14, 5)
        {
            SetIgnoreArmor(true);
        }
    }

    class Mage : Enemy
    {
        public Mage() : base("Маг", 30, 16, 2)
        {
            SetFreezeChance(20);
        }
    }

    class Boss : Enemy
    {
        public Boss(string name, int hp, int attack, int defense)
            : base(name, hp, attack, defense)
        {
        }
    }

    class Game
    {
        private Random random = new Random();
        private Player player = new Player();
        private int turn = 0;
        private bool running = true;

        private static bool isDefending;

        public void Start()
        {
            Console.WriteLine(" ТЕКСТОВЫЙ РОГАЛИК ");

            while (running && player.Hp > 0)
            {
                Console.WriteLine("\n ");
                Console.WriteLine("Ход: " + (turn + 1));
                player.ShowInfo();

                Console.WriteLine("\nНажмите Enter, чтобы продолжить, или 0 для выхода.");

                string input = Console.ReadLine();

                if (input == "0")
                    break;

                turn++;

                if (turn % 10 == 0)
                {
                    Console.WriteLine("\nПоявился босс!");
                    Fight(CreateBoss());
                }
                else if (random.Next(2) == 0)
                {
                    FindChest();
                }
                else
                {
                    Fight(CreateEnemy());
                }
            }

            if (player.Hp <= 0)
            {
                Console.WriteLine("\nВы погибли. Игра окончена.");
            }
            else
            {
                Console.WriteLine("\nИгра завершена.");
            }
            Console.WriteLine("Всего пройдено ходов: " + turn);
        }

        private Enemy CreateEnemy()
        {
            int type = random.Next(3);

            if (type == 0)
                return new Goblin();

            if (type == 1)
                return new Skeleton();

            return new Mage();
        }

        private Enemy CreateBoss()
        {
            int type = random.Next(4);

            if (type == 0)
            {
                Goblin goblin = new Goblin();

                Boss boss = new Boss("ВВГ", goblin.Hp * 2, (int)(goblin.Attack * 1.5), (int)(goblin.Defense * 1.2));

                boss.SetCritChance(goblin.CritChance + 10);
                return boss;
            }

            if (type == 1)
            {
                Skeleton skeleton = new Skeleton();

                Boss boss = new Boss("Ковальский", (int)(skeleton.Hp * 2.5), (int)(skeleton.Attack * 1.3), (int)(skeleton.Defense * 1.4));

                boss.SetIgnoreArmor(true);
                return boss;
            }

            if (type == 2)
            {
                Mage mage = new Mage();

                Boss boss = new Boss("Архимаг C++", (int)(mage.Hp * 1.8), (int)(mage.Attack * 1.6), (int)(mage.Defense * 1.1));

                boss.SetFreezeChance(mage.FreezeChance + 10);
                return boss;
            }

            Mage baseMage = new Mage();
            Skeleton baseSkeleton = new Skeleton();

            Boss pestov = new Boss("Пестов C--", (int)(baseSkeleton.Hp * 1.3), (int)(baseSkeleton.Attack * 1.8), (int)(baseSkeleton.Defense * 0.6));

            pestov.SetIgnoreArmor(true);
            pestov.SetFreezeChance(baseMage.FreezeChance + 15);

            return pestov;
        }

        private void Fight(Enemy enemy)
        {
            Console.WriteLine("\nВраг: " + enemy.Name);
            Console.WriteLine("HP: " + enemy.Hp + ", атака: " + enemy.Attack + ", защита: " + enemy.Defense);

            while (enemy.IsAlive() && player.Hp > 0)
            {
                bool defending = false;

                if (player.Frozen)
                {
                    Console.WriteLine("\nВы заморожены и пропускаете ход!");

                    player.Frozen = false;
                }
                else
                {
                    Console.WriteLine("\n1. Атаковать");
                    Console.WriteLine("2. Защищаться (шанс защиты 50%)");

                    int choice = ReadChoice(1, 2);

                    if (choice == 1)
                    {
                        int damage = Math.Max(1, player.Weapon.Damage - enemy.Defense);

                        enemy.TakeDamage(damage);

                        Console.WriteLine("Вы нанесли " + damage + " урона.");

                        if (!enemy.IsAlive())
                            break;
                    }
                    else
                    {
                        defending = true;
                        Console.WriteLine("Вы встали в защиту.");
                    }
                }

                isDefending = defending;
                enemy.AttackPlayer(player, random);
            }

            if (player.Hp > 0)
                Console.WriteLine("Враг " + enemy.Name + " побеждён!");
        }

        public static void ResolveEnemyAttack(Player player, Enemy enemy, int damage, Random random)
        {
            if (isDefending)
            {
                if (random.Next(100) < 40)
                {
                    Console.WriteLine("Вы уклонились от атаки!");
                    return;
                }

                if (enemy.IgnoreArmor)
                {
                    Console.WriteLine("Враг игнорирует вашу броню!");
                }
                else
                {
                    int blockPercent = random.Next(70, 101);

                    int blocked = (int)(player.Armor.Defense * blockPercent / 100.0);

                    damage -= blocked;

                    if (damage < 0)
                        damage = 0;

                    Console.WriteLine("Блок поглотил " + blocked + " урона.");
                }
            }
            else if (enemy.IgnoreArmor)
            {
                Console.WriteLine("Враг игнорирует вашу броню!");
            }

            player.TakeDamage(damage);

            Console.WriteLine("Враг нанёс вам " + damage + " урона.");
        }

        private void FindChest()
        {
            Console.WriteLine("\nВы нашли сундук!");

            int item = random.Next(3);

            if (item == 0)
            {
                player.Heal();

                Console.WriteLine("Вы нашли лечебное зелье. Здоровье восстановлено!");
            }
            else if (item == 1)
            {
                Weapon weapon = new Weapon("Меч путешественника", random.Next(18, 31));

                Console.WriteLine("Новое оружие: " + weapon);
                Console.WriteLine("Текущее оружие: " + player.Weapon);
                Console.WriteLine("1. Взять  2. Выбросить");

                if (ReadChoice(1, 2) == 1)
                {
                    player.EquipWeapon(weapon);
                    Console.WriteLine("Вы сменили оружие.");
                }
                else
                {
                    Console.WriteLine("Вы выбросили оружие.");
                }
            }
            else
            {
                Armor armor = new Armor("Кольчуга", random.Next(8, 21));

                Console.WriteLine("Новые доспехи: " + armor);
                Console.WriteLine("Текущие доспехи: " + player.Armor);
                Console.WriteLine("1. Взять  2. Выбросить");

                if (ReadChoice(1, 2) == 1)
                {
                    player.EquipArmor(armor);
                    Console.WriteLine("Вы сменили доспехи.");
                }
                else
                {
                    Console.WriteLine("Вы выбросили доспехи.");
                }
            }
        }

        private int ReadChoice(int min, int max)
        {
            while (true)
            {
                Console.Write("Ваш выбор: ");

                string input = Console.ReadLine();
                int choice;

                if (int.TryParse(input, out choice) && choice >= min && choice <= max)
                {
                    return choice;
                }

                Console.WriteLine("Ошибка. Введите число от " + min + " до " + max + ".");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}