using WebApiSpike.Biz.Entities;
using WebSpikeApi.Core.Contracts;

namespace WebApiSpike.WebUi.Models
{
	public class BookModel : IBookEntity
	{
		public BookModel() { }

		public BookModel(IBookEntity book)
		{
			Id = book.Id;
			Title = book.Title;
			Author = book.Author;
			Isbn = book.Isbn;
		}

		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string Isbn { get; set; }

		public Book ApplyTo(Book bookToSave)
		{
			var updated = new Book {Title = Title, Author = Author, Isbn = Isbn};

			if (bookToSave != null)
				updated.Id = bookToSave.Id;

			return updated;
		}

		public Book CreateFrom()
		{
			return ApplyTo(new Book());
		}

	}
}