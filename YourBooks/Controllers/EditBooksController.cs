using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YourBooks.Models;

namespace YourBooks.Controllers
{
    public class EditBooksController : Controller
    {
        private readonly YourBooksContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditBooksController(YourBooksContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Books
        public async Task<IActionResult> Index(string bookGenre, string searchString, string currUserId)
        {
            var books = from b in _context.Book
                        select b;

            var userId = _userManager.GetUserId(HttpContext.User);
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            books = books.Where(x => x.UserId == userId);

            IQueryable<string> genreQuery = from b in books
                                            orderby b.Genre
                                            select b.Genre;

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

            var book = await _context.Book.FirstOrDefaultAsync(b => b.Id == id);
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

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,ISBN,ReleaseDate,Genre,Rating")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.UserId = _userManager.GetUserId(HttpContext.User);
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,ISBN,ReleaseDate,Genre,Rating")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    book.UserId = _userManager.GetUserId(HttpContext.User);
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            var bookId = comment.BookId;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "EditBooks", new { id = bookId });
            //return Details(bookId).Result;
        }

    }
}