using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactorable
{
    class Program
    {
        private static void WriteUpdatedCars(string driver, IList<int> horsepowers)
        {
            Console.WriteLine($"{driver}'s cars have following horsepowers:");
            foreach (var hp in horsepowers)
            {
                Console.WriteLine(hp);
            }
        }
        static void Main(string[] args)
        {
            var driverList = new[] { "Kiraa", "Krieger", "Kiraa", "Mlpard", "Lana", "Lana", "Sterling", "Sterling" };

            Func<string, Car> generateCar = driver => new Car()
            {
                Driver = driver,
                Horsepower = driver.Length * 1000 + new Random().Next(2000)
            };

            var carList = driverList.Select(driver => generateCar(driver));

            var storage = new CarEvidence();
            foreach (var info in carList.Select(c => c.GetInfo()))
            {
                if (info.Item1 < 5000) //car too slow to worth filing
                {
                    continue;
                }
                storage.Add(info.Item2, info.Item1);
            }

            var sterlingsCarsHorsepowers = new List<int> { };
            var sterlingsSuccess = storage.TryBoostCars("Sterling", 1000, out sterlingsCarsHorsepowers);

            if (sterlingsSuccess)
            {
                WriteUpdatedCars("Sterling", sterlingsCarsHorsepowers);
            }
            else
            {
                Console.WriteLine("Sterling somehow lost all his cars");
            }

            var lanaCarsHorsepowers = new List<int> { };
            var lanaSuccess = storage.TryBoostCars("Lana", 1000, out sterlingsCarsHorsepowers);

            if (lanaSuccess)
            {
                WriteUpdatedCars("Lana", lanaCarsHorsepowers);
            }
            else
            {
                Console.WriteLine("Lana does not actually own any cars worth filing");
            }
        }
    }


    class Car
    {
        public const int MaxHorsepower = 20084781;

        public Car() { }
        public Car(int horsepower, string driverName)
        {
            Horsepower = horsepower;
            Driver = driverName;
        }

        private int _horsepower;
        public int Horsepower
        {
            get
            {
                return _horsepower;
            }
            set
            {
                if (value < MaxHorsepower)
                {
                    _horsepower = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public string Driver { get; set; }
        public IEnumerable<string> PartsList { get; set; }

        public Tuple<int, string> GetInfo() => Tuple.Create(Horsepower, Driver);

    }

    class CarEvidence
    {
        private Dictionary<string, List<int>> garage = new Dictionary<string, List<int>>();

        public void Add(string driver, int horspower)
        {
            if (!garage.ContainsKey(driver))
            {
                garage.Add(driver, new List<int>() { });

            }
            garage[driver].Add(horspower);
        }

        public bool TryBoostCars(string driver, int horsepower, out List<int> cars)
        {
            if (!garage.ContainsKey(driver))
            {
                cars = null;
                return false;
            }
            cars = garage[driver];
            return true;
        }

        public static string EvaluateDriver(string driver)
        {
            if (driver == null)
            {
                return $"{driver} is not a driver";
            }
            else if (driver.Length <= 3)
            {
                return $"{driver} is a bad driver";
            }
            else if (driver.Length <= 4)
            {
                return $"{driver} is an okay-ish driver";
            }
            else
            {
                return $"{driver} is a good driver";
            }
        }
    }
}
