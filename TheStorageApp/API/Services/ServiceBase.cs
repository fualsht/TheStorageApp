using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStorageApp.API.Data;

namespace TheStorageApp.API.Services
{
    public abstract class ServiceBase<T> : IService<T>
    {
        protected readonly ILogger<ServiceBase<T>> _logger;
        protected readonly DataContext _dataContext;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceBase(ILogger<ServiceBase<T>> logger, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
