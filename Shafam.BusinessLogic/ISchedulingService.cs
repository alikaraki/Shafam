using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.BusinessLogic
{
    public interface ISchedulingService
    {
        int AddAppointment(Appointment appointment);

        int ModifyAppointment(int appointmentId, Appointment appointment);

        List<Appointment> ViewDoctorSchedule(int doctorId);

        List<Appointment> ViewPatientSchedule(int patientId);

        int RequestAppointment(int patientId, int? doctorId, string reason = null);
    }
}