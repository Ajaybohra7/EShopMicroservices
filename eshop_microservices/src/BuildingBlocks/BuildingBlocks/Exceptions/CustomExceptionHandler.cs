﻿using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message:{exceptionMessage} , Time of occurence {time}",
                exception.Message, DateTime.UtcNow);
            (string Detail, string Title, int StatusCode) details = exception switch
            {
                InternalServerException => (
                 exception.Message, exception.GetType().Name,
                 context.Response.StatusCode = StatusCodes.Status500InternalServerError),

                ValidationException => (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                BadRequestException => (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest),

                NotFoundException => (
                       exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status404NotFound),
                _ => (
                 exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };
            
            var problemDetail=new ProblemDetails
            {
                Status = details.StatusCode,
                Title = details.Title,
                Detail = details.Detail,
                Instance=context.Request.Path
            };
            problemDetail.Extensions.Add("traceId", context.TraceIdentifier);
            if(exception is ValidationException validationException)
            {
               
                problemDetail.Extensions.Add("ValidationErrors", validationException.Errors);
            }

            await context.Response.WriteAsJsonAsync(problemDetail, cancellationToken);
            return true;




        }

    }
    
}
