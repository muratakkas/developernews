using System;
using Microsoft.AspNetCore.Mvc;
using Dews.News.Interfaces;
using Dews.Api.Results.News;
using Dews.Api.Requests.News;
using Microsoft.AspNetCore.Authorization;
using IdentityModel;
using System.Linq;
using System.Security.Claims;
using Dews.News.DTOs;
using Dews.Api.Extensions;

namespace Dews.Api.Controllers
{

    public class CategoryController : Controller
    {
        private readonly ICategoryManager CategoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            CategoryManager = categoryManager;
        }

        [HttpPost]
        [Route("api/category/GetList")]
        public GetCategoryResult GetList([FromBody]GetCategoryRequest getRequest)
        {
            try
            {
                if (getRequest == null) getRequest = new GetCategoryRequest();
                return new GetCategoryResult() { Items = CategoryManager.GetAll(getRequest.RequestParams) };
            }
            catch (Exception ex)
            {
                return new GetCategoryResult(ex);
            }
        }

        [HttpGet]
        [Route("api/category/{id}")]
        public GetCategoryByIdResult Get(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentNullException(nameof(id));
                return new GetCategoryByIdResult() { Item = CategoryManager.GetByID(id) };
            }
            catch (Exception ex)
            {
                return new GetCategoryByIdResult(ex);
            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/category")]
        public AddCategoryResult Save([FromBody]SaveCategoryRequest request)
        {
            try
            {
                if (request == null || request.Category == null) throw new ArgumentNullException(nameof(SaveCategoryRequest));

                // Check if the current user is authorized to make this operation 
                else CategoryManager.CheckIsUserAuthonticatedToEditDelete(User.GetUserId(), request.Category);

                CategoryManager.Add(request.Category);
                return new AddCategoryResult() { };
            }
            catch (Exception ex)
            {
                return new AddCategoryResult(ex);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("api/category/{id}")]
        public DeleteCategoryResult Delete(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentNullException(nameof(id));

                CategoryDTO savedCategory = CategoryManager.GetByID(id);
                if (savedCategory == null) throw new Exception("Category not found");
                if (savedCategory.CreateUser != Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value)) throw new UnauthorizedAccessException();

                CategoryManager.Delete(id);
                return new DeleteCategoryResult();
            }
            catch (Exception ex)
            {
                return new DeleteCategoryResult(ex);
            }
        }
    }
}
