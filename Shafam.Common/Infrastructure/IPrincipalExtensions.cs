﻿using System.Security.Claims;
using System.Security.Principal;
using Shafam.Common.DataModel;

namespace Shafam.Common.Infrastructure
{
    public static class IPrincipalExtensions
    {
        public static bool IsAnonymous(this IPrincipal principal)
        {
            return principal == null || !principal.Identity.IsAuthenticated;
        }

        public static bool IsDoctor(this IPrincipal principal)
        {
            return !principal.IsAnonymous() && principal.IsInRole(UserRole.Doctor.ToString());
        }

        public static bool IsPatient(this IPrincipal principal)
        {
            return !principal.IsAnonymous() && principal.IsInRole(UserRole.Patient.ToString());
        }

        public static bool IsIT(this IPrincipal principal)
        {
            return !principal.IsAnonymous() && principal.IsInRole(UserRole.IT.ToString());
        }

        public static bool IsStaff(this IPrincipal principal)
        {
            return !principal.IsAnonymous() && principal.IsInRole(UserRole.Staff.ToString());
        }

        public static bool IsLegal(this IPrincipal principal)
        {
            return !principal.IsAnonymous() && principal.IsInRole(UserRole.Legal.ToString());
        }

        public static bool IsFinance(this IPrincipal principal)
        {
            return !principal.IsAnonymous() && principal.IsInRole(UserRole.Finance.ToString());
        }

        public static string GetUsername(this IPrincipal principal)
        {
            return principal.Identity.Name;
        }

        public static string GetDepartment(this IPrincipal principal)
        {
            if (principal.IsAnonymous())
            {
                return null;
            }

            var claims = principal as ClaimsPrincipal;
            if (claims != null)
            {
                Claim department = claims.FindFirst(ClaimTypes.UserData);
                return department != null ? department.Value : null;
            }

            return null;
        }
    }
}