using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.Utility.Standard.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Factory;
using Hzdtf.CodeGenerator.Contract.Standard;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.CodeGenerator.Model.Standard;

namespace Hzdtf.CodeGenerator.UI.Framework
{
    /// <summary>
    /// 主界面
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        /// 数据源类型字典
        /// </summary>
        private IDictionary<string, string> dataSourceTypes;

        /// <summary>
        /// 构造方法
        /// </summary>
        public Main()
        {
            InitializeComponent();

            dgvTable.AutoGenerateColumns = false;

            BindDataSourceType();
        }

        /// <summary>
        /// 绑定数据源类型
        /// </summary>
        private void BindDataSourceType()
        {
            IReader<IDictionary<string, string>> reader = AutofacTool.Resolve<IReader<IDictionary<string, string>>>();
            dataSourceTypes = reader.Reader();
            if (dataSourceTypes.IsNullOrCount0())
            {
                return;
            }

            foreach (KeyValuePair<string, string> item in dataSourceTypes)
            {
                cbxDataSourceType.Items.Add(item.Key);
            }

            cbxDataSourceType.SelectedIndex = 0;
        }

        /// <summary>
        /// 点击测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestConn_Click(object sender, EventArgs e)
        {
            ISimpleFactory<string, IDbConnection> factory = AutofacTool.Resolve<ISimpleFactory<string, IDbConnection>>();
            IDbConnection dbConnection = factory.Create(cbxDataSourceType.SelectedItem.ToString());
            try
            {
                dbConnection.ConnectionString = GetDbConnectionString();
                dbConnection.Open();
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();

                    MessageBox.Show("连接成功");
                }
                else
                {
                    MessageBox.Show("连接失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="dataBase">数据库</param>
        /// <returns>数据库连接字符串</returns>
        private string GetDbConnectionString(string dataBase = null)
        {
            return string.Format(dataSourceTypes[cbxDataSourceType.SelectedItem.ToString()],
                txtHost.Text,
                string.IsNullOrWhiteSpace(dataBase) ? txtDataBase.Text : dataBase,
                txtUser.Text,
                txtPassword.Text);
        }

        /// <summary>
        /// 点击加载按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            IDbInfoService dbInfoService = AutofacTool.Resolve<IDbInfoService>();
            ReturnInfo<IList<TableInfo>> returnInfo = dbInfoService.Query(txtDataBase.Text, GetDbConnectionString(), cbxDataSourceType.SelectedItem.ToString());
            if (returnInfo.Success())
            {
                dgvTable.Tag = cbxDataSourceType.SelectedItem.ToString();
                dgvTable.DataSource = returnInfo.Data;
            }
            else
            {
                MessageBox.Show(returnInfo.Msg);
            }
        }

        /// <summary>
        /// 点击生成按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerator_Click(object sender, EventArgs e)
        {
            IList<TableInfo> tables = new List<TableInfo>();
            foreach (DataGridViewRow row in dgvTable.Rows)
            {
                DataGridViewCheckBoxCell checkCell = row.Cells[2] as DataGridViewCheckBoxCell;
                if (checkCell.Value != null && Convert.ToBoolean(checkCell.Value))
                {
                    tables.Add(row.DataBoundItem as TableInfo);
                }
            }

            if (tables.Count == 0)
            {
                MessageBox.Show("请勾选要生成的表");
                return;
            }

            FunctionType[] functionTypes = GetFunctionTypes();
            if (functionTypes.Length == 0)
            {
                MessageBox.Show("请勾选要生成的功能项");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNamespacePfx.Text))
            {
                MessageBox.Show("命名空间前辍不能为空");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            ICodeGeneratorService generatorService = AutofacTool.Resolve<ICodeGeneratorService>();
            ReturnInfo<bool> returnInfo = generatorService.Generator(tables, functionTypes, txtNamespacePfx.Text, dgvTable.Tag.ToString());
            Cursor.Current = Cursors.Default;
            if (returnInfo.Success())
            {
                MessageBox.Show("生成成功");
            }
            else
            {
                MessageBox.Show(returnInfo.Msg);
            }
        }

        /// <summary>
        /// 获取生成功能类型集合
        /// </summary>
        /// <returns>生成功能类型集合</returns>
        private FunctionType[] GetFunctionTypes()
        {
            IList<FunctionType> functionTypes = new List<FunctionType>();
            if (cbxModel.Checked)
            {
                functionTypes.Add(FunctionType.MODEL);
            }
            if (cbxPersistence.Checked)
            {
                functionTypes.Add(FunctionType.PERSISTENCE);
            }
            if (cbxService.Checked)
            {
                functionTypes.Add(FunctionType.SERVICE);
            }
            if (cbxFrameworkController.Checked)
            {
                functionTypes.Add(FunctionType.FRAMEWORK_CONTROLLER);
            }
            if (cbxCoreController.Checked)
            {
                functionTypes.Add(FunctionType.CORE_CONTROLLER);
            }

            return functionTypes.ToArray();
        }

        /// <summary>
        /// 点击全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvTable.Rows)
            {
                DataGridViewCheckBoxCell checkCell = row.Cells[2] as DataGridViewCheckBoxCell;
                checkCell.Value = cbxCheckAll.Checked;
            }
        }
    }
}
