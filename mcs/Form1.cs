using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CCWin;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;

namespace mcs
{
    public partial class Form1 : CCSkinMain
    {
        MCS mc = new MCS(Environment.CurrentDirectory + "\\Server");
        string ver = "1.0.0";
        string JavaPath = "";
        string Rundir = "";
        string Xmax = "";
        string Xmin = "";
        bool isserverrunning = false;
        private delegate void serverEventHandler(object sender, MCSEvent e);
        public Form1()
        {
            CCWin.SkinControl.ScrollBarDrawImage.ScrollVertShaft = Properties.Resources.ScrollVertShaft;
            CCWin.SkinControl.ScrollBarDrawImage.ScrollVertArrow = Properties.Resources.ScrollVertArrow;
            CCWin.SkinControl.ScrollBarDrawImage.ScrollVertThumb = Properties.Resources.ScrollVertThumb;
            InitializeComponent();
        }
        void Server_serverMessage(object sender, MCSEvent e)
        {
            if (server_infom.InvokeRequired)
                Invoke(new serverEventHandler(Server_serverMessage), new object[] { sender, e });
            else
            {
                if (server_infom.Text.Length >= 30000)
                    server_infom.Text = "";
                server_infom.Text = server_infom.Text + e.cmd + "\r\n";
                server_infom.SkinTxt.SelectionStart = server_infom.Text.Length - 1;
                server_infom.SkinTxt.ScrollToCaret();
            }
        }//事件-服务器回显消息通知
        private void Form1_Load(object sender, EventArgs e)
        {
            //mc.serverMessage += new MCS.serverEventHandler(Server_serverMessage); //订阅事件
            VerLabel.Text = "版本：" + ver;
            skinTabControl1.ItemSize = new Size(0, 1);
            skinTabControl2.ItemSize = new Size(0, 1);
            skinTabControl1.SelectedIndex = 0;
            skinTabControl2.SelectedIndex = 0;
            Thread load = new Thread(Loadwin);
            jardirectorytextbox.Text = Environment.CurrentDirectory + "\\Server";
            load.Start();//开始 启动线程
            //TODO:首次启动参数设置
        }
        private void Loadwin()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            IP.Text = "外网IP：" + searchIP2() + "          " + "内网IP：" + searchIP1();
            string[] settings = settingcheck();
            if (settings == null || settings[0] == null || settings[0] == "" || settings[1] == null || settings[1] == "" || settings[2] == null || settings[2] == "" || settings[3] == null || settings[3] == "")
            {
                skinTabControl1.SelectedIndex = 1;
                skinTabControl2.SelectedIndex = 0;
                SkinMessageBox("警告 :", "请完成基础设置", 1);
            }
            else
            {
                Rundir = settings[0];
                JavaPath = settings[1];
                Xmax = settings[2];
                Xmin = settings[3];
            }
            
