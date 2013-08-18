using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeJewels.Services.Controllers
{
    public class BaseApiController : ApiController
    {
        protected T ProcessAndHandleExceptions<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (ArgumentNullException ex) 
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    string.Format("{0} is not specified. Please specify it!", ex.ParamName)));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new HttpResponseException(
                    this.Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                throw new HttpResponseException(
                    this.Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(
                    this.Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message));
            }
        }
    }
}