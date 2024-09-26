using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace xpos341.datamodels;

public partial class TblCategory
{
    public int Id { get; set; }
    //[Required(ErrorMessage = "Harap isi Name Category")]
    //[StringLength(10)]
    //[MinLength(3, ErrorMessage = "Isi Minimum 3 Karakter")]
    public string NameCategory { get; set; } = null!;

    //[Required(ErrorMessage = "Harap isi Description")]
    public string? Description { get; set; }

    public bool? IsDelete { get; set; }

    public int CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
