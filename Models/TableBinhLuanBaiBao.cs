using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableBinhLuanBaiBao
{
    public int MaBinhLuanBaiBao { get; set; }

    public int MaNguoiDung { get; set; }

    public int MaBaiBao { get; set; }

    public string NoidungBinhLuan { get; set; } = null!;

    public DateOnly NgayBinhLuan { get; set; }

    public virtual TableBaiBao MaBaiBaoNavigation { get; set; } = null!;

    public virtual TableNguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
