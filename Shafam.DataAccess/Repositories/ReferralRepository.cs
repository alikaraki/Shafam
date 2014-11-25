using System.Collections.Generic;
using System.Linq;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;

namespace Shafam.DataAccess.Repositories
{
    public class ReferralRepository : IReferralRepository
    {
        private readonly ShafamDataContext _dataContext;

        public ReferralRepository(ShafamDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Referral AddReferral(int patientId, int referringDoctorId, int referredDoctorId, string notes = null)
        {
            var referral = new Referral
                           {
                               ReferredDoctorId = referredDoctorId,
                               ReferringDoctorId = referringDoctorId,
                               PatientId = patientId,
                               Notes = notes
                           };

            _dataContext.Referrals.Add(referral);
            _dataContext.SaveChanges();

            return referral;
        }

        public Referral GetReferral(int referralId)
        {
            return _dataContext.Referrals.First(r => r.ReferralId == referralId);
        }

        public IEnumerable<Referral> GetReferralsForReferredDoctor(int doctorId)
        {
            return _dataContext.Referrals.Where(r => r.ReferredDoctorId == doctorId).ToList();
        }

        public IEnumerable<Referral> GetReferralsForReferringDoctor(int doctorId)
        {
            return _dataContext.Referrals.Where(r => r.ReferringDoctorId == doctorId).ToList();
        }

        public IEnumerable<Referral> GetReferralsForPatient(int patientId)
        {
            return _dataContext.Referrals.Where(r => r.PatientId == patientId).ToList();
        }

        public void MarkAsSeen(int referralId)
        {
            Referral r = GetReferral(referralId);
            r.SeenByReferredDoctor = true;
            _dataContext.SaveChanges();
        }
    }
}
