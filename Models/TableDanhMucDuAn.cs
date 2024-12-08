using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableDanhMucDuAn
{
    public int MaDanhMucDa { get; set; }

    public string TenDanhMucDa { get; set; } = null!;

    public string? MotaDanhMuc { get; set; }

    public virtual ICollection<TableDuAn> TableDuAns { get; set; } = new List<TableDuAn>();
}
