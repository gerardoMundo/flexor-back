using FlexorTest.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FlexorTest.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // POST api/products
        [HttpPost]
        public ActionResult Post([FromBody] List<Product> products)
        {
            if(products.Count == 0 || products == null)
            {
                return BadRequest("No se agregaron productos.");
            }

            var subTotal = 0;
            decimal discount = 0;


            foreach (var product in products)
            {
                subTotal += product.Price * product.Quantity;
                discount += product.Discount;
            }

            decimal iva = (subTotal * 13) / 100;
            decimal total = (subTotal + iva) - discount;

            var response = new
            {
                Subtotal = subTotal,
                Iva = iva,
                Descuento = discount,
                Total = total,
            };

            return Ok(response);
        }
    }
}
