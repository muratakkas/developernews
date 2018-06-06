using System; 
using Microsoft.AspNetCore.Mvc; 
using Dews.News.Interfaces; 
using Dews.Api.Results.News; 
using Dews.Api.Requests.News; 
using Dews.Api.Results.Base;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Dews.News.DTOs;
using Dews.Api.Extensions;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dews.Api.Constants;

namespace Dews.Api.Controllers
{
  
    public class NewsController : Controller
    {
        private readonly INewsManager NewsManager;
        private readonly ICategoryManager CategoryManager;
        private readonly IHostingEnvironment AppEnvironment;
        public NewsController(INewsManager newsManager, ICategoryManager categoryManager, IHostingEnvironment env)
        {
            NewsManager = newsManager;
            CategoryManager = categoryManager;
            AppEnvironment = env;
        }

        private string GetNewsImagePath(int Id)
        {
            return Path.Combine(AppEnvironment.WebRootPath, Const.NEWS_UPLOAD_PATH, Id + ".jpg");
        }
         
        [HttpPost] 
        [Route("api/news/GetList")]
        public GetNewsResult GetList([FromBody]GetNewsRequest getRequest)
        {
            try
            {
                if (getRequest == null) throw new ArgumentNullException(nameof(getRequest));
                getRequest.ValidateRequest();
                return new GetNewsResult() { Items = NewsManager.GetAll(getRequest.RequestParams) };
            }
            catch (Exception ex)
            {
                return new GetNewsResult(ex);
            }
        }
         
        [HttpGet]
        [Route("api/news/{id}")]
        public GetNewsByIdResult Get(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentNullException(nameof(id));
                return new GetNewsByIdResult() { Item = NewsManager.GetByID(id) };
            }
            catch (Exception ex)
            {
                return new GetNewsByIdResult(ex);
            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/news")] 
        public AddNewsResult Save([FromBody]SaveNewsRequest addrequest)
        {
            try
            {
                if (addrequest == null || addrequest.News == null) throw new ArgumentNullException(nameof(SaveNewsRequest));

                CategoryDTO newsCategory = CategoryManager.GetByID(addrequest.News.CategoryId); 
                CategoryManager.CheckIsUserAuthonticatedToEditDelete(User.GetUserId(), newsCategory);

                // Check if the current user is authorized to make this operation
                if (!addrequest.News.Id.HasValue) addrequest.News.CreateUser = User.GetUserId(); 
                else NewsManager.CheckIsUserAuthonticatedToEditDelete(User.GetUserId(), addrequest.News);
                 
                NewsManager.Save(addrequest.News);

                ImageExtensions.SaveImage(GetNewsImagePath(addrequest.News.Id.Value), addrequest.News.Icon);

                return new AddNewsResult() { };
            }
            catch (Exception ex)
            {
                return new AddNewsResult(ex);
            }
        }


        [Authorize]
        [HttpDelete]
        [Route("api/news/{id}")] 
        public DeleteNewsResult Delete(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentNullException(nameof(id));

                NewsDTO savedNews = NewsManager.GetByID(id);
                if (savedNews == null) throw new Exception("News not found");
                if (savedNews.CreateUser != Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value)) throw new UnauthorizedAccessException();

                NewsManager.Delete(id);

                ImageExtensions.DeleteImage(GetNewsImagePath(id));

                return new DeleteNewsResult();
            }
            catch (Exception ex)
            {
                return new DeleteNewsResult(ex);
            }
        }
    }
}
