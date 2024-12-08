using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace websiteTUTHIEN.Models;

public partial class WebsiteTuthienContext : DbContext
{
    public WebsiteTuthienContext()
    {
    }

    public WebsiteTuthienContext(DbContextOptions<WebsiteTuthienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TableAdmin> TableAdmins { get; set; }

    public virtual DbSet<TableBaiBao> TableBaiBaos { get; set; }

    public virtual DbSet<TableBinhLuanBaiBao> TableBinhLuanBaiBaos { get; set; }

    public virtual DbSet<TableDanhMucBaiBao> TableDanhMucBaiBaos { get; set; }

    public virtual DbSet<TableDanhMucDuAn> TableDanhMucDuAns { get; set; }

    public virtual DbSet<TableDuAn> TableDuAns { get; set; }

    public virtual DbSet<TableHinhThucQuyenGop> TableHinhThucQuyenGops { get; set; }

    public virtual DbSet<TableNguoiDung> TableNguoiDungs { get; set; }

    public virtual DbSet<TableQuyenGop> TableQuyenGops { get; set; }

    public virtual DbSet<TableTinhThanh> TableTinhThanhs { get; set; }

    public virtual DbSet<TableVungMien> TableVungMiens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(" Data Source=NGONGDAYNE2K3;Initial Catalog=WEBSITE_TUTHIEN;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TableAdmin>(entity =>
        {
            entity.HasKey(e => e.MaAdmin);

            entity.ToTable("TableAdmin");

            entity.Property(e => e.MaAdmin).HasColumnName("maAdmin");
            entity.Property(e => e.Diachi)
                .HasMaxLength(100)
                .HasColumnName("diachi");
            entity.Property(e => e.Hinhanh)
                .HasMaxLength(255)
                .HasColumnName("hinhanh");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(100)
                .HasColumnName("matkhau");
            entity.Property(e => e.Namsinh)
                .HasColumnType("datetime")
                .HasColumnName("namsinh");
            entity.Property(e => e.Sdt)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("sdt");
            entity.Property(e => e.TenTk)
                .HasMaxLength(100)
                .HasColumnName("tenTk");
            entity.Property(e => e.Tenadmin)
                .HasMaxLength(100)
                .HasColumnName("tenadmin");
        });

        modelBuilder.Entity<TableBaiBao>(entity =>
        {
            entity.HasKey(e => e.MaBaiBao);

            entity.ToTable("TableBaiBao");

            entity.Property(e => e.MaBaiBao).HasColumnName("maBaiBao");
            entity.Property(e => e.HinhanhBaiBao)
                .HasMaxLength(255)
                .HasColumnName("hinhanhBaiBao");
            entity.Property(e => e.MaDanhMucBaiBao).HasColumnName("maDanhMucBaiBao");
            entity.Property(e => e.NgayDangBaiBao)
                .HasColumnType("datetime")
                .HasColumnName("ngayDangBaiBao");
            entity.Property(e => e.NoidungBaiBao).HasColumnName("noidungBaiBao");
            entity.Property(e => e.TenBaiBao)
                .HasMaxLength(500)
                .HasColumnName("tenBaiBao");
            entity.Property(e => e.TenTacGia)
                .HasMaxLength(100)
                .HasColumnName("tenTacGia");

            entity.HasOne(d => d.MaDanhMucBaiBaoNavigation).WithMany(p => p.TableBaiBaos)
                .HasForeignKey(d => d.MaDanhMucBaiBao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableBaiBao_TableDanhMucBaiBao");
        });

        modelBuilder.Entity<TableBinhLuanBaiBao>(entity =>
        {
            entity.HasKey(e => e.MaBinhLuanBaiBao);

            entity.ToTable("TableBinhLuanBaiBao");

            entity.Property(e => e.MaBinhLuanBaiBao).HasColumnName("maBinhLuanBaiBao");
            entity.Property(e => e.MaBaiBao).HasColumnName("maBaiBao");
            entity.Property(e => e.MaNguoiDung).HasColumnName("maNguoiDung");
            entity.Property(e => e.NgayBinhLuan)
                .HasColumnType("datetime")
                .HasColumnName("ngayBinhLuan");
            entity.Property(e => e.NoidungBinhLuan)
                .HasMaxLength(255)
                .HasColumnName("noidungBinhLuan");

            entity.HasOne(d => d.MaBaiBaoNavigation).WithMany(p => p.TableBinhLuanBaiBaos)
                .HasForeignKey(d => d.MaBaiBao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableBinhLuanBaiBao_TableBaiBao");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.TableBinhLuanBaiBaos)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableBinhLuanBaiBao_TableNguoiDung");
        });

        modelBuilder.Entity<TableDanhMucBaiBao>(entity =>
        {
            entity.HasKey(e => e.MaDanhMucBaiBao);

            entity.ToTable("TableDanhMucBaiBao");

            entity.Property(e => e.MaDanhMucBaiBao).HasColumnName("maDanhMucBaiBao");
            entity.Property(e => e.MotaDanhMuc)
                .HasMaxLength(500)
                .HasColumnName("motaDanhMuc");
            entity.Property(e => e.TenDanhMucBaiBao)
                .HasMaxLength(100)
                .HasColumnName("tenDanhMucBaiBao");
        });

        modelBuilder.Entity<TableDanhMucDuAn>(entity =>
        {
            entity.HasKey(e => e.MaDanhMucDa).HasName("PK_TableVungMien");

            entity.ToTable("TableDanhMucDuAn");

            entity.Property(e => e.MaDanhMucDa).HasColumnName("maDanhMucDA");
            entity.Property(e => e.MotaDanhMuc)
                .HasMaxLength(500)
                .HasColumnName("motaDanhMuc");
            entity.Property(e => e.TenDanhMucDa)
                .HasMaxLength(100)
                .HasColumnName("tenDanhMucDA");
        });

        modelBuilder.Entity<TableDuAn>(entity =>
        {
            entity.HasKey(e => e.MaDuAn);

            entity.ToTable("TableDuAn");

            entity.Property(e => e.MaDuAn).HasColumnName("maDuAn");
            entity.Property(e => e.CoNghiemTrong)
                .HasDefaultValue(false)
                .HasColumnName("coNghiemTrong");
            entity.Property(e => e.DaDuyetBai)
                .HasDefaultValue(false)
                .HasColumnName("daDuyetBai");
            entity.Property(e => e.DaKetThucDuAn)
                .HasDefaultValue(false)
                .HasColumnName("daKetThucDuAn");
            entity.Property(e => e.Hinhanh)
                .HasMaxLength(255)
                .HasColumnName("hinhanh");
            entity.Property(e => e.MaDanhMucDa).HasColumnName("maDanhMucDA");
            entity.Property(e => e.MaNguoiDung).HasColumnName("maNguoiDung");
            entity.Property(e => e.MaTinhThanh).HasColumnName("maTinhThanh");
            entity.Property(e => e.Ngaybatdau)
                .HasColumnType("datetime")
                .HasColumnName("ngaybatdau");
            entity.Property(e => e.Ngayketthuc)
                .HasColumnType("datetime")
                .HasColumnName("ngayketthuc");
            entity.Property(e => e.NoidungDuAn).HasColumnName("noidungDuAn");
            entity.Property(e => e.SoTienHienTai).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SoTienMucTieu).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TenDuAn)
                .HasMaxLength(500)
                .HasColumnName("tenDuAn");

            entity.HasOne(d => d.MaDanhMucDaNavigation).WithMany(p => p.TableDuAns)
                .HasForeignKey(d => d.MaDanhMucDa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableDuAn_TableDanhMucDA");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.TableDuAns)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableDuAn_TableNguoiDung");

            entity.HasOne(d => d.MaTinhThanhNavigation).WithMany(p => p.TableDuAns)
                .HasForeignKey(d => d.MaTinhThanh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableDuAn_TableTinhThanh");
        });

        modelBuilder.Entity<TableHinhThucQuyenGop>(entity =>
        {
            entity.HasKey(e => e.MaHinhThucQuyenGop);

            entity.ToTable("TableHinhThucQuyenGop");

            entity.Property(e => e.MaHinhThucQuyenGop).HasColumnName("maHinhThucQuyenGop");
            entity.Property(e => e.HinhThucQuyenGop).HasMaxLength(50);
        });

        modelBuilder.Entity<TableNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung);

