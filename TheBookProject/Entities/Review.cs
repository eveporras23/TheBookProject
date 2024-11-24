using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBookProject.Entities;

[Table("Reviews")]
public partial class Review
{
    [Key]
    public int Id { get; set; }
    public int BookId { get; set; }
    public int Type { get; set; }
    public string Opinion { get; set; }
    public int Rating { get; set; }

}