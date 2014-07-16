using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiSpike.Biz.Data;
using WebApiSpike.Biz.Entities;
using WebApiSpike.Services;

namespace WebApiSpike.Con
{
	class Program
	{
		static void Main(string[] args)
		{
			var repo = new FakeBookRepository();

			for (var i = 0; i < 100; i++)
			{
				repo.Add(new Book { Author = i.ToString().PadLeft(3, '0') + " - Shawn" });
			}

			var result = repo.All();

			Console.WriteLine(result.Count);

			Console.ReadLine();

			foreach (var book in result)
			{
				Console.WriteLine(book.Author);
			}

			Console.ReadLine();


			var service = new BookService(repo);
			var serviceResults = service.GetAll();

			for (var i = 100; i < 200; i++)
			{
				service.Save(new Book { Author = i.ToString().PadLeft(3, '0') + " - Shawn" });
			}

			foreach (var book in serviceResults)
			{
				Console.WriteLine(book.Author);
			}

			Console.ReadLine();
		}
	}
}
