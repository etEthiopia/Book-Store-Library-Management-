using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using LibraryManagement.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repository;
        private readonly IAuthorRepository _authorRepository;

        public BookController(IBookRepository repository, IAuthorRepository authorRepository)
        {
            _repository = repository;
            _authorRepository = authorRepository;
        }
        [Route("Book")]
        public IActionResult List(int? authorId, int? borrowerId)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }

            var book = _repository.GetAllWithAuthor().ToList();

            IEnumerable<Book> books;


            ViewBag.AuthorId = authorId;

            if(borrowerId != null)
            {
                books = _repository.FindWithAuthorAndBorrower(x => x.BorrowerId == borrowerId);
                return CheckBooksCount(books);
            }

            if (authorId == null)
            {
                books = _repository.GetAllWithAuthor();
                return CheckBooksCount(books);
            }
            else
            {
                var author = _authorRepository.GetWithBooks((int)authorId);

                if (author.Books == null || author.Books.Count == 0)
                    return View("EmptyAuthor", author);
            }

            books = _repository.FindWithAuthor(a => a.Author.AuthorId == authorId);


            
            return CheckBooksCount(books);
        }

        public IActionResult Search(int? authorId, int? borrowerId)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            var book = _repository.GetAllWithAuthor().ToList();

            IEnumerable<Book> books;

            ViewBag.AuthorId = authorId;

            if (borrowerId != null)
            {
                books = _repository.FindWithAuthorAndBorrower(x => x.BorrowerId == borrowerId);
                return CheckBooksCount(books);
            }

            if (authorId == null)
            {
                books = _repository.GetAllWithAuthor();
                return CheckBooksCount(books);
            }
            else
            {
                var author = _authorRepository.GetWithBooks((int)authorId);

                if (author.Books == null || author.Books.Count == 0)
                    return View("EmptyAuthor", author);
            }

            books = _repository.FindWithAuthor(a => a.Author.AuthorId == authorId);

            return CheckBooksCount(books);
        }

        private IActionResult CheckBooksCount(IEnumerable<Book> books)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            if (books == null || books.ToList().Count == 0)
            {
                return View("Empty");
            }
            else
            {
                foreach (Book b in books)
                {
                    Console.WriteLine($"------{b.BookId.ToString()}-{b.BorrowerId.ToString()}-----");
                }
                return View(books);
            }
        }

        public IActionResult Update(int id)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            Book book = _repository.FindWithAuthor(a => a.BookId == id).FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            var bookVM = new BookEditViewModel
            {
                Book = book,
                Authors = _authorRepository.GetAll()
            };

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(BookEditViewModel bookVM)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            Console.WriteLine($"------------");
            Console.WriteLine($"------------");
            Console.WriteLine($"------------");
            Console.WriteLine($"------------");
            Console.WriteLine($"---------------{bookVM.Book.BookId}--,--{bookVM.Book.Title}------------");
            if (!ModelState.IsValid)
            {
                bookVM.Authors = _authorRepository.GetAll();
                return View(bookVM);
            }
            
            string cv = bookVM.Book.Cover;
            if (cv.Length > 1)
            {
                cv.Replace('\\', '/');


                

                Console.WriteLine($"cover ===={cv}");
                try
                {
                    System.IO.File.Copy(cv, "C:/Users/Dagi/source/repos/LibraryManagement/wwwroot/images/" + bookVM.Book.BookId + "QQ" + bookVM.Book.AuthorId + ".jpg", true);
                    int n = bookVM.Book.BookId;
                    bookVM.Book.Cover = "images/" + n + "QQ" + bookVM.Book.AuthorId + ".jpg";
                }
                
                
                catch
                {
                
                }
            }
            _repository.Update(bookVM.Book);

            return RedirectToAction("List");
        }

        public IActionResult Create(int? authorId)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            Book book = new Book();

            if(authorId != null)
            {
                book.AuthorId = (int)authorId;
            }

            var bookVM = new BookEditViewModel
            {
                Authors = _authorRepository.GetAll(),
                Book = book
            };

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookEditViewModel bookVM)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            if (!ModelState.IsValid)
            {
                bookVM.Authors = _authorRepository.GetAll();
                return View(bookVM);
            }
            string cv = bookVM.Book.Cover;
            
                    cv.Replace('\\','/');

            try
            {
                Console.WriteLine($"cover ===={cv}");
                System.IO.File.Copy(cv, "C:/Users/Dagi/source/repos/LibraryManagement/wwwroot/images/" + bookVM.Book.BookId + "QQ" + bookVM.Book.AuthorId + ".jpg", true);
                bookVM.Book.Cover = "images/" + bookVM.Book.BookId + "QQ" + bookVM.Book.AuthorId + ".jpg";
            }
            catch
            {

            }
                _repository.Create(bookVM.Book);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int id, int? authorId)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            var book = _repository.GetById(id);

            _repository.Delete(book);

            return RedirectToAction("List", new { authorId = authorId });
        }

        public IActionResult Remove(int id)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            Book book = _repository.FindWithAuthor(a => a.BookId == id).FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            var bookVM = new BookEditViewModel
            {
                Book = book,
                Authors = _authorRepository.GetAll()
            };

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(BookEditViewModel bookVM)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }

            var book = _repository.GetById(bookVM.Book.BookId);

            _repository.Delete(book);

            

             return RedirectToAction("List");

           
        }
    }
}
