namespace ResumeProjectDemo.Entities
{
    public class Testimonial
    {
        public int TestimonialId { get; set; }
        public string NameSurname { get; set; }
        public string TitleAndCompany { get; set; }
        public string CommentDetail { get; set; }
        public string ImageUrl { get; set; }
        public bool IsConfirm { get; set; }
    }
}
