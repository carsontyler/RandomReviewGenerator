namespace RandomReviewGenerator
{
    public class Review
    {
        public int stars { get; set; }
        public string? text { get; set; }
        public string? username { get; set; }
        public DateTime timestamp { get; set; }
        public string? summary { get; set; }
        public int[]? helpful { get; set; }
    }

    public class RawReview
    {
        public string? reviewerID { get; set; }
        public string? ASIN { get; set; }
        public string? ReviewerName { get; set; }
        public int[]? Helpful { get; set; }
        public string? ReviewText { get; set; }
        public double? Overall { get; set; }
        public string? Summary { get; set; }
        public long? UnixReviewTime { get; set; }
        public DateTime? ReviewTime { get; set; }
    }
}