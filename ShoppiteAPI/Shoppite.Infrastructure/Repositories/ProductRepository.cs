using Shoppite.Core.Repositories;
using Shoppite.Infrastructure.Data;
using Shoppite.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Shoppite.Core.DTOs;
using Shoppite.Core.Extensions;
using Shoppite.Core.Entities;
using Shoppite.Core.Model;
using ReadSharp;
using System.Diagnostics;
using static Shoppite.Core.DTOs.AllProductsDTO;

namespace Shoppite.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected readonly Shoppite_masterContext _MasterContext;
        public ProductRepository(Shoppite_masterContext dbContext)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<ProductsDTO>> GetAllProductsByOrganizations(int orgId = 0,int? UserId = null, int orgCat_ID = 0)
        {            
            List<ProductsDTO> productsDTOs = new List<ProductsDTO>();
           // MasterProductDTO masterProductDTO = new MasterProductDTO();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "GetAllProductsByOrganizations";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@Org_ID", orgId));
                command.Parameters.Add(new SqlParameter("@OrgCat_ID", orgCat_ID));
                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        ProductsDTO productsDTO = new  ProductsDTO();
                        productsDTO.Id = Convert.ToInt32(result["Id"]);
                        productsDTO.Title = result["Title"].ToString();
                        productsDTO.Description = string.Empty;
                        productsDTO.Image = result["Image"].ToString();
                        productsDTO.Brand = result["Brand"].ToString();
                        productsDTO.Price = Convert.ToDouble(result["Price"]);
                        productsDTO.OldPrice = Convert.ToDouble(result["OldPrice"]);
                        productsDTO.ProductGUID = (Guid)result["ProductGUID"];
                        productsDTO.SpecificationNames = string.Empty;
                        productsDTO.Quantity = Convert.ToInt32(result["quantity"]);
                        productsDTO.orgId = Convert.ToInt32(result["orgId"]);
                        productsDTO.WishlistedProduct = productsDTO.WishlistedProduct;
                        productsDTO.BrandId= Convert.ToInt32(result["BrandId"]);
                        productsDTO.CategoryId = Convert.ToInt32(result["CategoryId"]);
                        productsDTO.Status = result["Status"].ToString();
                        productsDTO.StatusId = Convert.ToInt32(result["StatusId"]);
                        productsDTOs.Add(productsDTO);
                    }
                }
            }
           /* for (int i = 0; i < productsDTOs.Count; i++)
            {
                var DefaultSpecification = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(x => x.ProductGuid == productsDTOs[i].ProductGUID && x.OrgId == productsDTOs[i].orgId&&x.IsDefault==true);
                if (DefaultSpecification != null)
                {
                    var specification = await _MasterContext.SpecificationSetups.FirstOrDefaultAsync(x => x.SpecificationId == DefaultSpecification.SpecificationId && x.OrgId == DefaultSpecification.OrgId);
                   if(specification!=null)
                    {
                        productsDTOs[i].SpecificationId = (int)DefaultSpecification.SpecificationId;
                        productsDTOs[i].SpecificationNames = specification.SpecificationName;
                    }
                         
                }             
            }*/
            /*if (UserId!=null)
            {
                var getusername = await _MasterContext.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
                var wishlistList = await _MasterContext.CustomerWishlists.Where(x => x.UserName == getusername.Email&&x.OrgId==getusername.OrgId).ToListAsync();
                for (int i = 0; i < productsDTOs.Count; i++)
                {
                    for (int j = 0; j < wishlistList.Count; j++)
                    {
                        if (productsDTOs[i].SpecificationId == 0)
                        {
                            if (productsDTOs[i].Id == wishlistList[j].ProductId)
                            {
                                productsDTOs[i].WishlistedProduct = true;
                            }
                        }
                        else
                        {
                            var productspecDetails = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(x => x.SpecificationId == productsDTOs[i].SpecificationId && x.ProductGuid == productsDTOs[i].ProductGUID && x.OrgId == productsDTOs[i].orgId);
                            if (productsDTOs[i].Id == wishlistList[j].ProductId && productspecDetails.ProductSpecificationId == wishlistList[j].ProductSpecificationId)
                            {
                                productsDTOs[i].WishlistedProduct = true;
                            }
                        }
                    }
                }
            }*/
          /*  var productList = productsDTOs.GroupBy(p => p.Status,p => p,(key, g) => new DetailProductDTO { Status = key, productsDTOs = g.ToList() }).ToList();
            masterProductDTO.MainProductDTOs = productList;*/
            return productsDTOs;           
        }
        public async Task<List<ProductsDTO>> GetWishlistByUser(int orgId, int userId) 
        {
            List<ProductsDTO> productsDTOs = new List<ProductsDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetWishlistByUser";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@orgid", orgId));
                command.Parameters.Add(new SqlParameter("@userid", userId));
                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        ProductsDTO productsDTO = new ProductsDTO();
                        var ProductStrList = result["ProductList"].ToString();
                        var ProductList = ProductStrList.Split(',');
                        productsDTO.Id = Convert.ToInt32(result["Id"]);
                        productsDTO.Title = result["Title"].ToString();
                        productsDTO.Quantity = Convert.ToInt32(result["Qty"]);
                        productsDTO.Description = HtmlUtilities.ConvertToPlainText(result["Description"].ToString().Replace("\r\n", "")).Replace("\r\n", "");
                        productsDTO.Image = result["Image"].ToString();
                        productsDTO.Brand = result["Brand"].ToString();
                        productsDTO.ProductGUID = (Guid)result["ProductGUID"];
                        productsDTO.SpecificationNames = result["SpecificationNames"].ToString();
                        productsDTO.SpecificationId = Convert.ToInt32(result["SpecificationIds"]);
                        productsDTO.Price = Convert.ToDouble(result["Price"]);
                        productsDTO.OldPrice = Convert.ToDouble(result["OldPrice"]);
                        productsDTO.ProductList = ProductList;
                        productsDTO.orgId = Convert.ToInt32(orgId);
                        productsDTO.BrandId = Convert.ToInt32(result["BrandId"]);
                        productsDTO.CategoryId = Convert.ToInt32(result["CategoryId"]);
                        productsDTOs.Add(productsDTO);
                    }
                }
            }
                var getusername = await _MasterContext.Users.FirstOrDefaultAsync(u => u.UserId == userId && u.OrgId == orgId);
                var wishlistList = await _MasterContext.CustomerWishlists.Where(x => x.UserName == getusername.Email && x.OrgId == orgId).ToListAsync();
                for (int i = 0; i < productsDTOs.Count; i++)
                {
                    for (int j = 0; j < wishlistList.Count; j++)
                    {
                        if (productsDTOs[i].SpecificationId == 0)
                        {
                            if (productsDTOs[i].Id == wishlistList[j].ProductId)
                            {
                                productsDTOs[i].WishlistedProduct = true;
                            }
                        }
                        else
                        {
                            var productspecDetails = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(x => x.SpecificationId == productsDTOs[i].SpecificationId && x.ProductGuid == productsDTOs[i].ProductGUID && x.OrgId == orgId);
                            if (productsDTOs[i].Id == wishlistList[j].ProductId && productspecDetails.ProductSpecificationId == wishlistList[j].ProductSpecificationId)
                            {
                                productsDTOs[i].WishlistedProduct = true;
                            }
                        }
                }
            }
            for (int i = 0; i < productsDTOs.Count; i++)
            {
                var DefaultSpecification = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(x => x.ProductGuid == productsDTOs[i].ProductGUID && x.OrgId == productsDTOs[i].orgId&&x.IsDefault==true);
                if (DefaultSpecification != null)
                {
                   // var specification = await _MasterContext.SpecificationSetups.FirstAsync(x => x.SpecificationId == DefaultSpecification.SpecificationId && x.OrgId == orgId);
                    if (productsDTOs[i].SpecificationId != DefaultSpecification.SpecificationId)
                    {
                        productsDTOs[i].ProductList = null;

                    }
                }
            }
            return productsDTOs;
        }
        public async Task<List<ProductsDTO>> SearchProducts(int? orgId,string productname,int OrgCategoryId)
        {
            List<ProductsDTO> productsDTOs = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_SearchProduct";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@OrgId", orgId));
                command.Parameters.Add(new SqlParameter("@ProductName", productname));
                command.Parameters.Add(new SqlParameter("@OrgCategoryId", OrgCategoryId));
                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        ProductsDTO productsDTO = new ProductsDTO();      
                        productsDTO.Id = Convert.ToInt32(result["Id"]);
                        productsDTO.Title = result["Title"].ToString();
                    //    productsDTO.Description = HtmlUtilities.ConvertToPlainText(result["Description"].ToString().Replace("\r\n", "")).Replace("\r\n", "");
                        productsDTO.Image = result["Image"].ToString();
                        productsDTO.Brand = result["Brand"].ToString();
                        productsDTO.Price = Convert.ToDouble(result["Price"]);
                        productsDTO.OldPrice = result["OldPrice"] !=DBNull.Value ? Convert.ToDouble(result["OldPrice"]):0;
                        productsDTO.Quantity = Convert.ToInt32(result["qty"]);
                        productsDTO.ProductGUID = (Guid)result["ProductGUID"];
                       // productsDTO.SpecificationNames = result["SpecificationNames"].ToString();
                       // productsDTO.SpecificationId = Convert.ToInt32(result["SpecificationIds"]);
                        productsDTO.orgId = Convert.ToInt32(result["OrgId"]);
                        productsDTO.BrandId = Convert.ToInt32(result["BrandId"]);
                        productsDTO.CategoryId = Convert.ToInt32(result["CategoryId"]);
                        productsDTOs.Add(productsDTO);
                    }
                }
            }
            return productsDTOs;

        }
        public async Task<List<RecentlyViewedProductDTO>> GetRecentlyViewedProductsByCategory(int orgId,string IP)
        {
            List<RecentlyViewedProductDTO> recentlyVieweds = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_getproducts_Recentlyviewed_CategoryId";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@orgid", orgId));
                /*command.Parameters.Add(new SqlParameter("@categoryid", CategoryId));*/
                command.Parameters.Add(new SqlParameter("@IP",IP ));
                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        RecentlyViewedProductDTO product = new();
                        product.Id = Convert.ToInt32(result["ProductId"]);
                        product.Title= result["ProductName"].ToString();                      
                        product.Description = result["Description"].ToString();
                        product.ProductGuid = (Guid)result["ProductGuid"];
                        product.Image = result["Image"].ToString();
                        product.Ip = result["Ip"].ToString();
                        product.Brand = result["Brands"].ToString();
                        product.Price = Convert.ToDouble(result["Price"]);
                        product.OldPrice = result["OldPrice"] != DBNull.Value ? Convert.ToDouble(result["OldPrice"]) : 0;
                        product.OrgId = Convert.ToInt32(orgId);
                        product.productviewinsertdate = (DateTime)result["productviewinsertdate"];
                        product.STATUS = result["STATUS"].ToString();
                        product.Quantity = Convert.ToInt32(result["qty"]);
                        product.BrandId = Convert.ToInt32(result["BrandId"]);
                        product.CategoryId = Convert.ToInt32(result["CategoryId"]);
                        recentlyVieweds.Add(product);
                    }
                }
            }
            return recentlyVieweds;
        }
        public async Task<List<RecentlyViewedProductDTO>> MostViewedProductsByCategory(int orgId, int CategoryId, string IP)
        {
            List<RecentlyViewedProductDTO> recentlyVieweds = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_getproducts_Mostviewed_CategoryId";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@orgid", orgId));
                command.Parameters.Add(new SqlParameter("@categoryid", CategoryId));
                command.Parameters.Add(new SqlParameter("@IP", IP));
                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        RecentlyViewedProductDTO product = new();
                        product.Id = Convert.ToInt32(result["ProductId"]);
                        product.Title = result["ProductName"].ToString();
                        product.Ip = result["Ip"].ToString();
                        product.Description = result["Description"].ToString();
                        product.ProductGuid = (Guid)result["ProductGuid"];
                        product.SpecificationNames = result["SpecificationNames"].ToString();
                        product.SpecificationImage = result["SpecificationImage"].ToString();
                        product.SpecificationId = Convert.ToInt32(result["SpecificationIds"]);
                        product.Image = result["Image"].ToString();
                        product.Brand = result["Brands"].ToString();
                        product.Price = Convert.ToDouble(result["Price"]);
                        product.OldPrice = result["OldPrice"] != DBNull.Value ? Convert.ToDouble(result["OldPrice"]) : 0;
                        product.OrgId = Convert.ToInt32(orgId);
                        product.BrandId = Convert.ToInt32(result["BrandId"]);
                        product.CategoryId = Convert.ToInt32(result["CategoryId"]);
                        product.STATUS = result["STATUS"].ToString();
                        //product.NumOfViews = Convert.ToInt32(result["NumOfViews"]);
                        product.Quantity = Convert.ToInt32(result["qty"]);
                        recentlyVieweds.Add(product);
                    }
                }
            }
            return recentlyVieweds;
        }
        public async Task<List<ProductsByBestSellerDTO>> ProductByBestSellers(int? orgId,string type,int OrgCategoryId)
        {
            List<ProductsByBestSellerDTO> bestSellerDTOs= new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_getproducts_BY_BestSellers";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@orgid", orgId));
                command.Parameters.Add(new SqlParameter("@type", type));
                command.Parameters.Add(new SqlParameter("@OrgCategoryId", OrgCategoryId));
                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        ProductsByBestSellerDTO product = new();
                        product.Id = Convert.ToInt32(result["ProductId"]);
                        product.Title = result["ProductName"].ToString();
                        product.Image = result["Image"].ToString();
                        product.Brand = result["Brands"].ToString();
                        product.Price = Convert.ToDouble(result["Price"]);
                        product.ProductGUID = (Guid)result["ProductGuid"];
                        product.OldPrice = result["OldPrice"] != DBNull.Value ? Convert.ToDouble(result["OldPrice"]) : 0;
                        product.orgId = Convert.ToInt32(result["orgId"]);
                        product.Quantity = Convert.ToInt32(result["Qty"]);
                        product.BrandId = Convert.ToInt32(result["BrandId"]);
                        product.CategoryId = Convert.ToInt32(result["CategoryId"]);
                        bestSellerDTOs.Add(product);
                    }
                }
            }
            for (int i = 0; i < bestSellerDTOs.Count; i++)
            {
                var ProductDetails = await _MasterContext.ProductBasics.FirstOrDefaultAsync(x => x.ProductId == bestSellerDTOs[i].Id && x.OrgId == bestSellerDTOs[i].orgId);
                var DefaultSpecification = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(x => x.ProductGuid == ProductDetails.ProductGuid && x.OrgId == ProductDetails.OrgId && x.IsDefault==true);
                if (DefaultSpecification != null)
                {
                    var specification = await _MasterContext.SpecificationSetups.FirstOrDefaultAsync(x => x.SpecificationId == DefaultSpecification.SpecificationId && x.OrgId == DefaultSpecification.OrgId);
                    if(specification!=null)
                    {
                        bestSellerDTOs[i].SpecificationId = specification.SpecificationId;
                        bestSellerDTOs[i].SpecificationNames = specification.SpecificationName;
                    }                 
                }               
            }
            return bestSellerDTOs;
        }
        public async Task<List<ProductsDTO>> GetProductsByCategory(int? orgId,int CategoryId,int OrgCategoryId)
        {
            List<ProductsDTO> productsDTOs = new List<ProductsDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetProductList_By_CategoryId";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@OrgId", orgId));
                command.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                command.Parameters.Add(new SqlParameter("@OrgCategoryId", CategoryId));
                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        ProductsDTO productsDTO = new ProductsDTO();
                        productsDTO.Id = Convert.ToInt32(result["Id"]);
                        productsDTO.Title = result["Title"].ToString();
                        productsDTO.Image = result["Image"].ToString();
                        productsDTO.Brand = result["Brand"].ToString();
                        productsDTO.Price = Convert.ToDouble(result["Price"]);
                        productsDTO.OldPrice = Convert.ToDouble(result["OldPrice"]);
                        productsDTO.ProductGUID = (Guid)result["ProductGUID"];
                        productsDTO.Quantity = Convert.ToInt32(result["Qty"]);
                        productsDTO.orgId = Convert.ToInt32(result["orgId"]);
                        productsDTO.BrandId = Convert.ToInt32(result["BrandId"]);
                        productsDTO.CategoryId = Convert.ToInt32(result["CategoryId"]);
                        productsDTOs.Add(productsDTO);
                    }
                }
            }
            for (int i = 0; i < productsDTOs.Count; i++)
            {
                var DefaultSpecification = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(x => x.ProductGuid == productsDTOs[i].ProductGUID && x.OrgId == productsDTOs[i].orgId);
                if (DefaultSpecification != null)
                {
                    var specification = await _MasterContext.SpecificationSetups.FirstOrDefaultAsync(x => x.SpecificationId == DefaultSpecification.SpecificationId && x.OrgId == DefaultSpecification.OrgId);
                    if(specification!=null)
                    {
                        productsDTOs[i].SpecificationId = specification.SpecificationId;
                        productsDTOs[i].SpecificationNames = specification.SpecificationName;
                    }
                   
                }             
            }
            return productsDTOs;
        }
        public async Task<string> UpdateProductDetailsForVendor(UpdateProductDetail products)
        {
            var productDetails = await _MasterContext.ProductBasics.FirstOrDefaultAsync(a => a.OrgId ==products.OrgId&&a.ProductId==products.Id);
            var productPrice=await _MasterContext.ProductPrices.FirstOrDefaultAsync(a=>a.OrgId==products.OrgId && a.ProductGuid==productDetails.ProductGuid);
            if (productDetails != null&&productPrice!=null)
            {
                productDetails.Qty = products.Quantity;                
                _MasterContext.Entry(productDetails).State = EntityState.Detached;
                _MasterContext.Entry(productDetails).State = EntityState.Modified;

                productPrice.Price = products.Price;
                _MasterContext.Entry(productDetails).State = EntityState.Detached;
                _MasterContext.Entry(productDetails).State = EntityState.Modified;
                _MasterContext.ProductBasics.Update(productDetails);
                _MasterContext.ProductPrices.Update(productPrice);
                await _MasterContext.SaveChangesAsync();
                return "Success";
            }
            else
            {
                return "Something went Wrong..";
            }
        }
        public async Task<ProductDetailsForVendor> GetAllProductsForVendor(int orgId,int Id)
        {
            ProductDetailsForVendor productsDTO = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "GetAllProductsForVendor";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@orgid", orgId));
                command.Parameters.Add(new SqlParameter("@Id", Id));
                await this._MasterContext.Database.OpenConnectionAsync();

                using var result = await command.ExecuteReaderAsync();
                while (await result.ReadAsync())
                {                  
                    var ProductStrList = result["ProductList"].ToString();
                    var ProductList = ProductStrList.Split(',');
                    productsDTO.Id = Convert.ToInt32(result["Id"]);
                    productsDTO.Title = result["Title"].ToString();
                    productsDTO.Description = HtmlUtilities.ConvertToPlainText(result["Description"].ToString()).Replace("\r\n", "");
                    productsDTO.Image = result["Image"].ToString();
                    productsDTO.Brand = result["Brand"].ToString();
                    productsDTO.Price = Convert.ToDouble(result["Price"]);
                    productsDTO.OldPrice = Convert.ToDouble(result["OldPrice"]);
                    productsDTO.ProductList = ProductList;
                    productsDTO.Quantity = Convert.ToInt32(result["quantity"]);
                    productsDTO.orgId = Convert.ToInt32(orgId);
                    productsDTO.BrandId = Convert.ToInt32(result["BrandId"]);
                    productsDTO.CategoryId = Convert.ToInt32(result["CategoryId"]);
                    productsDTO.ProductGuid = (Guid)result["ProductGuid"];                  
                }
            }          
            return productsDTO;
        }
        public async Task<List<ProductsDTO>> GetSimilarProducts(int? orgId, int CategoryId,int  BrandId,int? UserId,int OrgCategoryId)
        {
            List<ProductsDTO> productsDTOs = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetSimilarProducts";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@OrgId", orgId));
                command.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                command.Parameters.Add(new SqlParameter("@BrandId", BrandId));
                command.Parameters.Add(new SqlParameter("@OrgCategoryId", OrgCategoryId));
                await this._MasterContext.Database.OpenConnectionAsync();

                using var result = await command.ExecuteReaderAsync();
                while (await result.ReadAsync())
                {
                    ProductsDTO productsDTO = new();
                    productsDTO.Id = Convert.ToInt32(result["Id"]);
                    productsDTO.Title = result["Title"].ToString();                  
                    productsDTO.Image = result["Image"].ToString();
                    productsDTO.Brand = result["Brand"].ToString();
                    productsDTO.Price = Convert.ToDouble(result["Price"]);
                    productsDTO.OldPrice = Convert.ToDouble(result["OldPrice"]);
                    productsDTO.ProductGUID = (Guid)result["ProductGUID"];
                    productsDTO.Quantity = Convert.ToInt32(result["Qty"]);
                    productsDTO.orgId = Convert.ToInt32(result["orgId"]);
                    productsDTO.BrandId = Convert.ToInt32(result["BrandId"]);
                    productsDTO.CategoryId = Convert.ToInt32(result["CategoryId"]);
                    productsDTOs.Add(productsDTO);
                }
            }
            for (int i = 0; i < productsDTOs.Count; i++)
            {
                var DefaultSpecification = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(x => x.ProductGuid == productsDTOs[i].ProductGUID && x.OrgId == productsDTOs[i].orgId && x.IsDefault == true);
                if (DefaultSpecification != null)
                {
                    var specification = await _MasterContext.SpecificationSetups.FirstOrDefaultAsync(x => x.SpecificationId == DefaultSpecification.SpecificationId && x.OrgId == DefaultSpecification.OrgId);
                    if (specification != null)
                    {
                        productsDTOs[i].SpecificationId = (int)DefaultSpecification.SpecificationId;
                        productsDTOs[i].SpecificationNames = specification.SpecificationName;
                    }

                }
            }
            if (UserId != null)
            {
                var getusername = await _MasterContext.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
                var wishlistList = await _MasterContext.CustomerWishlists.Where(x => x.UserName == getusername.Email && x.OrgId == getusername.OrgId).ToListAsync();
                for (int i = 0; i < productsDTOs.Count; i++)
                {
                    for (int j = 0; j < wishlistList.Count; j++)
                    {
                        if (productsDTOs[i].SpecificationId == 0)
                        {
                            if (productsDTOs[i].Id == wishlistList[j].ProductId)
                            {
                                productsDTOs[i].WishlistedProduct = true;
                            }
                        }
                        else
                        {
                            var productspecDetails = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(x => x.SpecificationId == productsDTOs[i].SpecificationId && x.ProductGuid == productsDTOs[i].ProductGUID && x.OrgId == productsDTOs[i].orgId);
                            if (productsDTOs[i].Id == wishlistList[j].ProductId && productspecDetails.ProductSpecificationId == wishlistList[j].ProductSpecificationId)
                            {
                                productsDTOs[i].WishlistedProduct = true;
                            }
                        }
                    }
                }
            }
            return productsDTOs;
        }
        public async Task<List<ProductVariationDTO>> GetProductVariationDetail(int OrgId, Guid ProductGUID)
        {
            List<ProductVariationDTO> productVariation = new();
           // productVariation.VariationDetails = new();
            using (var command = _MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetProductSpecifications";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                command.Parameters.Add(new SqlParameter("@ProductGUID", ProductGUID));
                await _MasterContext.Database.OpenConnectionAsync();

                using var result = await command.ExecuteReaderAsync();
                while (await result.ReadAsync())
                {
                    ProductVariationDTO productVariationdetails = new();
                    productVariationdetails.SpecificationNames = result["SpecificationNames"].ToString();
                    productVariationdetails.SpecificationId = Convert.ToInt32(result["SpecificationIds"]);
                    productVariationdetails.ProductGUId = (Guid)ProductGUID;
                    productVariationdetails.OrgId = Convert.ToInt32(OrgId);
                    productVariationdetails.IsSpecificationExist = Convert.ToBoolean(result["IsSpecificationExist"]);
                    productVariation.Add(productVariationdetails);
                }
            }
            
            for (int i = 0; i < productVariation.Count; i++)
            {
                if(productVariation[i].IsSpecificationExist==true)
                {
                    var DefaultSpecification = _MasterContext.ProductSpecifications.FirstOrDefault(x => x.ProductGuid == ProductGUID && x.OrgId == OrgId && x.IsDefault == true);
                    productVariation[i].DefaultSpecification = (int)DefaultSpecification.SpecificationId;
                }             
            }                         
            return productVariation;
        }
        public async Task<List<ProductsDTO>> GetProductDetailsBySpecification(int OrgId, Guid ProductGUID,int? SpecificationId, int? UserId)
        {
            List<ProductsDTO> productVariation = new();
            // productVariation.VariationDetails = new();
            using (var command = _MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetProductDetails";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                command.Parameters.Add(new SqlParameter("@ProductGUID", ProductGUID));
                command.Parameters.Add(new SqlParameter("@SpecificationId", SpecificationId));
                await _MasterContext.Database.OpenConnectionAsync();

                using var result = await command.ExecuteReaderAsync();
                while (await result.ReadAsync())
                {
                    ProductsDTO productsDTO = new();
                    var ProductStrList = result["ProductList"].ToString();
                    var ProductList = ProductStrList.Split(',');
                    productsDTO.ProductGUID= (Guid)ProductGUID;
                    productsDTO.Id = Convert.ToInt32(result["Id"]);
                    productsDTO.Title = result["Title"].ToString();
                    productsDTO.Description = HtmlUtilities.ConvertToPlainText(result["Description"].ToString()).Replace("\r\n", "");
                    productsDTO.Image = result["Image"].ToString();
                    productsDTO.SpecificationNames = result["SpecificationNames"].ToString();
                    productsDTO.SpecificationId= Convert.ToInt32(result["SpecificationIds"]);
                    productsDTO.Brand = result["Brand"].ToString();
                    productsDTO.Price = Convert.ToDouble(result["Price"]);
                    productsDTO.ProductList = ProductList;
                    productsDTO.Quantity = Convert.ToInt32(result["Quantity"]);
                    productsDTO.ProductPrice= Convert.ToDouble(result["ProductPrice"]);
                    productsDTO.ProductQty = Convert.ToInt32(result["ProductQty"]);
                   // productsDTO.ProductImage = result["ProductImage"].ToString();
                    productsDTO.orgId = Convert.ToInt32(OrgId);
                    productsDTO.CategoryId = Convert.ToInt32(result["CategoryId"]);
                    productVariation.Add(productsDTO);
                }
            }
            if (UserId != null)
            {
                var getusername = await _MasterContext.Users.FirstOrDefaultAsync(u => u.UserId == UserId && u.OrgId == OrgId);
                var wishlistList = await _MasterContext.CustomerWishlists.Where(x => x.UserName == getusername.Email && x.OrgId == OrgId).ToListAsync();
                for (int i = 0; i < productVariation.Count; i++)
                {
                    for (int j = 0; j < wishlistList.Count; j++)
                    {
                        if (productVariation[i].SpecificationId == 0)
                        {
                            if (productVariation[i].Id == wishlistList[j].ProductId)
                            {
                                productVariation[i].WishlistedProduct = true;
                            }
                        }
                        else
                        {
                            var productspecDetails = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(x => x.SpecificationId == productVariation[i].SpecificationId && x.ProductGuid == productVariation[i].ProductGUID && x.OrgId == OrgId);
                            if (productVariation[i].Id == wishlistList[j].ProductId && productspecDetails.ProductSpecificationId == wishlistList[j].ProductSpecificationId)
                            {
                                productVariation[i].WishlistedProduct = true;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < productVariation.Count; i++)
            {
                var DefaultSpecification = await _MasterContext.ProductSpecifications.Where(x => x.ProductGuid == productVariation[i].ProductGUID && x.OrgId == productVariation[i].orgId&&x.IsDefault==true).FirstOrDefaultAsync();
                if (DefaultSpecification != null)
                {
                    if (DefaultSpecification.IsDefault == true&&DefaultSpecification.SpecificationId== SpecificationId)
                    {
                        productVariation[i].Quantity = productVariation[i].ProductQty;
                        productVariation[i].Price = productVariation[i].ProductPrice;
                    }
                    
                    if (productVariation[i].SpecificationId != DefaultSpecification.SpecificationId)
                    {
                        productVariation[i].ProductList = null;
                    }                   
                }
            }
            return productVariation;
        }

        public async void UpdateProductImage(string oldpath,string newPath)
        {
            List<OrganizationCategoryDTO> organizationcategory = new List<OrganizationCategoryDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "UPDATE TOP(1) Product_Basic SET CoverImage = '"+ newPath + "' where CoverImage = '"+ oldpath + "'";

                command.CommandText = strSQL;
                command.CommandType = CommandType.Text;

                await this._MasterContext.Database.OpenConnectionAsync();

                await command.ExecuteReaderAsync();
                await this._MasterContext.Database.CloseConnectionAsync();
            }
            
        }
        public async void UpdateProductOtherImage(string oldpath, string newPath)
        {
            List<OrganizationCategoryDTO> organizationcategory = new List<OrganizationCategoryDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "UPDATE TOP(1) Product_Images SET Image = '" + newPath + "' where Image = '" + oldpath + "'";

                command.CommandText = strSQL;
                command.CommandType = CommandType.Text;

                await this._MasterContext.Database.OpenConnectionAsync();

                await command.ExecuteReaderAsync();
                await this._MasterContext.Database.CloseConnectionAsync();
            }

        }
        public async Task<List<AllProductsDTO>> GetAllProducts(int? OrgId,int? OrgCategoryId)
        {
            List<AllProductsDTO> productsDTOs = new();
            //productsDTOs.StausDetails = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetAllProducts_BY_STATUS";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                command.Parameters.Add(new SqlParameter("@OrgCategoryId", OrgCategoryId));

                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        AllProductsDTO productsDTO = new AllProductsDTO();
                        productsDTO.Id = Convert.ToInt32(result["Id"]);
                        productsDTO.Title = result["Title"].ToString();
                        productsDTO.Image = result["Image"].ToString();
                        productsDTO.Brand = result["Brand"].ToString();
                        productsDTO.Price = Convert.ToDouble(result["Price"]);
                        productsDTO.OldPrice = Convert.ToDouble(result["OldPrice"]);
                        productsDTO.ProductGUID = (Guid)result["ProductGUID"];
                        productsDTO.Quantity = Convert.ToInt32(result["quantity"]);
                        productsDTO.orgId = Convert.ToInt32(result["orgId"]);
                        productsDTO.BrandId = Convert.ToInt32(result["BrandId"]);
                        productsDTO.CategoryId = Convert.ToInt32(result["CategoryId"]);
                        productsDTO.Status = result["Status"].ToString();
                        productsDTOs.Add(productsDTO);
                    }
                }
            }

                //var specificationIds= productsDTOs.Select(x => x.SpecificationId).ToArray();
                var SpecifcationArray = productsDTOs.ToArray();
                for (int i = 0; i < SpecifcationArray.Length; i++)
                {
                    var DefaultSpecification = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(x => x.ProductGuid == SpecifcationArray[i].ProductGUID && x.OrgId == SpecifcationArray[i].orgId && x.IsDefault == true);
                    if (DefaultSpecification != null)
                    {
                        var specification = await _MasterContext.SpecificationSetups.FirstOrDefaultAsync(x => x.SpecificationId == DefaultSpecification.SpecificationId);
                        if (specification != null)
                        {
                            SpecifcationArray[i].SpecificationId = (int)DefaultSpecification.SpecificationId;
                            SpecifcationArray[i].SpecificationNames = specification.SpecificationName;
                        }
                    }
                }
            
            var productList = productsDTOs.GroupBy(x => new { x.Status, }, (key, g) => new { StatusType = key.Status, productstatusList = g.ToList() });
            foreach (var sp in productList)
            {
                AllProductsDTO status_Product = new AllProductsDTO();
                status_Product.Status = sp.StatusType;

                status_Product.ProductsByStatus = status_Product.ProductsByStatus == null ? new List<AllProductsDTO>() : status_Product.ProductsByStatus;
                foreach (var prod in sp.productstatusList)
                {
                    AllProductsDTO allProducts = new AllProductsDTO();
                    allProducts.SpecificationId = prod.SpecificationId;
                    allProducts.Price = prod.Price;
                    allProducts.Id = prod.Id;
                    allProducts.Title = prod.Title;
                    allProducts.ProductGUID = prod.ProductGUID;
                    allProducts.Brand = prod.Brand;
                    allProducts.OldPrice = prod.OldPrice;
                    allProducts.Quantity = prod.Quantity;
                    allProducts.orgId = prod.orgId;
                    allProducts.BrandId = prod.BrandId;
                    allProducts.CategoryId = prod.CategoryId;
                    status_Product.ProductsByStatus.Add(allProducts);
                }
                productsDTOs.Add(status_Product);
            }
            
            return productsDTOs;
        }
        public async Task<List<AllProductsDTO>> GetAllProductsByCategory(int? OrgId, int OrgCategoryId,int CategoryId)
        {
            List<AllProductsDTO> productsDTOs = new();
            //productsDTOs.StausDetails = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetAllProductsByCAtegoryid";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                command.Parameters.Add(new SqlParameter("@OrgCategoryId", OrgCategoryId));
                command.Parameters.Add(new SqlParameter("@CategoryId", CategoryId)); 
                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        AllProductsDTO productsDTO = new AllProductsDTO();
                        productsDTO.Id = Convert.ToInt32(result["Id"]);
                        productsDTO.Title = result["Title"].ToString();
                        productsDTO.Image = result["Image"].ToString();
                        productsDTO.Brand = result["Brand"].ToString();
                        productsDTO.Price = Convert.ToDouble(result["Price"]);
                        productsDTO.OldPrice = Convert.ToDouble(result["OldPrice"]);
                        productsDTO.ProductGUID = (Guid)result["ProductGUID"];
                        productsDTO.Quantity = Convert.ToInt32(result["quantity"]);
                        productsDTO.orgId = Convert.ToInt32(result["orgId"]);
                        productsDTO.BrandId = Convert.ToInt32(result["BrandId"]);
                        productsDTO.CategoryId = Convert.ToInt32(result["CategoryId"]);
                        productsDTOs.Add(productsDTO);
                    }
                }
            }
            var SpecifcationArray = productsDTOs.ToArray();
            for (int i = 0; i < SpecifcationArray.Length; i++)
            {
                var DefaultSpecification = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(x => x.ProductGuid == SpecifcationArray[i].ProductGUID && x.OrgId == SpecifcationArray[i].orgId && x.IsDefault == true);
                if (DefaultSpecification != null)
                {
                    var specification = await _MasterContext.SpecificationSetups.FirstOrDefaultAsync(x => x.SpecificationId == DefaultSpecification.SpecificationId);
                    if (specification != null)
                    {
                        SpecifcationArray[i].SpecificationId = (int)DefaultSpecification.SpecificationId;
                        SpecifcationArray[i].SpecificationNames = specification.SpecificationName;
                    }
                }
            }           
            return productsDTOs;
        }
    }
}
