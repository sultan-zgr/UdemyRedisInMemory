﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {

            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            options.SlidingExpiration = TimeSpan.FromSeconds(10);
            options.Priority = CacheItemPriority.High;
            //options.RegisterPostEvictionCallback(PostEvictionDelegate (key, value, reason, state) => _memoryCache.Set("callback", $"{key} -> {value} => sebep:{reason}"));

            _memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);

            return View();
        }
        public IActionResult Show()
        {
            _memoryCache.TryGetValue("zaman", out string zamancache);
            //_memoryCache.TryGetValue("callback", out string callback);
            ViewBag.zaman = zamancache;
            //ViewBag.callback = callback;
            return View();
        }
    }
}