            entity.ToTable("TableNguoiDung");

            entity.Property(e => e.MaNguoiDung).HasColumnName("maNguoiDung");
            entity.Property(e => e.AvatarNguoiDung)
                .HasMaxLength(300)
                .HasColumnName("avatarNguoiDung");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(100)
                .HasColumnName("matKhau");
            entity.Property(e => e.NamSinh).HasColumnType("datetime");
            entity.Property(e => e.SdtnguoiDung)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("SDTNguoiDung");
            entity.Property(e => e.TenNguoiDung)
                .HasMaxLength(100)
                .HasColumnName("tenNguoiDung");
            entity.Property(e => e.TenTk)
                .HasMaxLength(100)
                .HasColumnName("tenTK");
        });

        modelBuilder.Entity<TableQuyenGop>(entity =>
        {
            entity.HasKey(e => e.MaQuyenGop);

            entity.ToTable("TableQuyenGop");

            entity.Property(e => e.MaQuyenGop).ValueGeneratedNever();
            entity.Property(e => e.GhiChuQuyenGop).HasMaxLength(200);
            entity.Property(e => e.GiaTriQuyenGop).HasMaxLength(50);
            entity.Property(e => e.MaDuAn).HasColumnName("maDuAn");
            entity.Property(e => e.MaHinhThucQuyenGop).HasColumnName("maHinhThucQuyenGop");
            entity.Property(e => e.NgayQuyenGop).HasColumnType("datetime");
            entity.Property(e => e.TenNguoiQuyenGop).HasMaxLength(100);

            entity.HasOne(d => d.MaDuAnNavigation).WithMany(p => p.TableQuyenGops)
                .HasForeignKey(d => d.MaDuAn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableQuyenGop_TableDuAn");

            entity.HasOne(d => d.MaHinhThucQuyenGopNavigation).WithMany(p => p.TableQuyenGops)
                .HasForeignKey(d => d.MaHinhThucQuyenGop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableQuyenGop_TableHinhThucQuyenGop");
        });

        modelBuilder.Entity<TableTinhThanh>(entity =>
        {
            entity.HasKey(e => e.MaTinhThanh);

            entity.ToTable("TableTinhThanh");

            entity.Property(e => e.MaTinhThanh)
                .ValueGeneratedNever()
                .HasColumnName("maTinhThanh");
            entity.Property(e => e.MaVungMien).HasColumnName("maVungMien");
            entity.Property(e => e.TenTinhThanh)
                .HasMaxLength(60)
                .HasColumnName("tenTinhThanh");

            entity.HasOne(d => d.MaVungMienNavigation).WithMany(p => p.TableTinhThanhs)
                .HasForeignKey(d => d.MaVungMien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableTinhThanh_TableVungMien");
        });

        modelBuilder.Entity<TableVungMien>(entity =>
        {
            entity.HasKey(e => e.MaVungMien).HasName("PK_TableVungMien_1");

            entity.ToTable("TableVungMien");

            entity.Property(e => e.MaVungMien).HasColumnName("maVungMien");
            entity.Property(e => e.TenVungMien)
                .HasMaxLength(100)
                .HasColumnName("tenVungMien");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
