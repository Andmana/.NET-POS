using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace xpos341.datamodels;

public partial class Xpos341Context : DbContext
{
    public Xpos341Context()
    {
    }

    public Xpos341Context(DbContextOptions<Xpos341Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblVariant> TblVariants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Initial Catalog=XPOS_341; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblCateg__3214EC07B84EC803");
        });

        modelBuilder.Entity<TblVariant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblVaria__3214EC07ECF8CA2E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
