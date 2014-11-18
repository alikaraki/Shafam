using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface ITestRepository
    {
        Test AddTest(int visitationId, string testType);

        IEnumerable<Test> GetTestsForPatient(int patientId);

        IEnumerable<Test> GetTestsForVisitation(int patientId);

        void AddTestResult(int testId, string result);

        void MarkAsSeen(int testId);
    }
}