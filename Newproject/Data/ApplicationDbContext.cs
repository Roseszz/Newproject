using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newproject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Newproject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Author { get; set; }

        public DbSet<Order> Order { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedBook(builder);
            SeedCategory(builder);
            SeedAuthor(builder);

            SeedUser(builder);
            SeedRole(builder);
            SeedUserRole(builder);
        }


        private void SeedUserRole(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId = "1"
                },
                new IdentityUserRole<string>
                {
                    UserId = "2",
                    RoleId = "2"
                }
            );
        }

        private void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "Admin"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Customer",
                    NormalizedName = "Customer"
                }
            );
        }

        private void SeedUser(ModelBuilder builder)
        {
            //tạo tài khoản test cho admin & customer
            var admin = new IdentityUser
            {
                Id = "1",
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com"
            };
            var customer = new IdentityUser
            {
                Id = "2",
                Email = "customer@gmail.com",
                UserName = "customer@gmail.com",
                NormalizedUserName = "customer@gmail.com"
            };

            //khai báo thư viện để mã hóa mật khẩu cho user
            var hasher = new PasswordHasher<IdentityUser>();

            //set mật khẩu đã mã hóa cho từng user
            admin.PasswordHash = hasher.HashPassword(admin, "123456");
            customer.PasswordHash = hasher.HashPassword(customer, "123456");

            //add 2 tài khoản test vào bảng User
            builder.Entity<IdentityUser>().HasData(admin, customer);
        }

        private void SeedBook(ModelBuilder builder)
        {
            builder.Entity<Book>().HasData(
                new Book { Id = 1, Name = "Hamlet ", Quantity = 10, Price = 100.0, TimePublish = DateTime.Parse("12/09/1996"), AuthorId = 1, CategoryId = 1 },
                new Book { Id = 2, Name = "The Great Gatsby", Quantity = 10, Price = 200.0, TimePublish = DateTime.Parse("12/06/2000"), AuthorId = 2, CategoryId = 2 },
                new Book { Id = 3, Name = "One Hundred Years of Solitude", Quantity = 10, Price = 400.0, TimePublish = DateTime.Parse("12/07/2009"), AuthorId = 3, CategoryId = 3 },
                new Book { Id = 4, Name = "Don Quixote", Quantity = 10, Price = 700.0, TimePublish = DateTime.Parse("12/03/2012"), AuthorId = 4, CategoryId = 4 },
                new Book { Id = 5, Name = "Moby Dick ", Quantity = 10, Price = 200.0, TimePublish = DateTime.Parse("12/01/2005"), AuthorId = 5, CategoryId = 5 }
                );
        }
        private void SeedCategory(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = " Novel" },
                new Category { Id = 2, Name = " Fantasy" },
                new Category { Id = 3, Name = " Romance" },
                new Category { Id = 4, Name = " Horror" },
                new Category { Id = 5, Name = " Comedy" }
                );
        }
        private void SeedAuthor(ModelBuilder builder)
        {
            _ = builder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Long", Email = "Long@gmail.com", Age = 40 },
                new Author { Id = 2, Name = "Minh", Email = "Minh@gmail.com", Age = 70 },
                new Author { Id = 3, Name = "Khanh", Email = "Khanh@gmail.com", Age = 50 },
                new Author { Id = 4, Name = "Ha", Email = "Ha@gmail.com", Age = 30 },
                new Author { Id = 5, Name = "Hanh", Email = "Hanh@gmail.com", Age = 40 }
                );
        }
    }
}
