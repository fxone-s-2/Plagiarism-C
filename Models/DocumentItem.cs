using System;

namespace Plagiarism_C.Models
{
    public class DocumentItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
    }

   // Over-Posting
   // public class PlagiarismItemDTO
   // {
   //     public Guid Id { get; set; }
   //     public string Text { get; set; }
   //     public int Score { get; set; }
   //}
}
