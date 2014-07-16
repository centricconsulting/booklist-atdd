using System;
using System.Collections.Generic;
using WebApiSpike.Biz.Entities;
using WebApiSpike.Services.Contracts;
using WebSpikeApi.Core.Contracts;

namespace WebApiSpike.Services
{
	public class BookService : IBookService
	{
		private IRepository<Book, int> _repo;

		public BookService(IRepository<Book, int> repo)
		{
			_repo = repo;
		}

		public List<Book> GetAll()
		{
			try
			{
				return _repo.All();
			}
			catch (Exception)
			{
				return null;
			}
		}

		public Book GetById(int id)
		{
			try
			{
				return _repo.GetById(id);
			}
			catch
			{
				return null;
			}
		}

		public Book Save(Book book)
		{
			if(!book.IsValid())
			{
				return null;	
			}
			
			return book.Id > 0
				? _repo.Update(book)
				: _repo.Add(book);
		}

		public void Delete(Book book)
		{
			_repo.DeleteById(book.Id);
		}

		public void UnDelete(Book book)
		{
			_repo.UnDeleteById(book.Id);
		}
		public List<Book> GetAllDeleted()
		{
			return _repo.GetAllDeleted();
		}
	}
}
