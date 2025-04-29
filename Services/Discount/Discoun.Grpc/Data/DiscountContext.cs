﻿using Discoun.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupones { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon() { Id = 1, ProductName = "IPhone 12", Description = "IPhone 12 discount", Amount = 220 },
                new Coupon() { Id = 2, ProductName = "IPhone 13 Pro Max", Description = "IPhone 13 pro Max discount", Amount = 350 }
            );
        }

    }
}
