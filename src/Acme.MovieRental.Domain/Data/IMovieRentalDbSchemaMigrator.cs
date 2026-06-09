using System.Threading.Tasks;

namespace Acme.MovieRental.Data;

public interface IMovieRentalDbSchemaMigrator
{
    Task MigrateAsync();
}
