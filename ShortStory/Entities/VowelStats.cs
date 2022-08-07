namespace ShortStory.Entities
{
    public class VowelStats
    {
        public Guid Id { get; set; }
        public int SingleVowelCount { get; set; }
        public int PairVowelCount { get; set; }
        public int TotalCount { get; set; }
        public DateTime Created { get; set; }
    }
}
