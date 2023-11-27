using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace gRPCExample.Client
{
    internal class gRPCHandler : DelegatingHandler
    {        
        public gRPCHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            // Log or print the request URL
            Console.WriteLine($"Request URL: {request.RequestUri}");

            // Log or print the request headers
            Console.WriteLine("Request Headers:");
            foreach (var header in request.Headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }

            // Log or print the request body if it exists
            if (request.Content != null)
            {
                var contentBytes = await request.Content.ReadAsByteArrayAsync();
                string contentHex = BitConverter.ToString(contentBytes).Replace("-", string.Empty);
                Console.WriteLine($"Request Body: {contentHex}");
             
            }
            
            response = await base.SendAsync(request, cancellationToken);
            

            return response;
        }       
    }
}
