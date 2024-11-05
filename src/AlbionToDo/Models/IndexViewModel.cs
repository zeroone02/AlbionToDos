using AlbionToDo.Domain;

namespace AlbionToDo.Models
{
    public class IndexViewModel
    {
        public IEnumerable<ToDoTask> ToDoTasks { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
