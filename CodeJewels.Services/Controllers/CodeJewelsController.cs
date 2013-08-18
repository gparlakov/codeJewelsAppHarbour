using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeJewels.Data;
using CodeJewels.Models;
using CodeJewels.Services.Models;
using CodeJewels.Services.ModelValidators;

namespace CodeJewels.Services.Controllers
{
    public class CodeJewelsController : BaseApiController
    {
        // GET api/codejewels
        public IQueryable<CodeJewelModel> Get()
        {
            var collection = this.ProcessAndHandleExceptions(() =>
            {
                var context = new CodeJewelsDb();
                var jewels = context.CodeJewels.Select(c => new CodeJewelModel
                {
                    Id = c.Id,
                    Category = c.Category.Name,
                    Code = c.Code.Substring(0, 20),
                });

                return jewels;
            });

            return collection;
        }

        // GET api/codejewels?category=c#&code={code}
        public IQueryable<CodeJewelModel> Get(string category, string code)
        {
            var found = this.Get();
            if (category != null)
            {
                found = found.Where(c => c.Category == category);
            }
            if (code != null)
            {
                found = found.Where(c => c.Code.Contains(code));
            }

            return found;
        }


        // POST api/codejewels
        public HttpResponseMessage Post([FromBody]CodeJewel codeJewel)
        {
            return this.ProcessAndHandleExceptions(() => 
            { 
                Validator.ValidateCodeJewel(codeJewel);

                var dbContext = new CodeJewelsDb();

                GetOrCreateCategory(codeJewel, dbContext);

                dbContext.CodeJewels.Add(codeJewel);
                dbContext.SaveChanges();
                var codeJewelDetails = new CodeJewelDetails 
                {
                    Id = codeJewel.Id,
                    Category = codeJewel.Category.Name,
                    AuthorEmail = codeJewel.AuthorEMail,
                    Code = codeJewel.Code
                };

                var response = this.Request.CreateResponse(HttpStatusCode.Created, codeJewelDetails);

                var location = new Uri(this.Url.Link("DefaultApi", new { id = codeJewel.Id }));
                response.Headers.Location = location;

                return response;
            });
        }
        
        private void GetOrCreateCategory(CodeJewel codeJewel, CodeJewelsDb dbContext)
        {
            var categoryInDb = dbContext.Categories.FirstOrDefault(c => c.Name == codeJewel.Category.Name);
            if (categoryInDb != null)
            {
                codeJewel.Category = categoryInDb;
            }
            else
            {
                dbContext.Categories.Add(codeJewel.Category);
                dbContext.SaveChanges();
            }
        }

    }
}
