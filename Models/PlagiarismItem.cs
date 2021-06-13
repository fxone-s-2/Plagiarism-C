namespace Plagiarism_C.Models
{
    public class PlagiarismItem
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public string Secret { get; set; }
    }

    // Over-Posting
    public class PlagiarismItemDTO
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
    }
}
