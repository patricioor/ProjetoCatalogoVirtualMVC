using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        readonly IWebHostEnvironment _environment;
        readonly IProductService _productService;
        readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var product = await _productService.GetProductsAsync();

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(
                await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.Add(productDto);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId = 
                    new SelectList(await _categoryService.GetCategories(), "Id", "Name", productDto.CategoryId);
            }
            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var productDto = await _productService.GetByIdAsync(id);

            if (productDto == null)
                return NotFound();

            var categories = await _categoryService.GetCategories();

            ViewBag.CategoryId = new SelectList
                (categories, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return NotFound();

            var productDto = await _productService.GetByIdAsync(Id);

            if (productDto == null)
                return NotFound();

            return View(productDto);
        }

        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _productService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var productDto = await _productService.GetByIdAsync(id);

            if(id == null) return NotFound();

            var wwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + productDto.Image);
            var exists = System.IO.File.Exists(image);

            ViewBag.ImageExist = exists;

            return View(productDto);
        }
    }
}
