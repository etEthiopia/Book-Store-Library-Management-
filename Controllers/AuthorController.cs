using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using LibraryManagement.ViewModels;


namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _repository;

        public AuthorController(IAuthorRepository repository)
        {
            _repository = repository;
        }
        [Route("Author")]
        public IActionResult List()
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            if (!_repository.Any()) return View("Empty");

            var authors = _repository.GetAllWithBooks();

            return View(authors);
        }

        public IActionResult AuthorDetail()
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            var authors = _repository.GetAllWithBooks();

            if (authors?.ToList().Count == 0)
            {
                return View("Empty");
            }

            return View(authors);
        }

        public IActionResult Detail(int id)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            var author = _repository.GetById(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        public IActionResult Update(int id)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            var author = _repository.GetById(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Author author)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            string cv = author.Image;

            if (cv.Length > 1)
            {
                cv.Replace('\\', '/');


                Console.WriteLine($"cover ===={cv}");
                System.IO.File.Copy(cv, "C:/Users/Dagi/source/repos/LibraryManagement/wwwroot/images/" + "A" + author.AuthorId + ".jpg", true);
                author.Image = "images/" + "A" + author.AuthorId + ".jpg";
            }
                _repository.Update(author);
           
            return RedirectToAction("List");
        }

        public ViewResult Create()
        {
            return View(new CreateAuthorViewModel { Referer = Request.Headers["Referer"].ToString() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateAuthorViewModel authorVM)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            if (!ModelState.IsValid)
            {
                return View(authorVM);
            }
            string cv = authorVM.Author.Image;
            if (cv.Length > 1)
            {
                cv.Replace('\\', '/');


                Console.WriteLine($"cover ===={cv}");
                System.IO.File.Copy(cv, "C:/Users/Dagi/source/repos/LibraryManagement/wwwroot/images/" + "A" + authorVM.Author.AuthorId + ".jpg", true);
                authorVM.Author.Image = "images/" + "A" + authorVM.Author.AuthorId + ".jpg";
            }

            _repository.Create(authorVM.Author);

            if (!String.IsNullOrEmpty(authorVM.Referer))
            {
                return Redirect(authorVM.Referer);
            }

            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            if (!Admin.logged)
            {
                return RedirectToAction("Login", "Admin", 0);
            }
            var customer = _repository.GetById(id);

            _repository.Delete(customer);

            return RedirectToAction("List");
        }
    }
}
