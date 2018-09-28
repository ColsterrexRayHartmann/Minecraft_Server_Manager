using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;

namespace mcs
{
    public partial class MessageForm : CCSkinMain
    {
        public int OutValue;

        public MessageForm(string Title,string Text,int ButtonStyle)//ButtonStyle 1 确定 2是否
        {
            InitializeComponent();
            TitleLabel.Text = Title;
            TextLabel.Text = Text;
            if (ButtonStyle == 1)
            {
                Button1.Text = "确定";
                Button2.Visible = false;
            }
            if (ButtonStyle == 2)
            {
                Button1.Text = "否";
                Button2.Text = "是";
                Button2.Visible = true;
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Close_MouseLeave(object sender, EventArgs e)
        {
            Close.Image = Properties.Resources.closewindow;
        }

        private void Close_MouseMove(object sender, MouseEventArgs e)
        {
            Close.Image = Properties.Resources.closewindow1;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
                OutValue = 1;
                this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            OutValue = 2;
            this.Close();
        }
    }
}
