using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RandomReviewGenerator;
using System.Text.Json;

namespace RandomReviewSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public Review? Review { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Make a request to the API and retrieve a randomly generated review. 
        /// Updates the Review model and reloads the page, displaying the review container. 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostGenerateReview()
        {
            var httpClient = _httpClientFactory.CreateClient();
            string url = "https://localhost:44318/api/generate";
            //string url = "https://RandomReviewGenerator/api/generate"; // Url for docker. Not working, unsure how to fix this
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