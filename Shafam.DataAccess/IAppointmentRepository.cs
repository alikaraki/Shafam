using System;
using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IAppointmentRepository
    {
        Appointment AddAppointment(int patientId, int doctorId, DateTime dateTime, string reason = null);

        void CancelAppointment(int appointmentId);

        List<Appointment> GetAppointmentsForDoctor(int doctorId);

        List<Appointment> GetAppointmentsForPatient(int patientId);

        Appointment GetAppointment(int appointmentId);

        Doctor GetDoctorForAppointment(int doctorId);
    }
}