using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BloggingSite.Models;

namespace BloggingSite.Data
{
    public class BloggingSiteContext : IdentityDbContext
    {
        public BloggingSiteContext (DbContextOptions<BloggingSiteContext> options)
            : base(options)
        {
        }

        public DbSet<BloggingSite.Models.blog> blog { get; set; } = default!;

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
