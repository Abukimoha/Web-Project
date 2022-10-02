using LibraryMangement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LibraryMangement.Controllers
{
    public class BooktblController : Controller
    {
        private readonly Library_ManagementEntities bookDb = new Library_ManagementEntities();

        // GET: tblBooks
        public ActionResult Index(string search = "")
        {
            ViewBag.search = search;
            //   var products = _context.Products.ToList();
            // var products = _context.Products.Where(temp => temp.ProductId == 1 || temp.CategoryId==1).ToList();
            var Books = bookDb.Booktbls.Where(temp => temp.BookTitle.Contains(search)).ToList();
            return View(Books);
            
        }
        // GET: tblBooks Json
        public ActionResult GetAll()
        {
            var booklist = bookDb.Booktbls.ToList();
            return Json(new { data = booklist }, JsonRequestBehavior.AllowGet);
        }

        // GET: tblBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booktbl tblBook = bookDb.Booktbls.Find(id);
            if (tblBook == null)
            {
                return HttpNotFound();
            }
            return View(tblBook);
        }

        // GET: tblBooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,BookTitle,BookCategory,BookAuthor,BookCopies,BookPub,BookPubName,BookISBN,Copyright,DateAdded,Status")] Booktbl tblBook)
        {
            if (ModelState.IsValid)
            {
                Session["operationMsg"] = "Book added successfully";
                bookDb.Booktbls.Add(tblBook);
                bookDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblBook);
        }

        // Remove the session datas which are used for alerts
        // OperationAlert
        public ActionResult OperationAlert()
        {
            Session.Remove("operationMsg");
            return RedirectToAction("Index");

        }

        // GET: tblBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booktbl tblBook = bookDb.Booktbls.Find(id);
            if (tblBook == null)
            {
                return HttpNotFound();
            }
            return View(tblBook);
        }

        // POST: tblBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,BookTitle,BookCategory,BookAuthor,BookCopies,BookPub,BookPubName,BookISBN,Copyright,DateAdded,Status")] Booktbl tblBook)
        {
            if (ModelState.IsValid)
            {
                Session["operationMsg"] = "Book updated successfully";
                bookDb.Entry(tblBook).State = EntityState.Modified;
                bookDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblBook);
        }

        // GET: tblBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booktbl tblBook = bookDb.Booktbls.Find(id);
            if (tblBook == null)
            {
                return HttpNotFound();
            }
            return View(tblBook);
        }

        // POST: tblBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booktbl tblBook = bookDb.Booktbls.Find(id);
            bookDb.Booktbls.Remove(tblBook);
            bookDb.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bookDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}