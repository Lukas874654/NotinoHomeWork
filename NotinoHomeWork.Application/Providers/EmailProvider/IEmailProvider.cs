namespace NotinoHomeWork.Application.Providers.EmailProvider
{
    public interface IEmailProvider
    {
        public void SendFile(string toEmail, string filePath);
    }
}
