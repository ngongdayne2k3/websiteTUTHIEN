using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableBaiBao
{
    public int MaBaiBao { get; set; }

    public string TenBaiBao { get; set; } = null!;

    public string TenTacGia { get; set; } = null!;

    public string NoidungBaiBao { get; set; } = null!;

    public DateTime NgayDangBaiBao { get; set; }

    public string? HinhanhBaiBao { get; set; }

    public int MaDanhMucBaiBao { get; set; }

    public virtual TableDanhMucBaiBao MaDanhMucBaiBaoNavigation { get; set; } = null!;

    public virtual ICollection<TableBinhLuanBaiBao> TableBinhLuanBaiBaos { get; set; } = new List<TableBinhLuanBaiBao>();
}
