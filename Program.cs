using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet5
{
    public record Person
    {
        public string LastName { get; }
        public string FirstName { get; set; }

        public Person(string first, string last) => (FirstName, LastName) = (first, last);
    }

    public record Teacher : Person
    {
        public string Subject { get; }  

        public Teacher(string first, string last, string sub)
            : base(first, last) => Subject = sub;
    }

    public sealed record Student : Person
    {
        public int Level { get; }

        public Student(string first, string last, int level) : base(first, last) => Level = level;
	}

	public record Pet(string Name)
	{
		public void ShredTheFurniture() =>
			Console.WriteLine("Shredding furniture");
	}

	public record Dog(string Name) : Pet(Name)
	{
		public void WagTail() =>
			Console.WriteLine("It's tail wagging time");

		public override string ToString()
		{
			StringBuilder s = new();
			base.PrintMembers(s);
			return $"{s.ToString()} is a dog";
		}

		public static bool IsLetter(char c) => 
			c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';

		

	}

	public struct SomeClass
	{
		public double X { get; set; }

		public readonly bool Test()
		{
			
			return 3 * this.X > 0;
		}
	}

	public interface IData
	{
		int x { get; init; }
		int y { get;  init; }

		public decimal Calculate() => CalculateDefault(this);
		protected static int CalculateDefault(IData d)
		{
			return d.x + d.y;
		}
	}

	public class Data: IData
	{
		public int x { get; init; }
		public int y { get;  init; }

		public int Calculate()
		{
			
			return 777;
		}

		public enum Rainbow
		{
			Red,
			Orange,
			Yellow,
			Green,
			Blue,
			Indigo,
			Violet
		}

		public static RGBColor FromRainbow(Rainbow colorBand) =>
		colorBand switch
		{
			Rainbow.Red    => new RGBColor(0xFF, 0x00, 0x00),
			Rainbow.Orange => new RGBColor(0xFF, 0x7F, 0x00),
			Rainbow.Yellow => new RGBColor(0xFF, 0xFF, 0x00),
			Rainbow.Green  => new RGBColor(0x00, 0xFF, 0x00),
			Rainbow.Blue   => new RGBColor(0x00, 0x00, 0xFF),
			Rainbow.Indigo => new RGBColor(0x4B, 0x00, 0x82),
			Rainbow.Violet => new RGBColor(0x94, 0x00, 0xD3),
			_              => throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand)),
		};
	}

    public record RGBColor(int r, int g, int b);

    public class Data2: IData
	{
		public int x { get; init; }
		public int y { get;  init; }

		public int Calculate()
		{
			if (x > 10) {
				return IData.CalculateDefault(this);
			}
			return 777;
		}
	}

	public class EnumClass {
		public static async System.Collections.Generic.IAsyncEnumerable<int> GenerateSequence()
		{
			for (int i = 0; i < 20; i++) {
				await Task.Delay(100);
				yield return i;
			}
		}

		public async Task Run()
		{
			await foreach (var number in GenerateSequence()) {
				Console.WriteLine(number);
			}
		}
	}

    public class Program
    {
		
		static async Task Main(string[] args)
        {
			using var file = new System.IO.StreamWriter("WriteLines2.txt");

			Console.WriteLine("Hello World!");

            var person = new Person("Bill", "Wagner");
            var student = new Student("Bill", "Wagner", 11);

            Console.WriteLine(student == person); // false
			Console.WriteLine(person);

			var dog = new Dog("Dog name");
			Console.WriteLine(dog);

			// var (first, last) = person;
			// Console.WriteLine(first);
			// Console.WriteLine(last);

			Person brother = person with { FirstName = "Paul" };
			var brother2 = brother with { };
			brother2.FirstName = "New name";

			Console.WriteLine(brother);
			Console.WriteLine(brother2);

			IData d = new Data { x = 5, y = 9};
			Console.WriteLine(d.Calculate());

			Data2 d2 = new Data2 { x = 11, y = 9};
			Console.WriteLine(d2.Calculate());

			var enumClass = new EnumClass();
			await enumClass.Run();

			var words = new string[]
			{
							// index from start    index from end
				"The",      // 0                   ^9
				"quick",    // 1                   ^8
				"brown",    // 2                   ^7
				"fox",      // 3                   ^6
				"jumped",   // 4                   ^5
				"over",     // 5                   ^4
				"the",      // 6                   ^3
				"lazy",     // 7                   ^2
				"dog"       // 8                   ^1
			};

			Console.WriteLine($"The last word is {words[^1]}");
			var quickBrownFox = words[1..4];

			List<int> numbers = null;
			numbers ??= new List<int> { 42 };
			Console.WriteLine(numbers.First());

			var someString = $@"...{45}";
			var someString2 = @$"...{45}";

			Console.WriteLine(someString2);
        }
    }
}
