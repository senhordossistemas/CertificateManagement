namespace CertificateManagement.Api.Models;

public class CertificateResponse(bool isSuccess, string message, int userId)
{
    public bool IsSuccess { get; set; } = isSuccess;
    public string Message { get; set; } = message;
    public int UserId { get; set; } = userId;
}