using TM.WebServer.Entities;

namespace TM.WebServer.Factories
{
    public static class ResponseFactory
    {
        public static ResponseDTO<T> GoodResponse<T>(T response, int? count = null)
        {
            return new ResponseDTO<T>()
            {
                Data = response,
                Success = true,
                Count = count
            };
        }
    }
}
