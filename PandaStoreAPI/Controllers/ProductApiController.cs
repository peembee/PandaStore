using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PandaStore.Models.DTO;
using PandaStore.Data;
using PandaStore.Models;
using Microsoft.EntityFrameworkCore;

namespace PandaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        public readonly IMapper mapper;
        private readonly PandaStoreContext pandaContext;
        public ProductApiController(PandaStoreContext context, IMapper mapper)
        {
            pandaContext = context;
            this.mapper = mapper;
        }


        // GET: api/<Products>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            IEnumerable<Product> productList = await pandaContext.Products.ToListAsync();

            var products = productList.Select(p => new ProductDTO
            {
                FK_CategoryID = p.FK_CategoryID,
                ProductTitel = p.ProductTitel,
                Description = p.Description,
                Specification = p.Specification,
                Price = p.Price,
                InventoryQuantity = p.InventoryQuantity,
                ProductIsActive = p.ProductIsActive,
                NextDelivery = p.NextDelivery
            }).ToList();

            //var products = mapper.Map<IEnumerable<ProductDTO>>(productList);
            return Ok(products);
        }
    }
}
