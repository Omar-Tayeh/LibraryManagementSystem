﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Data.Model;
using LibraryManagmentSystem.Authorization;

namespace LibraryManagmentSystem.Controllers
{
    public class ReturnBookController : Controller
    {
        protected readonly LibraryDbContext _context;


        public ReturnBookController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: ReturnBook, show all transaction that are not returned unless the user is admin show all transactions.
        public IActionResult Index()
        {
            var transactions = _context.IssueTransactions.ToList();
            var isAdmin = User.IsInRole(Constants.AdminRole);
            if (isAdmin)
            {
                return View(transactions);
            }
            else
            {
                transactions = _context.IssueTransactions.Where(t => t.Status == false).ToList();
                return View(transactions);
            }
        }

        //return books to stock and mark the transaction as returned.
        public IActionResult Return(int transactionId)
        {
            var transaction = _context.IssueTransactions.Where(t => t.TransactionId.Equals(transactionId)).FirstOrDefault();
            var Book = _context.Books.Where(b => b.BookID.Equals(transaction.BookID)).FirstOrDefault();
            /*var Member = _context.Members.Where(m => m.MemberID.Equals(transaction.MemberID));*/

            if (transaction.Status == false)
            {
                if (Book != null)
                {
                    Book.Inventory = ++Book.Inventory;
                    transaction.Status = true;
                }
                else
                {
                    throw new Exception("Book was not found");
                }


            }
            else
            {
                return RedirectToAction("Index");
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

