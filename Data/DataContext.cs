
namespace projekt_programowanie.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Song> Songs => Set<Song>();
    }
}
