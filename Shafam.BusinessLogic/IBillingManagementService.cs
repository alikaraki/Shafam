using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;

namespace Shafam.BusinessLogic
{
    public interface IBillingManagementService
    {
        // For generate doctor bill:
        int GetNumberOfVisitationsForDoctor(int doctorId);

        List<Treatment> GetTreatmentsForDoctor(int doctorId);

        List<Test> GetTestsForDoctor(int doctorId);

        // For generate patient bill:
        int GetNumberOfVisitationsForPatient(int patientId);

        List<Treatment> GetTreatmentsForPatient(int patientId);

        List<Test> GetTestsForPatient(int patientId); 

        // For generate time period bill:
        int GetNumberOfVisitationForTime(DateTime begin, DateTime end);

        List<Treatment> GetTreatmentsForTime(DateTime begin, DateTime end);

        List<Test> GetTestsForTime(DateTime begin, DateTime end);

    }
}
