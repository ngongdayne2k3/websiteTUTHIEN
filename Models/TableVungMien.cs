using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableVungMien
{
    public int MaVungMien { get; set; }

    public string TenVungMien { get; set; } = null!;

    public virtual ICollection<TableDuAn> TableDuAns { get; set; } = new List<TableDuAn>();
}
