using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using RandomReviewGenerator.Controllers;
using RandomReviewGenerator.Services;
using RandomReviewSite.Options;
using RandomReviewSite.Pages;
using SentimentAnalyzer.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RandomReview.Tests
{
    public class APITests
    {
        #region Tests

        [Fact]
        public void GenerateReview_ReviewGeneratedSuccessfully()
        {
            var cacheService = new TestCacheService();

            var controller = new GenerateReviewService(cacheService);

            var result = controller.GenerateReview();

            Assert.NotNull(result);
            Assert.True(!string.IsNullOrEmpty(result.summary));
        }

        [Theory]
        [MemberData(nameof(GetSentimentData))]
        public void GenerateReview_SentimentScoreCalculatesPositiveCorrectly(SentimentPrediction sentiment, int expectedScore)
        {
            int resultScore = GenerateReviewService.CalculateScore(sentiment);

            Assert.Equal(expectedScore, resultScore);
        }

        [Fact]
        public async void TestHttpClient()
        {
            var mockFactory = MockHttpClientFactory();
            var mockOptions = new Mock<IOptions<ApplicationOptions>>();
            var page = new IndexModel(mockFactory.Object, mockOptions.Object);

            var result = await page.OnPostGenerateReview();

            Assert.NotNull(result);
        }

        #endregion

        #region Data

        private static List<object[]> GetSentimentData()
        {
            return new List<object[]>()
            {
                new object[] { new SentimentPrediction() { Prediction = true, Score = 5 }, 5 },
                new object[] { new SentimentPrediction() { Prediction = true, Score = (float)0.1 }, 3 },
                new object[] { new SentimentPrediction() { Prediction = true, Score = 3 }, 4 },
                new object[] { new SentimentPrediction() { Prediction = true, Score = (float)324587.143987 }, 5 },
                new object[] { new SentimentPrediction() { Prediction = false, Score = -10 }, 1 },
                new object[] { new SentimentPrediction() { Prediction = false, Score = -1 }, 2 },
                new object[] { new SentimentPrediction() { Prediction = false, Score = (float)-5.24592 }, 2 },
                new object[] { new SentimentPrediction() { Prediction = false, Score = (float)-512345.24592 }, 1 }
            };
        }

        private Mock<IHttpClientFactory> MockHttpClientFactory()
        {
            var mockFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"username\":\"botman3000\",\"text\":\"This is a generated review for unit testing\"}"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            return mockFactory;
        }

        #endregion

    }
}