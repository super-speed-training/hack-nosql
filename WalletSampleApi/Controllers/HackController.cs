using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WalletSampleApi.Models;

namespace WalletSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "jdoe", "ptparker" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<CustomerWallet> Get(string id)
        {
            return new CustomerWallet
            {
                Username = "jdoe",
                Coins = new List<CustomerCoin>
                {
                    new CustomerCoin
                    {
                        Symbol = "BTC",
                        BuyingRate = 6565.25,
                        BuyingAt = new DateTime(2018, 10, 9, 9, 32, 23),
                        USDValue = 6500
                    },
                    new CustomerCoin
                    {
                        Symbol = "ETH",
                        BuyingRate = 203.47,
                        BuyingAt = new DateTime(2018, 9, 7, 12, 38, 33),
                        USDValue = 200.23
                    },
                },
            };
        }

        private const string connectionString =
@"mongodb://hacknosql:hdphCdfxatVPskEVblpNklEto8v0zIPdRZ3gukfFf3QKlS958O0DoJFOtKTzBkW9KWvT70QpoJxe9lmMJlGiVQ==@hacknosql.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";

        // POST api/values
        [HttpPost]
        public void Post([FromBody] CoinPriceUpdate updateCoin)
        {
            var mclient = new MongoClient(connectionString);
            var db = mclient.GetDatabase("hack");
            var col = db.GetCollection<CoinPriceDB>("prices");

            col.InsertOne(new CoinPriceDB
            {
                _id = DateTime.Now.Ticks.ToString(),
                At = updateCoin.At,
                PriceList = updateCoin.PriceList,
            });
        }
    }
}
