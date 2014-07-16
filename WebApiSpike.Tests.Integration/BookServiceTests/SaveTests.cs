using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WebApiSpike.Biz.Data;
using WebApiSpike.Biz.Entities;
using WebApiSpike.Services;

namespace WebApiSpike.Tests.Integration.BookServiceTests
{
	[TestFixture]
	public class SaveTests
	{
		FakeBookRepository _repository;
		BookService _service;
		Book workingBook;

		[SetUp]
		public void SetUp()
		{
			_repository = new FakeBookRepository();
			_service = new BookService(_repository);
			workingBook = new Book() { Author = "Joe Smith", Title = "Joe's New Book", Isbn = "A1234" };
		}

		[TearDown]
		public void TearDown()
		{
			_repository.Clear();
		}

		[Test]
		public void new_book_creates_an_id_and_persistes_properly()
		{
			var result = _service.Save(workingBook);

			Assert.Greater(result.Id, 0);

			var queriedResult = _repository.GetById(result.Id);
			BookPropertiesAreTheSame(workingBook, queriedResult);
		}

		[Test]
		public void update_book_values_persist()
		{
			var addedBook = _repository.Add(workingBook);
			addedBook.Author = "Sally Jones";
			_service.Save(addedBook);
			var updatedBook = _repository.GetById(addedBook.Id);
			BookPropertiesAreTheSame(addedBook, updatedBook);
			Assert.AreEqual("Sally Jones", updatedBook.Author);
		}

		[Test]
		public void update_book_with_invalid_data()
		{
			workingBook.Isbn = string.Empty;
			Assert.Throws<ValidationException>(() => _service.Save(workingBook));
		}

		private static void BookPropertiesAreTheSame(Book original, Book modified)
		{
			Assert.AreEqual(original.Author, modified.Author);
			Assert.AreEqual(original.Title, modified.Title);
			Assert.AreEqual(original.Isbn, modified.Isbn);
		}
	}
}
