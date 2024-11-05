using AlbionToDo.Domain;

namespace AlbionToDo.Application.Abstractions.Repositories;
public interface IToDoTaskRepository
{
    Task<ToDoTask> InsertAsync(ToDoTask toDoTask);
    Task DeleteAsync(int id);
    Task UpdateAsync(ToDoTask toDoTask);
    Task<List<ToDoTask>> GetListAsync(int page, int pageSize);
    Task<List<ToDoTask>> GetListAsync();
    Task<List<ToDoTask>> GetListByStatusAsync(bool isCompleted, int page, int pageSize);
    Task<int> CountAsync();
    Task<ToDoTask> GetByIdAsync(int id);
}
