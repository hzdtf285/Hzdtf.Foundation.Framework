namespace Hzdtf.Compressor.Framework
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
            this.openJsFile = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectJs = new System.Windows.Forms.Button();
            this.btnJsCompress = new System.Windows.Forms.Button();
            this.labJsFile = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMergeCompress = new System.Windows.Forms.Button();
            this.btnSelectLanguageLib = new System.Windows.Forms.Button();
            this.lbxLanguageLibJsonFiles = new System.Windows.Forms.ListBox();
            this.openFileLanguageLib = new System.Windows.Forms.OpenFileDialog();
            this.txtJsonCompress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labCssFile = new System.Windows.Forms.Label();
            this.btnCssCompress = new System.Windows.Forms.Button();
            this.btnSelectCss = new System.Windows.Forms.Button();
            this.openCssFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labJsFile);
            this.groupBox1.Controls.Add(this.btnJsCompress);
            this.groupBox1.Controls.Add(this.btnSelectJs);
            this.groupBox1.Location = new System.Drawing.Point(23, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(757, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "JS单个文件压缩";
            // 
            // openJsFile
            // 
            this.openJsFile.DefaultExt = "js";
            this.openJsFile.FileName = "选择JS文件";
            this.openJsFile.Filter = "js文件|*.js";
            // 
            // btnSelectJs
            // 
            this.btnSelectJs.Location = new System.Drawing.Point(29, 33);
            this.btnSelectJs.Name = "btnSelectJs";
            this.btnSelectJs.Size = new System.Drawing.Size(75, 23);
            this.btnSelectJs.TabIndex = 0;
            this.btnSelectJs.Text = "选择JS文件";
            this.btnSelectJs.UseVisualStyleBackColor = true;
            this.btnSelectJs.Click += new System.EventHandler(this.btnSelectJs_Click);
            // 
            // btnJsCompress
            // 
            this.btnJsCompress.Location = new System.Drawing.Point(110, 33);
            this.btnJsCompress.Name = "btnJsCompress";
            this.btnJsCompress.Size = new System.Drawing.Size(75, 23);
            this.btnJsCompress.TabIndex = 1;
            this.btnJsCompress.Text = "压缩";
            this.btnJsCompress.UseVisualStyleBackColor = true;
            this.btnJsCompress.Click += new System.EventHandler(this.btnJsCompress_Click);
            // 
            // labJsFile
            // 
            this.labJsFile.AutoSize = true;
            this.labJsFile.Location = new System.Drawing.Point(27, 59);
            this.labJsFile.Name = "labJsFile";
            this.labJsFile.Size = new System.Drawing.Size(0, 12);
            this.labJsFile.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtJsonCompress);
            this.groupBox2.Controls.Add(this.lbxLanguageLibJsonFiles);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnMergeCompress);
            this.groupBox2.Controls.Add(this.btnSelectLanguageLib);
            this.groupBox2.Location = new System.Drawing.Point(23, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(757, 192);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "语系库Json合并压缩文件";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 2;
            // 
            // btnMergeCompress
            // 
            this.btnMergeCompress.Location = new System.Drawing.Point(431, 33);
            this.btnMergeCompress.Name = "btnMergeCompress";
            this.btnMergeCompress.Size = new System.Drawing.Size(75, 23);
            this.btnMergeCompress.TabIndex = 1;
            this.btnMergeCompress.Text = "合并压缩";
            this.btnMergeCompress.UseVisualStyleBackColor = true;
            this.btnMergeCompress.Click += new System.EventHandler(this.btnMergeCompress_Click);
            // 
            // btnSelectLanguageLib
            // 
            this.btnSelectLanguageLib.Location = new System.Drawing.Point(29, 33);
            this.btnSelectLanguageLib.Name = "btnSelectLanguageLib";
            this.btnSelectLanguageLib.Size = new System.Drawing.Size(75, 23);
            this.btnSelectLanguageLib.TabIndex = 0;
            this.btnSelectLanguageLib.Text = "选择语系库Json文件";
            this.btnSelectLanguageLib.UseVisualStyleBackColor = true;
            this.btnSelectLanguageLib.Click += new System.EventHandler(this.btnSelectLanguageLib_Click);
            // 
            // lbxLanguageLibJsonFiles
            // 
            this.lbxLanguageLibJsonFiles.FormattingEnabled = true;
            this.lbxLanguageLibJsonFiles.ItemHeight = 12;
            this.lbxLanguageLibJsonFiles.Location = new System.Drawing.Point(29, 59);
            this.lbxLanguageLibJsonFiles.Name = "lbxLanguageLibJsonFiles";
            this.lbxLanguageLibJsonFiles.Size = new System.Drawing.Size(705, 124);
            this.lbxLanguageLibJsonFiles.TabIndex = 3;
            // 
            // openFileLanguageLib
            // 
            this.openFileLanguageLib.FileName = "openFileDialog1";
            this.openFileLanguageLib.Filter = "Json文件|*.json";
            this.openFileLanguageLib.Multiselect = true;
            // 
            // txtJsonCompress
            // 
            this.txtJsonCompress.Location = new System.Drawing.Point(228, 33);
            this.txtJsonCompress.Name = "txtJsonCompress";
            this.txtJsonCompress.Size = new System.Drawing.Size(182, 21);
            this.txtJsonCompress.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "压缩后的文件前辍：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labCssFile);
            this.groupBox3.Controls.Add(this.btnCssCompress);
            this.groupBox3.Controls.Add(this.btnSelectCss);
            this.groupBox3.Location = new System.Drawing.Point(23, 337);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(757, 101);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CSS单个文件压缩";
            // 
            // labCssFile
            // 
            this.labCssFile.AutoSize = true;
            this.labCssFile.Location = new System.Drawing.Point(27, 59);
            this.labCssFile.Name = "labCssFile";
            this.labCssFile.Size = new System.Drawing.Size(0, 12);
            this.labCssFile.TabIndex = 2;
            // 
            // btnCssCompress
            // 
            this.btnCssCompress.Location = new System.Drawing.Point(138, 33);
            this.btnCssCompress.Name = "btnCssCompress";
            this.btnCssCompress.Size = new System.Drawing.Size(75, 23);
            this.btnCssCompress.TabIndex = 1;
            this.btnCssCompress.Text = "压缩";
            this.btnCssCompress.UseVisualStyleBackColor = true;
            this.btnCssCompress.Click += new System.EventHandler(this.btnCssCompress_Click);
            // 
            // btnSelectCss
            // 
            this.btnSelectCss.Location = new System.Drawing.Point(29, 33);
            this.btnSelectCss.Name = "btnSelectCss";
            this.btnSelectCss.Size = new System.Drawing.Size(94, 23);
            this.btnSelectCss.TabIndex = 0;
            this.btnSelectCss.Text = "选择CSS文件";
            this.btnSelectCss.UseVisualStyleBackColor = true;
            this.btnSelectCss.Click += new System.EventHandler(this.btnSelectCss_Click);
            // 
            // openCssFile
            // 
            this.openCssFile.DefaultExt = "css";
            this.openCssFile.FileName = "openFileDialog1";
            this.openCssFile.Filter = "CSS文件|*.css";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "压缩工具";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnJsCompress;
        private System.Windows.Forms.Button btnSelectJs;
        private System.Windows.Forms.OpenFileDialog openJsFile;
        private System.Windows.Forms.Label labJsFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbxLanguageLibJsonFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMergeCompress;
        private System.Windows.Forms.Button btnSelectLanguageLib;
        private System.Windows.Forms.OpenFileDialog openFileLanguageLib;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJsonCompress;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labCssFile;
        private System.Windows.Forms.Button btnCssCompress;
        private System.Windows.Forms.Button btnSelectCss;
        private System.Windows.Forms.OpenFileDialog openCssFile;
    }
}

