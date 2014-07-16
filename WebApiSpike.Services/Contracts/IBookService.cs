using System.Collections.Generic;
using WebApiSpike.Biz.Entities;

namespace WebApiSpike.Services.Contracts
{
	public interface IBookService
	{
		List<Book> GetAll();
		List<Book> GetAllDeleted();
		Book GetById(int id);
		Book Save(Book book);
		void Delete(Book book);
		void UnDelete(Book book);
	}
}