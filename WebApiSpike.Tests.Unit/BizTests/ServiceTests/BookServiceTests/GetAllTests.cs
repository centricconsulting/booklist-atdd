using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using WebApiSpike.Biz.Entities;

namespace WebApiSpike.Tests.Unit.BizTests.ServiceTests.BookServiceTests
{
	[TestFixture]
	public class GetAllTests : BookServiceTestsBase
	{
		[Test]
		public void should_call_repo_all_method()
		{
			_service.GetAll();
			_mock.Verify(repo => repo.All(), Times.Once());		
		}

		[Test]
		public void when_successful_should_return_list()
		{
			var newList = new List<Book>
			{
				new Book{ Id = 0 },
				new Book{ Id = 1 },
				new Book{ Id = 2 },
				new Book{ Id = 3 }
			};

			_mock.Setup(repo => repo.All()).Returns(newList);
			var result = _service.GetAll();
			Assert.AreEqual(newList.Count, result.Count);
		}

		[Test]
		public void when_exception_return_null()
		{
			_mock.Setup(repo => repo.All()).Throws(new Exception());
			Assert.IsNull(_service.GetAll());
		}
		
		[Test]
		public void getll_should_exclude_deleted_books()
		{
			var newList = new List<Book>
			{
				new Book{Id=1},
				new Book{Id=2,IsDeleted=true},
				new Book{Id=3}
			};
			_mock.Setup(r => r.All()).Returns(newList.Where(n => !n.IsDeleted).ToList());
			Assert.AreEqual(2,_service.GetAll().Count);
		}
		
	}
}