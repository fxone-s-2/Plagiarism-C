using Microsoft.EntityFrameworkCore;

namespace Plagiarism_C.Models
{
    public class PlagiarismContext : DbContext
    {
        public PlagiarismContext(DbContextOptions<PlagiarismContext> options)
    : base(options)
        {
        }

        public DbSet<PlagiarismItem> PlagiarismItems { get; set; }
    }
}
