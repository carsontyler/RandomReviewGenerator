using Markov;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RandomReviewGenerator.Services.Interfaces;
using SentimentAnalyzer;
using SentimentAnalyzer.Models;

namespace RandomReviewGenerator.Services;

public class GenerateReviewService : IGenerateReviewService
{
    private readonly IMemoryCache _cache;

    public GenerateReviewService(IMemoryCache memoryCache)
    {
        _cache = memoryCache;
    }

    public Review GenerateReview()
    {
        // Retrieve the trained data from cache
        string key = "markovChain";
        if (!_cache.TryGetValue<MarkovChain<string>>(key, out var chain) || chain == null)
            throw new Exception("Markov Chain not retrieved from cache, ensure the data set file is included and has valid data");

        // Generate the random review text
        var rand = new Random();
        var generatedReview = string.Join(" ", chain.Chain(rand));

        // Predict the sentiment, which assigns a score
        var sentiment = Sentiments.Predict(generatedReview);

        // Calculate the sentiment score
        var score = CalculateScore(sentiment);

        // Create the new Review and return it.
        int totalHelpful = rand.Next(0, 100);
        int helpful = rand.Next(0, totalHelpful);

        // Construct the Review
        var review = new Review()
        {
            text = generatedReview,
            timestamp = DateTime.Now,
            username = "botman3000",
            stars = score,
            summary = "My thoughts on this product",
            helpful = new int[] { helpful, totalHelpful }
        };

        return review;
    }

    /// <summary>
    /// Calculate the whole number of stars from the sentiment score. 
    /// First, if `sentiment.Prediction` is true, I assume the sentiment is positive.
    ///     Then, I multiply that score by 0.5 and add it to 2.5. 
    ///     When looking at raw sentiment scores, I roughly determined this gave the best distribution for positive scores. 
    /// If `sentiment.Prediction` is false, I assume the sentiment is negative. 
    ///     Then, I multiply that score by 0.125 and subtract it from 2.5. 
    ///     I multiply by a negative because the score for a negative sentiment is less than 0. 
    ///     Again, I roughly determined this gave the best distribution for negative scores. 
    ///     
    /// Lastly, I round the calculated score to the nearest whole number between 1 and 5. 
    /// </summary>
    /// <param name="sentiment">The sentiment for a generated review.</param>
    /// <returns>A whole number between 1 and 5 that represents the number of stars for a review.</returns>
    public static int CalculateScore(SentimentPrediction sentiment)
    {
        double score;

        if (sentiment.Prediction)
            score = 2.5 + sentiment.Score * 0.5;
        else
            score = 2.5 - sentiment.Score * -0.125;

        if (score > 5) score = 5;
        else if (score < 1) score = 1;
        else score = Math.Round(score, 0);

        return (int)score;
    }

    /// <summary>
    /// Read in the dataset and generate the MarkovChain object from the data set.
    /// </summary>
    /// <returns>The trained MarkovChain</returns>
    public static MarkovChain<string> GenerateData()
    {
        // Dataset courtesy of
        //      Ups and downs: Modeling the visual evolution of fashion trends with one-class collaborative filtering
        //      R.He, J.McAuley
        //      WWW, 2016
        string filepath = "Datasets/dataset.json";

        if (!File.Exists(filepath))
            throw new Exception(@"ERROR: File does not exist. Ensure a valid JSON file exists at `\Datasets\dataset.json`");

        string json = File.ReadAllText(filepath);
        var reviews = JsonConvert.DeserializeObject<List<RawReview>>(json) ?? new List<RawReview>();

        if (reviews.Count == 0)
            throw new Exception("ERROR: data set is empty, the MarkovChain cannot be trained.");

        var chain = new MarkovChain<string>(1);

        // Train the object by adding in a string array constructed from each word in a review.
        foreach (var review in reviews)
            chain.Add(review.ReviewText?.Split(' '), 1);

        return chain;
    }
}