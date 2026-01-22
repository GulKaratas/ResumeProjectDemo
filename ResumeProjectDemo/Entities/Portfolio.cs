namespace ResumeProjectDemo.Entities
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
