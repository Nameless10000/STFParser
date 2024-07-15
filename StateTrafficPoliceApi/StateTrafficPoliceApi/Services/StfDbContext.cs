using Microsoft.EntityFrameworkCore;
using StateTrafficPoliceApi.DbEntities;

namespace StateTrafficPoliceApi.Services
{
    public class StfDbContext(DbContextOptions opts) : DbContext(opts)
    {
        public DbSet<StfDiagnosticCardResponse> DiagnosticCardResponses { get; set; }

        public DbSet<StfDriverLicenseResponse> StfDriverLicenseResponses { get; set; }

        public DbSet<StfDtpResponse> StfDtpResponses { get; set; }

        public DbSet<StfFinesResponse> StfFinesResponses { get; set; }

        public DbSet<StfHistoryResponse> StfHistoryResponses { get; set; }

        public DbSet<StfRestrictResponse> StfRestrictResponse { get; set; }

        public DbSet<StfWantedResponse> StfWantedResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
