using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;
using System.Threading.Tasks;

namespace Shafam.DataAccess.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly IShafamDataContext _dataContext;

        public TestRepository(IShafamDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Test AddTest(int visitationId, string testType)
        {
            Visitation visitation = _dataContext.Visitations.First(v => v.VisitationId == visitationId);

            var test = new Test
                      {
                          VisitationId = visitationId,
                          Type = testType,
                          DoctorId = visitation.DoctorId,
                          PatientId = visitation.PatientId
                      };

            _dataContext.Tests.Add(test);
            _dataContext.Save();

            return test;
        }

        public List<Test> GetTestsForPatient(int patientId)
        {
            return _dataContext.Tests.Where(t => t.PatientId == patientId).ToList();
        }

        public List<Test> GetTestsForVisitation(int visitationId)
        {
            return _dataContext.Tests.Where(t => t.VisitationId == visitationId).ToList();
        }

        public void AddTestResult(int testId, string result)
        {
            Test test = _dataContext.Tests.First(t => t.TestId == testId);
            test.Result = result;
            _dataContext.Save();
        }

        public void MarkAsSeen(int testId)
        {
            Test test = _dataContext.Tests.First(t => t.TestId == testId);
            test.SeenByDoctor = true;
            _dataContext.Save();
        }
    }
}
