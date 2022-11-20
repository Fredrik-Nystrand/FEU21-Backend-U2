using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uppgift_2.Contexts;
using Uppgift_2.Models;

namespace Uppgift_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Create(Product req)
        {

            try
            {
                var category = await _context.Categories.FindAsync(req.CategoryId);
                if (category == null) return new BadRequestObjectResult(new ErrorHandler{ Message = "Could not find the category with the id: " + req.CategoryId, Error = "Could not create product" });

                var newProduct = new ProductEntity
                {
                    id = Guid.NewGuid().ToString(),
                    Name = req.Name,
                    Price = req.Price,
                    Description = req.Description,
                    ArtNo = req.ArtNo,
                    CategoryId = req.CategoryId,
                    Category = category,
                    TechnicalSpecifications = req.TechnicalSpecifications,
                };

                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();

                return new OkObjectResult(req);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler{ Message = e.Message, Error = "Could not create product" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var products = new List<ProductEntity>();
            try
            {
                foreach (var product in await _context.Products.ToListAsync())
                {
                    products.Add(product);
                }

                return new OkObjectResult(products);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not get products" });
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var product = await _context.Products.SingleOrDefaultAsync(x => x.id == id);
                if (product == null) return new BadRequestObjectResult(new ErrorHandler { Message = "Could not find the product with the id: " + id, Error = "Could not create product" });

                var category = await _context.Categories.FindAsync(product.CategoryId);
                if (category == null) return new BadRequestObjectResult(new ErrorHandler { Message = "Could not find the category with the id: " + product.CategoryId, Error = "Could not create product" });


                return new OkObjectResult(new ProductResponse
                {
                    id = product.id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    ArtNo = product.ArtNo,
                    Category = new CategoryResponse
                    {
                        id  = category.id,
                        Name = category.Name,
                    },
                    TechnicalSpecifications = product.TechnicalSpecifications,
                });
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not get product" });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ProductUpdateRequest req)
        {
            try
            {
                if (id != req.id) return new BadRequestObjectResult(new ErrorHandler { Message = "Id's do not match ", Error = "Could not update product" });

                var product = await _context.Products.FindAsync(id);
                if (product == null) return new BadRequestObjectResult(new ErrorHandler { Message = "Could not find the product with the id: " + id, Error = "Could not update product" });

                CategoryEntity category;

                if(req.CategoryId == product.CategoryId)
                {
                    category = await _context.Categories.FindAsync(product.CategoryId);
                    if (category == null) return new BadRequestObjectResult(new ErrorHandler { Message = "Could not find the category with the id: " + id, Error = "Could not update product" });
                }
                else
                {
                    category = await _context.Categories.FindAsync(req.CategoryId);
                    if (category == null) return new BadRequestObjectResult(new ErrorHandler { Message = "Could not find the category with the id: " + id, Error = "Could not update product" });
                }

                product.Name = req.Name;
                product.Price = req.Price;
                product.Description = req.Description;
                product.ArtNo = req.ArtNo;
                product.CategoryId = req.CategoryId;
                product.TechnicalSpecifications = req.TechnicalSpecifications;

                //_context.Products.Update(product);
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new OkObjectResult(new ProductResponse
                {
                    id = product.id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    ArtNo = product.ArtNo,
                    TechnicalSpecifications = product.TechnicalSpecifications,
                    Category = new CategoryResponse
                    {
                        id = category.id,
                        Name = category.Name,
                    }
                });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not get product" });
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null) return new BadRequestObjectResult(new ErrorHandler { Message = "Could not find the product with the id: " + id, Error = "Could not delete product" });

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not get product" });
            }

        }
    }
}
