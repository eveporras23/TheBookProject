using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheBookProject.Db.Entities;

public partial class Book
{
    public string Tittle { get; set; } = null!;

    public string? Author { get; set; } = null!;

    public string? Publisher { get; set; }

    public string? Category { get; set; }

    [Key]
    public string ISBN { get; set; } = null!;

    public string Origin { get; set; } = null!;

    public string? SubTittle { get; set; }

    public string? Description { get; set; }

    public string? PublishedDate { get; set; }

    public int? PageCount { get; set; }

    public string? Language { get; set; }
    public string? URLOrigin { get; set; }
    public double? Rating { get; set; }
    public double? RatingCount { get; set; }
}
