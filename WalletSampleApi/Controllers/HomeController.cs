using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using WalletSampleApi.Models;
using System.Net.Http;
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

        private static readonly Dictionary<string, double> coins =
        new Dictionary<string, double> {
            { "BTC", 6488 },
            { "ETH", 205 },
            { "XRP", 0.46 },
            { "BCH", 443 },
            { "EOS", 5.4 },
            { "XLM", 0.24 },
            { "LTC", 53.25 },
            { "USDT", 0.98 },
            { "ADA", 0.076 },
            { "XMR", 104 },
            { "TRX", 0.024 },
            { "DASH", 155 },
            { "BNB", 9.76 },
            { "NEO", 16.54 },
            { "ETC", 9.66 },
            { "XEM", 0.094 },
        };

        [HttpPost]
        public async Task<IActionResult> SimpleTest(string url, bool all)
        {
            var clen = coins.Count;
            var coinList = new List<(string, double)>();
            var rdm = new Random();

            foreach (var c in coins)
            {
                if (all || rdm.Next(10) < 7)
                {
                    coinList.Add((c.Key, c.Value));
                }
            }

            var priceUpdate = new CoinPriceUpdate
            {
                At = DateTime.Now,
                PriceList = new List<CoinPrice>(),
            };

            var plist = priceUpdate.PriceList;

            foreach (var (sym,  pri) in coinList)
            {
                var buy = pri + (rdm.NextDouble() - 0.5) * 0.24 * pri;
                var sell = buy - Math.Max(0.03, rdm.NextDouble() * 0.12) * pri;
                plist.Add(new CoinPrice
                {
                    Symbol = sym,
                    Buy = buy,
                    Sell = sell,
                });
            }

            var hcli = new HttpClient();
            await hcli.PostAsJsonAsync<CoinPriceUpdate>(url, priceUpdate);

            ViewData["msg"] = string.Format("Processed data @ {0}", DateTime.Now);

            return View();
        }
    }
}
