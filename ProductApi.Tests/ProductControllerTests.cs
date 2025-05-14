using Xunit;
using ProductApi.Controllers;
using ProductApi.Models;
using ProductApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _controller;
        private readonly IProductService _service;

        public ProductControllerTests()
        {
            _service = new ProductService();
            _controller = new ProductController(_service);
        }

        [Fact]
        public void GetAll_ReturnsEmptyInitially()
        {
            var result = _controller.GetAll() as OkObjectResult;
            var products = result?.Value as IEnumerable<Product>;
            Assert.Empty(products);
        }

        [Fact]
        public void Add_Product_ReturnsCreated()
        {
            var product = new Product { Name = "Test", Price = 100 };
            var result = _controller.Add(product) as CreatedAtActionResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void Get_ExistingProduct_ReturnsOk()
        {
            var product = new Product { Name = "Test", Price = 50 };
            _controller.Add(product);
            var result = _controller.Get(1) as OkObjectResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void Get_NonExistentProduct_ReturnsNotFound()
        {
            var result = _controller.Get(999);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_ValidProduct_ReturnsNoContent()
        {
            var product = new Product { Name = "Old", Price = 10 };
            _controller.Add(product);
            product.Name = "New";
            var result = _controller.Update(1, product);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Update_IdMismatch_ReturnsBadRequest()
        {
            var product = new Product { Id = 1, Name = "Mismatch", Price = 10 };
            var result = _controller.Update(2, product);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Delete_ExistingProduct_ReturnsNoContent()
        {
            var product = new Product { Name = "ToDelete", Price = 20 };
            _controller.Add(product);
            var result = _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_NonExistentProduct_ReturnsNoContent()
        {
            var result = _controller.Delete(999);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetAll_AfterAdding_ReturnsOne()
        
            _controller.Add(new Product { Name = "Prod1", Price = 5 });
            var result = _controller.GetAll() as OkObjectResult;
            var products = result?.Value as IEnumerable<Product>;
            Assert.Single(products);
        }

        [Fact]
        public void Add_MultipleProducts_IncrementsId()
        {
            _controller.Add(new Product { Name = "P1", Price = 1 });
            _controller.Add(new Product { Name = "P2", Price = 2 });
            var result = _controller.Get(2) as OkObjectResult;
            var product = result?.Value as Product;
            Assert.Equal(2, product?.Id);
        }

        //Add 5 More Test Cases

        // [Fact]

        // public void Add_Product_WithZeroPrice_ShouldBeAccepted()
        // {
        //     var product = new Product { Name = "FreeItem", Price = 0 };
        //     _controller.Add(product);
        //     var result = _controller.Get(product.Id) as OkObjectResult;
        //     Assert.NotNull(result);
        //     Assert.Equal(0, ((Product)result.Value!).Price);

        // }
 
        // [Fact]

        // public void Add_Product_WithLongName_ShouldBeAccepted()

        // {

        //     var longName = new string('A', 1000);

        //     var product = new Product { Name = longName, Price = 10 };

        //     _controller.Add(product);

        //     var result = _controller.Get(product.Id) as OkObjectResult;

        //     Assert.Equal(longName, ((Product)result?.Value!).Name);

        // }
 
        // [Fact]

        // public void GetAll_WhenEmpty_ReturnsIEnumerable()

        // {

        //     var result = _controller.GetAll() as OkObjectResult;

        //     Assert.IsAssignableFrom<IEnumerable<Product>>(result?.Value);

        // }
 
        // [Fact]

        // public void Delete_ThenUpdate_ReturnsNoContentButNoChange()

        // {

        //     var product = new Product { Name = "Temp", Price = 5 };

        //     _controller.Add(product);

        //     _controller.Delete(product.Id);

        //     var updateResult = _controller.Update(product.Id, product);

        //     Assert.IsType<NoContentResult>(updateResult);

        // }
 
        // [Fact]

        // public void Add_ThenGetAll_ShouldReturnCorrectProduct()

        // {

        //     var product = new Product { Name = "Unique", Price = 11 };

        //     _controller.Add(product);

        //     var result = _controller.GetAll() as OkObjectResult;

        //     var list = result?.Value as IEnumerable<Product>;

        //     Assert.Contains(list!, p => p.Name == "Unique");

        // }
                //Add more test cases 

        [Fact]

        public void Add_ReturnsCorrectResult()

        {

            // Act

            var result = _controller.Add(5, 3) as OkObjectResult;
 
            // Assert

            Assert.NotNull(result);

            Assert.Equal(200, result.StatusCode);

            Assert.Equal(8.0, result.Value);

        }
 
        [Fact]

        public void Subtract_ReturnsCorrectResult()

        {

            var result = _controller.Subtract(10, 4) as OkObjectResult;
 
            Assert.NotNull(result);

            Assert.Equal(200, result.StatusCode);

            Assert.Equal(6.0, result.Value);

        }
 
        [Fact]

        public void Divide_ReturnsCorrectResult()

        {

            var result = _controller.Divide(20, 5) as OkObjectResult;
 
            Assert.NotNull(result);

            Assert.Equal(200, result.StatusCode);

            Assert.Equal(4.0, result.Value);

        }
 
        [Fact]

        public void Divide_ByZero_ReturnsBadRequest()

        {

            var result = _controller.Divide(10, 0) as BadRequestObjectResult;
 
            Assert.NotNull(result);

            Assert.Equal(400, result.StatusCode);

            Assert.Equal("Cannot divide by zero.", result.Value);

        }
 
        // Add 10 more tests
        [Fact] 
        public void Add_Product_SetsIdCorrectly() { var p = new Product { Name = "A", Price = 1 }; _controller.Add(p); Assert.Equal(1, p.Id); }
        [Fact] 
        public void Add_ThenDelete_Product_NotFoundOnGet() { var p = new Product { Name = "B", Price = 1 }; _controller.Add(p); _controller.Delete(p.Id); var res = _controller.Get(p.Id); Assert.IsType<NotFoundResult>(res); }
        [Fact] 
        public void Update_Product_ChangesName() { var p = new Product { Name = "X", Price = 5 }; _controller.Add(p); p.Name = "Y"; _controller.Update(p.Id, p); var res = (_controller.Get(p.Id) as OkObjectResult)?.Value as Product; Assert.Equal("Y", res?.Name); }
        [Fact] 
        public void Add_NullName_StillAdds() { var p = new Product { Name = null, Price = 5 }; _controller.Add(p); var res = _controller.Get(p.Id) as OkObjectResult; Assert.NotNull(res); }
        [Fact] 
        public void Add_Product_WithNegativePrice() { var p = new Product { Name = "N", Price = -1 }; _controller.Add(p); var res = _controller.Get(p.Id) as OkObjectResult; Assert.Equal(-1, ((Product)res?.Value!).Price); }
        [Fact]
        public void Delete_EmptyList_NoCrash() { var result = _controller.Delete(1); Assert.IsType<NoContentResult>(result); }
        [Fact]
        public void GetAll_MultipleProducts_ReturnsCorrectCount() { _controller.Add(new Product { Name = "1", Price = 1 }); _controller.Add(new Product { Name = "2", Price = 2 }); var result = _controller.GetAll() as OkObjectResult; Assert.Equal(2, ((IEnumerable<Product>)result!.Value!).Count()); }
        [Fact]
        public void Update_NonExistent_DoesNothing() { var p = new Product { Id = 99, Name = "Ghost", Price = 0 }; var result = _controller.Update(99, p); Assert.IsType<NoContentResult>(result); }
        [Fact]
        public void Add_Product_CheckPrice() { var p = new Product { Name = "P", Price = 999.99M }; _controller.Add(p); var res = (_controller.Get(p.Id) as OkObjectResult)?.Value as Product; Assert.Equal(999.99M, res?.Price); }
        [Fact]
        public void Add_DuplicateNames_Allowed() { _controller.Add(new Product { Name = "Same", Price = 1 }); _controller.Add(new Product { Name = "Same", Price = 2 }); var result = _controller.GetAll() as OkObjectResult; Assert.Equal(2, ((IEnumerable<Product>)result!.Value!).Count()); }
    }
}
