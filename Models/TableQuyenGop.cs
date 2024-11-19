using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableQuyenGop
{
    public int MaQuyenGop { get; set; }

    public string TenNguoiQuyenGop { get; set; } = null!;

    public DateTime NgayQuyenGop { get; set; }

    public string GiaTriQuyenGop { get; set; } = null!;

    public string GhiChuQuyenGop { get; set; } = null!;

    public int MaHinhThucQuyenGop { get; set; }

    public int MaDuAn { get; set; }

    public virtual TableDuAn MaDuAnNavigation { get; set; } = null!;

    public virtual TableHinhThucQuyenGop MaHinhThucQuyenGopNavigation { get; set; } = null!;
}
