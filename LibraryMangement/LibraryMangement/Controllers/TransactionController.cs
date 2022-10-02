using LibraryMangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LibraryMangement.Controllers
{
    public class TransactionController : Controller
    {
        static int userId;          // Used to store user id.
        static string userName;
        static string search;// Used to store user name.

        private Library_ManagementEntities userDb = new Library_ManagementEntities();
        private Library_ManagementEntities bookDb = new Library_ManagementEntities();
        private Library_ManagementEntities transDb = new Library_ManagementEntities();


        // Returns user books Transaction view, here user can request for a book.
        public ActionResult Index(int? userId, string userName)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usertbl user = userDb.Usertbls.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }

            TransactionController.userId = (int)userId;
            TransactionController.userName = userName;

            return View(bookDb.Booktbls.ToList());
        }

        // Returns user home view.
        public ActionResult UserHome()
        {
            return View();
        }

        // Returns user about view.
        public ActionResult UserAbout()
        {
            return View();
        }

        // Returns user contact view.
        public ActionResult UserContact()
        {
            return View();
        }

        // Navbar menus.
        // Redirected to index view of borrow controller with user id and username.
        public ActionResult MenuTransaction()
        {
            return RedirectToAction("Index", "Transaction", new { userId, userName });
        }

        // Redirected to Requested view of user transaction controller with user id.
        public ActionResult MenuRequested()
        {
            return RedirectToAction("Requested", "UserTransaction", new { userId });
        }

        // Redirected to Received view of user transaction controller with user id.
        public ActionResult MenuReceived()
        {
            Session.Remove("receivedBadge");
            return RedirectToAction("Received", "UserTransaction", new { userId });
        }

        // Redirected to Rejected view of user transaction controller with user id.
        public ActionResult MenuRejected()
        {
            Session.Remove("rejectedBadge");
            return RedirectToAction("Rejected", "UserTransaction", new { userId });
        }

        // Borrow the book, redirect to index view.
        public ActionResult Transaction(int? bookId,string search = "")
        {
            /*try
            {*/
            if (transDb.Transactiontbls.Where(t => t.UserId == userId).Count() < 6)
            {
                if (bookId != null)
                {
                    Booktbl book = bookDb.Booktbls.FirstOrDefault(b => b.BookId == bookId);
                    if (book == null)
                    {
                        return HttpNotFound();
                    }
                    if (book.BookCopies > 0)
                    {
                        book.BookCopies = book.BookCopies - 1;
                        Transactiontbl trans = new Transactiontbl()
                        {
                            BookId = book.BookId,
                            BookTitle = book.BookTitle,
                            BookISBN = book.BookISBN,
                            TransactionDate = DateTime.Now.ToShortDateString(),
                            TranStatus = "Requested",
                            UserId = userId,
                            UserName = userName,
                        };
                        bookDb.SaveChanges();
                        transDb.Transactiontbls.Add(trans);
                        transDb.SaveChanges();
                        Session["requestMsg"] = "Requested successfully";
                    }
                    else
                    {
                        Session["requestMsg"] = "Sorry you cant take, Book copy is zero";
                    }
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                Session["requestMsg"] = "Sorry you cant take more than six books";
            }
            return RedirectToAction("Index", "Transaction", new { userId, userName });
            /*}
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
        }

        // Remove the session datas which are used for alerts
        // ReqAlert
        public ActionResult RequestAlert()
        {
            Session.Remove("requestMsg");
            return RedirectToAction("Index", "Transaction", new { userId, userName });
        }
    }
}