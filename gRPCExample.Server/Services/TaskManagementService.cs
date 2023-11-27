using Grpc.Core;
using gRPCExample.Server;
using Taskmanagement;

namespace gRPCExample.Server.Services
{
    public class TaskManagementService : TaskService.TaskServiceBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly List<MyTask> tasks = new List<MyTask>();
        public TaskManagementService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }        
        public override Task<MyTask> AddTask(MyTask request, ServerCallContext context)
        {
            tasks.Add(request);
            Console.WriteLine("task added");
            return Task.FromResult(request);
        }
        
        public override async Task GetTasks(Google.Protobuf.WellKnownTypes.Empty request, IServerStreamWriter<MyTask> responseStream, ServerCallContext context)
        {
            foreach (var task in tasks)
            {
                await responseStream.WriteAsync(task);
            }
        }
        
        public override Task<MyTask> CompleteTask(MyTask request, ServerCallContext context)
        {
            var task = tasks.FirstOrDefault(t => t.Id == request.Id);
            if (task != null)
            {
                task.Completed = true;
            }
            return Task.FromResult(task);
        }
    }
}
