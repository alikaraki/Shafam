﻿using System;
using System.Collections.Generic;
using System.Linq;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;

namespace Shafam.DataAccess.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IShafamDataContext _dataContext;

        public AppointmentRepository(IShafamDataContext dataContext)
        {
            _dataContext = dataContext;
        }
         
        public Appointment AddAppointment(int patientId, int doctorId, DateTime dateTime, string reason = null)
        {
            var appointment = new Appointment
                             {
                                 DoctorId = doctorId,
                                 PatientId = patientId,
                                 DateTime = dateTime,
                                 Reason = reason
                             };
            
            _dataContext.Appointments.Add(appointment);
            _dataContext.Save();

            return appointment;
        }

        public void CancelAppointment(int appointmentId)
        {
            Appointment appointment = GetAppointment(appointmentId);
            _dataContext.Appointments.Remove(appointment);
            _dataContext.Save();
        }

        public List<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            return _dataContext.Appointments.Where(a => a.DoctorId == doctorId).ToList();
        }

        public List<Appointment> GetAppointmentsForPatient(int patientId)
        {
            return _dataContext.Appointments.Where(a => a.PatientId == patientId).ToList();
        }


        public Appointment GetAppointment(int appointmentId)
        {
            return _dataContext.Appointments.First(a => a.AppointmentId == appointmentId);
        }
    }
}