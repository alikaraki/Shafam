using System.Collections.Generic;
using Shafam.Common.DataModel;
using System;

namespace Shafam.DataAccess
{
    public interface ITreatmentRepository
    {
        Treatment AddTreatment(int visitationId, string treatmentType);

        List<Treatment> GetTreatmentsForPatient(int patientId);

        List<Treatment> GetTreatmentsForVisitation(int visitationId);

        List<Treatment> GetTreatmentsForDoctor(int doctorId);
     
        void MarkAsCompleted(int treatmentId);

        List<Treatment> GetTreatmentsForTime(DateTime begin, DateTime end);

     }
}
