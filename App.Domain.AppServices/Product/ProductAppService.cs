﻿using App.Domain.Core.BaseData.Contracts.Services;
using App.Domain.Core.BaseData.Entities;
using App.Domain.Core.Permission.Contracts.Services;
using App.Domain.Core.Permission.Enums;
using App.Domain.Core.Product.Contracts.AppServices;
using App.Domain.Core.Product.Contracts.Services;
using App.Domain.Core.Product.Entities;
namespace App.Domain.AppServices.Product
{
    public class ProductAppService : IProductAppService
    {
        private readonly IPermissionService _permissionService;
        private readonly IProductService _productService;
        private readonly IColorService _colorService;

        public ProductAppService(IPermissionService permissionService,IProductService productService
            ,IColorService colorService)
        {
            _permissionService = permissionService;
            _productService = productService;
            _colorService = colorService;
        }
        #region GetAllMethods
        public List<Brand> GetAllBrands(int operatorId)
        {
            var permission = _permissionService.HasPermission(operatorId, (int)PermissionsEnum.ViewBrands);
            if (!permission)
            {
                throw new UnauthorizedAccessException();
            }
            var brands = _productService.GetAllBrands();
            return brands;
        }
        public List<Category> GetAllCategories(int operatorId)
        {
            var permission = _permissionService.HasPermission(operatorId, (int)PermissionsEnum.ViewCategories);
            if (!permission)
            {
                throw new UnauthorizedAccessException();
            }
            var categories = _productService.GetAllCategories();
            return categories;
        }

        public List<Core.Product.Entities.Product> GetAllProducts(int operatorId)
        {
            var permission = _permissionService.HasPermission(operatorId, (int)PermissionsEnum.ViewProducts);
            if (!permission)
            {
                throw new UnauthorizedAccessException();
            }
            var products = _productService.GetAllProducts();
            return products;
        }

        public List<Tag> GetAllTags(int operatorId)
        {
            var permission = _permissionService.HasPermission(operatorId, (int)PermissionsEnum.ViewTags);
            if (!permission)
            {
                throw new UnauthorizedAccessException();
            }
            var tags = _productService.GetAllTags();
            return tags;
        }
        public List<Model> GetAllModels(int operatorId)
        {
            var permission = _permissionService.HasPermission(operatorId, (int)PermissionsEnum.ViewModels);
            if (!permission)
            {
                throw new UnauthorizedAccessException();
            }
            var models = _productService.GetAllModels();
            return models;
        }
        public List<Color> GetAllColors(int operatorId)
        {
            var permission = _permissionService.HasPermission(operatorId, (int)PermissionsEnum.ViewColors);
            if (!permission)
            {
                throw new UnauthorizedAccessException();
            }
            var colors = _colorService.GetAllColors();
            return colors;
        }
        #endregion

        #region CreateMethods
        public int CreateModel(Model model)
        {
            var id = _productService.CreateModel(model);
            return id;
        }

        public int CreateBrand(Brand model)
        {
            var id = _productService.CreateBrand(model);
            return id;
        }

        public int CreateCategory(Category model)
        {
            var id = _productService.CreateCategory(model);
            return id;
        }

        public int CreateTag(Tag model)
        {
            var id = _productService.CreateTag(model);
            return id;
        }

        public int CreateProduct(Core.Product.Entities.Product model)
        {
            var id = _productService.CreateProduct(model);
            return id;
        }
        public int CreateColor(Color model)
        {
            var id = _colorService.CreateColor(model);
            return id;
        }


        #endregion

        #region UpdateMethods
        public void UpdateModel(Model model)
        {
            _productService.UpdateModel(model);
        }

        public void UpdateTag(Tag model)
        {
            _productService.UpdateTag(model);
        }

        public void UpdateCategory(Category model)
        {
            _productService.UpdateCategory(model);
        }

        public void UpdateBrand(Brand model)
        {
            _productService.UpdateBrand(model);
        }

        public void UpdateProduct(Core.Product.Entities.Product model)
        {
            _productService.UpdateProduct(model);
        }
        public void UpdateColor(Color model)
        {
            _colorService.UpdateColor(model);
        }
        #endregion

        #region GetMethods
        public Model GetModelById(int id)
        {
            return _productService.GetModelById(id);
        }

        public Tag GetTagById(int id)
        {
            return _productService.GetTagById(id);
        }

        public Category GetCategoryById(int id)
        {
            return _productService.GetCategoryById(id);
        }

        public Brand GetBrandById(int id)
        {
            return _productService.GetBrandById(id);
        }

        public Core.Product.Entities.Product GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }
        public Color GetColorById(int id)
        {
            return _colorService.GetColorById(id);
        }
        #endregion

        #region RemoveMethods
        public bool RemoveModel(int id)
        {            
            return _productService.RemoveModel(id);
        }

        public bool RemoveTag(int id)
        {            
            return _productService.RemoveTag(id);
        }

        public bool RemoveBrand(int id)
        {            
            return _productService.RemoveBrand(id);
        }

        public bool RemoveCategory(int id)
        {            
            return _productService.RemoveCategory(id);
        }

        public bool RemoveProduct(int id)
        {            
            return _productService.RemoveProduct(id);
        }
        public bool RemoveColor(int id)
        {
            return _colorService.RemoveColor(id);
        }

        #endregion
    }
}
