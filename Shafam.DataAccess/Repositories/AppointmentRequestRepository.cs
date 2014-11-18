using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;

namespace Shafam.DataAccess.Repositories
{
    public class AppointmentRequestRepository : IAppointmentRequestRepository
    {
        private readonly IShafamDataContext _dataContext;

        public AppointmentRequestRepository(IShafamDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public AppointmentRequest RequestAppointment(int patientId, int? doctorId = null, string reason = null)
        {
            var appointmentRequest = new AppointmentRequest
            {
                DoctorId = doctorId,
                PatientId = patientId,
                Reason = reason
            };

            _dataContext.AppointmentRequests.Add(appointmentRequest);
            _dataContext.Save();

            return appointmentRequest;
        }

        public void MarkAsSeen(int appointmentRequestId)
        {
            AppointmentRequest appointmentRequest = GetAppointmentRequest(appointmentRequestId);
            appointmentRequest.SeenByStaff = true;
            _dataContext.Save();
        }

        public void DeleteRequest(int appointmentRequestId)
        {
            AppointmentRequest appointmentRequest = GetAppointmentRequest(appointmentRequestId);
            _dataContext.AppointmentRequests.Remove(appointmentRequest);
            _dataContext.Save();
        }

        public AppointmentRequest GetAppointmentRequest(int appointmentRequestId)
        {
            return _dataContext.AppointmentRequests.First(ar => ar.AppointmentRequestId == appointmentRequestId);
        }

        public List<AppointmentRequest> GetAppointmentRequestsForPatient(int patientId)
        {
            return _dataContext.AppointmentRequests.Where(ar => ar.PatientId == patientId).ToList();
        }

        public List<AppointmentRequest> GetAppointmentRequestsForDoctor(int doctorId)
        {
            return _dataContext.AppointmentRequests.Where(ar => ar.DoctorId == doctorId).ToList();
        }
    }
}
