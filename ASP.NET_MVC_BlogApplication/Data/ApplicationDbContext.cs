﻿using Microsoft.EntityFrameworkCore;
using ASP.NET_MVC_BlogApplication.Models;

namespace ASP.NET_MVC_BlogApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs{ get; set; }
        public DbSet<BlogEntry> BlogEntries { get; set; }
    }
}
