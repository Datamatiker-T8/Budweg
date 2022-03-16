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
        // Propertys
        public int BrakeCaliberId { get; set; }
        public string CaliberName { get; set; }
        public string BudwegNo { get; set; }
        public bool StockStatus { get; set; }
        public string BrakeSystem { get; set; }
        public string LinkQRCode { get; set; }
        public byte[] QR_Code { get; set; }
        // Association
        public List<Feedback> Feedbacks;
        public List<Resource> Ressources;
        public BrakeCaliber(int brakeCaliberId, string caliberName, string budwegNo, bool stockStatus, string brakeSystem, string linkQRCode)
        {
            BrakeCaliberId = brakeCaliberId;
            CaliberName = caliberName;
            BudwegNo = budwegNo;
            StockStatus = stockStatus;
            BrakeSystem = brakeSystem;
            LinkQRCode = linkQRCode;
            Feedbacks = new();
            Ressources = new();
            QR_Code = (linkQRCode.Length > 0) ? GenerateQRCode(linkQRCode) : null;
        }

        public BrakeCaliber(string caliberName, string budwegNo, bool stockStatus, string brakeSystem, string linkQRCode) 
            : this( -1, caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode)
        { }
        public BrakeCaliber() : this(-1, "", "", false, "", "")
        { }

        public static byte[] GenerateQRCode(string linkQRCode)
        {
            //Readies QRCoder
            QRCodeGenerator qrGenerator = new();

            // Creates QRCode for the Link associated with the object
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(linkQRCode, QRCodeGenerator.ECCLevel.Q);
            byte[] pic = qrCodeData.GetRawData(QRCodeData.Compression.Uncompressed);

            // visual image of QR-code
            QRCode QR_Code = new(qrCodeData);
            Bitmap qrCodeImage = QR_Code.GetGraphic(20);

            return pic;
        }
    }
}
