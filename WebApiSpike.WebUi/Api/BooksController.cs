using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSpike.Biz.Entities;
using WebApiSpike.Services;
using WebApiSpike.Services.Contracts;
using WebApiSpike.WebUi.Models;
using WebSpikeApi.Core;
using WebSpikeApi.Core.Contracts;

namespace WebApiSpike.WebUi.Api
{
	public class BooksController : ApiController
	{
		protected readonly IBookService Service;
		public BooksController(IBookService bookService)
		{
			Service = bookService;
		}

		// GET api/books
		public IEnumerable<BookModel> Get()
		{
			var result = Service.GetAll();

			return result.Select(book => new BookModel(book)).ToList();
		}

		// GET api/books/5
		public HttpResponseMessage Get(int id)
		{
			var result = Service.GetById(id);
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

			var bookToSave = Service.GetById(book.Id);

			HttpResponseMessage response;

			try
			{
				var savedBook = Service.Save(book.ApplyTo(bookToSave));

				response = Request.CreateResponse(HttpStatusCode.Created, savedBook);
				// Get the url to retrieve the newly created book.
				response.Headers.Location
					= new Uri(Request.RequestUri, string.Format("books/{0}", savedBook.Id));
			}
			catch (ValidationException e)
			{
				response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
			}
			catch (FormatException e)
			{
				response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
			}
			catch(UniqueConstraintException e)
			{
				response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
			}
			

			return response;
		}

		// PUT api/books/5
		public void Put(int id, [FromBody]BookModel book)
		{
			var bookToUpdate = Service.GetById(book.Id);
			bookToUpdate = book.ApplyTo(bookToUpdate);
			Service.Save(bookToUpdate);
		}

		// DELETE api/books/5
		public HttpResponseMessage Delete(int id)
		{
			throw new NotImplementedException();
		}
	}
}