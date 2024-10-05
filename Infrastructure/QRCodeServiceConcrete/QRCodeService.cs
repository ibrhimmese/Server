using Application.Interfaces.QRCode;
using QRCoder;

namespace Infrastructure.QRCodeServiceConcrete;

public class QRCodeService : IQRCodeService
{
    public byte[] GenerateQRCodeGenerate(string text)
    {
        QRCodeGenerator generator = new QRCodeGenerator();
        QRCodeData data = generator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new(data);
        byte[] byteGraphic = qrCode.GetGraphic(10, new byte[] { 84, 99, 71 }, new byte[] { 240, 240, 240 });
        return byteGraphic;
    }


}
