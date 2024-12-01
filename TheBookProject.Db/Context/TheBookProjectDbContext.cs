using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TheBookProject.Db.Entities;

namespace TheBookProject.Db.Context;

public partial class TheBookProjectDbContext : DbContext
{
    public TheBookProjectDbContext()
    {
    }

    public TheBookProjectDbContext(DbContextOptions<TheBookProjectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasOne(d => d.ISBNNavigation).WithMany(p => p.Reviews).OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
