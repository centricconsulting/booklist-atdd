using System.ComponentModel.DataAnnotations;
using Moq;
using NUnit.Framework;
using WebApiSpike.Biz.Entities;

namespace WebApiSpike.Tests.Unit.BizTests.ServiceTests.BookServiceTests
{
	[TestFixture]
	public class SaveTests : BookServiceTestsBase
	{
		private Book _bookToAdd;
		private Book _bookToReturn;
		private Book _bookToUpdate;

		[SetUp]
		public void SetUp()
		{
			_bookToAdd = new Book { Title = "TitleOne", Author = "AuthorOne", Isbn ="A1234", IsDeleted = false};
			_bookToUpdate = new Book { Id = 10, Title = "TitleOne", Isbn ="A1234", Author = "AuthorOne", IsDeleted = false };
			_bookToReturn = new Book { Id = 10, Title = "TitleOne", Isbn ="A1234", Author = "AuthorOne", IsDeleted = false };
		}

		[Test]
		public void if_does_not_exist_call_add()
		{
			_service.Save(_bookToAdd);
			_mock.Verify(repo => repo.Add(_bookToAdd), Times.Once());
		}

		[Test]
		public void if_does_not_exist_should_return_entity_with_new_id()
		{
			_mock.Setup(repo => repo.Add(_bookToAdd)).Returns(_bookToReturn);
			var result = _service.Save(_bookToAdd);
			BooksShouldBeSame(_bookToReturn, result);
		}

		[Test]
		public void when_book_exists_calls_update()
		{
			_service.Save(_bookToUpdate);
			_mock.Verify(repo => repo.Update(_bookToUpdate), Times.Once());
		}

		[Test]
		public void when_book_exists_should_return_updated_book()
		{
			_mock.Setup(repo => repo.Update(_bookToUpdate)).Returns(_bookToReturn);
			var result = _service.Save(_bookToUpdate);
			BooksShouldBeSame(_bookToReturn, result);
		}

		[Test]
		public void if_isbn_is_blank_save_should_throw_exception()
		{
			_bookToAdd.Isbn = string.Empty;
			Assert.Throws<ValidationException>(() => _service.Save(_bookToAdd));
		}
	}
}