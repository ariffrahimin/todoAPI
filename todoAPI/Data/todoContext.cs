using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using todo.Models;

namespace todo.Data
{
    public partial class todoContext : DbContext
    {
        public todoContext()
        {
        }

        public todoContext(DbContextOptions<todoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Todo> Todos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.HasKey(e => e.ListId);

                entity.ToTable("todo");

                entity.Property(e => e.ListId).HasColumnName("ListID");

                entity.Property(e => e.ListContent).HasColumnType("VARCHAR(255)");

                entity.Property(e => e.ListStatus).HasColumnType("VARCHAR(255)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
