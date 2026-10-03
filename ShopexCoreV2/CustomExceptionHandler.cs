namespace ShopexCoreV2
{
    public class CustomExceptionHandler
    {
        private readonly IWebHostEnvironment _environment;
        public CustomExceptionHandler(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            var feature = httpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
            var error = feature?.Error;
            if (error != null)
            {
                // Log your exception;
            }
            await Task.CompletedTask;
        }
    }
}
