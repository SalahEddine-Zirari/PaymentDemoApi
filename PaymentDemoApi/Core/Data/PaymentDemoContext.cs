using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PaymentDemoApi.Models
{
    public partial class PaymentDemoContext : DbContext
    {
        public PaymentDemoContext()
        {
        }

        public PaymentDemoContext(DbContextOptions<PaymentDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CoOwner> CoOwners { get; set; } = null!;
        public virtual DbSet<MonthDetail> MonthDetails { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("DefaultConnection");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoOwner>(entity =>
            {
                entity.ToTable("CoOwner");

                entity.Property(e => e.Balance)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MonthlyFee).HasColumnType("money");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MonthDetail>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__MonthDet__55433A6B303C0B59");

                entity.Property(e => e.AmmountPaid)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaid)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("isPaid");

                entity.HasOne(d => d.CoOwner)
                    .WithMany(p => p.MonthDetails)
                    .HasForeignKey(d => d.CoOwnerId)
                    .HasConstraintName("FK__MonthDeta__CoOwn__276EDEB3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
