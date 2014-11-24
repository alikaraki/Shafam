using System.Collections.Generic;
using Shafam.Common.DataModel;
using System;

namespace Shafam.BusinessLogic
{
    public interface ISchedulingService
    {
        void AddAppointment(int patientId, int doctorId, DateTime DateTime, string reason);

        int ModifyAppointment(int appointmentId, Appointment appointment);

        List<Appointment> ViewDoctorSchedule(int doctorId);

        List<Appointment> ViewPatientSchedule(int patientId);

        Doctor GetDoctorForAppointment(int appointmentId);

        Patient GetPatientForAppointment(int appointmentId);

        List<Doctor> GetDoctorsForAppointments(List<Appointment> appointments);

        int RequestAppointment(int patientId, int? doctorId, string reason = null);
    }
}