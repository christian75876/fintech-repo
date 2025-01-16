using FintechLibrary.DTOs;
using FintechWebAPI.Class;
using FintechWebAPI.Entity;
using FintechWebAPI.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FintechWebAPI.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly DataContext _context;

        public CuentasController(DataContext context)
        {
            _context = context;
        }

        //Get: api/cuentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetCuentas()
        {
            var cuentas = await _context.cuentas.ToArrayAsync();
            if (cuentas == null || cuentas.Length == 0)
            {
                return Ok(new List<Cuenta>());
            }

            var cuentasDTO = cuentas.Select(CuentaMapper.CuentaDto).ToList();
            return Ok(cuentasDTO);
        }

        //Get: api/cuentas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDTO>> GetCuentaById(int id)
        {
            var cuenta = await _context.cuentas.FindAsync(id);
            return cuenta == null ? NotFound() : CuentaMapper.CuentaDto(cuenta);
        }
        
        //Post: api/cuentas
        [HttpPost]
        public async Task<ActionResult<AccountDTO>> CreateCuenta(AccountDTO cuentaDto)
        {
            var cuenta = CuentaMapper.ToEntity(cuentaDto);
            
            _context.cuentas.Add(cuenta);
            await _context.SaveChangesAsync();
            var resultDto = CuentaMapper.CuentaDto(cuenta);
            return CreatedAtAction(nameof(GetCuentaById), new { id = cuenta.Id }, resultDto);
        }
        
        //put: api/cuentas/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCuenta(int id, AccountDTO cuentaDto)
        {
            if (id != cuentaDto.Id)
            {
                return BadRequest("El Id del parámetro no coincide con el Id de la cuenta");
            }
            var existingCuenta = await _context.cuentas.FindAsync(id);
            if (existingCuenta == null)
            {
                return NotFound($"No se encontro una cuenta con el Id {id}.");
            }
            existingCuenta.Id = cuentaDto.Id;
            existingCuenta.AccountNumber = cuentaDto.AccountNumber;
            existingCuenta.Balance = cuentaDto.Balance;
            existingCuenta.AccountType = cuentaDto.AccountType;
            
            _context.Entry(existingCuenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(id))
                {
                    return NotFound($"No se pudo actualizar porque la cuenta con el Id: {id}, no existe");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        
        //Delete: api/cuentas/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCuenta(int id)
        {
            var cuenta = await _context.cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound($"No se encontró una cuenta con el Id {id}.");
            }

            var cuentaDto = CuentaMapper.CuentaDto(cuenta);

            try
            {
                _context.cuentas.Remove(cuenta);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al eliminar la cuenta: {e.Message}");
            }

            return Ok(cuentaDto);
        }

        private bool CuentaExists(int id)
        {
            return _context.cuentas.Any(e => e.Id == id);
        }
    }
