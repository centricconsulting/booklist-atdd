using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSpike.Api.Models;
using WebApiSpike.Biz.Data;
using WebApiSpike.Biz.Entities;
using WebApiSpike.Services;
using WebSpikeApi.Core.Contracts;

namespace WebApiSpike.Api.Controllers
{
	public class BooksController : ApiController
	{
		private IRepository<Book, int> _repo;
		private BookService _service;

		public BooksController()
		{
			_repo = new FakeBookRepository();
			_service = new BookService(_repo);
		}

		public BooksController(IRepository<Book, int> repo)
		{
			_repo = repo;
			_service = new BookService(_repo);
		}

		// GET api/books
		public IEnumerable<BookModel> Get()
		{
			var result = _service.GetAll();

			return result.Select(book => new BookModel(book)).ToList();
		}

		// GET api/books/5
		public HttpResponseMessage Get(int id)
		{
			var result = _service.GetById(id);
			return result == null
				? Request.CreateResponse(HttpStatusCode.NotFound)
				: Request.CreateResponse(HttpStatusCode.OK, result);
		}

		// POST api/books
		public HttpResponseMessage Post([FromBody]BookModel book)
		{
			if (book == null)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}

			var bookToSave = _service.GetById(book.Id);
			bookToSave = book.ApplyTo(bookToSave);
			var savedBook = _service.Save(bookToSave);

			var response = Request.CreateResponse(HttpStatusCode.Created, savedBook);
			// Get the url to retrieve the newly created book.
			response.Headers.Location
				= new Uri(Request.RequestUri, string.Format("books/{0}", savedBook.Id));

			return response;
		}

		// PUT api/books/5
		public void Put(int id, [FromBody]BookModel book)
		{
			var bookToUpdate = _service.GetById(book.Id);
			bookToUpdate = book.ApplyTo(bookToUpdate);
			_service.Save(bookToUpdate);
		}

		// DELETE api/books/5
		public HttpResponseMessage Delete(int id)
		{
			throw new NotImplementedException();
		}
	}
}