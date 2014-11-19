using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;
using System.Threading.Tasks;

namespace Shafam.DataAccess.Repositories
{
    public class VisitationRepository : IVisitationRepository
    {
        private readonly IShafamDataContext _dataContext;
        public VisitationRepository(IShafamDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Visitation CreateVisitation(int doctorId, int patientId, DateTime dateTime, string reason, string notes = null)
        {
            Patient patient = _dataContext.Patients.First(p => p.PatientId == patientId);
            Appointment appointment = _dataContext.Appointments.First(a => a.PatientId == patientId);

            var visitation = new Visitation
                            {
                                DoctorId = doctorId,
                                PatientId = patientId,
                                DateTime = dateTime,
                                Reason = reason,
                                Notes = notes,
                                AppointmentId = appointment.AppointmentId,
                                Medications = patient.Medications,
                                Tests = patient.Tests,
                                Treatments = patient.Treatments
                            };
            _dataContext.Visitations.Add(visitation);
            _dataContext.Save();

            return visitation;
        }

        public Visitation GetVisitation(int visitationId)
        {
            return _dataContext.Visitations.First(v => v.VisitationId == visitationId);
        }

        public IEnumerable<Visitation> GetVisitationsForPatient(int patientId)
        {
            return _dataContext.Visitations.Where(v => v.PatientId == patientId).ToList();
        }

        public IEnumerable<Visitation> GetVisitationsForDoctor(int doctorId)
        {
            return _dataContext.Visitations.Where(v => v.DoctorId == doctorId).ToList();
        }
    }
}
