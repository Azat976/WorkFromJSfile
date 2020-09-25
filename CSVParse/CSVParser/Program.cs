using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CSVParser
{

	public static class Program
	{
		public class Item
		{
			public string Name;
			public int Price;
			public int Index;

			public Item(string name, int price, int index)
			{
				Name = name;
				Price = price;
				Index = index;
			}
		}

		public static void Main()
		{
			Console.WriteLine("Reading, wait...");
			var file = File.ReadAllLines("1.csv", Encoding.UTF8);

			Console.WriteLine("Reading ended");
			Console.WriteLine("Serializing");

			var names = new List<Item>();
			var buffer = new HashSet<string>();

			var index = 1;
			var rnd = new Random();

			for (var i = 2895995; i < file.Length - 1; i++)
			{
				var tokens = file[i].Split('\t');

				var name = tokens[2];
				var stripName = "";

				if (name.Length < 5)
				{
					stripName = name;
				}
				else
					stripName = name.Remove(4);

				if (buffer.Add(stripName))
				{
					names.Add(new Item(name, rnd.Next(1, 1000), index));
					index++;
				}
			}

			Console.WriteLine(names.Count);

			var value = JsonConvert.SerializeObject(names, Formatting.Indented);

			File.WriteAllText("1.json", value, Encoding.UTF8);
			

			Console.WriteLine("Serializing ended");
		}
	}
}
