using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheBookProject.Db.Entities;

public partial class Review
{
    [Key]
    public int Id { get; set; }

    public string ISBN { get; set; } = null!;

    public string? Text { get; set; }

    public string? Origin { get; set; }

    public double? Rating { get; set; }

    [ForeignKey("ISBN")]
    [InverseProperty("Reviews")]
    public virtual Book ISBNNavigation { get; set; } = null!;
}
