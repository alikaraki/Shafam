using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;

namespace Shafam.BusinessLogic
{
    public interface ILegalManagementService
    {
        List<Patient> DoctorHistory(int doctorId);
        List<Visitation> PatientHistory(int patientId);
    }
}
