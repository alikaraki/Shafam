using System;
using System.Linq;
using System.Collections.Generic;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.BusinessLogic.Scheduling
{
    public class SchedulingService : ISchedulingService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentRequestRepository _appointmentRequestRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public SchedulingService(IAppointmentRepository appointmentRepository, 
                                IAppointmentRequestRepository appointmentRequestRepository,
                                IDoctorRepository doctorRepository,
                                IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentRequestRepository = appointmentRequestRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
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



        public List<Doctor> GetDoctorsForAppointments(List<Appointment> appointments)
        {
            List<int> doctorIds = new List<int>();
            foreach (Appointment appointment in appointments) 
            {
                doctorIds.Add(appointment.DoctorId);
            }
            List<Doctor> doctors = doctorIds.Select(Id => _doctorRepository.GetDoctor(Id)).ToList();
            return doctors;
        }


        public Doctor GetDoctorForAppointment(int appointmentId)
        {
            Appointment appointment = _appointmentRepository.GetAppointment(appointmentId);
            int doctorId = appointment.DoctorId;
            return _doctorRepository.GetDoctor(doctorId);
        }


        public Patient GetPatientForAppointment(int appointmentId)
        {
            Appointment appointment = _appointmentRepository.GetAppointment(appointmentId);
            int patientId = appointment.PatientId;
            return _patientRepository.GetPatient(patientId);
        }
    }
}
