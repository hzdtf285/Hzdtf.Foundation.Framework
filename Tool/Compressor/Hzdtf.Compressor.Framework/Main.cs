using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hzdtf.Compressor.Impl.Standard;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Compressor.Framework
{
    /// <summary>
    /// 主界面
    /// @ 黄振东
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击选择JS按钮
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnSelectJs_Click(object sender, EventArgs e)
        {
            var result = openJsFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                labJsFile.Text = openJsFile.FileName;    
            }
        }

        /// <summary>
        /// 点击JS压缩按钮
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnJsCompress_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(labJsFile.Text))
            {
                MessageBox.Show("请先选择JS文件");

                return;
            }

            try
            {

                JSCompressorUtil.CompressorFile(labJsFile.Text);

                MessageBox.Show("压缩成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 点击选择语系库按钮
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnSelectLanguageLib_Click(object sender, EventArgs e)
        {
            var result = openFileLanguageLib.ShowDialog();
            if (result == DialogResult.OK)
            {
                lbxLanguageLibJsonFiles.DataSource = openFileLanguageLib.FileNames;
            }
        }

        /// <summary>
        /// 点击合并压缩按钮
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnMergeCompress_Click(object sender, EventArgs e)
        {
            var files = lbxLanguageLibJsonFiles.DataSource as string[];
            if (files.IsNullOrLength0())
            {
                MessageBox.Show("请先选择语系库文件");

                return;
            }
            if (string.IsNullOrWhiteSpace(txtJsonCompress.Text))
            {
                MessageBox.Show("请先填写压缩后的文件前辍");

                return;
            }

            try
            {
                JSCompressorUtil.MergeAndCompressorJsonFiles(files, txtJsonCompress.Text);

                MessageBox.Show("合并压缩成功");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 点击选择CSS按钮
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnSelectCss_Click(object sender, EventArgs e)
        {
            var result = openCssFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                labCssFile.Text = openCssFile.FileName;
            }
        }

        /// <summary>
        /// 点击CSS压缩按钮
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnCssCompress_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(labCssFile.Text))
            {
                MessageBox.Show("请先选择CSS文件");

                return;
            }

            try
            {

                CssCompressorUtil.CompressorFile(labCssFile.Text);

                MessageBox.Show("压缩成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
