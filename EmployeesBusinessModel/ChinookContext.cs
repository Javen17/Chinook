using Chinook.BusinessModel.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesBusinessModel
{
    public class ChinookContext:DbContext
    {
        public DbSet<Album> Album { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        public DbSet<Song> Song { get; set; }

        public ChinookContext(DbContextOptions<ChinookContext> options)
            : base(options)
        {

        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasMany(g => g.Songs).WithOne(s => s.Genre);
            modelBuilder.Entity<Album>().HasMany(a => a.Songs).WithOne(s => s.Album);
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=ChinookDemo;MultipleActiveResultSets=True;Pooling=True;Min Pool Size=25;Max Pool Size=250;Application Name=Chinook;Trusted_Connection=True");
            }
        }

        
    }
}
