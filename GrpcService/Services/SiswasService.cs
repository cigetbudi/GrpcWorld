using Grpc.Core;
using GrpcService.Data;
using GrpcService.Protos;
using GrpcService.Models;

namespace GrpcService.Services
{
    public class SiswasService : RemoteSiswas.RemoteSiswasBase
    {
        private readonly ILogger<SiswasService> _logger;
        private readonly AppDbContext _db;
        public SiswasService(ILogger<SiswasService> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public override Task<SiswaModel> GetSiswaInfo(SiswaLookupModel req, ServerCallContext context)
        {
            SiswaModel model = new SiswaModel();
            var siswa = _db.Siswas.Find(req.SiswaId);

            _logger.LogInformation("Sending siswa response");
            if (siswa == null)
            {
                _logger.LogError("Data tidak ditemukan");
            }
            else if (siswa != null)
            {
                model.SiswaId = siswa.SiswaId;
                model.NamaDepan = siswa.NamaDepan;
                model.NamaBel = siswa.NamaBel;
                model.Sekolah = siswa.Sekolah;

            }
            else
            {
                _logger.LogError("Gagal koneksi");
            }

            return Task.FromResult(model);

        }

        public override Task<Reply> AddSiswa(SiswaModel req, ServerCallContext context)
        {
            var s = _db.Siswas.Find(req.SiswaId);

            if (s != null)
            {
                return Task.FromResult(new Reply()
                {
                    Result = $"Siswa {req.NamaDepan} {req.NamaBel} sudah ada.",
                    IsOk = false
                });
            }

            Siswa siswa = new Siswa()
            {
                NamaDepan = req.NamaDepan,
                NamaBel = req.NamaBel,
                Sekolah = req.Sekolah,
            };

            _logger.LogInformation("Masukkan data siswa");

            try
            {
                _db.Siswas.Add(siswa);
                var returnVal = _db.SaveChanges();
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(new Reply()
            {
                Result = $"Siswa {req.NamaDepan} {req.NamaBel} berhasil ditambahkan.",
                IsOk = true
            });

        }

        public override Task<Reply> UpdateSiswa(SiswaModel req, ServerCallContext context)
        {
            var s = _db.Siswas.Find(req.SiswaId);

            if (s == null)
            {
                return Task.FromResult(
                  new Reply()
                  {
                      Result = $"Siswa {req.NamaDepan} {req.NamaBel} tidak ditemukan.",
                      IsOk = false
                  }
                );
            }

            s.NamaDepan = req.NamaDepan;
            s.NamaBel = req.NamaBel;
            s.Sekolah = req.Sekolah;

            _logger.LogInformation("Update student");

            try
            {
                var returnVal = _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(
               new Reply()
               {
                   Result = $"Student {req.NamaDepan} {req.NamaBel} berhasil dirubah.",
                   IsOk = true
               }
            );
        }
        public override Task<Reply> DeleteSiswa(SiswaLookupModel req, ServerCallContext context)
        {
            var s = _db.Siswas.Find(req.SiswaId);

            if (s == null)
            {
                return Task.FromResult(
                  new Reply()
                  {
                      Result = $"Siswa dengan ID {req.SiswaId} tidak dapat ditemukan.",
                      IsOk = false
                  }
                );
            }

            _logger.LogInformation("Hapus Siswa");

            try
            {
                _db.Siswas.Remove(s);
                var returnVal = _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(
               new Reply()
               {
                   Result = $"Siswa dengan ID {req.SiswaId} berhasil dihapus.",
                   IsOk = true
               }
            );
        }

        public override Task<SiswaList> GetAllSiswa(Empty req, ServerCallContext context)
        {
            _logger.LogInformation("Get all Siswa");

            SiswaList list = new SiswaList();

            try
            {
                List<SiswaModel> studentList = new List<SiswaModel>();

                var students = _db.Siswas.ToList();

                foreach (var c in students)
                {
                    studentList.Add(new SiswaModel()
                    {
                        SiswaId = c.SiswaId,
                        NamaDepan = c.NamaDepan,
                        NamaBel = c.NamaBel,
                        Sekolah = c.Sekolah,
                    });
                }

                list.Items.AddRange(studentList);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(list);
        }
    }
}
