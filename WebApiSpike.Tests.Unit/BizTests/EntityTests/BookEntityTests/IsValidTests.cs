using System;
using System.ComponentModel.DataAnnotations;
using Moq;
using NUnit.Framework;
using WebApiSpike.Biz.Entities;

namespace WebApiSpike.Tests.Unit.BizTests.EntityTests.BookEntityTests
{
	[TestFixture]
	public class IsValidTests
	{
		[Test]
		public void is_valid_with_an_isbn()
		{
			var bookEntity = new Book() {Isbn = "A2222"};
			Assert.IsTrue(bookEntity.IsValid());
		}

		[TestCase("")]
		[TestCase(null)]
		[TestCase("   ")]
		public void is_not_valid_without_an_isbn(string isbnValue)
		{
			var bookEntity = new Book() {Isbn = isbnValue};
			Assert.Throws<ValidationException>(() => bookEntity.IsValid());
		}

		[TestCase("A")]
		[TestCase("AA222")]
		[TestCase("A22223")]
		[TestCase("1AAAA")]
		[TestCase("1234A")]
		public void is_not_valid_isbn_with_other_than_character_and_four_numerals_format(string isbnValue)
		{
			var bookEntity = new Book() {Isbn = isbnValue};
			Assert.Throws<FormatException>(() => bookEntity.IsValid());
		}

		
	}
	
}
