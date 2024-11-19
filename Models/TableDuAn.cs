using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableDuAn
{
    public int MaDuAn { get; set; }

    public string TenDuAn { get; set; } = null!;

    public string MotaDuAn { get; set; } = null!;

    public string Hinhanh { get; set; } = null!;

    public decimal SoTienMucTieu { get; set; }

    public decimal SoTienHienTai { get; set; }

    public DateTime Ngaybatdau { get; set; }

    public DateTime Ngayketthuc { get; set; }

    public int MaVungMien { get; set; }

    public int MaDanhMucDa { get; set; }

    public int MaNguoiDung { get; set; }

    public int MaTrangThai { get; set; }

    public int MaMucDoDuAn { get; set; }

    public virtual TableDanhMucDuAn MaDanhMucDaNavigation { get; set; } = null!;

    public virtual TableMucDoDuAn MaMucDoDuAnNavigation { get; set; } = null!;

    public virtual TableNguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual TableTrangThai MaTrangThaiNavigation { get; set; } = null!;

    public virtual TableVungMien MaVungMienNavigation { get; set; } = null!;

    public virtual ICollection<TableQuyenGop> TableQuyenGops { get; set; } = new List<TableQuyenGop>();
}
