﻿using BookStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
    }
}
