using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudProgram;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsDetailController : ControllerBase
    {
        private readonly JsonCRUDprogram _crud = new JsonCRUDprogram();

        // GET: api/patientsdetail
        [HttpGet]
        public ActionResult<IEnumerable<PatientDetails>> GetAll()
        {
            var patients = _crud.LoadData();
            return Ok(patients);
        }

        // GET: api/patientsdetail/1234567890
        [HttpGet("{mobile}")]
        public ActionResult<PatientDetails> GetByMobile(long mobile)
        {
            var patients = _crud.LoadData();
            var patient = patients.FirstOrDefault(p => p.MobileNumber == mobile);

            if (patient == null)
                return NotFound($"Patient with mobile number {mobile} not found.");

            return Ok(patient);
        }

        // POST: api/patientsdetail
        [HttpPost]
        public ActionResult AddPatient([FromBody] PatientDetails newPatient)
        {
            if (newPatient == null)
                return BadRequest("Invalid patient data.");

            var patients = _crud.LoadData();

            if (_crud.Duplicate(newPatient.MobileNumber, newPatient.Email))
                return Conflict("This mobile number or email already exists.");

            patients.Add(newPatient);
            _crud.SaveData(patients);

            return CreatedAtAction(nameof(GetByMobile), new { mobile = newPatient.MobileNumber }, newPatient);
        }

        // PUT: api/patientsdetail/1234567890
        [HttpPut("{mobile}")]
        public IActionResult UpdatePatient(long mobile, [FromBody] PatientDetails updated)
        {
            if (updated == null)
                return BadRequest("Invalid patient data.");

            var patients = _crud.LoadData();
            var existing = patients.FirstOrDefault(p => p.MobileNumber == mobile);

            if (existing == null)
                return NotFound($"Patient with mobile number {mobile} not found.");

            existing.Name = updated.Name ?? existing.Name;
            existing.Age = updated.Age != 0 ? updated.Age : existing.Age;
            existing.Email = updated.Email ?? existing.Email;
            existing.MobileNumber = updated.MobileNumber != 0 ? updated.MobileNumber : existing.MobileNumber;

            _crud.SaveData(patients);
            return NoContent();
        }

        // DELETE: api/patientsdetail/1234567890
        [HttpDelete("{mobile}")]
        public IActionResult DeletePatient(long mobile)
        {
            var patients = _crud.LoadData();
            var patient = patients.FirstOrDefault(p => p.MobileNumber == mobile);

            if (patient == null)
                return NotFound($"Patient with mobile number {mobile} not found.");

            patients.Remove(patient);
            _crud.SaveData(patients);
            return NoContent();
        }
    }
}
