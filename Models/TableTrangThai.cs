﻿using System;
using System.Collections.Generic;

namespace websiteTUTHIEN.Models;

public partial class TableTrangThai
{
    public int MaTrangThai { get; set; }

    public string TenTrangThai { get; set; } = null!;

    public virtual ICollection<TableDuAn> TableDuAns { get; set; } = new List<TableDuAn>();
}
