using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeJewels.Services.Models
{
    public class VoteModel
    {
        public int Id { get; set; }

        public int CodeJewelId { get; set; }

        public bool IsUpVote { get; set; }
    }
}