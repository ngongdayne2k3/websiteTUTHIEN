using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableNguoiDung
{
    public int MaNguoiDung { get; set; }

    public string TenNguoiDung { get; set; } = null!;

    public string? AvatarNguoiDung { get; set; }

    public string TenTk { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public decimal? SdtnguoiDung { get; set; }

    public string Email { get; set; } = null!;

    public string? DiaChi { get; set; }

    public DateTime? NamSinh { get; set; }

    public virtual ICollection<TableBinhLuanBaiBao> TableBinhLuanBaiBaos { get; set; } = new List<TableBinhLuanBaiBao>();

    public virtual ICollection<TableDuAn> TableDuAns { get; set; } = new List<TableDuAn>();
}
