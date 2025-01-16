using FintechWebAPI.Entity;
using FintechWebAPI.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FintechWebAPI.Controllers;
 [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase // Usar ControllerBase
    {
        private readonly DataContext _context;

        public TransactionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FintechTransaction>>> GetTransactions()
        {
            var transactions = await _context.Transactions.ToArrayAsync();
            if (transactions == null || transactions.Length == 0)
            {
                return Ok(new List<FintechTransaction>());
            }

            var transactionsDTO = transactions.Select(TransactionMapper.TransactionDTO).ToList();
            return Ok(transactionsDTO);
        }

        // GET: api/transactions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FintechTransaction>> GetTransactionById(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(TransactionMapper.TransactionDTO(transaction)); // Retorna DTO de transacción
        }

        // POST: api/transactions
        [HttpPost]
        public async Task<ActionResult<FintechTransaction>> CreateTransaction(FintechTransaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
        }

        // PUT: api/transactions/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTransaction(int id, FintechTransaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest("El Id del parámetro no coincide con el Id de la transacción.");
            }

            var existingTransaction = await _context.Transactions.FindAsync(id);
            if (existingTransaction == null)
            {
                return NotFound($"No se encontró una transacción con el Id {id}.");
            }

            existingTransaction.OriginAccountId = transaction.OriginAccountId;
            existingTransaction.DestinationAccountId = transaction.DestinationAccountId;
            existingTransaction.Amount = transaction.Amount;
            existingTransaction.Date = transaction.Date;
            existingTransaction.TypeTransaction = transaction.TypeTransaction;

            _context.Entry(existingTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound($"No se pudo actualizar porque la transacción con el Id: {id}, no existe");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/transactions/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound($"No se encontró una transacción con el Id {id}.");
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return Ok(TransactionMapper.TransactionDTO(transaction)); // Retorna DTO de la transacción eliminada
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }

