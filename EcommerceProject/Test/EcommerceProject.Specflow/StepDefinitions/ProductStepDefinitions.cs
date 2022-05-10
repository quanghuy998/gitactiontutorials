using EcommerceProject.API;
using EcommerceProject.API.Dtos;
using EcommerceProject.Infrastructure.Database;
using EcommerceProject.Specflow.Core;
using EcommerceProject.Specflow.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace EcommerceProject.Specflow.StepDefinitions
{
    [Binding]
    internal class ProductStepDefinitions : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private Table _table;
        private HttpResponseMessage _response;
        private readonly ScenarioContext _scenarioContext;

        public ProductStepDefinitions(CustomWebApplicationFactory<Startup> factory, ScenarioContext scenarioContext)
        {
            _client = factory.CreateClient();
            _scenarioContext = scenarioContext;
        }

        [Given(@"The following product dataset")]
        public void GivenTheFollowingProductDataset(Table table)
        {
            _table = table;
        }

        [When(@"Admin wants to create an product")]
        public async Task WhenAdminWantsToCreateAnProduct()
        {
            var row = _table.Rows.First();
            var request = new CreateProductRequest()
            {
                Name = row[1],
                Currency = row[3],
                MoneyValue = Convert.ToDecimal(row[2]),
                TradeMark = row[4],
                Origin = row[5],
                Discription = row[6]
            };

            string json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            await _client.PostAsync("/api/products", httpContent);
        }

        [Then(@"The product repository should has an product")]
        public async Task ThenTheProductRepositoryShouldHasAnProduct(Table table)
        {
            var row = table.Rows.First();
            var expectedProduct = new Product(row[1], new MoneyValue(Convert.ToDecimal(row[2]), row[3]), row[4], row[5], row[6]);

            var productId = Convert.ToInt32(row[0]);
            var response = await _client.GetAsync($"/api/products/{productId}");
            var result = response.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<Product>(result);

            product.Name.Should().Be(expectedProduct.Name);
            product.Price.Value.Should().Be(expectedProduct.Price.Value);
            product.Price.Currency.Should().Be(expectedProduct.Price.Currency);
            product.Origin.Should().Be(expectedProduct.Origin);
            product.TradeMark.Should().Be(expectedProduct.TradeMark);
            product.Discription.Should().Be(expectedProduct.Discription);
        }

        [Given(@"The product repository already exists the following products")]
        public async Task GivenTheProductRepositoryAlreadyExistsTheFollowingProducts(Table table)
        {
            _table = table;
            foreach (var row in table.Rows)
            {
                var request = new CreateProductRequest()
                {
                    Name = row[1],
                    Currency = row[3],
                    MoneyValue = Convert.ToDecimal(row[2]),
                    TradeMark = row[4],
                    Origin = row[5],
                    Discription = row[6]
                };

                string json = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                await _client.PostAsync("/api/products", httpContent);
            }
        }

        [When(@"User wants to get all products")]
        public async Task WhenUserWantsToGetAllProducts()
        {
            _response = await _client.GetAsync("/api/products");
        }

        [Then(@"The product repository should return all products")]
        public void ThenTheProductRepositoryShouldReturnAllProducts(Table table)
        {
            var expectedProducts = new List<Product>();
            foreach (var row in table.Rows)
            {
                var product = new Product(row[1], new MoneyValue(Convert.ToDecimal(row[2]), row[3]), row[4], row[5], row[6]);
                product.Id = Convert.ToInt32(row[0]);
                expectedProducts.Add(product);
            }

            var result = _response.Content.ReadAsStringAsync().Result;
            var products = JsonConvert.DeserializeObject<List<Product>>(result);

            products.Count.Should().Be(expectedProducts.Count);
        }

        [When(@"User wants to get detais of an product with id (.*)")]
        public async Task WhenUserWantsToGetDetaisOfAnProductWithId(int productId)
        {
            _response = await _client.GetAsync($"/api/products/{productId}");
        }

        [Then(@"The product repository should return required product dataset")]
        public void ThenTheProductRepositoryShouldReturnRequiredProductDataset(Table table)
        {
            var row = table.Rows.First();
            var expectedProduct = new Product(row[1], new MoneyValue(Convert.ToDecimal(row[2]), row[3]), row[4], row[5], row[6]);
            expectedProduct.Id = Convert.ToInt32(row[0]);

            var result = _response.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<Product>(result);

            product.Id.Should().Be(expectedProduct.Id);
            product.Name.Should().Be(expectedProduct.Name);
            product.Price.Value.Should().Be(expectedProduct.Price.Value);
            product.Price.Currency.Should().Be(expectedProduct.Price.Currency);
            product.Origin.Should().Be(expectedProduct.Origin);
            product.TradeMark.Should().Be(expectedProduct.TradeMark);
            product.Discription.Should().Be(expectedProduct.Discription);
        }

        [When(@"User wants to update product with id (.*) according to the following dataset")]
        public async Task WhenUserWantsToUpdateProductWithIdAccordingToTheFollowingDataset(int productId, Table table)
        {
            _table = table;
            var row = table.Rows.First();
            var request = new UpdateProductRequest()
            {
                Name = row[1],
                Currency = row[3],
                Value = Convert.ToDecimal(row[2]),
                TradeMark = row[4],
                Origin = row[5],
                Discription = row[6]
            };

            string json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PutAsync($"/api/products/{productId}", httpContent);
        }

        [Then(@"In the product repository the data of the product is (.*) is changed")]
        public async Task ThenInTheProductRepositoryTheDataOfTheProductIsIsChanged(int productId)
        {
            var row = _table.Rows.First();
            var expectedProduct = new Product(row[1], new MoneyValue(Convert.ToDecimal(row[2]), row[3]), row[4], row[5], row[6]);

            var response = await _client.GetAsync($"/api/products/{productId}");
            var result = response.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<Product>(result);

            product.Name.Should().Be(expectedProduct.Name);
            product.Price.Value.Should().Be(expectedProduct.Price.Value);
            product.Price.Currency.Should().Be(expectedProduct.Price.Currency);
            product.Origin.Should().Be(expectedProduct.Origin);
            product.TradeMark.Should().Be(expectedProduct.TradeMark);
            product.Discription.Should().Be(expectedProduct.Discription);
        }


        [When(@"User wants to delete product with id (.*)")]
        public async Task WhenUserWantsToDeleteProductWithId(int productId)
        {
            await _client.DeleteAsync($"/api/products/{productId}");
        }

        [Then(@"There is no product with id (.*) and the return status is (.*)")]
        public async Task ThenThereIsNoProductWithIdAndTheReturnStatusIs(int productId, string status)
        {
            var response = await _client.GetAsync($"/api/products/{productId}");
            response.StatusCode.ToString().Should().Be(status);
        }

    }
}
