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
    public class AdminController : Controller
    {
        private readonly Admin a;
        


       

        public IActionResult Login(int? aid)
        {



            Console.WriteLine("Logggginnnnn");   

            return View();
        }

        public IActionResult Logout(int? aid)
        {
            Admin.logged = false;
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Login(AdminViewModel bookVM)
             
        {
            
            Console.WriteLine($"---------------{bookVM.Admin.AdminId}--,--{bookVM.Admin.Password}------------");
            if (bookVM.Admin.AdminId == Admin.ID && bookVM.Admin.Password == Admin.PWD)
            {
                Admin.logged = true;
                return RedirectToAction("List","Book",0);
            }
            else
            {
                return View();
            }
            

            
        }

    }
}
