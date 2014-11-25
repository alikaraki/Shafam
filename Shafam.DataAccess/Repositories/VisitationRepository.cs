using System;
using System.Collections.Generic;
using System.Linq;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;

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
            var visitation = new Visitation
                            {
                                DoctorId = doctorId,
                                PatientId = patientId,
                                DateTime = dateTime,
                                Reason = reason,
                                Notes = notes
                            };

            _dataContext.Visitations.Add(visitation);
            _dataContext.Save();

            return visitation;
        }

        public Visitation GetVisitation(int visitationId)
        {
            return _dataContext.Visitations.First(v => v.VisitationId == visitationId);
        }

        public List<Visitation> GetVisitationsForPatient(int patientId)
        {
            return _dataContext.Visitations.Where(v => v.PatientId == patientId).ToList();
        }

        public List<Visitation> GetVisitationsForDoctor(int doctorId)
        {
            return _dataContext.Visitations.Where(v => v.DoctorId == doctorId).ToList();
        }

        public List<Visitation> GetVisitationsforPatientWithDoctor(int patientId, int doctorId)
        {
            return _dataContext.Visitations.Where(v => v.PatientId == patientId && v.DoctorId == doctorId).ToList();
        }

        public List<Visitation> GetVisitationsForTime(DateTime begin, DateTime end)
        {
            return _dataContext.Visitations.Where(v => v.DateTime >= begin && v.DateTime <= end).ToList();
        }
    }
}