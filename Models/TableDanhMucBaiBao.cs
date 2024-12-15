using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableDanhMucBaiBao
{
    public int MaDanhMucBaiBao { get; set; }

    public string TenDanhMucBaiBao { get; set; }

    public string MotaDanhMuc { get; set; }

    public virtual ICollection<TableBaiBao> TableBaiBaos { get; set; } = new List<TableBaiBao>();
}
