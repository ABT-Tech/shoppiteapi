using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shoppite.Application.Commands;
using Shoppite.Application.Queries;
using Shoppite.Application.Responses;
using Shoppite.Core.DTOs;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly IProductRepository _productRepository;
        public ProductsController(IMediator mediator, IConfiguration config, IProductRepository productRepository)
        {
            _mediator = mediator;
            _config = config;
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetAllProductsByOrganizations(int org_Id, int? UserId)
        {
            return await _mediator.Send(new GetAllProductsByOrganizationsQuery(org_Id, UserId));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetMostSellerProductsByOrganizations(int? org_Id, int? UserId,int OrgCategoryId)
        {
            return await _mediator.Send(new GetProductsByBestSellers(org_Id, "Best Deals", OrgCategoryId));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetLastVisitedProductsByOrganizations(int org_Id, int? UserId)
        {
            return await _mediator.Send(new GetAllProductsByOrganizationsQuery(org_Id, UserId));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetWishlistByUser(int org_Id, int user_Id)
        {
            return await _mediator.Send(new GetWishlistByUserQuery(org_Id, user_Id));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> SearchProducts(int org_Id, string productname)
        {
            return await _mediator.Send(new SearchProduct(org_Id, productname));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetRecentlyViewedProductsByCategory(int org_Id, string IP)
        {
            return await _mediator.Send(new GetRecentlyViewedProductsByCategory(org_Id, IP));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetMostViewedProductsByCategory(int org_Id, int CategoryId, string IP)
        {
            return await _mediator.Send(new GetMostViewedProductsByCategory(org_Id, CategoryId, IP));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetProductsByBestSellers(int? org_Id, string type,int OrgICategoryd)
        {
            return await _mediator.Send(new GetProductsByBestSellers(org_Id, type, OrgICategoryd));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetAllProductsByCategory(int OrgId, int CategoryId)
        {
            return await _mediator.Send(new GetProductsByCategory(OrgId, CategoryId));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> UpdateProductDetail([FromBody] UpdateProductDetail productDetails)
        {
            return await _mediator.Send(new UpdateProductDetails(productDetails));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetAllProductDetailsForVendor(int OrgId, int Id)
        {
            return await _mediator.Send(new GetAllProductForVendor(OrgId, Id));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetSimilarProducts(int OrgId, int CategoryId, int BrandId, int? UserId)
        {
            return await _mediator.Send(new GetSimilarProducts(OrgId, CategoryId, BrandId,UserId));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetProductVariation(int OrgId, Guid ProductGUId)
        {
            return await _mediator.Send(new GetProductVariationQuery(OrgId, ProductGUId));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetProductDetailsBySpecifcation(int OrgId, Guid ProductGUID, int? SpecificationId, int? UserId)
        {
            return await _mediator.Send(new GetProductDetailsBySpecification(OrgId, ProductGUID, SpecificationId, UserId));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetProductsImages()
        {
            var orglist = await _mediator.Send(new GetAllOrganizationQuery(null));
            orglist = orglist.Where(x => x.IsPublished == true).ToList();
            if (orglist != null)
            {
                foreach (var org in orglist)
                {
                    List<ProductResponse> productResponses = new List<ProductResponse>();
                    productResponses = await _mediator.Send(new GetAllProductsByOrganizationsQuery(org.OrgId, null));
                    foreach (var product in productResponses)
                    {
                        try
                        {
                            var image = product.Image;
                            Uri uri = new Uri(image);
                            var newImageFile = RestoreImage(org, uri);
                            _productRepository.UpdateProductImage(image, newImageFile);
                            foreach (var otherimages in product.ProductList)
                            {
                                try
                                {
                                    uri = new Uri(otherimages);
                                    var newOtherImage = RestoreImage(org, uri);
                                    _productRepository.UpdateProductOtherImage(otherimages, newOtherImage);
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            return null;
        }

        private string RestoreImage(OrganizationResponse org, Uri uri)
        {
            var extention = System.IO.Path.GetExtension(uri.LocalPath);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);
            string rootPath = _config.GetSection("CurrentFilePath").Value;
            string destinationPath = _config.GetSection("DestinationFilePath").Value;
            string fileName = System.IO.Path.Combine(rootPath, filename);
            if (System.IO.File.Exists(fileName))
            {
                var dest = org.OrgId.ToString() + "\\Products\\";
                if (!System.IO.Directory.Exists(destinationPath + "\\" + dest))
                {
                    System.IO.Directory.CreateDirectory(destinationPath + "\\" + dest);
                }
                dest = dest + org.ShopName.ToString() + "_Product_" + DateTime.Now.Ticks + extention;
                destinationPath = destinationPath + "\\" + dest;
                System.IO.File.Move(fileName, destinationPath);
                return "https://markets.shooppy.in/images/Markets/" + dest.Replace("\\", "/");
            }
            return null;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetAllProducts(int? OrgId, int? OrgCategoryId)
        {
            return await _mediator.Send(new GetAllProductsQuery(OrgId,OrgCategoryId));
        }
    }
}
