﻿using System;
using System.Collections.Generic;

namespace DoAn.Models;

public partial class Scheduledetail
{
    public int ScheduleId { get; set; }

    public int StaffId { get; set; }

    public DateTime? Date { get; set; }

    public bool? Status { get; set; }

    public virtual Schedule Schedule { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
