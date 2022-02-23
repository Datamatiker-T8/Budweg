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
        public string ModelNumber { get; set; }
        public QRCode QR_Code { get; set; } // Todo 

        public List<Ressource> ressources;
        public string LinkQRCode { get; set; }

        public BrakeCaliber(string modelNumber, string linkQRCode)
        {
            ModelNumber = modelNumber;
            LinkQRCode = linkQRCode;
            GenerateQRCode();
        }

        public Bitmap GenerateQRCode()
        {
            //Readies QRCoder
            QRCodeGenerator qrGenerator = new();

            // Creates QRCode for the Link associated with the object
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(LinkQRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode QR_Code = new(qrCodeData);

            // Saves QRCode in .exe folder and should show in the program (for now its done in the console)
            Bitmap qrCodeImage = QR_Code.GetGraphic(20);
            string filepathTest = "C:\\Users\\Mads\\1. studieår\\Projekter\\Budweg"; // TEST
            string filepath = filepathTest + ".jpg";
            qrCodeImage.Save(filepath);

            return qrCodeImage;
        }
    }
}
