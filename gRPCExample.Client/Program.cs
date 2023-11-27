using Grpc.Net.Client;
using Google.Protobuf;
using System.Net;
using Microsoft.Extensions.Logging;

namespace gRPCExample.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Configure the proxy
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var gRPCHandler = new gRPCHandler(httpClientHandler);
            var httpClient = new HttpClient(gRPCHandler);            

            var channel = GrpcChannel.ForAddress("http://localhost:5191", new GrpcChannelOptions { HttpClient = httpClient });
            var client = new Taskmanagement.TaskService.TaskServiceClient(channel);
            await client.AddTaskAsync(new Taskmanagement.MyTask()
            {
                Id = 1,
                Description = "Test",
                Completed = false
            });
        }
    }
}