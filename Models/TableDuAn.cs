using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableDuAn
{
    public int MaDuAn { get; set; }

    public string TenDuAn { get; set; }

    public string NoidungDuAn { get; set; }

    public string Hinhanh { get; set; }

    public decimal SoTienMucTieu { get; set; }

    public decimal? SoTienHienTai { get; set; }

    public DateTime Ngaybatdau { get; set; }

    public DateTime Ngayketthuc { get; set; }

    public int MaTinhThanh { get; set; }

    public int MaDanhMucDa { get; set; }

    public int? MaNguoiDung { get; set; }

    public bool? DaKetThucDuAn { get; set; }

    public bool? DaDuyetBai { get; set; }

    public bool? CoNghiemTrong { get; set; }

    public virtual TableDanhMucDuAn MaDanhMucDaNavigation { get; set; }

    public virtual TableNguoiDung MaNguoiDungNavigation { get; set; }

    public virtual TableTinhThanh MaTinhThanhNavigation { get; set; }

    public virtual ICollection<TableQuyenGop> TableQuyenGops { get; set; } = new List<TableQuyenGop>();
}
