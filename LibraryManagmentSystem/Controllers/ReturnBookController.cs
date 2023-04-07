using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Data.Model;
using LibraryManagmentSystem.ViewModel;
using LibraryManagmentSystem.Data.Interfaces;
using System.Transactions;

namespace LibraryManagmentSystem.Controllers
{
    public class ReturnBookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        protected readonly LibraryDbContext _context;


        public ReturnBookController(IBookRepository bookRepository, IMemberRepository memberRepository, LibraryDbContext context)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _context = context;
        }

        // GET: ReturnBook
        public async Task<IActionResult> Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _context.IssueTransactions != null ?
                            View(await _context.IssueTransactions.ToListAsync()) :
                            Problem("Entity set 'LibraryDbContext.IssueTransactions'  is null.");
            }
            else
            {
                return _context.IssueTransactions != null ?
                            View(await _context.IssueTransactions.Where(i => i.MemberID.ToString().Equals(search)).ToListAsync()) :
                            Problem("Entity set 'LibraryDbContext.IssueTransactions'  is null.");
            }
        }

        public IActionResult Return(int transactionId)
        {
            var transaction = _context.IssueTransactions.Where(t => t.TransactionId.Equals(transactionId)).FirstOrDefault();
            var Book = _context.Books.Where(b => b.BookID.Equals(transaction.BookID)).FirstOrDefault();
            var Member = _context.Members.Where(m => m.MemberID.Equals(transaction.MemberID));

            if (transaction.Status == false)
            {
                Book.Inventory = ++Book.Inventory;
                transaction.Status = true;
            }
            else
            {
                throw new Exception("Transaction Returned");
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
