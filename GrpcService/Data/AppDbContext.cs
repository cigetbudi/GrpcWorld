using GrpcService.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Siswa>().HasData(
                GetSiswas()
                );
        }

        public DbSet<Siswa> Siswas { get; set; }

        private static List<Siswa> GetSiswas()
        {
            List<Siswa> siswas = new List<Siswa>() {
              new Siswa() {    // 1
                SiswaId=11,
                NamaDepan="Ann",
                NamaBel="Fox",
                Sekolah="Nursing",
              },
              new Siswa() {    // 2
                SiswaId=22,
                NamaDepan="Sam",
                NamaBel="Doe",
                Sekolah="Mining",
              },
              new Siswa() {    // 3
                SiswaId=33,
                NamaDepan="Sue",
                NamaBel="Cox",
                Sekolah="Business",
              },
              new Siswa() {    // 4
                SiswaId=44,
                NamaDepan="Tim",
                NamaBel="Lee",
                Sekolah="Computing",
              },
              new Siswa() {    // 5
                SiswaId=55,
                NamaDepan="Jan",
                NamaBel="Ray",
                Sekolah="Computing",
              },
            };

            return siswas;
        }
    }
}
