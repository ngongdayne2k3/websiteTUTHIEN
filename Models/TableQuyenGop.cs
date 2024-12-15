using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableQuyenGop
{
    public int MaQuyenGop { get; set; }

    public string TenNguoiQuyenGop { get; set; }

    public DateTime NgayQuyenGop { get; set; }

    public string GiaTriQuyenGop { get; set; }

    public string GhiChuQuyenGop { get; set; }

    public int MaHinhThucQuyenGop { get; set; }

    public int MaDuAn { get; set; }

    public bool? DaXacNhan { get; set; }

    public virtual TableDuAn MaDuAnNavigation { get; set; }

    public virtual TableHinhThucQuyenGop MaHinhThucQuyenGopNavigation { get; set; }
}
