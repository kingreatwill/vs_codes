using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;

namespace Lazywg.Common.Helper
{
    /// <summary>
    /// 二维码生成帮助类
    /// </summary>
    public class QRCodeHelper
    {
        private static QRCodeHelper _instance = null;
        private readonly static object _locker = new object();
        private QRCodeHelper() { }

        public static QRCodeHelper Instance
        {
            get
            {

                if (_instance == null)
                {
                    lock (_locker)
                    {
                        _instance = new QRCodeHelper();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 创建二维码图片
        /// </summary>
        /// <param name="codeStr">生成二维码数据</param>
        /// <param name="imgDir">图片存储目录</param>
        /// <param name="imgName">图片名称</param>
        /// <param name="scale">图片大小</param>
        /// <param name="centerImg">中间图片</param>
        /// <returns>返回是否创建成功</returns>
        public bool CreateQRCodeImg(string codeStr, string imgDir, out string imgName, int scale = 10, Image centerImg = null)
        {

            return CreateQRCode(codeStr, imgDir, out imgName, scale, centerImg);
        }

        /// <summary>
        /// 创建二维码图片
        /// </summary>
        /// <param name="codeStr">生成二维码数据</param>
        /// <param name="imgDir">图片存储目录</param>
        /// <param name="imgName">图片名称</param>
        /// <param name="encodeMode">编码模式</param>
        /// <param name="errorCorrect">容错方式</param>
        /// <param name="imgEncode">图片编码</param>
        /// <param name="scale">图片大小</param>
        /// <param name="centerImg">中间图片</param>
        /// <returns>返回是否创建成功</returns>
        public bool CreateQRCodeImg(string codeStr, string imgDir, string imgName, ImageFormat formate, int scale = 10, Image centerImg = null)
        {
            return CreateQRCode(codeStr, imgDir, imgName, formate, scale, centerImg);
        }

        /// <summary>
        /// 创建二维码图片
        /// </summary>
        /// <param name="codeStr"></param>
        /// <param name="imgDir"></param>
        /// <param name="imgName"></param>
        /// <param name="scale"></param>
        /// <param name="centerImg"></param>
        /// <returns></returns>
        private bool CreateQRCode(string codeStr, string imgDir, out string imgName, int scale = 10, Image centerImg = null)
        {

            //创建二维码生成类  
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式  
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度  
            qrCodeEncoder.QRCodeScale = scale;
            //设置编码版本  
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正  
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //qrCodeEncoder.QRCodeBackgroundColor = Color.Green;
            //qrCodeEncoder.QRCodeForegroundColor = Color.Brown;

            //生成二维码图片  
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeStr, Encoding.GetEncoding("UTF-8"));

            if (centerImg != null)
            {
                image = new Bitmap(CombinImage(image, centerImg,60));
            }

            return SaveImg(imgDir, image, out imgName);
        }

        private bool CreateQRCode(string codeStr, string imgDir, string imgName, ImageFormat formate, int scale = 10, Image centerImg = null)
        {

            //创建二维码生成类  
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式  
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度  
            qrCodeEncoder.QRCodeScale = scale;
            //设置编码版本  
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正  
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //qrCodeEncoder.QRCodeBackgroundColor = Color.Green;
            //qrCodeEncoder.QRCodeForegroundColor = Color.Brown;

            //生成二维码图片  
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeStr, Encoding.GetEncoding("UTF-8"));

            if (centerImg != null)
            {
                image = new Bitmap(CombinImage(image, centerImg, 60));
            }

            return SaveImg(imgDir, image, imgName, formate);
        }

        /// <summary>     
        /// 调用此函数后使此两种图片合并，类似相册，有个     
        /// 背景图，中间贴自己的目标图片     
        /// </summary>     
        /// <param name="imgBack">粘贴的源图片</param>     
        /// <param name="img">粘贴的目标图片</param>     
        private Image CombinImage(Image imgBack, Image img,int size)
        {
            if (img.Height != size || img.Width != size)
            {
                img = KiResizeImage(img, size, size, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);      

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框     

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);     

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }

        /// <summary>     
        /// Resize图片     
        /// </summary>     
        /// <param name="bmp">原始Bitmap</param>     
        /// <param name="newW">新的宽度</param>     
        /// <param name="newH">新的高度</param>     
        /// <param name="Mode">保留着，暂时未用</param>     
        /// <returns>处理以后的图片</returns>     
        private Image KiResizeImage(Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量     
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }

        private static bool SaveImg(string imgDir, Bitmap img, out string imgName)
        {
            //判断目录是否存在
            if (!Directory.Exists(imgDir))
            {
                //当前目录不存在，则创建  
                Directory.CreateDirectory(imgDir);
            }

            string guid = Guid.NewGuid().ToString().Replace("-", string.Empty);

            imgName = guid + ".png";

            string imgPath = imgDir + @"\" + imgName;

            img.Save(imgPath, ImageFormat.Png);
            return true;
        }

        private  bool SaveImg(string imgDir, Bitmap img, string imgName, ImageFormat format)
        {
            //判断目录是否存在
            if (!Directory.Exists(imgDir))
            {
                //当前目录不存在，则创建  
                Directory.CreateDirectory(imgDir);
            }

            imgName += GetImageExtention(format);

            string imgPath = imgDir + @"\" + imgName;

            img.Save(imgPath, format);

            return true;
        }

        private string GetImageExtention(ImageFormat format)
        {
            if (format == ImageFormat.Png)
            {
                return ".png";
            }
            if (format == ImageFormat.Jpeg)
            {
                return ".jpg";
            }
            if (format == ImageFormat.Icon)
            {
                return ".icon";
            }
            if (format == ImageFormat.Gif)
            {
                return ".gif";
            }
            throw new NotSupportedException(string.Format("不支持的图片类型：{0}", format.ToString()));
        }
    }
}
