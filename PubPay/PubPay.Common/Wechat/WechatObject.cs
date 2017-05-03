using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PubPay.Common.Wechat
{
    [Serializable]
    public abstract class WechatObject
    {
        /// <summary>
        /// 商户公众号
        /// </summary>
        [XmlElement("appid")]
        public string AppID { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [XmlElement("mch_id")]
        public string MerchantID { get; set; }

        /// <summary>
        /// 子商户公众号
        /// </summary>
        [XmlElement("sub_appid")]
        public string SubMerchantAppID { get; set; }

        /// <summary>
        /// 子商户号
        /// </summary>
        [XmlElement("sub_mch_id")]
        public string SubMerchantID { get; set; }

        /// <summary>
        ///设备号 终端设备号(门店号或收银设备ID)，注意：PC网页或公众号内支付请传"WEB"
        /// </summary>
        [XmlElement("device_info")]
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 随机字符串 不长于32位
        /// </summary>
        [XmlElement("nonce_str")]
        public string NonceStr { get; set; }

        /// <summary>
        /// 签名类型 HMAC-SHA256 签名类型，目前支持HMAC-SHA256和MD5，默认为MD5
        /// </summary>
        [XmlElement("sign_type")]
        public string SignType { get; set; }
    }
}
