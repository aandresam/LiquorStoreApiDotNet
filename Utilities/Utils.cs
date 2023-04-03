using LiquorStoreApi.Wrappers;

namespace LiquorStoreApi.Utilities
{
    public class Utils
    {

        public static Response<object> InexpectedError(Exception ex)
        {
            return new Response<object>(false, $"Ocurrió un error inesperado: {ex.Message}");
        }
    }
}
