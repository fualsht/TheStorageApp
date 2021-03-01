using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheStorageApp.API.Data;

namespace TheStorageApp.API.Controllers
{
    public abstract class APIControllerBase<T> : ControllerBase
    {
        protected readonly ILogger<APIControllerBase<T>> _logger;
        protected readonly DataContext _dataContext;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public APIControllerBase(ILogger<APIControllerBase<T>> logger, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
