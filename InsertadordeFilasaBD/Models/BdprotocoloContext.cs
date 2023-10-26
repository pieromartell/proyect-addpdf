using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InsertadordeFilasaBD.Models;

public partial class BdprotocoloContext : DbContext
{
    public BdprotocoloContext()
    {
    }

    public BdprotocoloContext(DbContextOptions<BdprotocoloContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Protocolo> Protocolos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=FAMILIA-MARTELL; DataBase=bdprotocolo; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("PK__producto__DC53BE3CEEAE7377");

            entity.ToTable("producto");

            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Nameproduct)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nameproduct");
        });

        modelBuilder.Entity<Protocolo>(entity =>
        {
            entity.HasKey(e => e.Idprotocolo).HasName("PK__protocol__EC5589637FD3DC54");

            entity.ToTable("protocolo");

            entity.Property(e => e.Idprotocolo).HasColumnName("idprotocolo");
            entity.Property(e => e.Document)
                .IsUnicode(false)
                .HasColumnName("document");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Lote).HasColumnName("lote");
            entity.Property(e => e.Namedocument)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("namedocument");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.Protocolos)
                .HasForeignKey(d => d.Idproducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__protocolo__idpro__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
