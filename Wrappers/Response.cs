namespace LiquorStoreApi.Wrappers
{
    public class Response<O>
    {
        public bool Succeded { get; set; }
        public O? Message { get; set; }

        public Response()
        {
            
        }

        public Response(bool succeded,O message)
        {
            this.Succeded = succeded;
            this.Message = message;
        }

    }
}
