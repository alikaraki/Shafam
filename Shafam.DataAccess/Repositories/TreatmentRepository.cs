﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;
using System.Threading.Tasks;

namespace Shafam.DataAccess.Repositories
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly IShafamDataContext _dataContext;

        public TreatmentRepository(IShafamDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Treatment AddTreatment(int visitationId, string treatmentType)
        {
            var treatment = new Treatment
                            {
                                VisitationId = visitationId,
                                TreatmentType = treatmentType
                            };

            _dataContext.Treatments.Add(treatment);
            _dataContext.Save();

            return treatment;
        }

        public List<Treatment> GetTreatmentsForPatient(int patientId)
        {
            return _dataContext.Treatments.Where(t => t.PatientId == patientId).ToList();
        }

        public List<Treatment> GetTreatmentsForVisitation(int visitationId)
        {
            return _dataContext.Treatments.Where(t => t.VisitationId == visitationId).ToList();
        }
    }
}
