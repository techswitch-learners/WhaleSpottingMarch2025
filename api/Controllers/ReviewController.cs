using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Models.DatabaseModels;
using WhaleSpottingBackend.Repositories;

namespace WhaleSpottingBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private readonly ISightingRepository _sightingRepository;
    private readonly IReviewRepository _reviewRepository;
    public ReviewController(IReviewRepository reviewRepository, ISightingRepository sightingRepository)
    {
        _reviewRepository = reviewRepository;
        _sightingRepository = sightingRepository;
    }

    // POST: api/update-status
    [HttpPost("update-status")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateStatus([FromBody] CreateReviewRequest reviewRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string? adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminId is null)
        {
            return Unauthorized("Unauthorized");
        }
        SightingReview newReview = new(
            reviewRequest.SightingId,
            adminId,
            reviewRequest.Approved,
            DateTime.UtcNow,
            reviewRequest.Comments
        );
        _reviewRepository.AddSightingReview(newReview);

        if (reviewRequest.UpdatedSighting is not null)
        {
            Sighting sighting = _sightingRepository.GetSightingByID(reviewRequest.SightingId);
            sighting.Description = reviewRequest.UpdatedSighting.Description ?? sighting.Description;
            sighting.SpeciesId = reviewRequest.UpdatedSighting.Species.Id;
            _sightingRepository.UpdateSighting(sighting);
        }
        return Ok("Review completed successfully.");
    }
}
