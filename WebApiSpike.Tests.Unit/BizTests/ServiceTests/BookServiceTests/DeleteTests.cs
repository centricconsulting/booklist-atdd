using Moq;
using NUnit.Framework;
using WebApiSpike.Biz.Entities;

namespace WebApiSpike.Tests.Unit.BizTests.ServiceTests.BookServiceTests
{
	public class DeleteTests : BookServiceTestsBase
	{
		private Book _book;

		[SetUp]
		public void Setup()
		{
			_book  = new Book() {Id = 1, Isbn = "A1234"};
			_service.Save(_book);
		}

		[Test]
		public void should_call_delete_by_id_method()
		{
			var book = new Book { Id = 47 };
			_service.Delete(book);
			_mock.Verify(repo => repo.DeleteById(book.Id), Times.Once());
		}


		[Test]
		public void verify_undeleted_method()
		{
			_service.Delete(_book);
			_service.UnDelete(_book);
			_mock.Verify(repo => repo.UnDeleteById(_book.Id), Times.Once());
		}

		[Test]
		public void should_invoke_repo_getalldeleted()
		{
			_service.GetAllDeleted();
			_mock.Verify(repo => repo.GetAllDeleted(), Times.Once());
		}
	}
}