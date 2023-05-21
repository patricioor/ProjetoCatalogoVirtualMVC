using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategories();
            return View(categories);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDto)
        {
            if(ModelState.IsValid)
            {
                await _categoryService.Add(categoryDto);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryDto = await _categoryService.GetById(id);

            if(categoryDto == null)
                return NotFound();

            return View(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CategoryDTO categoryDto)
        {
            if(ModelState.IsValid)
            {
#pragma warning disable CS0168 // A variável foi declarada, mas nunca foi usada
                try
                {
                    await _categoryService.Update(categoryDto);
                } 
                catch(Exception ex)
                {
                    throw;
                }
#pragma warning restore CS0168 // A variável foi declarada, mas nunca foi usada
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryDto = await _categoryService.GetById(id);

            if (categoryDto == null)
                return NotFound();

            return View(categoryDto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            await _categoryService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryDto = await _categoryService.GetById(id);

            if(categoryDto == null)
                return NotFound();

            return View(categoryDto);
        }


    }
}
