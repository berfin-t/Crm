using System.Threading.Tasks;

namespace Crm.Data;

public interface ICrmDbSchemaMigrator
{
    Task MigrateAsync();
}
