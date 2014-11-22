using Shafam.Common.DataModel;

namespace Shafam.BusinessLogic
{
    public interface IAccountManagementService
    {
        /// <summary>
        /// Creates an account for the patient and returns the username.
        /// Password is same as username.
        /// </summary>
        string CreateAccountForPatient(int patientId);

        /// <summary>
        /// Creates an account for the doctor and returns the username.
        /// Password is same as username.
        /// </summary>
        string CreateAccountForDoctor(int doctorId);

        /// <summary>
        /// Creates an account for the staff and returns the username.
        /// Password is same as username.
        /// </summary>
        string CreateAccountForStaff(int staffId, UserRole role);
    }
}
