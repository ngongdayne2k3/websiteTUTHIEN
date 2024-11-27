using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableDuAn
{
    public int MaDuAn { get; set; }

    public string TenDuAn { get; set; } = null!;

    public string NoidungDuAn { get; set; } = null!;

    public string Hinhanh { get; set; } = null!;

    public decimal SoTienMucTieu { get; set; }

    public decimal SoTienHienTai { get; set; }

    public DateTime Ngaybatdau { get; set; }

    public DateTime Ngayketthuc { get; set; }

    public int MaTinhThanh { get; set; }

    public int MaDanhMucDa { get; set; }

    public int MaNguoiDung { get; set; }

    public bool DaKetThucDuAn { get; set; }

    public bool DaDuyetBai { get; set; }

    public bool CoNghiemTrong { get; set; }

    public virtual TableDanhMucDuAn MaDanhMucDaNavigation { get; set; } = null!;

    public virtual TableNguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual TableTinhThanh MaTinhThanhNavigation { get; set; } = null!;

    public virtual ICollection<TableQuyenGop> TableQuyenGops { get; set; } = new List<TableQuyenGop>();
}
