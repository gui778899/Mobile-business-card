using Newtonsoft.Json;
using PWApp.Data.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWApp.Helpers
{
    public static class QrGenerator
    {
        public static byte[] Generate(CustomerModel item)
        {
            string qrDataAsString = JsonConvert.SerializeObject(item);
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            byte[] stringCompressed = StringCompressor.Zip(qrDataAsString);
            string result = Encoding.UTF8.GetString(stringCompressed);
            //byte[] bytes = Encoding.UTF8.GetBytes(result);
            //string decompressedData = StringCompressor.Unzip(bytes);
            QRCodeData codeData = qrCodeGenerator.CreateQrCode(qrDataAsString, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode pngByteQRCode = new PngByteQRCode(codeData);
            byte r, g, b, a;
            Color.FromHex("#002748").ToRgba(out r, out g, out b, out a);
            byte[] darkcolor = { r, g, b, a };
            Color.FromHex("#FFF").ToRgba(out r, out g, out b, out a);
            byte[] lightcolor = { r, g, b, a };
            byte[] qrCodeBytes = pngByteQRCode.GetGraphic(10, darkcolor, lightcolor, true);
            return qrCodeBytes;
        }
    }
}
