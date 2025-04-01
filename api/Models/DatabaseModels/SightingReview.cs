using System.ComponentModel.DataAnnotations.Schema;
namespace WhaleSpottingBackend.Models.DatabaseModels;

public class SightingReview
{
    public int Id { get; set; } 

    [Column("ReportID")]
    public int ReportID { get; set; }
    [ForeignKey("ReportID")] 
    public Sighting Sighting { get; set; }
    
    [Column("AdminID")]
    public int AdminID { get; set; }

    [Column("Approved")]
    public bool Approved { get; set; }

    [Column("StatusDate")]
    public DateTime StatusDate { get; set; }
    [Column("Comments")]
    public string? Comments { get; set; }
    

    public SightingReview() { }
    public SightingReview(int id, int reportID, int adminID, bool approved, DateTime statusDate,string comments)
    {
        Id = id;
        ReportID = reportID;
        AdminID = adminID;
        Approved = approved;
        StatusDate=statusDate;
        Comments = comments;
    }
}
