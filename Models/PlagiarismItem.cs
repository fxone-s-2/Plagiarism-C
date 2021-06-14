using System;

namespace Plagiarism_C.Models
{
    public class PlagiarismItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public string Secret { get; set; }
    }

    // Over-Posting
    public class PlagiarismItemDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
    }
}
