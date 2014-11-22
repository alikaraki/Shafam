using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;

namespace Shafam.DataAccess.Migrations
{
    public sealed class ShafamMigrationConfiguration : DbMigrationsConfiguration<ShafamDataContext>
    {
        public ShafamMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
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

            defaultUsers.ForEach(u => context.Accounts.AddOrUpdate(us => us.Username, u));

            var doctor1 = new Doctor
            {
                FirstName = "John",
                LastName = "Smith",
                Gender = Gender.Male,
                Specialty = "Family doctor"
            };

            var doctor2 = new Doctor
            {
                FirstName = "Amy",
                LastName = "Montrose",
                Gender = Gender.Female,
                Specialty = "Neurologist"
            };

            context.Doctors.AddOrUpdate(d => d.FirstName, doctor1);
            context.Doctors.AddOrUpdate(d => d.FirstName, doctor2);

            var doctor1Account = new Account { Username = "John", Password = "john", Role = UserRole.Doctor, UserId = doctor1.DoctorId };
            var doctor2Account = new Account { Username = "Amy", Password = "amy", Role = UserRole.Doctor, UserId = doctor2.DoctorId };

            context.Accounts.AddOrUpdate(a => a.Username, doctor1Account);
            context.Accounts.AddOrUpdate(a => a.Username, doctor2Account);

            var patients = new List<Patient>()
                                  {
                                      new Patient {FirstName = "Harold", LastName = "Truett", Gender = Gender.Male, Age = 20},
                                      new Patient {FirstName = "Elinor", LastName = "Rosenow", Gender = Gender.Female, Age = 63},
                                      new Patient {FirstName = "Meta", LastName = "Tutor", Gender = Gender.Female, Age = 14},
                                      new Patient {FirstName = "Derick", LastName = "Ranallo", Gender = Gender.Male, Age = 41},
                                      new Patient {FirstName = "Flossie", LastName = "Brase", Gender = Gender.Female, Age = 27},
                                      new Patient {FirstName = "Kaylene", LastName = "Pease", Gender = Gender.Female, Age = 32},
                                      new Patient {FirstName = "Jarrod", LastName = "Kenley", Gender = Gender.Male, Age = 71},
                                  };

            patients.ForEach(p => context.Patients.AddOrUpdate(pt => pt.FirstName, p));

            context.SaveChanges();

            AddPatient(doctor1, patients[0]);
            AddPatient(doctor1, patients[2]);
            AddPatient(doctor1, patients[3]);
            AddPatient(doctor1, patients[6]);

            AddPatient(doctor2, patients[1]);
            AddPatient(doctor2, patients[2]);
            AddPatient(doctor2, patients[3]);
            AddPatient(doctor2, patients[4]);
            AddPatient(doctor2, patients[5]);

            context.SaveChanges();
        }

        private void AddPatient(Doctor doctor, Patient patient)
        {
            if (doctor.Patients.FirstOrDefault(p => p.PatientId == patient.PatientId) == null)
            {
                doctor.Patients.Add(patient);
            }
        }
    }
}