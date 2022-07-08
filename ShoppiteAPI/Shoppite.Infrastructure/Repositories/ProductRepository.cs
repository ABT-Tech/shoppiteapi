using Microsoft.Data.SqlClient;
using Shoppite.Core.DTOs;
using Shoppite.Core.Entities;
using Shoppite.Core.Extensions;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Repositories
{
    public class ProductRepository: IProductRepository
    {
        public async Task<List<Product_DTO>> GetproductNavList()
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Product_DTO> product_DTO = new List<Product_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallproducts";
                command.Parameters.AddWithValue("@org_id", 1);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                product_DTO = extensionMethods.DataReaderMapToList<Product_DTO>(dataReader);
                connection.Close();
                return product_DTO;
            }
        }

        public async Task<List<Product_DTO>> GetproductList(int category_id, int sub_category_id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Product_DTO> product_DTO = new List<Product_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallproduct_by_subcategory_id";
                command.Parameters.AddWithValue("@org_id", 1);
                command.Parameters.AddWithValue("@category_id", category_id);
                command.Parameters.AddWithValue("@sub_ctg_id", sub_category_id);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                product_DTO = extensionMethods.DataReaderMapToList<Product_DTO>(dataReader);
                connection.Close();
                return product_DTO;
            }
        }

        public async Task<List<Product_DTO>> Getproductdisc(int category_id, int sub_category_id, int product_id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Product_DTO> product_DTO = new List<Product_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getproduct_by_id";
                command.Parameters.AddWithValue("@org_id", 1);
                command.Parameters.AddWithValue("@category_id", category_id);
                command.Parameters.AddWithValue("@sub_ctg_id", sub_category_id);
                command.Parameters.AddWithValue("@id", product_id);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                product_DTO = extensionMethods.DataReaderMapToList<Product_DTO>(dataReader);
                connection.Close();
                return product_DTO;
            }
        }

        public async Task<CartProduct> PostCartProduct(CartProduct cartProduct)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "addcart";
                command.Parameters.AddWithValue("@org_id", cartProduct.org_id);
                command.Parameters.AddWithValue("@user_id", cartProduct.user_id);
                command.Parameters.AddWithValue("@category_id", cartProduct.category_id);
                command.Parameters.AddWithValue("@sub_ctg_id", cartProduct.sub_ctg_id);
                command.Parameters.AddWithValue("@product_id", cartProduct.product_id);
                await command.ExecuteNonQueryAsync();
                return cartProduct;
            }
        }

        public async Task<List<CartProduct>> GetCartProduct(int org_id, int user_id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<CartProduct> cartProduct = new List<CartProduct>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getcartproduct";
                command.Parameters.AddWithValue("@org_id", org_id);
                command.Parameters.AddWithValue("@user_id", user_id);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                cartProduct = extensionMethods.DataReaderMapToList<CartProduct>(dataReader);
                connection.Close();
                return cartProduct;
            }
        }

        public async Task<WishList_DTO> PostWishList(WishList_DTO wishList_DTO)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "addwishlist";
                command.Parameters.AddWithValue("@org_id", wishList_DTO.org_id);
                command.Parameters.AddWithValue("@user_id", wishList_DTO.user_id);
                command.Parameters.AddWithValue("@category_id", wishList_DTO.category_id);
                command.Parameters.AddWithValue("@sub_ctg_id", wishList_DTO.sub_ctg_id);
                command.Parameters.AddWithValue("@product_id", wishList_DTO.product_id);
                await command.ExecuteNonQueryAsync();
                return wishList_DTO;
            }
        }

        public async Task<List<WishList_DTO>> GetWishList(int org_id, int user_id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<WishList_DTO> wishList = new List<WishList_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getWishList";
                command.Parameters.AddWithValue("@org_id", org_id);
                command.Parameters.AddWithValue("@user_id", user_id);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                wishList = extensionMethods.DataReaderMapToList<WishList_DTO>(dataReader);
                connection.Close();
                return wishList;
            }
        }

        public async Task<List<WishList_DTO>> DeleteWishList(int org_id, int user_id, int id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<WishList_DTO> wishListD = new List<WishList_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "deleteWishList";
                command.Parameters.AddWithValue("@org_id", org_id);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
                connection.Close();
                wishListD= await GetWishList(org_id, user_id);
                return wishListD;
            }
        }

        public async Task<List<CartProduct>> DeleteCartList(int org_id, int user_id, int id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<CartProduct> dcartProduct = new List<CartProduct>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "deleteCartList";
                command.Parameters.AddWithValue("@org_id", org_id);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
                connection.Close();
                dcartProduct = await GetCartProduct(org_id, user_id);
                return dcartProduct;
            }
        }
        

        public async Task<List<CartProduct>> UpdateCartQuantity(int org_id, int user_id, int id, int prod_quantity)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<CartProduct> ucartProduct = new List<CartProduct>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "updateCartList";
                command.Parameters.AddWithValue("@org_id", org_id);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@prod_quantity", prod_quantity);
                await command.ExecuteNonQueryAsync();
                connection.Close();
                ucartProduct = await GetCartProduct(org_id, user_id);
                return ucartProduct;
            }
        }

        public async Task<Product_DTO> PostProduct(Product_DTO product_DTO)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "addproducts";
                command.Parameters.AddWithValue("@id", product_DTO.id);
                command.Parameters.AddWithValue("@org_id", product_DTO.org_id);
                command.Parameters.AddWithValue("@category_id", product_DTO.category_id);
                command.Parameters.AddWithValue("@sub_ctg_id", product_DTO.sub_ctg_id);
                command.Parameters.AddWithValue("@company_name", product_DTO.company_name);
                command.Parameters.AddWithValue("@product_name", product_DTO.product_name);
                command.Parameters.AddWithValue("@product_description", product_DTO.product_description);
                command.Parameters.AddWithValue("@product_code", product_DTO.product_code);
                command.Parameters.AddWithValue("@product_price", product_DTO.product_price);
                command.Parameters.AddWithValue("@product_discount", product_DTO.product_discount);
                command.Parameters.AddWithValue("@product_quantity", product_DTO.product_quantity);
                command.Parameters.AddWithValue("@product_image", product_DTO.product_image);
                command.Parameters.AddWithValue("@is_available", product_DTO.is_available);
                await command.ExecuteNonQueryAsync();
                return product_DTO;
            }
        }

        public async Task<List<Product_DTO>> GetAllProduct(int org_id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Product_DTO> product_DTO = new List<Product_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallproduct";
                command.Parameters.AddWithValue("@org_id", org_id);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                product_DTO = extensionMethods.DataReaderMapToList<Product_DTO>(dataReader);
                connection.Close();
                return product_DTO;
            }
        }

        public async Task<List<Product_DTO>> DeleteProduct(int id, int org_id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Product_DTO> product_DTO = new List<Product_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "deleteproducts";
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
                connection.Close();
                product_DTO = await GetAllProduct(org_id);
                return product_DTO;
            }
        }

    }



}
