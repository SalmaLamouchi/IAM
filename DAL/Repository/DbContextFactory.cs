using DAL.Entities;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DAL.Repository
{
    public class DbContextFactory : IDbContextFactory, IDisposable
    {
        public DbContextFactory(IOptions<DbContextSettings> settings)
        {
            // Utilisez la chaîne de connexion "CarteDbConnection"
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                            .UseNpgsql(settings.Value.AuthDbConnection) // Assurez-vous d'utiliser le bon champ (CarteDbConnection)
                            .EnableSensitiveDataLogging()
                            .Options;


            DbContext = new AuthDbContext(options);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public AuthDbContext DbContext { get; private set; }

        ~DbContextFactory()
        {
            Dispose();
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}

