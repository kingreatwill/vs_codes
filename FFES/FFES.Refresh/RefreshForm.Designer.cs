namespace FFES.Refresh
{
    partial class RefreshForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_top = new System.Windows.Forms.Panel();
            this.button_vistor = new System.Windows.Forms.Button();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.label_url = new System.Windows.Forms.Label();
            this.textBox_result = new System.Windows.Forms.TextBox();
            this.label_result = new System.Windows.Forms.Label();
            this.button_clear = new System.Windows.Forms.Button();
            this.panel_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_top
            // 
            this.panel_top.Controls.Add(this.button_vistor);
            this.panel_top.Controls.Add(this.textBox_url);
            this.panel_top.Controls.Add(this.label_url);
            this.panel_top.Location = new System.Drawing.Point(12, 22);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(655, 49);
            this.panel_top.TabIndex = 0;
            // 
            // button_vistor
            // 
            this.button_vistor.Location = new System.Drawing.Point(570, 11);
            this.button_vistor.Name = "button_vistor";
            this.button_vistor.Size = new System.Drawing.Size(75, 23);
            this.button_vistor.TabIndex = 2;
            this.button_vistor.Text = "访问";
            this.button_vistor.UseVisualStyleBackColor = true;
            this.button_vistor.Click += new System.EventHandler(this.button_vistor_Click);
            // 
            // textBox_url
            // 
            this.textBox_url.Location = new System.Drawing.Point(62, 13);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(493, 21);
            this.textBox_url.TabIndex = 1;
            this.textBox_url.Text = "http://localhost:8081/api/dict/RefreshDictionary";
            // 
            // label_url
            // 
            this.label_url.AutoSize = true;
            this.label_url.Location = new System.Drawing.Point(3, 16);
            this.label_url.Name = "label_url";
            this.label_url.Size = new System.Drawing.Size(53, 12);
            this.label_url.TabIndex = 0;
            this.label_url.Text = "访问地址";
            // 
            // textBox_result
            // 
            this.textBox_result.Location = new System.Drawing.Point(12, 121);
            this.textBox_result.Multiline = true;
            this.textBox_result.Name = "textBox_result";
            this.textBox_result.Size = new System.Drawing.Size(655, 395);
            this.textBox_result.TabIndex = 1;
            // 
            // label_result
            // 
            this.label_result.AutoSize = true;
            this.label_result.Location = new System.Drawing.Point(12, 93);
            this.label_result.Name = "label_result";
            this.label_result.Size = new System.Drawing.Size(53, 12);
            this.label_result.TabIndex = 2;
            this.label_result.Text = "返回结果";
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(539, 88);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(118, 23);
            this.button_clear.TabIndex = 3;
            this.button_clear.Text = "清空结果数据";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // RefreshForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 546);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.label_result);
            this.Controls.Add(this.textBox_result);
            this.Controls.Add(this.panel_top);
            this.Name = "RefreshForm";
            this.Text = "FFES";
            this.panel_top.ResumeLayout(false);
            this.panel_top.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.Label label_url;
        private System.Windows.Forms.Button button_vistor;
        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.TextBox textBox_result;
        private System.Windows.Forms.Label label_result;
        private System.Windows.Forms.Button button_clear;
    }
}

