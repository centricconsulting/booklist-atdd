using WebApiSpike.Biz.Entities;
using WebSpikeApi.Core.Contracts;

namespace WebApiSpike.BoundaryModels
{
	public class BookModel : IBookEntity
	{
		public BookModel() { }

		public BookModel(IBookEntity book)
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
			if (bookToSave == null)
			{
				bookToSave = new Book();
			}

			bookToSave.Author = Author;
			bookToSave.Title = Title;

			return bookToSave;
		}

		public Book CreateFrom()
		{
			return ApplyTo(new Book());
		}




		public bool IsValid()
		{
			throw new System.NotImplementedException();
		}
	}
}