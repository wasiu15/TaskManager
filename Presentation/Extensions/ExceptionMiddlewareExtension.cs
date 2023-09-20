using Application.Dtos;
using Domain.Exceptions;
using Domain.Exceptions.BaseExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace Presentation.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            NotAuthorizedException => StatusCodes.Status401Unauthorized,
                            NoContentException => StatusCodes.Status204NoContent,
                            DuplicateRequestException => StatusCodes.Status409Conflict,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        //TODO: call warning logger
                        Log.Error("Something went wrong {Error}", contextFeature.Error);

                        Log.Error(contextFeature.Error, contextFeature.Error.StackTrace);

                        switch (context.Response.StatusCode)
                        {
                            case (int)HttpStatusCode.BadRequest:
                                {
                                    var err = new GenericResponse<ErrorDetails>
                                    {
                                        ResponseCode = "400",
                                        ResponseMessage = contextFeature.Error.Message
                                    };
                                    await context.Response.WriteAsync(JsonConvert.SerializeObject(err));
                                    break;
                                }
                            case (int)HttpStatusCode.NotFound:
                                {
                                    var err = new GenericResponse<ErrorDetails>
                                    {
                                        ResponseCode = "404",
                                        ResponseMessage = contextFeature.Error.Message
                                    };
                                    await context.Response.WriteAsync(JsonConvert.SerializeObject(err));
                                    break;
                                }
                            case (int)HttpStatusCode.Unauthorized:
                                {
                                    GenericResponse<ErrorDetails> err;
                                    if (contextFeature.Error.Message.Contains("40"))
                                    {
                                        err = new GenericResponse<ErrorDetails>
                                        {
                                            ResponseCode = "410",
                                            ResponseMessage = "Expired Token"
                                        };
                                    }
                                    else
                                    {
                                        err = new GenericResponse<ErrorDetails>
                                        {
                                            ResponseCode = "401",
                                            ResponseMessage = "Unauthorized access attempt"
                                        };
                                    }

                                    await context.Response.WriteAsync(JsonConvert.SerializeObject(err));
                                    break;
                                }
                            case (int)HttpStatusCode.Conflict:
                                {
                                    var err = new GenericResponse<ErrorDetails>
                                    {
                                        ResponseCode = "409",
                                        ResponseMessage = contextFeature.Error.Message
                                    };
                                    await context.Response.WriteAsync(JsonConvert.SerializeObject(err));
                                    break;
                                }
                            case (int)HttpStatusCode.NoContent:
                                {
                                    var err = new GenericResponse<ErrorDetails>
                                    {
                                        ResponseCode = "204",
                                        ResponseMessage = contextFeature.Error.Message
                                    };
                                    await context.Response.WriteAsync(JsonConvert.SerializeObject(err));
                                    break;
                                }
                            default:
                                {
                                    var err = new GenericResponse<ErrorDetails>
                                    {
                                        ResponseCode = "500",
                                        ResponseMessage = "An error occurred, our team is looking into it"
                                    };
                                    await context.Response.WriteAsync(JsonConvert.SerializeObject(err));
                                    break;
                                }
                        }
                    }
                });
            });
        }
    }
}
