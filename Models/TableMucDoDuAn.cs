using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableMucDoDuAn
{
    public int MaMucDoDuAn { get; set; }

    public string TenMucDoDuAn { get; set; } = null!;

    public virtual ICollection<TableDuAn> TableDuAns { get; set; } = new List<TableDuAn>();
}
