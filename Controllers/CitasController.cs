#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitaVehiculosApi.Data;
using CitaVehiculosApi.Models;
using Microsoft.AspNetCore.Cors;

namespace CitaVehiculosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly ApiContext _context;

        public CitasController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitas()
        {
            var response = await _context.Citas.ToListAsync();
            return  new JsonResult(Ok(response));
        }

        // GET: api/Citas/placa/52342
        [HttpGet("placa/{placa}")]
        public async Task<ActionResult<List<Cita>>> GetCita(string placa)
        {
            var citas = await _context.Citas.Where(cita =>
                cita.Placa == placa
            ).ToListAsync();

            if (citas.Count == 0)
            {
                return new JsonResult(NotFound("No tiene cita registrada"));
            }

            return new JsonResult(Ok(citas));
        }

        // GET: api/Citas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);

            if (cita == null)
            {
                return new JsonResult(NotFound("No se encontro cita con el numero de ID"));
            }

            return new JsonResult(Ok(cita));
        }

        // PUT: api/Citas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, Cita cita)
        {
            if (id != cita.Id)
            {
                return new JsonResult(BadRequest());
            }

            _context.Entry(cita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(id))
                {
                    return new JsonResult(NotFound("No se encontro cita con el numero de ID"));
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(NoContent());
        }

        // POST: api/Citas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cita>> PostCita(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();

            var response = CreatedAtAction("GetCita", new { id = cita.Id }, cita);
            return new JsonResult(Ok(response));
        }

        // DELETE: api/Citas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return new JsonResult(NotFound("No se encontro cita con el numero de ID"));
            }

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();

            return new JsonResult(NoContent());
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.Id == id);
        }
    }
}
