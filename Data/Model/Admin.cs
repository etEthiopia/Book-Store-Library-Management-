using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Model
{
    public class Admin
    {
        public static bool logged = false;
        public int AId { get; set; }
        public string AdminId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Password { get; set; }
        public static string ID = "dagiNegussu";
        public static string PWD = "qwerty123";
        public Admin() {
        
    }
    public Admin(int aid)
    {
            AId = aid;
    }

    public Admin(int aid, string aidd, string pass)
    {
            AId = aid;
            AdminId = aidd;
            Password = pass;
    }

    }
}
