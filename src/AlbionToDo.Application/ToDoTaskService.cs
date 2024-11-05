using AlbionToDo.Application.Abstractions.Repositories;
using AlbionToDo.Application.Abstractions.Services;
using AlbionToDo.Domain;

namespace AlbionToDo.Application
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;
        private readonly IToDoExcelService _toDoExcelService;

        public ToDoTaskService(
            IToDoTaskRepository toDoTaskRepository,
            IToDoExcelService toDoExcelService
           )
        {
            _toDoTaskRepository = toDoTaskRepository;
            _toDoExcelService = toDoExcelService;
        }
        public Task<ToDoTask> CreateAsync(ToDoTask toDoTask)
        {
            return _toDoTaskRepository.InsertAsync(toDoTask);
        }

        public Task DeleteAsync(int id)
        {
            return _toDoTaskRepository.DeleteAsync(id);
        }

        public async Task SubmitStatusAsync(int id, bool isCompleted)
        {
            var task = await _toDoTaskRepository.GetByIdAsync(id);
            if (task != null && task.IsCompleted != isCompleted)
            {
                task.IsCompleted = isCompleted;
                await _toDoTaskRepository.UpdateAsync(task);
            }
        }

        public Task<List<ToDoTask>> GetListAsync(int page, int pageSize)
        {
            return _toDoTaskRepository.GetListAsync(page, pageSize);
        }

        public Task<int> CountAsync()
        {
            return _toDoTaskRepository.CountAsync();
        }
        public Task<List<ToDoTask>> GetByStatusAsync(bool isCompleted, int page, int pageSize)
        {
            return _toDoTaskRepository.GetListByStatusAsync(isCompleted, page, pageSize);
        }

    }
}
