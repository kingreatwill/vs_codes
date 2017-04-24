using Lazywg.Helper;
using Lazywg.Logger;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace FFES.Refresh
{
    public partial class RefreshForm : Form
    {
        /// <summary>
        /// UI线程的同步上下文
        /// </summary>
        SynchronizationContext m_SyncContext = null;
        Form _form = null;
        char[] split = { ';', '；' };
        public RefreshForm()
        {
            InitializeComponent();
            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext.Current;
            _form = this;
            //new Thread(RequestUrl).Start();
            RequestUrl();
        }


        private void button_vistor_Click(object sender, EventArgs e)
        {
            string url = this.textBox_url.Text;
            if (!string.IsNullOrEmpty(url))
            {
                string[] array = url.Split(split);
                foreach (var item in array)
                {
                    HttpGet(item);
                }
            }
        }

        private void RequestUrl()
        {
            //while (true)
            //{
               List<HttpJson> jsons = HttpJsonLoad.LoadFile(RefreshConfig.HttpJsonFile);
                foreach (var item in jsons)
                {
                    new Thread(HandlerHttpJson).Start(item);
                }
            //    Thread.Sleep(FFESRefresh.RefreshConfig.SleepTime);
            //}
        }

        private void HandlerHttpJson(object obj) {
            while (true)
            {
                m_SyncContext.Post(SetForm, RefreshConfig.IsDebug);

                HttpJson json = (HttpJson)obj;
                string result = string.Empty;
                if (string.Equals(json.HttpMethod,"get",StringComparison.OrdinalIgnoreCase))
                {
                    result = HttpHelper.HttpGet(json.Url, JsonHelper.DeserializeJsonToDict(json.Params));
                }
                else
                {
                    result = HttpHelper.HttpPost(json.Url, JsonHelper.DeserializeJsonToDict(json.Params));
                }
                LogHelper.WriteLog(json.HttpMethod, string.Format("url:{0}", json.Url));
                LogHelper.WriteLog(json.HttpMethod, string.Format("result:{0}", result));
                if (RefreshConfig.IsDebug)
                {
                    string txt = string.Format("{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + result);
                    m_SyncContext.Post(SetResult, txt);
                }
                Thread.Sleep(json.SleepTime * 1000);
            }
        }
        private void HttpGet(string url)
        {
            m_SyncContext.Post(SetForm, RefreshConfig.IsDebug);

            LogHelper.WriteLog("HttpGet", string.Format("url:{0}", url));
            string result = HttpHelper.HttpGet(url, string.Empty);
            LogHelper.WriteLog("HttpGet", string.Format("result:{0}", result));
            if (RefreshConfig.IsDebug)
            {
                string txt = string.Format("{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + result);
                m_SyncContext.Post(SetResult, txt);
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            this.textBox_result.Text = string.Empty;
        }

        //第三步：定义更新UI控件的方法
        /// <summary>
        /// 更新文本框内容的方法
        /// </summary>
        /// <param name="text"></param>
        private void SetResult(object text)
        {
            string txt = this.textBox_result.Text + text.ToString();
            this.textBox_result.Text = txt;
        }

        private void SetForm(object state)
        {
            bool isDebug = (bool)state;
            if (!isDebug)
            {
                _form.Hide();
                return;
            }
            _form.Show();
        }
    }
}
