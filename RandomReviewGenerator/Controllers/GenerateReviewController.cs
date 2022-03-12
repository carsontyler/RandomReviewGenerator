using Markov;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SentimentAnalyzer;
using SentimentAnalyzer.Models;

namespace RandomReviewGenerator.Controllers
{
    [ApiController]
    [Route("api")]
    public class GenerateReviewController : ControllerBase
    {
        private readonly ILogger<GenerateReviewController> _logger;
        private readonly IMemoryCache _cache;

        public GenerateReviewController(ILogger<GenerateReviewController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet("generate")]
        public Review GenerateReview()
        {
            // Retrieve the trained data from cache
            string key = "markovChain";
            var chain = _cache.Get<MarkovChain<string>>(key);

            // Generate the random review text
            var rand = new Random();
            var generatedReview = string.Join(" ", chain.Chain(rand));
            var sentiment = Sentiment(generatedReview);

            // Calculate the sentiment score
            double score = sentiment.Prediction ? 2.5 + sentiment.Score * 0.5 : 2.5 - sentiment.Score * -0.125;
            if (score > 5) score = 5;
            else if (score < 1) score = 1;
            else score = Math.Round(score, 0);

            // create the new Review and return it.
            int totalHelpful = rand.Next(0, 100);
            int helpful = rand.Next(0, totalHelpful);

            var review = new Review()
            {
                text = generatedReview,
                timestamp = DateTime.UtcNow,
                username = "botman3000",
                stars = (int)score,
                summary = "My thoughts on this product",
                helpful = new int[] { helpful, totalHelpful }
            };

            return review;
        }

        private SentimentPrediction Sentiment(string review)
        {
            return Sentiments.Predict(review);
        }

        public static MarkovChain<string> GenerateData()
        {
            // Generate dataset 
            // Dataset courtesy of
            //      Ups and downs: Modeling the visual evolution of fashion trends with one-class collaborative filtering
            //      R.He, J.McAuley
            //      WWW, 2016
            string json = System.IO.File.ReadAllText("Datasets/Tools_and_Home_Improvement_5.json");
            var reviews = JsonConvert.DeserializeObject<List<RawReview>>(json);
            var chain = new MarkovChain<string>(1);

            foreach (var review in reviews)
            {
                chain.Add(review.ReviewText.Split(' '), 1);
            }

            return chain;
        }
    }
}