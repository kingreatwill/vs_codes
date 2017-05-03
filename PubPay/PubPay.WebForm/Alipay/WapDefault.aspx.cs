using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using PubPay.Common;
using PubPay.Common.Alipay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PubPay.WebForm.Alipay
{
    public partial class WapDefault : System.Web.UI.Page
    {
        TextLogger logger = new TextLogger("alipay");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAlipay_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //商户订单号，商户网站订单系统中唯一订单号，必填
            string out_trade_no = WIDout_trade_no.Text.Trim();

            //订单名称，必填
            string subject = WIDsubject.Text.Trim();

            //付款金额，必填
            string total_fee = WIDtotal_fee.Text.Trim();

            //商品描述，可空
            string body = WIDbody.Text.Trim();


            IAopClient client = null;
            if (string.IsNullOrEmpty(AlipayConfig.App_RSAKeyFile))
            {
                client = new DefaultAopClient(AlipayConfig.ServerUrl_AppGatway, AlipayConfig.AppID, AlipayConfig.App_RSAKey);
            }
            else
            {
                client = new DefaultAopClient(AlipayConfig.ServerUrl_AppGatway, AlipayConfig.AppID, RSAFromPkcs8.GetFileName(AlipayConfig.App_RSAKeyFile), true);
            }

            AlipayTradeWapPayRequest alipayRequest = new AlipayTradeWapPayRequest();//创建API对应的request
            alipayRequest.SetNotifyUrl(AlipayConfig.NotifyUrl);
            alipayRequest.SetReturnUrl(AlipayConfig.ReturnUrl);//在公共参数中设置回跳和通知地址
            AlipayTradeWapPayModel model = new AlipayTradeWapPayModel();
            model.SellerId = AlipayConfig.Partner;
            model.OutTradeNo = out_trade_no;
            model.TotalAmount = total_fee;
            model.Subject = subject;
            model.Body = body;
            alipayRequest.SetBizModel(model);

            string form = string.Empty;
            try
            {
                // form = client.SdkExecute(alipayRequest).Body; //调用SDK生成表单
                //form = client.Execute(alipayRequest).Body; //调用SDK生成表单  iform
                form = client.pageExecute(alipayRequest).Body; //调用SDK生成表单 new page
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            logger.WriteLog("请求：" + form);

            Response.ContentType = "text/html;charset=" + AlipayConfig.InputCharset;
            Response.Write(form);
        }
    }
}