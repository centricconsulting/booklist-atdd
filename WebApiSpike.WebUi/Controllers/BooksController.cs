using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using WebApiSpike.Biz.Entities;
using WebApiSpike.Services.Contracts;
using WebApiSpike.WebUi.Models;
using WebSpikeApi.Core;

namespace WebApiSpike.WebUi.Controllers
{
	public class BooksController : BootstrapBaseController
	{
		protected readonly IBookService BookService;

		public BooksController(IBookService bookService)
		{
			BookService = bookService;
		}

		public ActionResult Index()
		{
			var raw = BookService
									.GetAll()
									.OrderBy(b => b.Title);
			var model = raw.Select(book => new BookModel(book)).ToList();

			return View(model);
		}

		public ActionResult Details(int id)
		{
			var book = BookService.GetById(id);
			var model = new BookModel(book);
			return View(model);
		}

		[HttpPost]
		public ActionResult Create(BookModel model)
		{
			if (ModelState.IsValid)
			{
				var book = model.CreateFrom();
				SaveModel(book, "Your information was saved!");
				
				return RedirectToAction("Index");
			}
			Error("there were some errors in your form.");
			return View(model);
		}

		public ActionResult Create()
		{
			return View(new BookModel());
		}

		public ActionResult Edit(int id)
		{
			var book = BookService.GetById(id);
			var model = new BookModel(book);
			return View("Create", model);
		}

		[HttpPost]
		public ActionResult Edit(BookModel model, int id)
		{
			if (ModelState.IsValid)
			{
				var book = BookService.GetById(id);
				SaveModel(model.ApplyTo(book), "The model was updated!");

				return RedirectToAction("index");
			}
			return View("Create", model);
		}

		private void SaveModel(Book book, string message)
		{
			try
			{
				BookService.Save(book);
				Success(message);
			}
			catch (ValidationException e)
			{
				Error("The model failed to update! - " + e.Message);
			}
			catch (FormatException e)
			{
				Error("The model failed to update! - " + e.Message);
			}
			catch(UniqueConstraintException ux)
			{
				Error("The model failed to update! - " + ux.Message);
			}
		}

		public ActionResult Delete(int id)
		{
			var book = BookService.GetById(id);
			BookService.Delete(book);
			Information("Your widget was deleted");
			if (BookService.GetAll().Count == 0)
			{
				Attention("You have deleted all the models! Create a new one to continue the demo.");
			}
			return RedirectToAction("index");
		}
	}
}
