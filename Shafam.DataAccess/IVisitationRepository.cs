using System;
using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IVisitationRepository
    {
        Visitation CreateVisitation(int doctorId, int patientId, DateTime dateTime, string reason, string notes = null);

        Visitation GetVisitation(int visitationId);

        IEnumerable<Visitation> GetVisitationsForPatient(int patientId);

        IEnumerable<Visitation> GetVisitationsForDoctor(int doctorId);
    }
}