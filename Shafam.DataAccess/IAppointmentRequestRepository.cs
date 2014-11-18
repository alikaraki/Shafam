using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IAppointmentRequestRepository
    {
        AppointmentRequest RequestAppointment(int patientId, int? doctorId, string reason = null);

        void MarkAsSeen(int appointmentRequestId);

        void DeleteRequest(int appointmentRequestId);

        AppointmentRequest GetAppointmentRequest(int appointmentRequestId);

        List<AppointmentRequest> GetAppointmentRequestsForPatient(int patientId);

        List<AppointmentRequest> GetAppointmentRequestsForDoctor(int doctorId);
    }
}