using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApiSpike.Biz.Entities;
using WebSpikeApi.Core.Contracts;

namespace WebApiSpike.Api.Models
{
	public class BookModel : IBookEntity
	{
		public BookModel() { }

		public BookModel(Book book)
		{
			Id = book.Id;
			Title = book.Title;
			Author = book.Author;
		}

		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }

		public string Isbn { get; set; }

		public Book ApplyTo(Book bookToSave)
		{
			if (bookToSave == null) bookToSave = new Book();

			bookToSave.Author = Author;
			bookToSave.Title = Title;

			return bookToSave;
		}

		public bool IsValid()
		{
			throw new NotImplementedException();
		}
	}
}