using AlbionToDo.Application.Abstractions.Repositories;
using AlbionToDo.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlbionToDo.EFCore.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly AlbionToDoDbContext _context;
        public ToDoTaskRepository(AlbionToDoDbContext context)
        {
            _context = context;
        }

        public async Task<ToDoTask> InsertAsync(ToDoTask toDoTask)
        {
            await _context.ToDoTasks.AddAsync(toDoTask);
            await _context.SaveChangesAsync();
            return toDoTask;
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.ToDoTasks.FindAsync(id);
            if (task != null)
            {
                _context.ToDoTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(ToDoTask toDoTask)
        {
            _context.ToDoTasks.Update(toDoTask);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ToDoTask>> GetListAsync(int page, int pageSize)
        {
            return await _context.ToDoTasks
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<ToDoTask>> GetListAsync()
        {
            return await _context.ToDoTasks.ToListAsync();
        }

        public async Task<List<ToDoTask>> GetListByStatusAsync(bool isCompleted, int page, int pageSize)
        {
            return await _context.ToDoTasks
                .Where(task => task.IsCompleted == isCompleted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.ToDoTasks.CountAsync();
        }

        public async Task<ToDoTask> GetByIdAsync(int id)
        {
            return await _context.ToDoTasks.FindAsync(id);
        }
    }
}
