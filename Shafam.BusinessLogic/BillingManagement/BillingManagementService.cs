﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.BusinessLogic.BillingManagement
{
    public class BillingManagementService : IBillingManagementService
    {
        private readonly IVisitationRepository _visitationRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly ITestRepository _testRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicationRepository _medicationRepositpry;

        public BillingManagementService(IVisitationRepository visitationRepository, 
                                ITreatmentRepository treatmentRepository,
                                ITestRepository testRepository,
                                IDoctorRepository doctorRepository,
                                IPatientRepository patientRepository,
                                IMedicationRepository medicationRepository)
        {
            _visitationRepository = visitationRepository;
            _treatmentRepository = treatmentRepository;
            _testRepository = testRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _medicationRepositpry = medicationRepository;
        }

        public int GetNumberOfVisitationsForDoctor(int doctorId)
        {
            List<Visitation> visitations = _visitationRepository.GetVisitationsForDoctor(doctorId);
            int size = visitations.Count;
            return size;
        }

        public List<Treatment> GetTreatmentsForDoctor(int doctorId)
        {
            return _treatmentRepository.GetTreatmentsForDoctor(doctorId);
        }

        public List<Test> GetTestsForDoctor(int doctorId)
        {
            return _testRepository.GetTestsForDoctor(doctorId);
        }

        public List<Medication> GetMedicationsForDoctor(int doctorId)
        {
            return _medicationRepositpry.GetMedicationsForDoctor(doctorId);
        }

        public int GetNumberOfVisitationsForPatient(int patientId)
        {
            List<Visitation> visitations = _visitationRepository.GetVisitationsForPatient(patientId);
            int size = visitations.Count;
            return size;
        }

        public List<Treatment> GetTreatmentsForPatient(int patientId)
        {
            return _treatmentRepository.GetTreatmentsForPatient(patientId);
        }

        public List<Test> GetTestsForPatient(int patientId)
        {
            return _testRepository.GetTestsForPatient(patientId);
        }

        public List<Medication> GetMedicationsForPatient(int patientId)
        {
            return _medicationRepositpry.GetMedicationsForPatient(patientId);
        }


        public int GetNumberOfVisitationForTime(DateTime begin, DateTime end)
        {
            List<Visitation> visitations = _visitationRepository.GetVisitationsForTime(begin, end);
            int size = visitations.Count;
            return (size);
        }

        public List<Treatment> GetTreatmentsForTime(DateTime begin, DateTime end)
        {
            return _treatmentRepository.GetTreatmentsForTime(begin, end);
        }

        public List<Test> GetTestsForTime(DateTime begin, DateTime end)
        {
            return _testRepository.GetTestsForTime(begin, end);
        }

        public List<Medication> GetMedicationsForTime(DateTime begin, DateTime end)
        {
            return _medicationRepositpry.GetMedicationsForTime(begin, end);
        }


    }
}
