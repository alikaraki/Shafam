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

        List<Test> GetTestsForPatient(int patientId);

        List<Test> GetTestsForVisitation(int visitationId);

        Test GetTestsForTestId(int testId);

        void AddTestResult(int testId, string result);

        void MarkAsSeen(int testId);
    }
}