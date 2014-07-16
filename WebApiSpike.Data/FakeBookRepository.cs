using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiSpike.Biz.Entities;
using WebSpikeApi.Core;
using WebSpikeApi.Core.Contracts;

namespace WebApiSpike.Biz.Data
{
	public class FakeBookRepository : IRepository<Book, int>
	{
		private static readonly List<Book> _store = new List<Book>();

		public FakeBookRepository()
		{
			if (_store.Count > 0) return;

			var rand = new Random();
			for (var i = 0; i < 10; i++)
			{
				var randomNumber = rand.Next(0, 1000).ToString().PadLeft(0, '0');
				Add(new Book
						{
							Title = randomNumber + " - Title",
							Author = randomNumber + " - Author",
							Isbn = "A" + randomNumber.ToString().PadLeft(4, '0'),
							IsDeleted = false
						});
			}
		}

		public void Clear()
		{
			_store.Clear();
		}

		public List<Book> All()
		{
			return _store;
		}

		public Book GetById(int id)
		{
			return _store.FirstOrDefault(b => b.Id == id);
		}

		public Book Add(Book entity)
		{
			CheckConstraints(entity);
			var newId = _store.Count > 0
				? _store.Max(b => b.Id) + 1
				: 1;
			entity.Id = newId;
			_store.Add(entity);
			return entity;
		}

		public Book Update(Book entity)
		{
			CheckConstraints(entity);
			var toRemove = _store.Where(b => b.Id == entity.Id);

			foreach (var book in toRemove)
			{
				book.Title = entity.Title;
				book.Author = entity.Author;
				book.IsDeleted = entity.IsDeleted;
				book.Isbn = entity.Isbn;
			}

			return entity;
		}

		public void Delete(Book entity)
		{
			DeleteById(entity.Id);
		}

		public void DeleteById(int id)
		{
			var deletedBook = GetById(id);
			deletedBook.IsDeleted = true;
		}

		private void CheckConstraints(Book b)
		{
			var isbnIsNotUnique = _store.Any(bidb =>
					bidb.Isbn.ToLower() == b.Isbn.ToLower()
					&& bidb.Id != b.Id);

			if (isbnIsNotUnique) throw new UniqueConstraintException("ISBN must be unique.");
		}

		public void UnDelete(Book entity)
		{
			UnDeleteById(entity.Id);
		}

		public void UnDeleteById(int id)
		{
			var deletedBook = GetById(id);
			deletedBook.IsDeleted = false;
		}

		public List<Book> GetAllDeleted()
		{
			return _store.Where(b => b.IsDeleted).ToList();
		}
	}
}
