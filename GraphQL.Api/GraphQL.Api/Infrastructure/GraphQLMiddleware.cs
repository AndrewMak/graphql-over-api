﻿using GraphQL.DataLoader;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Api.Infrastructure
{
    public class GraphQLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDocumentWriter _writer;
        private readonly IDocumentExecuter _executor;

        public GraphQLMiddleware(RequestDelegate next, IDocumentWriter writer, IDocumentExecuter executor)
        {
            _next = next;
            _writer = writer;
            _executor = executor;
        }

        public async Task InvokeAsync(HttpContext httpContext, IServiceProvider serviceProvider)
        {
            var _executor = new DocumentExecuter();
            var result = await _executor.ExecuteAsync(doc =>
            {
                doc.Listeners.Add(serviceProvider.GetRequiredService<DataLoaderDocumentListener>());

            }).ConfigureAwait(false);


        }
    }
}
