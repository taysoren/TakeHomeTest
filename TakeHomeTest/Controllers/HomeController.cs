using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using TakeHomeTest.Models;

namespace TakeHomeTest.Controllers {
	public class HomeController : Controller {
		
		#region Data Members
		public PeopleContext _db = new PeopleContext();
		#endregion

		#region Pages
		public ActionResult About() {
			ViewBag.Message = "My application description page.";

			return View();
		}

		public ActionResult Contact() {
			ViewBag.Message = "My contact page.";

			return View();
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public JsonResult GetData(object name) {
			var persons = _db.People.Where(p => p.FirstName.Contains((string)name) || p.LastName.Contains((string)name));
			return Json(persons, JsonRequestBehavior.AllowGet);
		}

		// GET: Home
		public ActionResult Index() {
			return View(_db.People.ToList());
		}

		// GET: Home/Details/5
		public ActionResult Details(int id) {
			return View();
		}
		#endregion //Pages

		// GET: Home/Create
		public ActionResult Create() {
			return View();
		}

		// POST: Home/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			try {
				// TODO: Add insert logic here
				return RedirectToAction("Index");
			}
			catch {
				return View();
			}
		}

		// GET: Home/Edit/5
		public ActionResult Edit(int id) {
			return View();
		}

		// POST: Home/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection) {
			try {
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch {
				return View();
			}
		}

		// GET: Home/Delete/5
		public ActionResult Delete(int id) {
			return View();
		}

		// POST: Home/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection) {
			try {
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch {
				return View();
			}
		}
	}
}