namespace QRCodeGeneratorApp.Api.CustomMiddleware.CustomMiddlewareServices
{
    public interface IMyCustomMiddlewareScopedService
    {
        string GetInstanceId();
    }

    public class MyCustomMiddlewareScopedService : IMyCustomMiddlewareScopedService
    {
        private readonly string _instanceId;

        public MyCustomMiddlewareScopedService()
        {
            this._instanceId = Guid.NewGuid().ToString();
        }

        public string GetInstanceId()
        {
            return _instanceId;
        }
    }
}
