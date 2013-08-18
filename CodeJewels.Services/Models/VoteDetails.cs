using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeJewels.Services.Models
{
    public class VoteDetails
    {
        public bool IsUpVote { get; set; }

        public CodeJewelModel CodeJewel { get; set; }

        public int Id { get; set; }
    }
}