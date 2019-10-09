using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryManagement.Data.Repository
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public AdminRepository(LibraryDbContext context) : base(context) { }

        public IEnumerable<Admin> FindWithAdminIdAndPassword(Func<Admin, bool> predicate)
        {
            return _context.Admins
                .Include(a => a.AdminId)
                .Include(a => a.Password)
                .Where(predicate);
        }
    }
}
