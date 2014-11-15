using System.Data.Entity;

namespace Shafam.DataAccess.Infrastructure
{
    public class ShafamDropCreateAlwaysInitializer : DropCreateDatabaseAlways<ShafamDataContext>, IDatabaseInitializer
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
