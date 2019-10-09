using LibraryManagement.Data.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data
{
    public class DbInitializer
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            LibraryDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<LibraryDbContext>();

            UserManager<IdentityUser> userManager = applicationBuilder.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();

            // Add Lender

            

            var user = new IdentityUser("Miroslav Mikus");
            await userManager.CreateAsync(user, "%Miro1");

            // Add Customers
            var justin = new Customer { Name = "Dawit Yonas" };

            var willie = new Customer { Name = "Abel Tefera" };

            var leoma = new Customer { Name = "Bereket  Yohannes" };

            var admin = new Admin(1,"dagiNegussu", "qwerty123");

            context.Customers.Add(justin);
            context.Customers.Add(willie);
            context.Customers.Add(leoma);

            


            // Add Author

            var authorWiiliam = new Author
            {
                Name = "Willam Shakespeare",
                Image = "images/A5.jpg",
                Books = new List<Book>()
                {
                    new Book { Title = "Othello",  Category = "Romance Drama", Year="1680", Cover = "images/10QQ5.jpg" },
                   new Book { Title = "Jullius Cesar",   Category = "Politics Drama", Year="1700", Cover = "images/11QQ5.jpg" },
                   new Book { Title = "The Winters Tale",   Category = "Romance Drama", Year="1690", Cover = "images/12QQ5.jpg" }



                }
            };

            var authorSisay = new Author
            {
                Name = "Sisay Negussu",
                Image = "images/A4.jpg",
                Books = new List<Book>()
                {
                   new Book { Title = "Yerqiq Ashara",   Category = "Politics Drama", Year="2005", Cover = "images/7QQ4.jpg" },
                   new Book { Title = "Sawna",   Category = "Romance Drama", Year="2010", Cover = "images/8QQ4.jpg" },
                   new Book { Title = "Sememen",   Category = "Thriller Drama", Year="2000", Cover = "images/9QQ4.jpg" }

                }
            };


            var authorBealu = new Author
            {
                Name = "Bealu Girma",
                Image = "images/A3.jpg",
                Books = new List<Book>()
                {
                    new Book { Title = "Oromay",  Category = "Politics Thriller", Year="1980", Cover = "images/3QQ3.jpg" },
                   new Book { Title = "Derasiw",   Category = "Biography Drama",Year="1974", Cover = "images/4QQ3.jpg" },
                   new Book { Title = "Keadmas Bashagar",   Category = "Romance Politics", Year="1978", Cover = "images/5QQ3.jpg" },
                   new Book { Title = "Ye Hilina Dewl",  Category = "Thriller Drama", Year="1971", Cover = "images/6QQ3.jpg" }

                }
            };


            var authorHaddis = new Author
            {
                Name = "Hadis Alemayehu",
                Image = "images/A2.jpg",
                Books = new List<Book>()
                {
                    new Book { Title = "Fiker Eske Mekaber",   Category = "Romance Drama", Year="1921", Cover="images/2QQ2.jpg" }
                   
                }
            };

            var authorSidney = new Author
            {
                Name = "Sidney Sheldon",
                Image = "images/A1.jpg",
                Books = new List<Book>()
                {
                   new Book { Title = "Master of the Game",  Category = "Thriller Drama", Year="1981", Cover="images/1QQ1.jpg" }
                }
            };

            context.Authors.Add(authorSidney);
            context.Authors.Add(authorHaddis);
            context.Authors.Add(authorBealu);
            context.Authors.Add(authorSisay);
            context.Authors.Add(authorWiiliam);
            context.SaveChanges();
        }
    }
}
