namespace EmailSender.Backend.EmailService.Models
{
    public class ErrorResult
    {
        public string ErrorCode { get; set; }
        
        public string ErrorDesc { get; set; }
        
        public string InnerMessage { get; set; }
    }
}