namespace Hzdtf.CodeGenerator.UI.Framework
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDataBase = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxDataSourceType = new System.Windows.Forms.ComboBox();
            this.groupbox3 = new System.Windows.Forms.GroupBox();
            this.cbxIsTenant = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxPKType = new System.Windows.Forms.ComboBox();
            this.cbxCoreController = new System.Windows.Forms.CheckBox();
            this.cbxModel = new System.Windows.Forms.CheckBox();
            this.btnGenerator = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNamespacePfx = new System.Windows.Forms.TextBox();
            this.cbxService = new System.Windows.Forms.CheckBox();
            this.cbxFrameworkController = new System.Windows.Forms.CheckBox();
            this.cbxPersistence = new System.Windows.Forms.CheckBox();
            this.groupbox11 = new System.Windows.Forms.GroupBox();
            this.cbxCheckAll = new System.Windows.Forms.CheckBox();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.Name1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Choice = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupbox3.SuspendLayout();
            this.groupbox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDataBase);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHost);
            this.groupBox1.Controls.Add(this.btnTestConn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxDataSourceType);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据源";
            // 
            // txtDataBase
            // 
            this.txtDataBase.Location = new System.Drawing.Point(445, 37);
            this.txtDataBase.Name = "txtDataBase";
            this.txtDataBase.Size = new System.Drawing.Size(105, 21);
            this.txtDataBase.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(386, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "数据库：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(445, 62);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(105, 21);
            this.txtPassword.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(386, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "密码：";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(248, 63);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(121, 21);
            this.txtUser.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(201, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "用户：";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(248, 36);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(121, 21);
            this.txtHost.TabIndex = 5;
            this.txtHost.Text = "127.0.0.1";
            // 
            // btnTestConn
            // 
            this.btnTestConn.Location = new System.Drawing.Point(677, 41);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(75, 23);
            this.btnTestConn.TabIndex = 4;
            this.btnTestConn.Text = "测试连接";
            this.btnTestConn.UseVisualStyleBackColor = true;
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "主机：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "类型：";
            // 
            // cbxDataSourceType
            // 
            this.cbxDataSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDataSourceType.FormattingEnabled = true;
            this.cbxDataSourceType.Location = new System.Drawing.Point(62, 36);
            this.cbxDataSourceType.Name = "cbxDataSourceType";
            this.cbxDataSourceType.Size = new System.Drawing.Size(121, 20);
            this.cbxDataSourceType.TabIndex = 0;
            // 
            // groupbox3
            // 
            this.groupbox3.Controls.Add(this.cbxIsTenant);
            this.groupbox3.Controls.Add(this.label7);
            this.groupbox3.Controls.Add(this.cbxPKType);
            this.groupbox3.Controls.Add(this.cbxCoreController);
            this.groupbox3.Controls.Add(this.cbxModel);
            this.groupbox3.Controls.Add(this.btnGenerator);
            this.groupbox3.Controls.Add(this.label3);
            this.groupbox3.Controls.Add(this.txtNamespacePfx);
            this.groupbox3.Controls.Add(this.cbxService);
            this.groupbox3.Controls.Add(this.cbxFrameworkController);
            this.groupbox3.Controls.Add(this.cbxPersistence);
            this.groupbox3.Location = new System.Drawing.Point(12, 648);
            this.groupbox3.Name = "groupbox3";
            this.groupbox3.Size = new System.Drawing.Size(775, 121);
            this.groupbox3.TabIndex = 1;
            this.groupbox3.TabStop = false;
            this.groupbox3.Text = "生成项";
            // 
            // cbxIsTenant
            // 
            this.cbxIsTenant.AutoSize = true;
            this.cbxIsTenant.Location = new System.Drawing.Point(611, 28);
            this.cbxIsTenant.Name = "cbxIsTenant";
            this.cbxIsTenant.Size = new System.Drawing.Size(72, 16);
            this.cbxIsTenant.TabIndex = 10;
            this.cbxIsTenant.Text = "是否租户";
            this.cbxIsTenant.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(400, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "主键类型：";
            // 
            // cbxPKType
            // 
            this.cbxPKType.FormattingEnabled = true;
            this.cbxPKType.Items.AddRange(new object[] {
            "整型",
            "字符串",
            "Guid",
            "雪花算法"});
            this.cbxPKType.Location = new System.Drawing.Point(471, 26);
            this.cbxPKType.Name = "cbxPKType";
            this.cbxPKType.Size = new System.Drawing.Size(121, 20);
            this.cbxPKType.TabIndex = 8;
            // 
            // cbxCoreController
            // 
            this.cbxCoreController.AutoSize = true;
            this.cbxCoreController.Location = new System.Drawing.Point(299, 74);
            this.cbxCoreController.Name = "cbxCoreController";
            this.cbxCoreController.Size = new System.Drawing.Size(84, 16);
            this.cbxCoreController.TabIndex = 7;
            this.cbxCoreController.Text = "Core控制器";
            this.cbxCoreController.UseVisualStyleBackColor = true;
            // 
            // cbxModel
            // 
            this.cbxModel.AutoSize = true;
            this.cbxModel.Checked = true;
            this.cbxModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxModel.Location = new System.Drawing.Point(17, 74);
            this.cbxModel.Name = "cbxModel";
            this.cbxModel.Size = new System.Drawing.Size(48, 16);
            this.cbxModel.TabIndex = 6;
            this.cbxModel.Text = "模型";
            this.cbxModel.UseVisualStyleBackColor = true;
            // 
            // btnGenerator
            // 
            this.btnGenerator.Location = new System.Drawing.Point(677, 70);
            this.btnGenerator.Name = "btnGenerator";
            this.btnGenerator.Size = new System.Drawing.Size(75, 23);
            this.btnGenerator.TabIndex = 5;
            this.btnGenerator.Text = "生成";
            this.btnGenerator.UseVisualStyleBackColor = true;
            this.btnGenerator.Click += new System.EventHandler(this.btnGenerator_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "命名空间前辍：";
            // 
            // txtNamespacePfx
            // 
            this.txtNamespacePfx.Location = new System.Drawing.Point(115, 26);
            this.txtNamespacePfx.Name = "txtNamespacePfx";
            this.txtNamespacePfx.Size = new System.Drawing.Size(268, 21);
            this.txtNamespacePfx.TabIndex = 3;
            // 
            // cbxService
            // 
            this.cbxService.AutoSize = true;
            this.cbxService.Checked = true;
            this.cbxService.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxService.Location = new System.Drawing.Point(125, 74);
            this.cbxService.Name = "cbxService";
            this.cbxService.Size = new System.Drawing.Size(48, 16);
            this.cbxService.TabIndex = 2;
            this.cbxService.Text = "服务";
            this.cbxService.UseVisualStyleBackColor = true;
            // 
            // cbxFrameworkController
            // 
            this.cbxFrameworkController.AutoSize = true;
            this.cbxFrameworkController.Location = new System.Drawing.Point(179, 74);
            this.cbxFrameworkController.Name = "cbxFrameworkController";
            this.cbxFrameworkController.Size = new System.Drawing.Size(114, 16);
            this.cbxFrameworkController.TabIndex = 1;
            this.cbxFrameworkController.Text = "Framework控制器";
            this.cbxFrameworkController.UseVisualStyleBackColor = true;
            // 
            // cbxPersistence
            // 
            this.cbxPersistence.AutoSize = true;
            this.cbxPersistence.Checked = true;
            this.cbxPersistence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxPersistence.Location = new System.Drawing.Point(71, 74);
            this.cbxPersistence.Name = "cbxPersistence";
            this.cbxPersistence.Size = new System.Drawing.Size(48, 16);
            this.cbxPersistence.TabIndex = 0;
            this.cbxPersistence.Text = "持久";
            this.cbxPersistence.UseVisualStyleBackColor = true;
            // 
            // groupbox11
            // 
            this.groupbox11.Controls.Add(this.cbxCheckAll);
            this.groupbox11.Controls.Add(this.dgvTable);
            this.groupbox11.Controls.Add(this.btnLoad);
            this.groupbox11.Location = new System.Drawing.Point(13, 124);
            this.groupbox11.Name = "groupbox11";
            this.groupbox11.Size = new System.Drawing.Size(774, 518);
            this.groupbox11.TabIndex = 2;
            this.groupbox11.TabStop = false;
            this.groupbox11.Text = "表";
            // 
            // cbxCheckAll
            // 
            this.cbxCheckAll.AutoSize = true;
            this.cbxCheckAll.Location = new System.Drawing.Point(676, 80);
            this.cbxCheckAll.Name = "cbxCheckAll";
            this.cbxCheckAll.Size = new System.Drawing.Size(48, 16);
            this.cbxCheckAll.TabIndex = 2;
            this.cbxCheckAll.Text = "全选";
            this.cbxCheckAll.UseVisualStyleBackColor = true;
            this.cbxCheckAll.CheckedChanged += new System.EventHandler(this.cbxCheckAll_CheckedChanged);
            // 
            // dgvTable
            // 
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name1,
            this.Description,
            this.Choice});
            this.dgvTable.Location = new System.Drawing.Point(16, 20);
            this.dgvTable.MultiSelect = false;
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.RowTemplate.Height = 23;
            this.dgvTable.Size = new System.Drawing.Size(644, 482);
            this.dgvTable.TabIndex = 1;
            // 
            // Name1
            // 
            this.Name1.DataPropertyName = "Name";
            this.Name1.HeaderText = "名称";
            this.Name1.Name = "Name1";
            this.Name1.Width = 200;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "描述";
            this.Description.Name = "Description";
            this.Description.Width = 300;
            // 
            // Choice
            // 
            this.Choice.HeaderText = "选择";
            this.Choice.Name = "Choice";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(676, 39);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "加载";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 781);
            this.Controls.Add(this.groupbox11);
            this.Controls.Add(this.groupbox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "代码生成器(.net)";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupbox3.ResumeLayout(false);
            this.groupbox3.PerformLayout();
            this.groupbox11.ResumeLayout(false);
            this.groupbox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTestConn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxDataSourceType;
        private System.Windows.Forms.GroupBox groupbox3;
        private System.Windows.Forms.Button btnGenerator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNamespacePfx;
        private System.Windows.Forms.CheckBox cbxService;
        private System.Windows.Forms.CheckBox cbxFrameworkController;
        private System.Windows.Forms.CheckBox cbxPersistence;
        private System.Windows.Forms.GroupBox groupbox11;
        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Choice;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.TextBox txtDataBase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbxModel;
        private System.Windows.Forms.CheckBox cbxCheckAll;
        private System.Windows.Forms.CheckBox cbxCoreController;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxPKType;
        private System.Windows.Forms.CheckBox cbxIsTenant;
    }
}

