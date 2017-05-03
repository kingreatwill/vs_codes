using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace PubPay.Common.Alipay
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 版本：3.4
    /// 修改日期：2016-03-08
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// </summary>
    public class AlipayConfig
    {

        //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

        // 合作身份者ID，签约账号，以2088开头由16位纯数字组成的字符串，查看地址：https://b.alipay.com/order/pidAndKey.htm
        public static string Partner
        {
            get
            {
                return ConfigurationManager.AppSettings["Partner"];
            }
        }

        // 收款支付宝账号，以2088开头由16位纯数字组成的字符串，一般情况下收款账号就是签约账号
        public static string SellerID = Partner;

        // MD5密钥，安全检验码，由数字和字母组成的32位字符串，查看地址：https://b.alipay.com/order/pidAndKey.htm
        public static string MD5Key
        {
            get
            {
                return ConfigurationManager.AppSettings["MD5Key"];
            }
        }

        //商户的私钥,原始格式，RSA公私钥生成：https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.nBDxfy&treeId=58&articleId=103242&docType=1
        public static string RSAKey
        {
            get
            {
                return ConfigurationManager.AppSettings["RSAKey"];
            }
        }

        //商户的私钥,原始格式，RSA公私钥生成：https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.nBDxfy&treeId=58&articleId=103242&docType=1
        public static string RSAKeyFile
        {
            get
            {
                return ConfigurationManager.AppSettings["RSAKeyFile"];
            }
        }

        //支付宝的公钥，查看地址：https://b.alipay.com/order/pidAndKey.htm 
        public static string AlipayKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AlipayKey"];
            }
        }

        /// <summary>
        /// 服务网关
        /// </summary>
        public static string ServerUrl_Gatway
        {
            get
            {
                return ConfigurationManager.AppSettings["GatWay"];
            }
        }

        // 服务器异步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数,必须外网可以正常访问
        public static string NotifyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["NotifyUrl"];
            }
        }
        // 页面跳转同步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数，必须外网可以正常访问
        public static string ReturnUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ReturnUrl"];
            }
        }

        // 签名方式
        public static string SignType
        {
            get
            {
                return ConfigurationManager.AppSettings["SignType"];
            }
        }

        // 调试用，创建TXT日志文件夹路径，见AlipayCore.cs类中的LogResult(string sWord)打印方法。
        public static string LogPath
        {
            get
            {
                return ConfigurationManager.AppSettings["LogPath"];
            }
        }

        // 字符编码格式 目前支持 gbk 或 utf-8
        public static string InputCharset
        {
            get
            {
                return ConfigurationManager.AppSettings["InputCharset"];
            }
        }

        // 支付类型 ，无需修改
        public static string PaymentType
        {
            get
            {
                return ConfigurationManager.AppSettings["PaymentType"];
            }
        }

        /// <summary>
        /// 即时支付 服务名
        /// </summary>
        public static string ApiName_InstantPayment = "create_direct_pay_by_user";

        /// <summary>
        /// 服务网关
        /// </summary>
        public static string ServerUrl_AppGatway
        {
            get
            {
                return ConfigurationManager.AppSettings["AppGatWay"];
            }
        }

        /// <summary>
        /// 应用ID
        /// </summary>
        public static string AppID
        {
            get
            {
                return ConfigurationManager.AppSettings["AppID"];
            }
        }

        /// <summary>
        /// 应用 RSA key
        /// </summary>
        public static string App_RSAKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AppRSAKey"];
            }
        }

        /// <summary>
        /// 应用 RSA key file
        /// </summary>
        public static string App_RSAKeyFile
        {
            get
            {
                return ConfigurationManager.AppSettings["AppRSAKeyFile"];
            }
        }
        /// <summary>
        /// 应用 支付宝公钥
        /// </summary>
        public static string App_AlipayKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AppAlipayKey"];
            }
        }

        // 服务器异步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数,必须外网可以正常访问
        public static string App_NotifyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["AppNotifyUrl"];
            }
        }

        // 页面跳转同步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数，必须外网可以正常访问
        public static string App_ReturnUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["AppReturnUrl"];
            }
        }

        //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑


        //↓↓↓↓↓↓↓↓↓↓请在这里配置防钓鱼信息，如果没开通防钓鱼功能，请忽视不要填写 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

        //防钓鱼时间戳  若要使用请调用类文件submit中的Query_timestamp函数
        public static string AntiPhishingKey = "";

        //客户端的IP地址 非局域网的外网IP地址，如：221.0.0.1
        public static string ExterInvokeIp = "";

        //↑↑↑↑↑↑↑↑↑↑请在这里配置防钓鱼信息，如果没开通防钓鱼功能，请忽视不要填写 ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    }
}