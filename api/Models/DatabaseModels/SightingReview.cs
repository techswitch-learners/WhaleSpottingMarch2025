using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WhaleSpottingBackend.Models.DatabaseModels;

public class SightingReview
{
    public int Id { get; set; }

    [Column("SightingId")]
    public int SightingId { get; set; }
    [ForeignKey("SightingId")]
    public Sighting Sighting { get; set; }

    [Column("AdminId")]
    public string AdminId { get; set; }
    [ForeignKey("AdminId")]
    public User Admin { get; set; }

    [Column("Approved")]
    public bool Approved { get; set; }

    [Column("StatusDate")]
    public DateTime StatusDate { get; set; }
    [Column("Comments")]
    public string? Comments { get; set; }


    public SightingReview() { }
    public SightingReview(int sightingID, string adminID, bool approved, DateTime statusDate, string comments)
    {
        SightingId = sightingID;
        AdminId = adminID;
        Approved = approved;
        StatusDate = statusDate;
        Comments = comments;
    }
}
