namespace NetCoreWebApiDemo.Models.Product
{
    public class ProductSaveDto
    {
        /// <example>Macbook Pro</example>
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
