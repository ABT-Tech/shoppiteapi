using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Commands;
using Shoppite.Application.Queries;
using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Core.DTOs.BestBuyProducts> GetProductsForBestBuy()
        {
            BestBuyProducts bestBuyProducts = new BestBuyProducts();
            bestBuyProducts.product_DTOs1 = new List<Product_DTO>();
            
            bestBuyProducts.product_DTOs2 = new List<Product_DTO>();
            
            var allProductsForBestBuy = await _mediator.Send(new GetAllProductQuery());
            for (int i = 0; i <= 5; i++)
            {
                var objproduct_DTOs1 = new Product_DTO();
                objproduct_DTOs1.id = allProductsForBestBuy[i].id;
                objproduct_DTOs1.org_id = allProductsForBestBuy[i].org_id;
                objproduct_DTOs1.sub_ctg_id = allProductsForBestBuy[i].sub_ctg_id;
                objproduct_DTOs1.category_id = allProductsForBestBuy[i].category_id;
                objproduct_DTOs1.company_name = allProductsForBestBuy[i].company_name;
                objproduct_DTOs1.product_name = allProductsForBestBuy[i].product_name;
                objproduct_DTOs1.product_description = allProductsForBestBuy[i].product_description;
                objproduct_DTOs1.product_code = allProductsForBestBuy[i].product_code;
                objproduct_DTOs1.product_price = allProductsForBestBuy[i].product_price;
                objproduct_DTOs1.product_discount = allProductsForBestBuy[i].product_discount;
                objproduct_DTOs1.product_quantity = allProductsForBestBuy[i].product_quantity;
                objproduct_DTOs1.product_image = allProductsForBestBuy[i].product_image;
                objproduct_DTOs1.is_available = allProductsForBestBuy[i].is_available;
                bestBuyProducts.product_DTOs1.Add(objproduct_DTOs1);
            }
            for (int j = 6; j <= 11; j++)
            {
                var objproduct_DTOs2 = new Product_DTO();
                objproduct_DTOs2.id = allProductsForBestBuy[j].id;
                objproduct_DTOs2.org_id = allProductsForBestBuy[j].org_id;
                objproduct_DTOs2.sub_ctg_id = allProductsForBestBuy[j].sub_ctg_id;
                objproduct_DTOs2.category_id = allProductsForBestBuy[j].category_id;
                objproduct_DTOs2.company_name = allProductsForBestBuy[j].company_name;
                objproduct_DTOs2.product_name = allProductsForBestBuy[j].product_name;
                objproduct_DTOs2.product_description = allProductsForBestBuy[j].product_description;
                objproduct_DTOs2.product_code = allProductsForBestBuy[j].product_code;
                objproduct_DTOs2.product_price = allProductsForBestBuy[j].product_price;
                objproduct_DTOs2.product_discount = allProductsForBestBuy[j].product_discount;
                objproduct_DTOs2.product_quantity = allProductsForBestBuy[j].product_quantity;
                objproduct_DTOs2.product_image = allProductsForBestBuy[j].product_image;
                objproduct_DTOs2.is_available = allProductsForBestBuy[j].is_available;
                bestBuyProducts.product_DTOs2.Add(objproduct_DTOs2);
            }
            return bestBuyProducts;
        }

        [HttpGet]
        [Route("{category_id}/{sub_category_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Product_DTO>> GetProductlist(int category_id, int sub_category_id)
        {
            return await _mediator.Send(new GetProductListQuery(category_id, sub_category_id));
        }

        [HttpGet]
        [Route("{category_id}/{sub_category_id}/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Product_DTO>> GetProductdisc(int category_id, int sub_category_id, int product_id)
        {
            return await _mediator.Send(new GetProducDiscQuery(category_id, sub_category_id, product_id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Core.DTOs.CartProduct> AddToCart(CartProduct cartProduct)
        {
            return await _mediator.Send(new CreateAddToCartCommand(cartProduct));
        }

        [HttpGet]
        [Route("{org_id}/{user_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.CartProduct>> GetCartList(int org_id, int user_id)
        {
            return await _mediator.Send(new GetAllCartProductQuery(org_id, user_id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Core.DTOs.WishList_DTO> PostWishList(WishList_DTO wishList_DTO)
        {
            return await _mediator.Send(new CreateWishListCommand(wishList_DTO));
        }

        [HttpGet]
        [Route("{org_id}/{user_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.WishList_DTO>> GetWishList(int org_id, int user_id)
        {
            return await _mediator.Send(new GetAllWishListQuery(org_id, user_id));
        }

        [HttpGet]
        [Route("{org_id}/{user_id}/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.WishList_DTO>> DeleteWishList(int org_id, int user_id, int id)
        {
            return await _mediator.Send(new DeleteWishListQuery(org_id, user_id, id));
        }

        [HttpGet]
        [Route("{org_id}/{user_id}/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.CartProduct>> DeleteCartList(int org_id, int user_id, int id)
        {
            return await _mediator.Send(new DeleteCartListQuery(org_id, user_id, id));
        }
    }
}
