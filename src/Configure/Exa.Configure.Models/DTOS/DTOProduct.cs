namespace Exa.Configure.Models.DTOS
{
    public class DTOProduct : DTOBase
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public string CategoryId { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new List<string>();
        public List<string> Tags { get; set; } = new List<string>();
    }
}
