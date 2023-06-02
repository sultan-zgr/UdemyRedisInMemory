using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private IDistributedCache _distributedCache;
        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
           
        }
        public async Task <IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

            cacheEntryOptions.AbsoluteExpiration=DateTime.UtcNow.AddMinutes(10);

            _distributedCache.SetString("name", "Sultan", cacheEntryOptions);
              
           
           

            return View();
        }
        public IActionResult Show() 
        {
            string name = _distributedCache.GetString("name");
            ViewBag.name = name;
            return View();
        }
        public IActionResult Remove() 
        {
            _distributedCache.Remove("name");
            return View();
        }
    }
}
