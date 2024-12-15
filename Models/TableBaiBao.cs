using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableBaiBao
{
    public int MaBaiBao { get; set; }

    public string TenBaiBao { get; set; }

    public string TenTacGia { get; set; }

    public string NoidungBaiBao { get; set; }

    public DateTime NgayDangBaiBao { get; set; }

    public string HinhanhBaiBao { get; set; }

    public int MaDanhMucBaiBao { get; set; }

    public virtual TableDanhMucBaiBao MaDanhMucBaiBaoNavigation { get; set; }

    public virtual ICollection<TableBinhLuanBaiBao> TableBinhLuanBaiBaos { get; set; } = new List<TableBinhLuanBaiBao>();
}
