using LiquorStoreApi.Exceptions;
using LiquorStoreApi.Wrappers;
using System.Net;
using System.Text.Json;

namespace LiquorStoreApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {

        private readonly RequestDelegate _request;


        public ErrorHandlerMiddleware(RequestDelegate request)
        {
            this._request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this._request(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<object>();

                switch (exception)
                {
                    case ApiExceptions ex:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Succeded = false;
                        responseModel.Message = ex.Message;

                        break;
                    case UserNotFoundException ex:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Succeded = false;
                        responseModel.Message = ex.Message;

                        break;
                    case ProductNotFoundException ex:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Succeded = false;
                        responseModel.Message = ex.Message;

                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);

                await context.Response.WriteAsync(result);
            }
        }
    }
}
