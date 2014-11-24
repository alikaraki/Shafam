using System;
using System.Collections.Generic;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.BusinessLogic.Scheduling
{
    public class SchedulingService : ISchedulingService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentRequestRepository _appointmentRequestRepository;

        public SchedulingService(IAppointmentRepository appointmentRepository, IAppointmentRequestRepository appointmentRequestRepository)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentRequestRepository = appointmentRequestRepository;
        }

        public void AddAppointment(int patientId, int doctorId, DateTime dateTime, string reason)
        {
            _appointmentRepository.AddAppointment(patientId, doctorId, dateTime, reason);
        }

        public int ModifyAppointment(int appointmentId, Appointment appointment)
        {
            _appointmentRepository.GetAppointment(appointmentId);
            _appointmentRepository.CancelAppointment(appointmentId);
            _appointmentRepository.AddAppointment(appointment.PatientId, appointment.DoctorId,
                                                    appointment.DateTime, appointment.Reason);
            return appointment.AppointmentId;
        }

        public List<Appointment> ViewDoctorSchedule(int doctorId)
        {
            return _appointmentRepository.GetAppointmentsForDoctor(doctorId);
        }

        public List<Appointment> ViewPatientSchedule(int patientId)
        {
            return _appointmentRepository.GetAppointmentsForPatient(patientId);
        }

        public int RequestAppointment(int patientId, int? doctorId = null, string reason = null)
        {
            AppointmentRequest appRequest = _appointmentRequestRepository.RequestAppointment(patientId, doctorId, reason);
            return appRequest.AppointmentRequestId;
        }

    }
}
