namespace react.pizza.backend.Models
{
    public class CatalogItemDto
    {
        public int Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Cost { get; set; }
    }
}