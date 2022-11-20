using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Uppgift_2.Contexts;
using Uppgift_2.Models;

namespace Uppgift_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category req)
        {
            var newCategory = new CategoryEntity
            {
                id = Guid.NewGuid().ToString(),
                Name = req.Name,
            };

            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == req.Name);

                if (category != null) return new BadRequestObjectResult(new ErrorHandler { Message = "Could not find a category with that name", Error = "Could not create category" });

                

                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();


            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not create category"});
            }



            return new OkObjectResult(newCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var categories = new List<CategoryEntity>();
            try
            {
                foreach(var category in await _context.Categories.ToListAsync())
                {
                    categories.Add(category);
                }

                return new OkObjectResult(categories);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not get categories" });
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.id == id);

                if (category == null) return new BadRequestObjectResult(new ErrorHandler { Message = "Could not find a category with that id", Error = "Could not get category" });

                var products = new List<ProductResponse>();

                foreach(var product in await _context.Products.ToListAsync())
                {
                   if(product.CategoryId == id)
                    {
                        products.Add(new ProductResponse
                        {
                            id = product.id,
                            Name = product.Name,
                            Price = product.Price,
                            Description = product.Description,
                            ArtNo = product.ArtNo,
                            TechnicalSpecifications = product.TechnicalSpecifications,
                        });
                    }
                }





                return new OkObjectResult(new CategoryResponse
                {
                    id = category.id,
                    Name = category.Name,
                    Products = products
                });
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not get category" });
            }




        }
    }
}
