using Hzdtf.Utility.Standard.Safety;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hzdtf.EncryptionAndDecryption.Framework
{
    /// <summary>
    /// 主窗体
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
        /// 点击加密按钮
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlaintext.Text))
            {
                MessageBox.Show("请输入明文");
                return;
            }

            try
            {
                txtCiphertext.Text = DESUtil.Encrypt(txtPlaintext.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 点击复制密文按钮
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnCopyCiphertext_Click(object sender, EventArgs e)
        {
            CopyFromTextControll(txtCiphertext, "密文");
        }

        /// <summary>
        /// 点击解密按钮
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnDecode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCiphertext.Text))
            {
                MessageBox.Show("请输入密文");
                return;
            }

            try
            {
                txtPlaintext.Text = DESUtil.Decrypt(txtCiphertext.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 点击复制明文按钮
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnCopyPlaintext_Click(object sender, EventArgs e)
        {
            CopyFromTextControll(txtPlaintext, "明文");
        }

        /// <summary>
        /// 点击粘贴到明文按钮
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnPasteToPlaintext_Click(object sender, EventArgs e)
        {
            PasteToTextControll(txtPlaintext);
        }

        /// <summary>
        /// 点击粘贴到密文按钮
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnPasteCiphertext_Click(object sender, EventArgs e)
        {
            PasteToTextControll(txtCiphertext);
        }

        /// <summary>
        /// 复制来自文本控件
        /// </summary>
        /// <param name="textBox">文本控件</param>
        /// <param name="name">名称</param>
        private void CopyFromTextControll(TextBox textBox, string name)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show($"没有{name}可复制");
                return;
            }
            Clipboard.SetDataObject(textBox.Text);
            MessageBox.Show($"{name}已复制");
        }

        /// <summary>
        /// 粘贴到文本控件里
        /// </summary>
        /// <param name="textBox">文本控件</param>
        private void PasteToTextControll(TextBox textBox)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData != null && iData.GetDataPresent(DataFormats.Text))
            {
                textBox.Text = (string)iData.GetData(DataFormats.Text);
                return;
            }

            MessageBox.Show("没有文本可粘贴");
        }
    }
}
