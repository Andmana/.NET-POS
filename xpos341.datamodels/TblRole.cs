﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace xpos341.datamodels;

[Table("TblRole")]
public partial class TblRole
{
    [Key]
    public int Id { get; set; }

    [StringLength(80)]
    public string RoleName { get; set; } = null!;

    public bool? IsDelete { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }
}
