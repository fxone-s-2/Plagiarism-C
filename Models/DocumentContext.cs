using Microsoft.EntityFrameworkCore;

namespace Plagiarism_C.Models
{
    public class DocumentContext : DbContext
    {
        public DocumentContext(DbContextOptions<DocumentContext> options)
    : base(options)
        {
        }

        public DbSet<DocumentItem> DocumentItems { get; set; }
    }
}
