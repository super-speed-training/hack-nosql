using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSampleApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleTest()
        {
            return View();
        }

        private static readonly string[] coins =
        new [] {
            "BTC",
            "ETH",
            "XRP",
            "BCH",
            "EOS",
            "XLM",
            "LTC",
            "USDT",
            "ADA",
            "XMR",
            "TRX",
            "DASH",
            "BNB",
            "NEO",
            "ETC",
            "XEM",
        };

        [HttpPost]
        public IActionResult SimpleTest(string url)
        {
            return Content(string.Join(',', coins));
        }
    }
}
