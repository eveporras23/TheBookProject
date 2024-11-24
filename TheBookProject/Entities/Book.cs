using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBookProject.Entities;

[Table("Books")]
public partial class Book
{
    [Key]
    public int Id { get; set; }
    public string Tittle { get; set; }
    public string Author { get; set; }
    public string? Publisher { get; set; }
    public string? Category { get; set; }
    public string? ISBN_10 { get; set; }
    public double? ISBN_13 { get; set; }
}