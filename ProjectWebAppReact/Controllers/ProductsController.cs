using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWebAppReact.Data;
using ProjectWebAppReact.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAppReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProjectWebAppReactDbContext _projectWebAppReactDbContext;

        public ProductsController(ProjectWebAppReactDbContext projectWebAppReactDbContext)
        {
            _projectWebAppReactDbContext = projectWebAppReactDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return await _projectWebAppReactDbContext.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _projectWebAppReactDbContext.Products.FindAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _projectWebAppReactDbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _projectWebAppReactDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] Product product)
        {
            _projectWebAppReactDbContext.Products.Add(product);
            await _projectWebAppReactDbContext.SaveChangesAsync();

            return CreatedAtAction("Create", new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _projectWebAppReactDbContext.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound();
            }

            _projectWebAppReactDbContext.Products.Remove(product);
            await _projectWebAppReactDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
