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
            var test = new Test
                      {
                          VisitationId = visitationId,
                          Type = testType
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
            throw new NotImplementedException();
        }

        public void MarkAsSeen(int testId)
        {
            throw new NotImplementedException();
            //_dataContext.Tests.Where(t => t.TestId == testId).
        }
    }
}
