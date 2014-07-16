using System.Linq;
using NUnit.Framework;
using WebApiSpike.Biz.Data;
using WebApiSpike.Biz.Entities;
using WebApiSpike.Services;

namespace WebApiSpike.Tests.Integration.BookServiceTests
{
	[TestFixture]
	public class DeleteTests
	{
		FakeBookRepository _repository;
		BookService _service;

		[SetUp]
		public void Setup()
		{
			_repository = new FakeBookRepository();
			_repository.Clear();
			_service = new BookService(_repository);
		}

		[Test]
		public void deleting_a_book_sets_isdeleted_true()
		{
			var bookToBeDeleted = new Book() { Id = 1, Isbn = "A1111" };
			_repository.Add(bookToBeDeleted);
			_service.Delete(bookToBeDeleted);
			Assert.IsTrue(_service.GetById(bookToBeDeleted.Id).IsDeleted);
		}

		[Test]
		public void get_all_deleted_books()
		{
			SetupBooks();
			var deletedBooks = _service.GetAllDeleted();
			Assert.AreEqual(1, deletedBooks.Count);
			Assert.AreEqual(2, deletedBooks.First().Id);
		}

		[Test]
		public void verify_undelete_book()
		{
			SetupBooks();
			var bookToUndelete = _service.GetAllDeleted().First();
			_service.UnDelete(bookToUndelete);
			var activeBook = _service.GetById(bookToUndelete.Id);
			Assert.IsFalse(activeBook.IsDeleted);
		}

		private void SetupBooks()
		{
			_repository.Add(new Book { Id = 1, Isbn = "A1111" });
			_repository.Add(new Book { Id = 2, Isbn = "A1112", IsDeleted = true });
			_repository.Add(new Book { Id = 3, Isbn = "A1113" });
		}


	}
}
