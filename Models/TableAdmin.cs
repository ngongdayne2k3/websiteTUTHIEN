using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableAdmin
{
    public int MaAdmin { get; set; }

    public string TenTk { get; set; } = null!;

    public string Matkhau { get; set; } = null!;

    public string Tenadmin { get; set; } = null!;

    public string Hinhanh { get; set; } = null!;

    public decimal Sdt { get; set; }

    public string Diachi { get; set; } = null!;

    public DateTime Namsinh { get; set; }
}
