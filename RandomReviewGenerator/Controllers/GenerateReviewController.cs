using Microsoft.AspNetCore.Mvc;
using RandomReviewGenerator.Services.Interfaces;

namespace RandomReviewGenerator.Controllers
{
    [ApiController]
    [Route("api")]
    [ProducesResponseType(typeof(Review), StatusCodes.Status200OK)]
    public class GenerateReviewController : ControllerBase
    {
        private readonly IGenerateReviewService _generateReviewService;

        public GenerateReviewController(IGenerateReviewService generateReviewService)
        {
            _generateReviewService = generateReviewService;
        }

        /// <summary>
        /// Generate a random review.
        /// </summary>
        /// <returns>A newly created Review</returns>
        /// <remarks>
        /// Generates a sample review from a pre-trained MarkovChain.
        /// The review includes a random review summary, a star rating based on the sentiment of that review,
        ///     and a random number for "found helpful". The username is predefined 
        /// </remarks>
        /// <response code="200">Returns the newly created review</response>
        [HttpGet("generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Review> GenerateReview()
        {
            var review = _generateReviewService.GenerateReview();

            return Ok(review);
        }
    }
}