using Linkup.Common;
using Newtonsoft.Json;
using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace Sheng.Enterprise.Web
{
    public class EnterpriseController : Controller
    {
        public UserContext UserContext { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowedAnonymous), false).Length != 0)
            {
                return;
            }

            this.UserContext = SessionContainer.GetUserContext(filterContext.HttpContext);
            if (this.UserContext == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Login");
                return;
            }

            List<string> list = new List<string>();
            int num = 2015;
            int year = DateTime.Now.Year;
            do
            {
                list.Add(num.ToString());
                num++;
            } while (num <= year);

            ViewBag.UserContext = this.UserContext;
            ViewBag.RootOrganization = this.UserContext.RootOrganization;
            ViewBag.User = this.UserContext.User;
            ViewBag.Domain = this.UserContext.Domain;
            ViewBag.YearList = list;
        }

        protected T RequestArgs<T>() where T : class
        {
            return JsonConvert.DeserializeObject<T>(new StreamReader(HttpContext.Request.InputStream).ReadToEnd());
        }


        protected virtual ContentResult RespondResult()
		{
			return this.RespondResult(true, null);
		}

		protected virtual ContentResult RespondResult(bool success, string message)
		{
			return this.RespondResult(success, message, null);
		}

		protected virtual ContentResult RespondResult(bool success, string message, object data)
		{
			ApiResult apiResult = new ApiResult
			{
				Success = success,
				Message = message,
				Data = data
			};
			return this.RespondResult(apiResult);
		}

		protected virtual ContentResult RespondResult(ApiResult apiResult)
		{
			return new ContentResult
			{
				ContentEncoding = Encoding.UTF8,
				Content = JsonHelper.NewtonsoftSerializer(apiResult)
			};
		}

		protected virtual ContentResult RespondDataResult(object data)
		{
			return this.RespondResult(true, null, data);
		}
	}
}
