using CodeJewels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeJewels.Services.ModelValidators
{
    public static class Validator
    {
        public static void ValidateCodeJewel(CodeJewel codeJewel)
        {
            if (codeJewel == null)
            {
                throw new ArgumentNullException("codeJewel", "code jewel can't be null");
            }

            ValidateCategory(codeJewel.Category);
            
            if (codeJewel.AuthorEMail == null)
            {
                throw new ArgumentNullException("AuthorEMail", "code jewel's authorEMail can't be null");
            }

            if (codeJewel.Code == null)
            {
                throw new ArgumentNullException("Code", "code jewel's code can't be null");
            }
        }

        public static void ValidateCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("Category", "Category can't be null");
            }

            if (category.Name == null)
            {
                throw new ArgumentNullException("Category name", "Category name can't be null");
            }
        }

        internal static void ValidateVote(Vote vote)
        {
            if (vote == null)
            {
                throw new ArgumentNullException("Vote", "Vote can't be null!");
            }

            if (vote.IsUpVote == null)
            {
                throw new ArgumentNullException("IsUpVote", "IsUpVote can't be null!");
            }
        }
    }
}