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
        private byte[] qR_Bytes;

        public byte[] QR_Bytes
        {
            get { return qR_Bytes; }
            set
            {
                if (value == null)
                {
                    qR_Bytes = GenerateQRCode(LinkQRCode);
                }
                else
                {
                    qR_Bytes = value;
                }
            }
        }
        public int BrakeCaliberId { get; set; }
        public string CaliberName { get; set; }
        public string BudwegNo { get; set; }
        public bool StockStatus { get; set; }
        public string BrakeSystem { get; set; }
        public string LinkQRCode { get; set; }
        // Association
        public List<Feedback> Feedbacks;
        public List<Resource> Ressources;
        public BrakeCaliber(int brakeCaliberId, string caliberName, string budwegNo, bool stockStatus, string brakeSystem, string linkQRCode, byte[] qR_Bytes)
        {
            BrakeCaliberId = brakeCaliberId;
            CaliberName = caliberName;
            BudwegNo = budwegNo;
            StockStatus = stockStatus;
            BrakeSystem = brakeSystem;
            LinkQRCode = linkQRCode;
            Feedbacks = new();
            Ressources = new();
            QR_Bytes = qR_Bytes;
        }

        public BrakeCaliber(string caliberName, string budwegNo, bool stockStatus, string brakeSystem, string linkQRCode, byte[] qR_Bytes)
            : this(-1, caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode, qR_Bytes)
        { }
        public BrakeCaliber() : this(-1, "", "", false, "", "", null)
        { }

        public static byte[] GenerateQRCode(string linkQRCode)
        {
            //Readies QRCoder
            QRCodeGenerator qrGenerator = new();

            // Creates QRCode for the Link associated with the object
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(linkQRCode, QRCodeGenerator.ECCLevel.M);
            byte[] QRarray = qrCodeData.GetRawData(QRCodeData.Compression.Uncompressed);

            return QRarray;
        }
    }
}
