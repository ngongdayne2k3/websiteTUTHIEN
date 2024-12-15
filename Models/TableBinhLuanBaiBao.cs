using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableBinhLuanBaiBao
{
    public int MaBinhLuanBaiBao { get; set; }

    public int MaNguoiDung { get; set; }

    public int MaBaiBao { get; set; }

    public string NoidungBinhLuan { get; set; }

    public DateTime NgayBinhLuan { get; set; }

    public virtual TableBaiBao MaBaiBaoNavigation { get; set; }

    public virtual TableNguoiDung MaNguoiDungNavigation { get; set; }
}
