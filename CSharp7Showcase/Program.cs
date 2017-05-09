using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7Showcase
{
    class Person
    {
        public int Age { get; set; }
        public bool LikesExplosives { get; set; }
        public int FingerCount { get; set; }

        public Person() { }

        //Expression-bodied members
        //now works with constructors,
        //getters, setters,
        //...
        public Person(int age, bool explodes, int fingers) =>
            (Age, LikesExplosives, FingerCount) = (age, LikesExplosives, FingerCount);


        //Tuple deconstructor
        public void Deconstruct(out int age, out int count)
        {
            age = Age;
            count = FingerCount;
        }

        public void Deconstruct(out bool explosive, out int age, out int count)
        {
            explosive = LikesExplosives;
            age = Age;
            count = FingerCount;
        }
    }

    class Program
    {
        //Digit separators
        public const long HugeNumber = 134225542542;
        public const long NewHugeNumber = 134_225_542_542;

        [Flags]
        public enum DrinkContents
        {
            Juice = 1,
            Milk = 2,
            Water = 4,
            Alcohol = 8
        }

        //Binary format
        [Flags]
        public enum NewDrinkContents
        {
            Juice = 0b0000_0001,
            Milk = 0b0000_0010,
            Water = 0b0000_0100,
            Alcohol = 0b0000_1000
        }

        static void Main(string[] args)
        {
            //Tuples
            var oldTuple = Tuple.Create(30, 40);
            var newTuple = (30, 40); //value type

            //Named tuples
            var namedTuple = (numberOfKittens: 30, numberOfPuppies: 40);

            //Accessing tuple fields
            var kittens = newTuple.Item1;
            var puppies = newTuple.Item2;

            kittens = namedTuple.numberOfKittens;
            puppies = namedTuple.numberOfPuppies;

            //Tuple Deconstruction
            (kittens, puppies) = namedTuple;

            var p = new Person { Age = 15, FingerCount = 20, LikesExplosives = false };

            //Object Deconstruction - must implemenet Deconstruct method
            var (age, fingers) = p;

            //The _ thingy I have no idea how to call 
            //because i stumbled upon it by accident and I have found no docs
            var (explosive, _, _) = p;
            //Console.WriteLine(_); //nefunguje
            Console.WriteLine(explosive);

            //Local Functions
            Func<int, int> MinusOne = n => n - 1;

            int PlusOne(int n) => n + 1;

            int PlusTwo(int n)
            {
                return n + 2;
            }

            Console.WriteLine(PlusTwo(age));

            var input = Console.ReadLine();

            //Out Var
            int.TryParse(input, out var parsedInput1);
            Console.WriteLine(parsedInput1);

            //The _ thingy I have no idea how to call 
            //because i stumbled upon it by accident and I have found no docs
            var isParsable = int.TryParse(input, out var _);


            new Person()
            {
                LikesExplosives = true,
                FingerCount = 19,
                Age = 43
            }.Deconstruct(out explosive, out var _, out var _);


            //Is-expression pattern matching
            int? ProcessInput(object o)
            {
                if (o is int i
                    || (o is string s && int.TryParse(s, out i)))
                {
                    return i;
                }
                else
                {
                    return null;
                }
            }

            var parsedInput2 = ProcessInput(input);

            //Switch-statment pattern matching
            switch (parsedInput2)
            {
                case int i when i % 5 == 0 && i % 3 == 0:
                    Console.WriteLine("FizzBuzz");
                    break;
                case int i when i % 3 == 0:
                    Console.WriteLine("Fizz");
                    break;
                case int i when i % 5 == 0:
                    Console.WriteLine("Buzz");
                    break;
                default:
                    Console.WriteLine(parsedInput2);
                    break;
                //null matching -- evaulates before default
                case null:
                    Console.WriteLine("Null");
                    break;
            }

            //Throw expressions
            var repIsParsable = isParsable
                ? "Sparsovatelne"
                : throw new ArgumentException("Input musi byt sparsovatelny!");
        }

        //And more to come...
        //Also, resharper is catching up!
    }
}
