using EcommerceProject.API;
using EcommerceProject.IntergrationTest.Helpers;
using EcommerceProject.IntergrationTest.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceProject.IntergrationTest.IntergrationTest
{
    public class ProductControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ProductControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenInformation_WhenGettingAllProduct_ThenItShoudBeReturnSeedingProduct()
        {
            var seedingProduct = Utilities.GetSeedingProduct();

            HttpResponseMessage response = await _client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;
            var products = JsonConvert.DeserializeObject<List<Product>>(result);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(seedingProduct.Count, products.Count);
        }
    }
}
