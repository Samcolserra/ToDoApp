using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTOs;
using ToDoApp.Entities;
using ToDoApp.Repositories;

namespace ToDoApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/todos")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository _repository;

        public ToDoController(IToDoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<ToDo> GetToDos()
        {
            return _repository.GetToDos();
        }

        [HttpGet("{id}")]
        public ToDo GetToDo(Guid id)
        {
            return _repository.GetToDo(id);
        }

        [HttpPost]
        public ToDo CreateToDo(ToDoDTO toDoDTO)
        {
            var userId = User.FindFirst("id").Value.ToString();

            ToDo toDo = new ToDo
            (
                Guid.Parse(userId),
                toDoDTO._name,
                toDoDTO._status,
                toDoDTO._description
            );

            _repository.CreateToDo(toDo);
            return toDo;
        }

        [HttpPut("{id}")]
        public void UpdateToDo(Guid id, ToDoDTO toDoDTO)
        {
            ToDo toDo = _repository.GetToDo(id);

            if (toDo == null){ return; }

            ToDo updatedToDo = new ToDo
            (
                Guid.NewGuid(),
                toDoDTO._name,
                toDoDTO._status,
                toDoDTO._description
            );

            _repository.UpdateToDo(id, updatedToDo);
        }

        [HttpDelete("{id}")]
        public void DeleteToDo(Guid id)
        {
            ToDo toDo = _repository.GetToDo(id);

            if (toDo == null){ return; }

            _repository.DeleteToDo(id);
        }
    }
}