using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YourBooks.Models;

namespace YourBooks.Controllers
{
    public class BooksController : Controller
    {
        private readonly YourBooksContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BooksController(YourBooksContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Books
        public async Task<IActionResult> Index(string bookGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from b in _context.Book
                                            orderby b.Genre
                                            select b.Genre;

            var books = from b in _context.Book
                        select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(bookGenre))
            {
                books = books.Where(x => x.Genre == bookGenre);
            }

            var bookGenreVM = new BookGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Books = await books.ToListAsync()
            };

            return View(bookGenreVM);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            var comments = from c in _context.Comments
                           where c.BookId == id
                           select c;
            book.Comments = comments.ToList();

            foreach (var item in book.Comments)
            {
                item.User = _userManager.FindByIdAsync(item.UserId).Result;
            }

            return View(book);
        }

        // POST: Books/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, string commentText)
        {
            if (ModelState.IsValid && commentText != null && commentText != "")
            {
                var comment = new Comment();
                comment.Text = commentText;
                comment.BookId = id;
                comment.UserId = _userManager.GetUserId(HttpContext.User);
                _context.Add(comment);
                await _context.SaveChangesAsync();
            }
            return Details(id).Result;
        }

        [HttpGet]
        public IActionResult OnGetComments(int id)
        {
            var comments = from c in _context.Comments
                           where c.BookId == id
                           select c;

            var ajaxComments = new List<AjaxComment>();

            foreach (var item in comments)
            {
                ajaxComments.Add(new AjaxComment()
                {
                    Id = item.Id,
                    Text = item.Text,
                    UserName = _userManager.FindByIdAsync(item.UserId).Result.UserName
                });
            }

            return new JsonResult(ajaxComments);
        }

    }
}