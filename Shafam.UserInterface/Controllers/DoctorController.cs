using System.Net;
using System.Web.Mvc;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.UserInterface.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        // GET: /Doctor/
        public ActionResult Index()
        {
            return View(_doctorRepository.GetDoctors());
        }

        // GET: /Doctor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = _doctorRepository.GetDoctor(id.Value);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: /Doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Doctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DoctorId,FirstName,LastName")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _doctorRepository.AddDoctor(doctor);
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        // GET: /Doctor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = _doctorRepository.GetDoctor(id.Value);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: /Doctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DoctorId,FirstName,LastName")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _doctorRepository.UpdateDoctor(doctor);
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: /Doctor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = _doctorRepository.GetDoctor(id.Value);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: /Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _doctorRepository.DeleteDoctor(id);
            return RedirectToAction("Index");
        }
    }
}
