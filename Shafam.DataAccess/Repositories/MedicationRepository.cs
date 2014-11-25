using System.Collections.Generic;
using System.Linq;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;
using System;

namespace Shafam.DataAccess.Repositories
{
    public class MedicationRepository : IMedicationRepository
    {
        private readonly IShafamDataContext _dataContext;

        public MedicationRepository(IShafamDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Medication AddMedication(int visitationId, string name, string quantity, string instructions)
        {
            Visitation visitation = _dataContext.Visitations.First(v => v.VisitationId == visitationId);

            var medication = new Medication
                             {
                                 VisitationId = visitationId,
                                 DoctorId = visitation.DoctorId,
                                 PatientId = visitation.PatientId,
                                 Name = name,
                                 Quantity = quantity,
                                 Instructions = instructions
                             };

            _dataContext.Medications.Add(medication);
            _dataContext.Save();

            return medication;
        }

        public List<Medication> GetMedicationsForPatient(int patientId)
        {
            return _dataContext.Medications.Where(m => m.PatientId == patientId).ToList();
        }

        public List<Medication> GetMedicationsForVisitation(int visitationId)
        {
            return _dataContext.Medications.Where(m => m.VisitationId == visitationId).ToList();
        }

        public List<Medication> GetMedicationsForDoctor(int doctorId)
        {
            return _dataContext.Medications.Where(m => m.DoctorId == doctorId).ToList();
        }

        public List<Medication> GetMedicationsForTime(DateTime begin, DateTime end)
        {
            List<Visitation> visitations = _dataContext.Visitations.Where(v => v.DateTime >= begin && v.DateTime <= end).ToList();
            List<Medication> medications = new List<Medication>();
            foreach (Visitation visitation in visitations)
            {
                int visitationId = visitation.VisitationId;
                List<Medication> visitationMedications = _dataContext.Medications.Where(m => m.VisitationId == visitationId).ToList();
                foreach (Medication visitationMedication in visitationMedications)
                {
                    medications.Add(visitationMedication);
                }
            }
            return medications;
        }
    }
}