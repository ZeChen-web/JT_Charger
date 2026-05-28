using HybirdFrameworkCore.Entity;
using HybirdFrameworkCore.AutoTask;
using Microsoft.AspNetCore.Mvc;

namespace WebStarter.Controllers.System;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class TaskController
{
    /// <summary>
    /// 获取任务列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("/GetAll")]
    public Result<List<TaskInfo>> GetAll()
    {
        List<TaskInfo> result = new();
        foreach (var (key, value) in TaskInit.TaskMap)
        {
            result.Add(new TaskInfo()
            {
                Name = key,
                Interval = value.Interval(),
                Stoped = value.Stoped()
            });
        }

        return Result<List<TaskInfo>>.Success(result);
    }

    /// <summary>
    /// 停止任务
    /// </summary>
    /// <param name="taskName"></param>
    /// <returns></returns>
    [HttpGet("/stop/{taskName}")]
    public Result<bool> Stop(string taskName)
    {
        if (TaskInit.TaskMap.TryGetValue(taskName, out var task))
        {
            task.Stop();
            return Result<bool>.Success(task.Stoped());
        }

        return Result<bool>.Fail("任务不存在");
    }


    /// <summary>
    /// 启动任务
    /// </summary>
    /// <param name="taskName"></param>
    /// <returns></returns>
    [HttpGet("/start/{taskName}")]
    public Result<bool> Start(string taskName)
    {
        if (TaskInit.TaskMap.TryGetValue(taskName, out var task))
        {
            task.Start();
            return Result<bool>.Success(task.Stoped());
        }

        return Result<bool>.Fail("任务不存在");
    }
}
