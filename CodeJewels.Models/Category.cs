using System.Collections.Generic;

namespace CodeJewels.Models
{
    public class Category
    {
        public int Id { get; set; }

        public virtual ICollection<CodeJewel> CodeJewels { get; set; }

        public string Name { get; set; }
    }
}