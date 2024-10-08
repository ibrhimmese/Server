﻿using Application.ExceptionTypes;
using Infrastructure.Exceptions.Extensions;
using Infrastructure.Exceptions.HttpProblemDetails;
using Infrastructure.Exceptions.Types;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Exceptions.Handlers;

public class HttpExceptionHandler : ExceptionHandler
{
    private HttpResponse? _response;
    public HttpResponse Response
    {
        get => _response ?? throw new ArgumentNullException(nameof(_response));
        set => _response = value;
    }
    protected override Task HandleException(BusinessException businessException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new BusinessProblemDetails(businessException.Message).AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(Exception exception)
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        string details = new InternalServerErrorProblemDetails(exception.Message).AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(ValidationException validationException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new ValidationProblemDetails(validationException.Errors).AsJson();
        return Response.WriteAsync(details);
    }
    protected override Task HandleException(AuthorizationException authorizationException)
    {
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        string details = new AuthorizationProblemDetails(authorizationException.Message).AsJson();
        return Response.WriteAsync(details);
    }
    protected override Task HandleException(AuthenticationException authenticationException)
    {
        Response.StatusCode = StatusCodes.Status403Forbidden;
        string details = new AuthenticationProblemDetails(authenticationException.Message).AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(NotFoundException notFoundException)
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        string details = new NotFoundProblemDetails(notFoundException.Message).AsJson();
        return Response.WriteAsync(details);
    }
    protected override Task HandleException(OperationException operationException)
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        string details = new OperationExceptionProblemDetails(operationException.Message).AsJson();
        return Response.WriteAsync(details);
    }

}
