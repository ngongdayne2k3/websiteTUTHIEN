using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableTinhThanh
{
    public int MaTinhThanh { get; set; }

    public string TenTinhThanh { get; set; } = null!;

    public int MaVungMien { get; set; }

    public virtual TableVungMien MaVungMienNavigation { get; set; } = null!;

    public virtual ICollection<TableDuAn> TableDuAns { get; set; } = new List<TableDuAn>();
}
