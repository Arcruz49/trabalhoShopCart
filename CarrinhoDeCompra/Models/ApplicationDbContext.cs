using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarrinhoDeCompra.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CadCarrinhoProduto> CadCarrinhoProdutos { get; set; }

    public virtual DbSet<CadProduto> CadProdutos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=dbShopCart;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CadCarrinhoProduto>(entity =>
        {
            entity.HasKey(e => e.CdCarrinho).HasName("PK__cadCarri__3DD6D22F94FF14E6");

            entity.ToTable("cadCarrinho_Produtos");

            entity.Property(e => e.CdCarrinho).HasColumnName("cdCarrinho");
            entity.Property(e => e.CdProduto).HasColumnName("cdProduto");

            entity.HasOne(d => d.CdProdutoNavigation).WithMany(p => p.CadCarrinhoProdutos)
                .HasForeignKey(d => d.CdProduto)
                .HasConstraintName("FK_cadCarrinho_Produtos_cdProduto");
        });

        modelBuilder.Entity<CadProduto>(entity =>
        {
            entity.HasKey(e => e.CdProduto).HasName("PK__cadProdu__8897B773E76EB23A");

            entity.ToTable("cadProdutos");

            entity.Property(e => e.CdProduto).HasColumnName("cdProduto");
            entity.Property(e => e.Caminho)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("caminho");
            entity.Property(e => e.NmProduto)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("nmProduto");
            entity.Property(e => e.Preco)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("preco");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
