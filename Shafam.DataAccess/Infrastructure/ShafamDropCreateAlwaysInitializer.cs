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
            var defaultUsers = new List<User>
                               {
                                   new User {Username = "Doctor", Password = "doctor", Role = UserRole.Doctor},
                                   new User {Username = "Patient", Password = "patient", Role = UserRole.Patient},
                                   new User {Username = "Staff", Password = "staff", Role = UserRole.Staff},
                                   new User {Username = "IT", Password = "it", Role = UserRole.IT},
                                   new User {Username = "Legal", Password = "legal", Role = UserRole.Legal},
                                   new User {Username = "Finance", Password = "finance", Role = UserRole.Finance},
                               };

            defaultUsers.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
        }
    }
}