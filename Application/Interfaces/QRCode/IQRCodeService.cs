namespace Application.Interfaces.QRCode;

public interface IQRCodeService
{
    byte[] GenerateQRCodeGenerate(string text);
}
