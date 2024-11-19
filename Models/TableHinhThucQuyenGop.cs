using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableHinhThucQuyenGop
{
    public int MaHinhThucQuyenGop { get; set; }

    public string HinhThucQuyenGop { get; set; } = null!;

    public virtual ICollection<TableQuyenGop> TableQuyenGops { get; set; } = new List<TableQuyenGop>();
}
