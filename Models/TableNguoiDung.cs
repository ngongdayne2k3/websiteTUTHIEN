using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableNguoiDung
{
    public int MaNguoiDung { get; set; }

    public string TenNguoiDung { get; set; } = null!;

    public string AvatarNguoiDung { get; set; } = null!;

    public string TenTk { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public decimal SdtnguoiDung { get; set; }

    public string Email { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public DateTime NamSinh { get; set; }

    public virtual ICollection<TableBaiBao> TableBaiBaos { get; set; } = new List<TableBaiBao>();

    public virtual ICollection<TableDuAn> TableDuAns { get; set; } = new List<TableDuAn>();
}
