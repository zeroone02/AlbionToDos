using AlbionToDo.Domain;

namespace AlbionToDo.Application.Abstractions.Services;
public interface IToDoTaskService
{
    Task<ToDoTask> CreateAsync(ToDoTask toDoTask);
    Task DeleteAsync(int id);
    Task SubmitStatusAsync(int id, bool isCompleted);
    Task<List<ToDoTask>> GetListAsync(int page, int pageSize);
    Task<int> CountAsync();
    Task<List<ToDoTask>> GetByStatusAsync(bool isCompleted, int page, int pageSize);

}
