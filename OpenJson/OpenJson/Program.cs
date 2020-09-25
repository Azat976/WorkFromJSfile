
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

using System.Linq;

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

		static async Task Main()
        {
			var jsonText = File.ReadAllText("1.json");

			var file = JsonConvert.DeserializeObject<List<Item>>(jsonText);

			var files = file.OrderByDescending(x => x.Index).Where(p => p.Price < 100);
			var files1 = file.OrderBy(x => x.Price).Where(p => p.Price > 100).Where(p => p.Price <200);
			var files2 = file.OrderBy(x => x.Price).Where(p => p.Price > 200).Where(p => p.Price < 300);
			var files3 = file.OrderBy(x => x.Price).Where(p => p.Price > 300).Where(p => p.Price < 1000);

			var desc = new[] { files, files1, files2, files3 };

			for (int i=0; i <4; i++)
			{
				var value3 = JsonConvert.SerializeObject(desc[i], Formatting.Indented);
				File.WriteAllText(string.Format("test{0}.txt", i), value3);
			}
			Console.WriteLine("Serializing ended");
		}
    }
}
