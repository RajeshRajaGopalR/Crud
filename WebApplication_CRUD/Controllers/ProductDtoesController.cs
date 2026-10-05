using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_CRUD.Data;
using WebApplication_CRUD.Model;
using WebApplication_CRUD.Service;

namespace WebApplication_CRUD.Controllers
{

    public class ProductDtoesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _service;

        public ProductDtoesController(
      AppDbContext context,
      IProductService service)
        {
            _context = context;
            _service = service;
        }

        // GET: ProductDtoes

        public async Task<IActionResult> Index()
        {
            //return View(await _context.Products.ToListAsync());
            var products = await _service.GetAllAsync();

            return View(products);
        }

        // GET: ProductDtoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDto = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productDto == null)
            {
                return NotFound();
            }

            return View(productDto);
        }

        // GET: ProductDtoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductDtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Quantity")] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        // GET: ProductDtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDto = await _context.Products.FindAsync(id);
            if (productDto == null)
            {
                return NotFound();
            }
            return View(productDto);
        }

        // POST: ProductDtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Quantity")] ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductDtoExists(productDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        // GET: ProductDtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDto = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productDto == null)
            {
                return NotFound();
            }

            return View(productDto);
        }

        // POST: ProductDtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productDto = await _context.Products.FindAsync(id);
            if (productDto != null)
            {
                _context.Products.Remove(productDto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductDtoExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
