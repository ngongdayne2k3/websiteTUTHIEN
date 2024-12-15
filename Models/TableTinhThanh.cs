using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableTinhThanh
{
    public int MaTinhThanh { get; set; }

    public string TenTinhThanh { get; set; }

    public int MaVungMien { get; set; }

    public virtual TableVungMien MaVungMienNavigation { get; set; }

    public virtual ICollection<TableDuAn> TableDuAns { get; set; } = new List<TableDuAn>();
}
