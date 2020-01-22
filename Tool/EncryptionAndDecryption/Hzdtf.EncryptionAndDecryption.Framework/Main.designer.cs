namespace Hzdtf.EncryptionAndDecryption.Framework
{
    partial class Main
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPlaintext = new System.Windows.Forms.TextBox();
            this.groupbox2 = new System.Windows.Forms.GroupBox();
            this.txtCiphertext = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnCopyCiphertext = new System.Windows.Forms.Button();
            this.btnCopyPlaintext = new System.Windows.Forms.Button();
            this.btnDecode = new System.Windows.Forms.Button();
            this.btnPasteToPlaintext = new System.Windows.Forms.Button();
            this.btnPasteCiphertext = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupbox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPlaintext);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 163);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "明文";
            // 
            // txtPlaintext
            // 
            this.txtPlaintext.Location = new System.Drawing.Point(15, 20);
            this.txtPlaintext.Multiline = true;
            this.txtPlaintext.Name = "txtPlaintext";
            this.txtPlaintext.Size = new System.Drawing.Size(729, 124);
            this.txtPlaintext.TabIndex = 0;
            // 
            // groupbox2
            // 
            this.groupbox2.Controls.Add(this.txtCiphertext);
            this.groupbox2.Location = new System.Drawing.Point(12, 245);
            this.groupbox2.Name = "groupbox2";
            this.groupbox2.Size = new System.Drawing.Size(760, 163);
            this.groupbox2.TabIndex = 1;
            this.groupbox2.TabStop = false;
            this.groupbox2.Text = "密文";
            // 
            // txtCiphertext
            // 
            this.txtCiphertext.Location = new System.Drawing.Point(15, 20);
            this.txtCiphertext.Multiline = true;
            this.txtCiphertext.Name = "txtCiphertext";
            this.txtCiphertext.Size = new System.Drawing.Size(729, 124);
            this.txtCiphertext.TabIndex = 0;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(181, 205);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 2;
            this.btnEncrypt.Text = "加密";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnCopyCiphertext
            // 
            this.btnCopyCiphertext.Location = new System.Drawing.Point(262, 205);
            this.btnCopyCiphertext.Name = "btnCopyCiphertext";
            this.btnCopyCiphertext.Size = new System.Drawing.Size(75, 23);
            this.btnCopyCiphertext.TabIndex = 3;
            this.btnCopyCiphertext.Text = "复制密文";
            this.btnCopyCiphertext.UseVisualStyleBackColor = true;
            this.btnCopyCiphertext.Click += new System.EventHandler(this.btnCopyCiphertext_Click);
            // 
            // btnCopyPlaintext
            // 
            this.btnCopyPlaintext.Location = new System.Drawing.Point(571, 205);
            this.btnCopyPlaintext.Name = "btnCopyPlaintext";
            this.btnCopyPlaintext.Size = new System.Drawing.Size(75, 23);
            this.btnCopyPlaintext.TabIndex = 5;
            this.btnCopyPlaintext.Text = "复制明文";
            this.btnCopyPlaintext.UseVisualStyleBackColor = true;
            this.btnCopyPlaintext.Click += new System.EventHandler(this.btnCopyPlaintext_Click);
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(490, 205);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(75, 23);
            this.btnDecode.TabIndex = 4;
            this.btnDecode.Text = "解密";
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // btnPasteToPlaintext
            // 
            this.btnPasteToPlaintext.Location = new System.Drawing.Point(100, 205);
            this.btnPasteToPlaintext.Name = "btnPasteToPlaintext";
            this.btnPasteToPlaintext.Size = new System.Drawing.Size(75, 23);
            this.btnPasteToPlaintext.TabIndex = 6;
            this.btnPasteToPlaintext.Text = "粘贴到明文";
            this.btnPasteToPlaintext.UseVisualStyleBackColor = true;
            this.btnPasteToPlaintext.Click += new System.EventHandler(this.btnPasteToPlaintext_Click);
            // 
            // btnPasteCiphertext
            // 
            this.btnPasteCiphertext.Location = new System.Drawing.Point(409, 205);
            this.btnPasteCiphertext.Name = "btnPasteCiphertext";
            this.btnPasteCiphertext.Size = new System.Drawing.Size(75, 23);
            this.btnPasteCiphertext.TabIndex = 7;
            this.btnPasteCiphertext.Text = "粘贴到密文";
            this.btnPasteCiphertext.UseVisualStyleBackColor = true;
            this.btnPasteCiphertext.Click += new System.EventHandler(this.btnPasteCiphertext_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 420);
            this.Controls.Add(this.btnPasteCiphertext);
            this.Controls.Add(this.btnPasteToPlaintext);
            this.Controls.Add(this.btnCopyPlaintext);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.btnCopyCiphertext);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.groupbox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "加解密";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupbox2.ResumeLayout(false);
            this.groupbox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPlaintext;
        private System.Windows.Forms.GroupBox groupbox2;
        private System.Windows.Forms.TextBox txtCiphertext;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnCopyCiphertext;
        private System.Windows.Forms.Button btnCopyPlaintext;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.Button btnPasteToPlaintext;
        private System.Windows.Forms.Button btnPasteCiphertext;
    }
}

