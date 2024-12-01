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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\patry\\Documents\\CENFOTEC\\repos\\TheBookProject\\TheBookProject.Db\\TheBookProject.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
