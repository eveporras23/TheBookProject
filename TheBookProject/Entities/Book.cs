using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBookProject.Entities;

[Table("Books")]
public partial class Book
{
    [Key]
    public string ISBN { get; set; }
     public string? Origin { get; set; }
    public string? Tittle { get; set; }
    public string? SubTittle { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public string? Description { get; set; }
    public string? PublishedDate { get; set; }
    public int? PageCount { get; set; }
    public string? Category { get; set; }
    public string? Language { get; set; }
   
}