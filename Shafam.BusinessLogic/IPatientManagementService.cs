using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.BusinessLogic
{
    public interface IPatientManagementService
    {
        Patient ViewPatient(int patientId);

        List<Patient> ViewAllPatients(int doctorId);

        IEnumerable<Patient> ViewPatientsForStaff(int staffId);
            
        Patient AddPatient(string firstName, string lastName, int age, Gender gender, string healthCardNumber, string phoneNumber, string address);

        Patient AddPatient(int doctorId, string firstName, string lastName, int age, Gender gender, string healthCardNumber, string phoneNumber, string address);

        Patient AddPatient(Patient patient, int doctorId);

        void AssignDoctorToPatient(int doctorId, int patientId);
        
        bool ReferPatient(int patientId, int referringDocId, int referredDocId);
    }
}