using System;
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
                Specialty = "Surgeant",
                Department = Department.Neurology
            };

            var doctor2 = new Doctor
            {
                FirstName = "Amy",
                LastName = "Montrose",
                Gender = Gender.Female,
                Specialty = "Neurologist",
                Department = Department.Surgery
            };

            doctor1 = AddOrUpdate(context, doctor1);
            doctor2 = AddOrUpdate(context, doctor2);

            var staff1 = new Staff {FirstName = "Michael", Department = Department.Neurology};
            var staff2 = new Staff {FirstName = "Sara", Department = Department.Surgery};

            if (!context.Staffs.Any(s => s.FirstName.Equals(staff1.FirstName) && s.Department == staff1.Department))
            {
                context.Staffs.AddOrUpdate(s => s.FirstName, staff1);
            }
            else
            {
                staff1 = context.Staffs.First(s => s.FirstName.Equals(staff1.FirstName) && s.Department == staff1.Department);
            }

            if (!context.Staffs.Any(s => s.FirstName.Equals(staff2.FirstName) && s.Department == staff2.Department))
            {
                context.Staffs.AddOrUpdate(s => s.FirstName, staff2);
            }
            else
            {
                staff2 = context.Staffs.First(s => s.FirstName.Equals(staff2.FirstName) && s.Department == staff2.Department);
            }

            context.SaveChanges();

            var doctor1Account = new Account { Username = "John", Password = "john", Role = UserRole.Doctor, UserId = doctor1.DoctorId };
            var doctor2Account = new Account { Username = "Amy", Password = "amy", Role = UserRole.Doctor, UserId = doctor2.DoctorId };
            var staff1Account = new Account { Username = "Michael", Password = "michael", Role = UserRole.Staff, UserId = staff1.StaffId};
            var staff2Account = new Account { Username = "Sara", Password = "sara", Role = UserRole.Staff, UserId = staff2.StaffId};

            context.Accounts.AddOrUpdate(a => a.Username, doctor1Account);
            context.Accounts.AddOrUpdate(a => a.Username, doctor2Account);
            context.Accounts.AddOrUpdate(a => a.Username, staff1Account);
            context.Accounts.AddOrUpdate(a => a.Username, staff2Account);

            context.SaveChanges();

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

            patients.ForEach(p =>
            {
                if (!context.Patients.Any(ps => ps.FirstName.Equals(p.FirstName, StringComparison.InvariantCultureIgnoreCase)))
                    context.Patients.AddOrUpdate(pt => pt.FirstName, p);
            });

            context.SaveChanges();

            patients.ForEach(p =>
            {
                if (!context.Accounts.Any(a => a.Username.Equals(p.FirstName, StringComparison.InvariantCultureIgnoreCase)))
                { 
                    Patient patient = context.Patients.First(pt => pt.FirstName == p.FirstName && pt.LastName == p.LastName);
                    Account account = new Account { Username = patient.FirstName.ToLower(), Password = patient.FirstName.ToLower(), Role = UserRole.Patient, UserId = patient.PatientId };
                    context.Accounts.AddOrUpdate(a => a.Username, account);
                }
            });

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
            if (doctor.Patients.FirstOrDefault(p => p.FirstName == patient.FirstName && p.LastName == patient.LastName) == null)
            {
                doctor.Patients.Add(patient);
            }
        }

        private Doctor AddOrUpdate(ShafamDataContext context, Doctor doctor)
        {
            Doctor existing = context.Doctors.FirstOrDefault(d => d.FirstName == doctor.FirstName && d.LastName == doctor.LastName);

            if (existing != null)
            {
                existing.Department = doctor.Department;
                existing.Specialty = doctor.Specialty;
            }
            else
            {
                context.Doctors.Add(doctor);
            }

            context.SaveChanges();

            return context.Doctors.FirstOrDefault(d => d.FirstName == doctor.FirstName && d.LastName == doctor.LastName);
        }
    }
}