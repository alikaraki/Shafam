using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IReferralRepository
    {
        Referral AddReferral(int patientId, int referringDoctorId, int referredDoctorId, string notes = null);

        Referral GetReferral(int referralId);

        IEnumerable<Referral> GetReferralsForDoctor(int doctorId);

        IEnumerable<Referral> GetReferralsForPatient(int patientId);

        void MarkAsSeen(int referralId);
    }
}
