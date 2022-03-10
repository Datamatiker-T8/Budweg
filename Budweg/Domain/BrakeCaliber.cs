using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using System.Drawing;

namespace Budweg.Domain
{
     public class BrakeCaliber
    {
        public int BrakeCaliberId { get; set; }
        public string BudwegNo { get; set; }
        public Bitmap QR_Code { get; set; }

        public List<Resource> ressources;
        public string LinkQRCode { get; set; }

        public BrakeCaliber(string modelNumber, string linkQRCode)
        {
            BudwegNo = modelNumber;
            LinkQRCode = linkQRCode;
            QR_Code = GenerateQRCode(linkQRCode);
        }

        public Bitmap GenerateQRCode(string linkQRCode)
        {
            //Readies QRCoder
            QRCodeGenerator qrGenerator = new();

            // Creates QRCode for the Link associated with the object
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(linkQRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode QR_Code = new(qrCodeData);
            Bitmap qrCodeImage = QR_Code.GetGraphic(20);

            return qrCodeImage;
        }
    }
}
