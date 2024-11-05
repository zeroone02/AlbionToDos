namespace AlbionToDo.Application.Abstractions.Services;
public interface IToDoExcelService
{
    /// <summary>
    ///Выгрузка списка задач со всей информацией по ним в excel файл.
    /// </summary>
    /// <returns></returns>
    Task<MemoryStream> ExportToExcelAsync();
}
