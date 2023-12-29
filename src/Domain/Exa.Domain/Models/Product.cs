using Exa.Configure.Models;
using Exa.Domain.SubModels;
using System.Reflection.Metadata.Ecma335;

namespace Exa.Domain.Models
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public string CategoryId { get; set; } = string.Empty;
        public List<Image> Images { get; set; } = new List<Image>();
        public List<string> Tags { get; set; } = new List<string>();
    }
}
