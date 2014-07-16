using Moq;
using NUnit.Framework;
using WebApiSpike.Biz.Entities;
using WebApiSpike.Services;
using WebSpikeApi.Core.Contracts;

namespace WebApiSpike.Tests.Unit.BizTests.ServiceTests.BookServiceTests
{
	public class BookServiceTestsBase
	{
		internal Mock<IRepository<Book, int>> _mock;
		internal BookService _service;

		[SetUp]
		public void SetUpBase()
		{
			_mock = new Mock<IRepository<Book, int>>();
			_service = new BookService(_mock.Object);
		}

		internal static void BooksShouldBeSame(Book expected, Book result)
		{
			Assert.Greater(expected.Id, 0);
			Assert.AreEqual(expected.Id, result.Id);
			Assert.AreEqual(expected.Title, result.Title);
			Assert.AreEqual(expected.Author, result.Author);
			Assert.AreEqual(expected.IsDeleted, result.IsDeleted);
		}
	}
}