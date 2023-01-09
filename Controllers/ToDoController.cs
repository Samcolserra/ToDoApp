using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTOs;
using ToDoApp.Entities;
using ToDoApp.Repositories;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/todos")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository _repository;
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoRepository repository, IToDoService toDoService)
        {
            _toDoService = toDoService;
            _repository = repository;
        }

        [HttpGet]
        public List<ToDoDatabaseModel> GetToDos()
        {
            //return _repository.GetToDos();
            
            var userId = Guid.Parse(User.FindFirst("id").Value.ToString());

            return _toDoService.GetToDos(userId);
        }

        [HttpGet("{id}")]
        public ToDo GetToDo(Guid id)
        {
            return _repository.GetToDo(id);
        }

        [HttpPost]
        public ToDoDatabaseModel CreateToDo(ToDoDTO toDoDTO)
        {
            var userId = Guid.Parse(User.FindFirst("id").Value.ToString());

            ToDo toDo = new ToDo
            (
                userId,
                toDoDTO._name,
                toDoDTO._status,
                toDoDTO._description
            );

            //_repository.CreateToDo(toDo);
            return _toDoService.CreateToDo(toDo);
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