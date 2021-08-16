using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matador
{
    class Program
    {
        static void Main(string[] args)
        {
            // build game
            var game = new Matador();
            game.AddStreet("Rådhusplads");
            game.AddStreet("Fængsel");
            game.AddStreet("Hjørring");
            game.AddStreet("Viborg");
            game.AddStreet("Ålborg");
            game.AddStreet("Århus");

            var availableCars = new List<Car> { new Car { Name = "Ford" }, new Car { Name = "BMW" },
                new Car { Name = "Toyota" }, new Car { Name = "Ferarri" }, new Car { Name = "Volkswagen" } };

            Console.WriteLine("\n--WELCOME TO MATADOR--\n(Type exit to stop.)");

            // Number of players
            Console.WriteLine("\nNumber of players: ( max 5 )");
            var input = Console.ReadLine();

            if (input.ToLower() != "exit")
            {
                var valid = int.TryParse(input, out int nr);

                // Check valid number of players
                while (!valid || nr < 1 || nr > 5)
                {
                    Console.WriteLine("Not a valid number!");
                    input = Console.ReadLine();
                    if (input.ToLower() == "exit") break;
                    valid = int.TryParse(input, out nr);
                }

                // assign cars to players
                for (int i = 0; i < nr; i++)
                {
                    game.Cars.Add(availableCars[i]);
                }
                // put cars on Start
                game.CarsToStart();
                game.PrintBoard();

                // play the game
                input = "";
                while (input.ToLower() != "2")
                {
                    Console.WriteLine("\nChose option: \n1.Move car\n2.Exit");
                    input = Console.ReadLine().Trim().ToLower();
                    switch (input)
                    {
                        case "1":
                            // move car
                            Console.WriteLine("Chose car to move:");
                            var car = Console.ReadLine().ToLower();
                            Console.WriteLine("How many spaces to move:");
                            var distance = Console.ReadLine().ToLower();
                            var validNr = int.TryParse(distance, out int d);
                            game.MoveCar(car, d);
                            game.PrintBoard();
                            break;
                        case "2":
                            break;
                        default:
                            break;
                    }
                    

                }
            }
        }
    }
    class Matador
    {
        public Street start;
        public List<Car> Cars = new List<Car>();
        public Matador()
        {
            start = new Street { Name = "Start" };
            start.next = start;
        }
        public void CarsToStart()
        {
            foreach(var car in Cars)
            {
                car.Street = start;
            }
        }
        public void AddStreet(string name)
        {
            var temp = start;
            while(temp.next != start)
            {
                temp = temp.next;
            }
            temp.next = new Street { Name = name };
            temp.next.next = start;
        }
        public void PrintBoard()
        {
            var temp = start;
            Console.WriteLine("\n--BOARD--\n");
            Console.Write(temp.Name);
            while (temp.next != start)
            {
                foreach(var car in Cars)
                {
                    if (car.Street == temp)
                    {
                        Console.Write($" --- {car.Name}");
                    }
                }
                temp = temp.next;
                Console.Write($"\n{temp.Name}");
            }
            Console.WriteLine("\n\n");
        }
        public void MoveCar(string carName, int distance)
        {
            var car = Cars.Where(x => x.Name.ToLower() == carName).FirstOrDefault();
            if (car == null)
            {
                Console.WriteLine("Car name doesn't exist!");
            }
            else
            {
                for (int i = 0; i < distance; i++)
                {
                    car.Street = car.Street.next;
                }
                Console.WriteLine($"Car moved to {car.Street.Name}\n");
            }
        }
        
    }
    class Street
    {
        public string Name;
        public int Price;
        public Street next;
    }
    class Car
    {
        public string Name;
        public Street Street;
        public int Money;
    }
}
