using ToDoApp.Entities;

namespace ToDoApp.Repositories
{
    public class TemporaryToDoRepository : IToDoRepository
    {
        public List<ToDo> _toDoList {get; private set;} = new()
        {
            new ToDo(Guid.NewGuid(), "Study", Status.OnGoing),
            new ToDo(Guid.NewGuid(), "Walk", Status.Completed),
            new ToDo(Guid.NewGuid(), "Shower", Status.NotStarted),
        };

        public void CreateToDo(ToDo toDo)
        {
            _toDoList.Add(toDo);
        }
        public IEnumerable<ToDo> GetToDos()
        {
            return _toDoList;
        }
        public ToDo GetToDo(Guid id)
        {
            return _toDoList.Where(toDo => toDo._toDoId == id).SingleOrDefault();
        }
        public void UpdateToDo(Guid id, ToDo updatedToDo)
        {
            int index = _toDoList.FindIndex(toDo => toDo._toDoId == id);
            _toDoList[index].UpdateToDo(updatedToDo._name, updatedToDo._description, updatedToDo._status);
        }
        public void DeleteToDo(Guid id)
        {
            int index = _toDoList.FindIndex(_toDo => _toDo._toDoId == id);
            _toDoList.RemoveAt(index);
        }
    }
}