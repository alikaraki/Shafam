using System.Data.Entity;
using Shafam.DataAccess.Migrations;

namespace Shafam.DataAccess.Infrastructure
{
    public class ShafamMigrateDatabaseInitializer : MigrateDatabaseToLatestVersion<ShafamDataContext, ShafamMigrationConfiguration>, IDatabaseInitializer
    {
        public void Initialize()
        {
            Database.SetInitializer(this);

            using (var context = new ShafamDataContext())
            {
                context.Database.Initialize(false);
            }
        }
    }
}
