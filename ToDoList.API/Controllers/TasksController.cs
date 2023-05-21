using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Repositories;
using ToDoList.Models.Enums;
using ToDoList.Models;

namespace ToDoList.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    public TasksController (ITaskRepository taskRepository)
    {
          _taskRepository=taskRepository;
    }
    //Viết API Get ALl Tasks
    //api/tasks?name=...
    [HttpGet]
    public async Task<IActionResult>  GetAll([FromQuery] TaskListSearch taskListSearch)
    {
        var tasks= await _taskRepository.GetTaskList(taskListSearch);
        var taskDtos=tasks.Select(x=>new TaskDto()
           { 
              Status=x.Status,
              Name=x.Name,
              AssigneeId=x.AssigneeId,
              CreatedDate=x.CreatedDate,
              Priority=x.Priority,
              Id=x.Id,
              AssigneeName=x.Assignee!=null? x.Assignee.FirstName + ' '+ x.Assignee.LastName:"N/A"
           });
        return Ok(taskDtos);
    }
    [HttpPost]
    public async Task<IActionResult>  Create([FromBody] TaskCreateRequest request)
    {
         if(!ModelState.IsValid)
         {
             return BadRequest(ModelState);
         }
        var newtask= await _taskRepository.Create(new Entities.Task()
        {
            Name=request.Name,
            Status=Status.Open,
            Priority=request.Priority,
            CreatedDate=DateTime.Now,
            Id=request.Id
        });
        return CreatedAtAction(nameof(GetById),new {id=newtask.Id},newtask);
    }
    //api/tasks/xxx
     [HttpGet]
     [Route("{id}")]
    public async Task<IActionResult>  GetById([FromRoute] Guid id )
    {
        var task= await _taskRepository.GetById(id);
        if(task==null) return NotFound($"{id} is not found");
        return Ok(new TaskDto()
            {
                Name = task.Name,
                Status = task.Status,
                Id = task.Id,
                AssigneeId = task.AssigneeId,
                Priority = task.Priority,
                CreatedDate = task.CreatedDate
            });
    }
     [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(Guid id, TaskUpdateRequest request)
    {
         if(!ModelState.IsValid)
         {
             return BadRequest(ModelState);
         }
         var taskFromDb= await _taskRepository.GetById(id);
        if(taskFromDb==null) 
        {
            return NotFound($"{id} is not found");
        }
        
        taskFromDb.Name=request.Name;
        taskFromDb.Priority=request.Priority;
        var updatedtask= await _taskRepository.Update(taskFromDb);
        return Ok(new TaskDto()
        {
            Name=updatedtask.Name,
            Status=updatedtask.Status,
            Priority=updatedtask.Priority,
            Id=updatedtask.Id,
            AssigneeId=updatedtask.AssigneeId,
            CreatedDate=updatedtask.CreatedDate
        });
    }
}
