using AlbionToDo.Application.Abstractions.Services;
using AlbionToDo.Domain;
using AlbionToDo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlbionToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToDoTaskService _toDoTaskService;
        private readonly IToDoExcelService _excelService;
        private const int PageSize = 5;

        public HomeController(IToDoTaskService toDoTaskService, IToDoExcelService excelService)
        {
            _toDoTaskService = toDoTaskService;
            _excelService = excelService;
        }

        public async Task<IActionResult> Index(int page = 1, bool? isCompleted = null)
        {
            List<ToDoTask> tasks;

            if (isCompleted.HasValue)
            {
                tasks = await _toDoTaskService.GetByStatusAsync(isCompleted.Value, page, PageSize);
            }
            else
            {
                tasks = await _toDoTaskService.GetListAsync(page, PageSize);
            }

            var totalItems = await _toDoTaskService.CountAsync();
            var model = new IndexViewModel
            {
                ToDoTasks = tasks,
                PageViewModel = new PageViewModel(totalItems, page, PageSize)
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ToDoTask toDoTask)
        {
            await _toDoTaskService.CreateAsync(toDoTask);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _toDoTaskService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Home/SubmitStatusAsync")]
        public async Task<IActionResult> SubmitStatusAsync(int id, bool isCompleted)
        {
            await _toDoTaskService.SubmitStatusAsync(id, isCompleted);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var stream = await _excelService.ExportToExcelAsync();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ToDoTasks.xlsx");
        }
    }
}
