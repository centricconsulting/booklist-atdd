using System;
using Moq;
using NUnit.Framework;
using WebApiSpike.Biz.Entities;

namespace WebApiSpike.Tests.Unit.BizTests.ServiceTests.BookServiceTests
{
	[TestFixture]
	public class when_calling_get_by_id : BookServiceTestsBase
	{
		private Book _book;
		private const int idToFind = 10;

		[SetUp]
		public void SetUp()
		{
			_book = new Book { Id = idToFind };
		}

		[Test]
		public void should_call_repo_get_by_id_method()
		{
			_service.GetById(idToFind);
			_mock.Verify(repo => repo.GetById(idToFind), Times.Once());
		}

		[Test]
		public void it_should_return_the_correct_book()
		{
			_mock.Setup(repo => repo.GetById(idToFind)).Returns(_book);
			var result = _service.GetById(idToFind);

			Assert.AreEqual(_book.Id, result.Id);
		}

		[Test]
		public void it_returns_null_if_book_is_not_found()
		{
			_mock.Setup(repo => repo.GetById(idToFind)).Returns(_book);
			var result = _service.GetById(idToFind + 100);

			Assert.IsNull(result);
		}

		[Test]
		public void it_returns_null_if_repo_throws_an_exception()
		{
			_mock.Setup(repo => repo.GetById(idToFind)).Throws<Exception>();
			var result = _service.GetById(idToFind);

			Assert.IsNull(result);
		}
	}
}