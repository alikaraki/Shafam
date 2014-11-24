using System;
using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IVisitationRepository
    {
        Visitation CreateVisitation(int doctorId, int patientId, DateTime dateTime, string reason, string notes = null);

        Visitation GetVisitation(int visitationId);

        List<Visitation> GetVisitationsForPatient(int patientId);

        List<Visitation> GetVisitationsForDoctor(int doctorId);

        List<Visitation> GetVisitationsForTime(DateTime begin, DateTime end);

    }
}