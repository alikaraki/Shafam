using System.Collections.Generic;
using System.Data.Entity;
using Shafam.Common.DataModel;

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

        protected override void Seed(ShafamDataContext context)
        {
            var defaultUsers = new List<Account>
                               {
                                   new Account {Username = "Doctor", Password = "doctor", Role = UserRole.Doctor},
                                   new Account {Username = "Patient", Password = "patient", Role = UserRole.Patient},
                                   new Account {Username = "Staff", Password = "staff", Role = UserRole.Staff},
                                   new Account {Username = "IT", Password = "it", Role = UserRole.IT},
                                   new Account {Username = "Legal", Password = "legal", Role = UserRole.Legal},
                                   new Account {Username = "Finance", Password = "finance", Role = UserRole.Finance},
                               };

            defaultUsers.ForEach(u => context.Accounts.Add(u));
            context.SaveChanges();
        }
    }
}