using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WhaleSpottingBackend.Models.DatabaseModels;

public class SightingReview
{
    public int Id { get; set; }

    [Column("SightingID")]
    public int SightingID { get; set; }
    [ForeignKey("SightingID")]
    public Sighting Sighting { get; set; }

    [Column("AdminID")]
    public string AdminID { get; set; }
    [ForeignKey("AdminID")]
    public User User { get; set; }

    [Column("Approved")]
    public bool Approved { get; set; }

    [Column("StatusDate")]
    public DateTime StatusDate { get; set; }
    [Column("Comments")]
    public string? Comments { get; set; }


    public SightingReview() { }
    public SightingReview(int sightingID, string adminID, bool approved, DateTime statusDate, string comments)
    {
        SightingID = sightingID;
        AdminID = adminID;
        Approved = approved;
        StatusDate = statusDate;
        Comments = comments;
    }
}
