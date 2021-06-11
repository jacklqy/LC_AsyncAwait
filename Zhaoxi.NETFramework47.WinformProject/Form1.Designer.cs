namespace Zhaoxi.NETFramework47.WinformProject
{
    partial class Form1
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
            this.btnSync = new System.Windows.Forms.Button();
            this.btnAsync = new System.Windows.Forms.Button();
            this.lblAsyncResult = new System.Windows.Forms.Label();
            this.lblSync = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(69, 64);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(167, 61);
            this.btnSync.TabIndex = 0;
            this.btnSync.Text = "同步按钮";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnAsync
            // 
            this.btnAsync.Location = new System.Drawing.Point(69, 192);
            this.btnAsync.Name = "btnAsync";
            this.btnAsync.Size = new System.Drawing.Size(167, 58);
            this.btnAsync.TabIndex = 1;
            this.btnAsync.Text = "异步按钮";
            this.btnAsync.UseVisualStyleBackColor = true;
            this.btnAsync.Click += new System.EventHandler(this.btnAsync_Click);
            // 
            // lblAsyncResult
            // 
            this.lblAsyncResult.AutoSize = true;
            this.lblAsyncResult.Location = new System.Drawing.Point(345, 234);
            this.lblAsyncResult.Name = "lblAsyncResult";
            this.lblAsyncResult.Size = new System.Drawing.Size(67, 15);
            this.lblAsyncResult.TabIndex = 2;
            this.lblAsyncResult.Text = "异步结果";
            // 
            // lblSync
            // 
            this.lblSync.AutoSize = true;
            this.lblSync.Location = new System.Drawing.Point(345, 110);
            this.lblSync.Name = "lblSync";
            this.lblSync.Size = new System.Drawing.Size(67, 15);
            this.lblSync.TabIndex = 3;
            this.lblSync.Text = "同步结果";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblSync);
            this.Controls.Add(this.lblAsyncResult);
            this.Controls.Add(this.btnAsync);
            this.Controls.Add(this.btnSync);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnAsync;
        private System.Windows.Forms.Label lblAsyncResult;
        private System.Windows.Forms.Label lblSync;
    }
}

