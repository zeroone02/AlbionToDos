using AlbionToDo.Application.Abstractions.Repositories;
using AlbionToDo.Application.Abstractions.Services;
using OfficeOpenXml;

namespace AlbionToDo.Application;
public class ToDoExcelService : IToDoExcelService
{
    private readonly IToDoTaskRepository _toDoTaskRepository;
    public ToDoExcelService(IToDoTaskRepository repository)
    {
        _toDoTaskRepository = repository;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }
    public async Task<MemoryStream> ExportToExcelAsync()
    {
        var tasks = await _toDoTaskRepository.GetListAsync();

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("ToDoTasks");

        worksheet.Cells[1, 1].Value = "Id";
        worksheet.Cells[1, 2].Value = "Описание";
        worksheet.Cells[1, 3].Value = "Статус";

        for (int i = 0; i < tasks.Count; i++)
        {
            worksheet.Cells[i + 2, 1].Value = tasks[i].Id;
            worksheet.Cells[i + 2, 2].Value = tasks[i].Description;
            worksheet.Cells[i + 2, 3].Value = tasks[i].IsCompleted ? "Завершено" : "В процессе";
        }

        var stream = new MemoryStream();
        package.SaveAs(stream);
        stream.Position = 0;

        return stream;
    }
}
