using Markov;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace RandomReview.Tests
{
    internal class TestCacheService : IMemoryCache
    {
        public ICacheEntry CreateEntry(object key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(object key, out object value)
        {
            if (key == "markovChain")
            {
                value = GenerateMarkovChain();
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        private MarkovChain<string> GenerateMarkovChain()
        {
            var chain = new MarkovChain<string>(1);
            chain.Add(_dataset, 1);
            return chain;
        }

        private readonly string[] _dataset = new string[] { "I", "hate", "it", "when", "my", "shirt", "collars,", "not", "otherwise", "secured", "in", "place", "by", "buttons,", "end", "up", "in", "weird", "places", "throughout", "the", "day.", "I", "purchased", "some", "steel", "collar", "stays", "to", "use", "with", "these", "magnets", "but", "they", "were", "only", "vaguely", "magnetic.", "I", "ended", "up", "using", "2", "of", "these", "magnets", "-", "one", "in", "the", "collar", "with", "the", "stay", "and", "the", "other", "inside", "my", "shirt,", "to", "lock", "my", "collar", "in", "place.", "They", "work", "flawlessly.", "They", "are", "the", "perfect", "size,", "and", "there", "are", "plenty", "of", "magnets", "in", "case", "you", "forget", "to", "remove", "them", "at", "the", "end", "of", "the", "day.", "These", "little", "magnets", "are", "really", "powerful", "for", "there", "size.", "I", "am", "using", "them", "to", "make", "secret", "compartments", "in", "custom", "made", "boxes.", "Each", "one", "hols", "about", ".8.", "I", "wanted", "something", "this", "small", "to", "mount", "on", "the", "back", "of", "filagree", "wood", "piece", "I", "cut.", "They", "could", "then", "be", "mounted", "on", "refrigerators.", "Works", "well.", "Should", "be", "able", "to", "remove", "the", "pieces", "from", "the", "refrig", "without", "breaking", "yet", "will", "hold", "well.", "I", "use", "these", "to", "magnetize", "my", "Warhammer", "40K", "miniatures", "together", "allowing", "me", "to", "swap", "out", "their", "various", "parts", "and", "weapons.", "", "They", "provide", "excellent", "holding", "power", "along", "with", "small", "size", "to", "fit", "in", "snug", "areas.", "They", "are", "soo", "freaking", "annoying!!", "", "Why?!", "You", "spend", "all", "this", "time,", "da*n", "near", "breaking", "off", "your", "fingernail", "trying", "to", "separate", "these", "li'l", "buggers,", "and", "when", "you", "do", "finally", "get", "them", "apart,", "you", "accidentally", "hold", "your", "hand", "in", "a", "certain", "way", "or", "angle", "and", "they", "snap", "right", "back", "together", "again", "with", "reckless", "abandon!", "", "So", "yes,", "annoying,", "but", "that's", "only", "because", "they", "are", "soo", "good", "at", "what", "they", "do.", "", "You", "too", "will", "be", "happily", "annoyed", "with", "your", "purchase.", "", "I'd", "stick", "my", "reputation", "on", "that", "statement...", "with", "these", "magnets!", "am", "using", "for", "40k", "models,", "they", "are", "a", "great", "size", "for", "adding", "jet", "packs,", "i", "use", "them", "for", "both", "orks", "and", "space", "marines,", "my", "stormboyz", "just", "use", "death", "company", "jet", "packs", "and", "my", "death", "company", "are", "also", "assault", "for", "codex", "marines", "or", "other", "codexes,", "have", "also", "use", "dhtem", "for", "eldar", "guardian", "weapons", "platform,", "will", "buy", "again", "when", "i", "run", "out", "they", "work", "for", "all", "kinds", "of", "customizations", "and", "these", "are", "small", "enough", "that", "it", "doens't", "break", "superglue", "over", "time", "(1/4'", "and", "above", "do)", "also", "note", "glue", "on", "the", "magnets", "before", "you", "prime", "or", "they", "will", "also", "pull", "off", "you", "want", "superglue", "from", "plastic", "to", "magnet", "for", "best", "results", "then", "paint", "over", "it.", "The", "color", "pictures", "and", "exploded", "diagrams", "are", "an", "outstanding", "introduction", "and", "knowledge", "builder.", "Mr.", "Proulx", "full", "color", "pages", "helped", "me", "solve", "problems", "and", "understand", "better", "ways", "to", "use", "this", "great", "joinery", "technique.", "Good", "simple", "projects", "to", "start", "you", "using", "the", "Kreg", "tool", "for", "joints.", "The", "book", "can", "take", "into", "the", "shop", "where", "you", "don't", "want", "to", "take", "a", "computer", "or", "tablet.", "If", "you", "have", "a", "pocket", "holl", "jig", "then", "this", "book", "is", "for", "you!", "There", "are", "some", "nice", "projects", "in", "this", "book", "and", "they", "are", "simple", "enough", "even", "I", "could." };
    }
}
