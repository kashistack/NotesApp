
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Interfaces;
using Notes.Domain.Enums;
using System.Threading.Tasks;

namespace Notes.Web.Controllers
{
    public class NotesController : Controller
    {
        private readonly INoteService _service;
        public NotesController(INoteService service) => _service = service;

        public async Task<IActionResult> Index()
        {
            var notes = await _service.GetAllAsync();
            return View(notes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string? title, string? content, Priority priority = Priority.Low)
        {
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(content))
            {
                ModelState.AddModelError("", "Add title or content");
                var notes = await _service.GetAllAsync();
                return View("Index", notes);
            }
            await _service.CreateAsync(title, content, priority);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string? title, string? content, Priority priority = Priority.Low)
        {
            var ok = await _service.UpdateAsync(id, title, content, priority);
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
