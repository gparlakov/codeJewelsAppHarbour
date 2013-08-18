using CodeJewels.Data;
using CodeJewels.Models;
using CodeJewels.Services.Models;
using CodeJewels.Services.ModelValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeJewels.Services.Controllers
{
    public class VotesController : BaseApiController
    {
        // GET api/votes/getVotesFor/{codeJewelId}
        [ActionName("getVotesFor")]
        public IQueryable<VoteModel> GetVotes(int codeJewelId)
        {
            return this.ProcessAndHandleExceptions(() =>
            {
                if (codeJewelId <= 0)
                {
                    throw new ArgumentOutOfRangeException("codeJewelId", "id of code hewel must be positive number");
                }

                var context = new CodeJewelsDb();
                var votes = context.Votes.Where(v => v.CodeJewel.Id == codeJewelId);

                if (votes == null)
                {
                    return new List<VoteModel>().AsQueryable();
                }

                var votesModel = votes.Select(v => new VoteModel
                {
                    Id = v.Id,
                    CodeJewelId = codeJewelId,
                    IsUpVote = v.IsUpVote
                });

                return votesModel;
            });
        }
        // POST api/votes/vote/{codeJewelId}
        [ActionName("vote")]
        public HttpResponseMessage PostVote([FromUri]int codeJewelId, [FromBody]Vote vote)
        {
            return this.ProcessAndHandleExceptions(() =>
            {
                Validator.ValidateVote(vote);

                var dbContext = new CodeJewelsDb();
                var codeJewel = dbContext.CodeJewels.Include("Category").FirstOrDefault(cj => cj.Id == codeJewelId);
                
                if (codeJewel == null)
                {
                    throw new ArgumentOutOfRangeException("No such Code jewel found - invalid vote");
                }

                vote.CodeJewel = codeJewel;
                dbContext.Votes.Add(vote);
                dbContext.SaveChanges();
                
                var voteDetails = new VoteDetails
                {
                    Id = vote.Id,
                    CodeJewel = new CodeJewelModel
                    {
                        Id = codeJewel.Id,
                        Category = codeJewel.Category.Name,
                        Code = codeJewel.Code.Length >= 20 ? codeJewel.Code.Substring(0, 20) : codeJewel.Code,
                    },
                    IsUpVote = vote.IsUpVote,
                };

                var response = this.Request.CreateResponse(HttpStatusCode.Created, voteDetails);
                var location = new Uri(this.Url.Link("CodeJewelVoteApi", new { id = vote.Id }));
                response.Headers.Location = location;

                return response;
            });
        }
    }
}
