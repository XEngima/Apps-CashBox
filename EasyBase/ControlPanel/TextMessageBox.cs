using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyBase
{
	public partial class TextMessageBox : Form
	{
		public TextMessageBox()
		{
			InitializeComponent();
		}

		public TextMessageBox(string text)
		{
			InitializeComponent();
			ContentText = text;
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		public string ContentText
		{
			get { return textBox.Text; }
			set { textBox.Text = value; }
		}

		public static void Show(string text)
		{
			TextMessageBox messageBox = new TextMessageBox(text);
			messageBox.ShowDialog();
		}
	}
}
