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
        public List<ToDoDatabaseModel> GetToDos(Status? status)
        {
            var userId = Guid.Parse(User.FindFirst("id").Value.ToString());

            if (status == null) { return _toDoService.GetToDos(userId); }

            return _toDoService.GetToDos(userId, (Status)status);
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

            return _toDoService.CreateToDo(userId, toDo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateToDo(Guid id, ToDoDTO toDoDTO)
        {
            var userId = Guid.Parse(User.FindFirst("id").Value.ToString());

            ToDo updatedToDo = new ToDo
            (
                userId,
                toDoDTO._name,
                toDoDTO._status,
                toDoDTO._description
            );

            var (success, content) = _toDoService.UpdateToDo(userId, id, updatedToDo);

            if (!success) { return BadRequest(content); }

            return Ok(content);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst("id").Value.ToString());

            var (success, content) = _toDoService.DeleteToDo(userId, id);

            if (!success) { return BadRequest(content); }

            return Ok(content);
        }
    }
}