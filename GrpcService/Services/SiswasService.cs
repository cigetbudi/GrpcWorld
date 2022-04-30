using Grpc.Core;
using GrpcService.Data;
using GrpcService.Protos;

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

            if (siswa != null)
            {
                model.SiswaId = siswa.SiswaId;
                model.NamaDepan = siswa.NamaDepan;
                model.NamaBel = siswa.NamaBel;
                model.Sekolah = siswa.Sekolah;

            }

            return Task.FromResult(model);

        }
    }
}