            mc.serverMessage += new MCS.serverEventHandler(Server_serverMessage);
        }//启动线程
        //界面设计
        #region
        private void min_MouseMove(object sender, MouseEventArgs e)
        {
            min.Image = Properties.Resources.minwindow;
        }
        private void min_MouseLeave(object sender, EventArgs e)
        {
            min.Image = Properties.Resources.minwindow1;
        }
        private void Close_MouseMove(object sender, MouseEventArgs e)
        {
            Close.Image = Properties.Resources.closewindow1;
        }
        private void Close_MouseLeave(object sender, EventArgs e)
        {
            Close.Image = Properties.Resources.closewindow;
        }
        private void Close_Click(object sender, EventArgs e)
        {
            if (isserverrunning)
            {
                SkinMessageBox("警告 :", "请先关闭服务器", 1);
                return;
            }
            Environment.Exit(0);
        }
        private void min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void settingbutton_MouseMove(object sender, MouseEventArgs e)
        {
            settingbutton.Image = Properties.Resources.button_setting_db;
        }
        private void settingbutton_MouseLeave(object sender, EventArgs e)
        {
            settingbutton.Image = Properties.Resources.button_setting_b;
        }
        private void settingbutton_MouseDown(object sender, MouseEventArgs e)
        {
            settingbutton.Image = Properties.Resources.button_setting_g;
        }
        private void settingbutton_MouseUp(object sender, MouseEventArgs e)
        {
            settingbutton.Image = Properties.Resources.button_setting_db;
        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            skinTabControl1.SelectedIndex = 0;
        }
        private void skinLabel11_Click(object sender, EventArgs e)
        {
            skinTabControl2.SelectedIndex = 0;
            skinLabel11.ForeColor = Color.CornflowerBlue;

            skinLabel12.ForeColor = Color.Black;
            skinLabel13.ForeColor = Color.Black;
            skinLabel14.ForeColor = Color.Black;
            skinLabel15.ForeColor = Color.Black;
        }
        private void skinLabel12_Click(object sender, EventArgs e)
        {
            if (File.Exists(Rundir + "\\server.properties"))
            {
                skinTabControl2.SelectedIndex = 1;
                skinLabel12.ForeColor = Color.CornflowerBlue;

                
                string[,] pro = new string [mc.GetConf().Length,2];
                pro = mc.GetConf();
                skinTextBox7.Text = unicode_js_1(find("motd", pro));
                skinComboBox1.SelectedIndex = find1("allow-nether", pro);
                skinComboBox8.SelectedIndex = find1("pvp", pro);
                skinComboBox2.SelectedIndex = find1("online-mode", pro);
                skinComboBox9.SelectedIndex = find1("enable-command-block", pro);
                skinComboBox3.SelectedIndex = find1("spawn-monsters", pro);
                skinComboBox5.SelectedIndex = find1("spawn-animals", pro);
                skinComboBox6.SelectedIndex = find1("white-list", pro);
                skinTextBox8.Text = find("server-port", pro);
                skinTextBox9.Text = find("level-seed", pro);
                skinTextBox10.Text = find("max-players", pro);

                if (find("difficulty", pro) == "0")
                    skinComboBox7.SelectedIndex = 0;
                if (find("difficulty", pro) == "1")
                    skinComboBox7.SelectedIndex = 1;
                if (find("difficulty", pro) == "2")
                    skinComboBox7.SelectedIndex = 2;
                if (find("difficulty", pro) == "3")
                    skinComboBox7.SelectedIndex = 3;


                skinLabel11.ForeColor = Color.Black;
                skinLabel13.ForeColor = Color.Black;
                skinLabel14.ForeColor = Color.Black;
                skinLabel15.ForeColor = Color.Black;
            }
            else
            {
                SkinMessageBox("警告 :", "配置文件不存在。", 1);
            }
        }
        private void skinLabel13_Click(object sender, EventArgs e)
        {
            skinTabControl2.SelectedIndex = 2;
            skinLabel13.ForeColor = Color.CornflowerBlue;

            skinLabel11.ForeColor = Color.Black;
            skinLabel12.ForeColor = Color.Black;
            skinLabel14.ForeColor = Color.Black;
            skinLabel15.ForeColor = Color.Black;
        }
        private void skinLabel14_Click(object sender, EventArgs e)
        {
            skinTabControl2.SelectedIndex = 3;
            skinLabel14.ForeColor = Color.CornflowerBlue;

            skinLabel11.ForeColor = Color.Black;
            skinLabel12.ForeColor = Color.Black;
            skinLabel13.ForeColor = Color.Black;
            skinLabel15.ForeColor = Color.Black;
        }
        private void skinLabel15_Click(object sender, EventArgs e)
        {
            skinTabControl2.SelectedIndex = 4;
            skinLabel15.ForeColor = Color.CornflowerBlue;

            skinLabel11.ForeColor = Color.Black;
            skinLabel12.ForeColor = Color.Black;
            skinLabel13.ForeColor = Color.Black;
            skinLabel14.ForeColor = Color.Black;
        }
        #endregion
        private string searchIP1()
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }//获取内网IP
        private string searchIP2()//获取外网IP
        {
            WebClient client = new WebClient();
            byte[] bytRecv = client.DownloadData("http://ip.tool.chinaz.com/"); 
            string str = System.Text.Encoding.GetEncoding("UTF-8").GetString(bytRecv);
            string headstr = "<dd class=\"fz24\">";
            int leftindex = str.IndexOf(headstr) + headstr.Length;
            int rightindex =  str.IndexOf("</dd>", leftindex);
            return str.Substring(leftindex, rightindex - leftindex);
        }
        /*
        private string ram()
        {
            //TODO：内存获取
            
        }
        */
        private void Button_serverrun_Click(object sender, EventArgs e)//未完成
        {
            string cmd = "-Xms" + Xmin + "m -Xmx" + Xmax + "m -jar \"" + Rundir + "\\Server.jar\"";
            MessageBox.Show(cmd);
            //return;
            if (mc.Run(JavaPath, cmd))
            {
                isserverrunning = true;
                server_stats.Text = "服务器状态：运行中";
            }
            else
            {
                server_infom.SkinTxt.AppendText("[警告]服务器启动失败");
            }
            
        }
        private void settingbutton_Click(object sender, EventArgs e)
        {
            skinTabControl1.SelectedIndex = 1;
        }
        private void skinCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (skinCheckBox1.Checked)
            {
                mc.serverMessage -= Server_serverMessage;
            }
            else
            {
                mc.serverMessage += new MCS.serverEventHandler(Server_serverMessage);
            }
        }//是否接受信息
        private void Button_serverstop_Click(object sender, EventArgs e)
        {
            if (isserverrunning)
            {
                mc.Stop();
                isserverrunning = false;
                server_stats.Text = "服务器状态：未开启";
            }
        }
        private string[] settingcheck()
        {
            string conpath = Environment.CurrentDirectory + "\\MCSM.conf";
            string[] r = { "","","",""};

            if (File.Exists(conpath))
            {
                StreamReader sw = new StreamReader(conpath);
                string line = "";
                while (!sw.EndOfStream)
                {
                    line = sw.ReadLine();
                    if (line != "[setting]")
                    {
                        //line = line.Replace(" ","");
                        string[] con = line.Split('=');
                        if (con[1] != "" && con[1] != null)
                        {
                            con[0] = con[0].Replace(" ", "");
                            if (con[1].Substring(0, 1) == " ")
                            {
                                con[1] = con[1].Remove(0, 1);
                            }

                            if (con[0] == "jardirectory")
                            {
                                jardirectorytextbox.Text = con[1];
                                r[0] = con[1];
                                mc.changerundir(con[1]);
                            }

                            if (con[0] == "javapath")
                            {
                                javapathtextbox.Text = con[1];
                                r[1] = con[1];
                            }

                            if (con[0] == "max")
                            {
                                maxtextbox.Text = con[1];
                                r[2] = con[1];
                            }

                            if (con[0] == "min")
                            {
                                mintextbox.Text = con[1];
                                r[3] = con[1];
                            }
                        }
                    }
                }
            }
            return r;
        }//读取开服器设置
        private string openfile(string Title,string Filter)//选择文件 （浏览）
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = Title;
            ofd.Filter = Filter;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            else
            {
                return "";
            }
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "选择服务端运行目录";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                jardirectorytextbox.Text = fbd.SelectedPath;
            }
        }
        public int SkinMessageBox(string Title, string Text, int Buttonstyle)//自定义信息框
        {
            MessageForm mb = new MessageForm(Title, Text, Buttonstyle);
            mb.ShowDialog();
            return mb.OutValue;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            javapathtextbox.Text = openfile("选择Java路径", "java.exe|java.exe");
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!isserverrunning)
            {
                if (jardirectorytextbox.Text == "" || javapathtextbox.Text == "" || maxtextbox.Text == "" || mintextbox.Text == "")
                {
                    SkinMessageBox("警告 :", "信息不完整", 1);
                }
                else
                {
                    string value = "[setting]" + "\r\n" + "jardirectory = " + jardirectorytextbox.Text
                                    + "\r\n" + "javapath = " + javapathtextbox.Text + "\r\n" + "max = " + maxtextbox.Text + "\r\n" + "min = " + mintextbox.Text;
                    MCS.WriteText(Environment.CurrentDirectory + @"\MCSM.conf", value);
                    mc.changerundir(jardirectorytextbox.Text);
                    string[] r = settingcheck();
                    if (r != null)
                    {
                        SkinMessageBox("提示 :", "保存完成", 1);
                    }
                }
            }
        }
        private void InsButton_Click(object sender, EventArgs e)
        {
            
            if (Directory.Exists(jardirectorytextbox.Text))
            {
                if (SkinMessageBox("警告 :", "将删除原服务端文件，是否继续？", 2)==2)
                {
                    Directory.Delete(jardirectorytextbox.Text,true);
                    Directory.CreateDirectory(jardirectorytextbox.Text);
                    if (mc.Instal(openfile("选择服务端文件", "服务端文件|*.jar")))
                    {
                        SkinMessageBox("提示 :","服务端安装完成",1);
                    }
                }
            }
            
        }
        public string find(string target, string[,] arry)
        {
            int row = arry.GetLength(0);
            for (int i = 0; i <= row-1; i++)
            {
                if (target == arry[i, 0])
                    return arry[i, 1];
            }
            return "";
        }//搜索二维数组1
        public int find1(string target, string[,] arry)
        {
            int row = arry.GetLength(0);
            for (int i = 0; i <= row - 1; i++)
            {
                if (target == arry[i, 0])
                {
                    if (arry[i, 1] == "true")
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            return 1;
        }//搜索二维数组2
        public string putout(CCWin.SkinControl.SkinComboBox cb)
        {
            switch (cb.SelectedIndex)
            {
                case 0:
                    return "true";
                case 1:
                    return "false";
            }
            return "";
        }
        public int find2(string target, string[,] arry)
        {
            int row = arry.GetLength(0);
            for (int i = 0; i <= row - 1; i++)
            {
                if (target == arry[i, 0])
                    return i;
            }
            return -1;
        }
        public string unicode_0(string str)
        {
            string outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    String ss = ((int)str[i]).ToString("x");
                    if (ss.Length != 4)
                    {
                        for (int jj = 0; jj <= 4 - ss.Length; jj++)
                        {
                            ss = "0" + ss;
                        }
                    }
                    outStr += "\\u" + ss;
                }
            }
            return outStr;
        }// 中英文转unicode
        public string unicode_js_1(string str)
        {
            string outStr = "";
            Regex reg = new Regex(@"(?i)\\u([0-9a-f]{4})");
            outStr = reg.Replace(str, delegate (Match m1) {return ((char)Convert.ToInt32(m1.Groups[1].Value, 16)).ToString();});
            return outStr;
        }// unicode转中文
        private void skinButton1_Click(object sender, EventArgs e)
        {
            //TODO:保存配置

            string[,] pro = new string[mc.GetConf().Length, 2];
            pro = mc.GetConf();
            pro[find2("allow-nether", pro),1]= putout(skinComboBox1);
            pro[find2("difficulty", pro), 1] = skinComboBox7.SelectedIndex.ToString();

            /*
            string value = Properties.Resources.servercon;
            value = value.Replace("地狱", putout(skinComboBox1));
            value = value.Replace("正版", putout(skinComboBox2));
            value = value.Replace("怪物", putout(skinComboBox3));
            value = value.Replace("动物", putout(skinComboBox5));
            value = value.Replace("白名单", putout(skinComboBox6));
            value = value.Replace("互殴", putout(skinComboBox8));
            value = value.Replace("命令方块", putout(skinComboBox9));
            value = value.Replace("端口", skinTextBox8.Text);
            value = value.Replace("种子", skinTextBox9.Text);
            value = value.Replace("最大玩家数", skinTextBox10.Text);
            value = value.Replace("最大玩家数", skinTextBox10.Text);
            value = value.Replace("简介", unicode_0(skinTextBox7.Text));
            value = value.Replace("难度", skinComboBox7.SelectedIndex.ToString());
            */

        }
    }
}
