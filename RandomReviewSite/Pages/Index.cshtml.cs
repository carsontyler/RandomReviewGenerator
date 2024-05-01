using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RandomReviewGenerator;
using RandomReviewSite.Options;
using System.Text.Json;

namespace RandomReviewSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApplicationOptions _applicationOptions;

        public Review? Review { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory, IOptions<ApplicationOptions> applicationOptions)
        {
            _httpClientFactory = httpClientFactory;
            _applicationOptions = applicationOptions.Value;
        }

        /// <summary>
        /// Make a request to the API and retrieve a randomly generated review. 
        /// Updates the Review model and reloads the page, displaying the review container. 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostGenerateReview()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var url = _applicationOptions.ApiUrl + "api/generate";
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                Review = await JsonSerializer.DeserializeAsync<Review>(contentStream);
            }

            return Page();
        }
    }
}