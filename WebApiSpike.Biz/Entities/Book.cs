using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading;
using WebSpikeApi.Core.Contracts;

namespace WebApiSpike.Biz.Entities
{
	public class Book : EntityBase<int>, IBookEntity
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string Isbn { get; set; }

		public new bool IsValid()
		{
			if (string.IsNullOrWhiteSpace(Isbn)) throw new ValidationException("ISBN is Required");
			var regEx = new Regex(@"\A[a-zA-Z]\d{4}\z");
			if (!regEx.IsMatch(Isbn)) throw new FormatException("ISBN format is not valid.");
			return base.IsValid();
		}
	}
}