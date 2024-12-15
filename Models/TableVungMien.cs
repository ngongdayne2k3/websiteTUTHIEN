using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableVungMien
{
    public int MaVungMien { get; set; }

    public string TenVungMien { get; set; }

    public virtual ICollection<TableTinhThanh> TableTinhThanhs { get; set; } = new List<TableTinhThanh>();
}
