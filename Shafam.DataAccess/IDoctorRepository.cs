﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IDoctorRepository
    {
        void AddDoctor(Doctor doctor);

        List<Doctor> GetDoctors();

        Doctor GetDoctor(int doctorId);

        IEnumerable<Doctor> GetDoctorsInDepartment(Department department);

        void UpdateDoctor(Doctor doctor);

        void DeleteDoctor(int doctorId);

        void AssignPatient(int doctorId, int patientId);

        List<Patient> GetPatientsForDoctor(int doctorId);

        List<Appointment> GetAppointmentsForDoctor(int doctorId);

    }
}