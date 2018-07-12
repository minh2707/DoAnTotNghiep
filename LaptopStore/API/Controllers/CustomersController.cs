//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebAPI.Models;

//namespace WebAPI.Controllers
//{
//    [Produces("application/json")]
//    [Route("api/Customers")]
//    public class CustomersController : Controller
//    {
//        private readonly LaptopStoreContext _context;

//        public CustomersController(LaptopStoreContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Customers
//        [HttpGet]
//        public IEnumerable<Customer> GetCustomer()
//        {
//            return _context.Customer;
//        }

//        // GET: api/Customers/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetCustomer([FromRoute] string id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerId == id);

//            if (customer == null)
//            {
//                return NotFound();
//            }

//            return Ok(customer);
//        }

//        // PUT: api/Customers/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutCustomer([FromRoute] string id, [FromBody] Customer customer)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != customer.CustomerId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(customer).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!CustomerExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Customers
//        [HttpPost]
//        [Route("Create")]
//        public JsonResult PostCustomer([FromBody] Customer customer)
//        {
//            var cus = _context.Customer.Find(customer.CustomerId);
//            if (cus != null)
//            {
//                throw new Exception("Exist CustomerId the same!");
//            }
//            _context.Customer.Add(customer);
//            _context.SaveChanges();
//            return Json(customer);
//        }

//        // DELETE: api/Customers/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteCustomer([FromRoute] string id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerId == id);
//            if (customer == null)
//            {
//                return NotFound();
//            }

//            _context.Customer.Remove(customer);
//            await _context.SaveChangesAsync();

//            return Ok(customer);
//        }

//        private bool CustomerExists(string id)
//        {
//            return _context.Customer.Any(e => e.CustomerId == id);
//        }
//    }
//}