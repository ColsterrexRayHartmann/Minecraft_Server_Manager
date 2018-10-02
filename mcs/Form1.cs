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
using System.Collections;
using System.Net.Mail;

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

        //界面控制
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
            if (SkinMessageBox("确认关闭？", "是，关闭    否，最小化", 2) == 2)
            {
                退出ToolStripMenuItem_Click(this, new EventArgs());
            }
            else
            {
                this.ShowInTaskbar = false;
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }
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


                string[,] pro = new string[mc.GetConf().Length, 2];
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

            if (Directory.Exists(Rundir + "\\mods"))
            {
                string[] files = Enumeratefiles(Rundir + "\\mods");
                foreach (string v in files)
                {
                    if (v.Substring(v.Length - 3, 3) == "jar")
                    {
                        ModsList.Items.Add(new CCWin.SkinControl.SkinListBoxItem(v));
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(Rundir + "\\mods");
            }
        }
        private void skinLabel14_Click(object sender, EventArgs e)
        {
            skinTabControl2.SelectedIndex = 3;
            skinLabel14.ForeColor = Color.CornflowerBlue;

            skinLabel11.ForeColor = Color.Black;
            skinLabel12.ForeColor = Color.Black;
            skinLabel13.ForeColor = Color.Black;
            skinLabel15.ForeColor = Color.Black;

            if (Directory.Exists(Rundir + "\\plugins"))
            {
                string[] files = Enumeratefiles(Rundir + "\\plugins");
                foreach (string v in files)
                {
                    if (v.Substring(v.Length - 3, 3) == "jar")
                    {
                        PluginsList.Items.Add(new CCWin.SkinControl.SkinListBoxItem(v));
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(Rundir + "\\plugins");
            }
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

            string[] a = getwebcode("https://raw.githubusercontent.com/NiTian1207/Minecraft_Server_Manager/master/Donors.txt", "UTF-8").Split(new char[2] { '\r', '\n' });
            foreach (string i in a)
            {
                if (i != null && i != "" && i != "Donors:")
                {
                    skinListBox4.Items.Add(new CCWin.SkinControl.SkinListBoxItem(i));
                }
            }
            //获取捐助表

            
        }//启动线程
        public string searchIP1()
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
        public string searchIP2()//获取外网IP
        {
            WebClient client = new WebClient();
            byte[] bytRecv = client.DownloadData("http://ip.tool.chinaz.com/"); 
            string str = System.Text.Encoding.GetEncoding("UTF-8").GetString(bytRecv);
            string headstr = "<dd class=\"fz24\">";
            int leftindex = str.IndexOf(headstr) + headstr.Length;
            int rightindex =  str.IndexOf("</dd>", leftindex);
            return str.Substring(leftindex, rightindex - leftindex);
        }
        private void Button_serverrun_Click(object sender, EventArgs e)//未完成
        {
            string cmd = "-Xms" + Xmin + "m -Xmx" + Xmax + "m -jar \"" + Rundir + "\\Server.jar\"";
            Button_serverrun.Enabled = false; 
            //return;
            if (mc.Run(JavaPath, cmd))
            {
                isserverrunning = true;
                server_stats.Text = "服务器状态：运行中";
            }
            else
            {
                server_infom.SkinTxt.AppendText("[警告]服务器启动失败");
                Button_serverrun.Enabled = true;
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
                Button_serverrun.Enabled = true;
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
        private string find(string target, string[,] array)
        {
            int row = array.GetLength(0);
            for (int i = 0; i <= row-1; i++)
            {
                if (target == array[i, 0])
                    return array[i, 1];
            }
            return "";
        }//搜索二维数组1
        private int find1(string target, string[,] array)
        {
            int row = array.GetLength(0);
            for (int i = 0; i <= row - 1; i++)
            {
                if (target == array[i, 0])
                {
                    if (array[i, 1] == "true")
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
        private string putout(CCWin.SkinControl.SkinComboBox cb)
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
        private int find2(string target, string[,] arry)
        {
            int row = arry.GetLength(0);
            for (int i = 0; i <= row - 1; i++)
            {
                if (target == arry[i, 0])
                    return i;
            }
            return -1;
        }
        private string unicode_0(string str)
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
        private string unicode_js_1(string str)
        {
            string outStr = "";
            Regex reg = new Regex(@"(?i)\\u([0-9a-f]{4})");
            outStr = reg.Replace(str, delegate (Match m1) {return ((char)Convert.ToInt32(m1.Groups[1].Value, 16)).ToString();});
            return outStr;
        }// unicode转中文
        private void skinButton1_Click(object sender, EventArgs e)//保存配置
        {
            if (!isserverrunning)
            {
                string[,] pro = new string[mc.GetConf().Length, 2];
                pro = mc.GetConf();
                pro[find2("allow-nether", pro), 1] = putout(skinComboBox1);
                pro[find2("difficulty", pro), 1] = skinComboBox7.SelectedIndex.ToString();
                pro[find2("spawn-monsters", pro), 1] = putout(skinComboBox3);
                pro[find2("pvp", pro), 1] = putout(skinComboBox8);
                pro[find2("enable-command-block", pro), 1] = putout(skinComboBox9);
                pro[find2("max-players", pro), 1] = skinTextBox10.Text;
                pro[find2("server-port", pro), 1] = skinTextBox8.Text;
                pro[find2("spawn-animals", pro), 1] = putout(skinComboBox5);
                pro[find2("white-list", pro), 1] = putout(skinComboBox6);
                pro[find2("online-mode", pro), 1] = putout(skinComboBox2);
                pro[find2("level-seed", pro), 1] = skinTextBox9.Text;
                pro[find2("motd", pro), 1] = unicode_0(skinTextBox7.Text);
                string value = "";
                int x = 0;
                foreach (string i in pro)
                {
                    if (x % 2 == 0)
                        value += i + "=";
                    else
                        value += i + "\r\n";
                    x++;
                }
                if (MCS.WriteText(Rundir + "\\server.properties", value))
                    SkinMessageBox("提示 :", "保存完成", 1);
                else
                    SkinMessageBox("提示 :", "保存失败", 1);
            }
            else
            {
                SkinMessageBox("警告 :", "关闭服务器后再试", 1);
            }
        }
        private string[] Enumeratefiles(string directroy)//枚举文件 返回文件名 string[] 
        {
            ArrayList value = new ArrayList();
            DirectoryInfo dir = new DirectoryInfo(directroy);
            FileSystemInfo[] files = dir.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i] as FileInfo;
                if (file != null)
                {
                    value.Add(file.Name);
                }
            }
            return (string[])value.ToArray(typeof(string));
        }
        private void skinButton11_Click(object sender, EventArgs e)//刷新mod
        {
            ModsList.Items.Clear();
            if (Directory.Exists(Rundir + "\\mods"))
            {
                string[] files = Enumeratefiles(Rundir + "\\mods");
                foreach (string v in files)
                {
                    if (v.Substring(v.Length - 3, 3) == "jar")
                    {
                        ModsList.Items.Add(new CCWin.SkinControl.SkinListBoxItem(v));
                    }
                }
            }
        }
        private void skinButton9_Click(object sender, EventArgs e)//添加mod
        {
            string filepath = openfile("选择mod文件", "Mod|*.jar");
            if (filepath != "" && filepath != null)
            {
                string[] v = filepath.Split('\\');
                string filename = v[v.Length - 1];
                try
                {
                    File.Copy(filepath, Rundir + "\\mods\\" + filename);
                }
                catch (Exception ex)
                {
                    SkinMessageBox("警告 :", ex.Message, 1);
                }
                ModsList.Items.Add(new CCWin.SkinControl.SkinListBoxItem(filename));
            }
        }
        private void skinButton10_Click(object sender, EventArgs e)//删除mod
        {
            if (ModsList.SelectedIndex != -1)
            {
                try
                {
                    File.Delete(Rundir + "\\mods\\" + ModsList.SelectedItem);
                }
                catch (Exception ex)
                {
                    SkinMessageBox("警告 :", ex.Message, 1);
                }
                ModsList.Items.Remove((CCWin.SkinControl.SkinListBoxItem)ModsList.SelectedItem);
            }
        }
        private void skinButton12_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Rundir + "\\mods");
        }//打开mods文件夹
        private void skinButton16_Click(object sender, EventArgs e)//添加plugins
        {
            string filepath = openfile("选择插件", "插件|*.jar");
            if (filepath != "" && filepath != null)
            {
                string[] v = filepath.Split('\\');
                string filename = v[v.Length - 1];
                try
                {
                    File.Copy(filepath, Rundir + "\\plugins\\" + filename);
                }
                catch (Exception ex)
                {
                    SkinMessageBox("警告 :", ex.Message, 1);
                }
                PluginsList.Items.Add(new CCWin.SkinControl.SkinListBoxItem(filename));
            }
        }
        private void skinButton15_Click(object sender, EventArgs e)//删除plugins
        {
            if (PluginsList.SelectedIndex != -1)
            {
                try
                {
                    File.Delete(Rundir + "\\plugins\\" + PluginsList.SelectedItem);
                }
                catch (Exception ex)
                {
                    SkinMessageBox("警告 :", ex.Message, 1);
                }
                PluginsList.Items.Remove((CCWin.SkinControl.SkinListBoxItem)PluginsList.SelectedItem);
            }
        }
        private void skinButton14_Click(object sender, EventArgs e)//刷新pluugins
        {
            PluginsList.Items.Clear();
            if (Directory.Exists(Rundir + "\\plugins"))
            {
                string[] files = Enumeratefiles(Rundir + "\\plugins");
                foreach (string v in files)
                {
                    if (v.Substring(v.Length - 3, 3) == "jar")
                    {
                        PluginsList.Items.Add(new CCWin.SkinControl.SkinListBoxItem(v));
                    }
                }
            }
        }
        private void skinButton13_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Rundir + "\\plugins");
        }//打开plugins文件夹
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/NiTian1207/Minecraft_Server_Manager");
        }
        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/NiTian1207/Minecraft_Server_Manager/commit/c8be7d6");
        }
        public static string getwebcode(string url, string encoder)
        {
            WebClient myWebClient = new WebClient();
            byte[] myDataBuffer = myWebClient.DownloadData(url);
            string SourceCode = Encoding.GetEncoding(encoder).GetString(myDataBuffer);
            return SourceCode;
        }
        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/NiTian1207/Minecraft_Server_Manager/commits/master");
        }
        private void Copydirectory(string olddir,string newdir)
        {
            ArrayList files = new ArrayList(Directory.GetFiles(olddir));
            foreach (string i in files)
            {
                string[] str = i.Split('\\');
                File.Copy(i, newdir + "\\" + str[str.Length - 1]);
            }
            ArrayList dirs = new ArrayList(Directory.GetDirectories(olddir));
            foreach (string i in dirs)
            {
                string[] str = i.Split('\\');
                string dir = newdir + "\\" + str[str.Length - 1];
                Directory.CreateDirectory(dir);
                Copydirectory(i, dir);
            }
        }//复制目录
        private void skinButton6_Click(object sender, EventArgs e)
        {
            if (SkinMessageBox("警告 :", "此操作将删除原地图，是否继续？", 2) == 2)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowDialog();
                string path = fbd.SelectedPath;
                if (path != null && path != "")
                {
                    if (Directory.Exists(Rundir + "\\world"))
                        Directory.Delete(Rundir + "\\world", true);
                    Directory.CreateDirectory(Rundir + "\\world");
                    Copydirectory(path, Rundir + "\\world");
                    SkinMessageBox("提示 :", "导入完成", 1);
                }
            }
            
        }
        private void skinButton7_Click(object sender, EventArgs e)
        {
            Directory.Delete(Rundir + "\\world", true);
        }
        private void skinButton8_Click(object sender, EventArgs e)
        {
            DateTime time = new DateTime();
            time = DateTime.Now;
            string dirname = time.Year + "." + time.Month + "." + time.Day + " " + time.Hour + "." + time.Minute + "." + time.Second;
            if (!Directory.Exists(Rundir + "\\Map_Backup"))
            {
                Directory.CreateDirectory(Rundir + "\\Map_Backup");
                Directory.CreateDirectory(Rundir + "\\Map_Backup\\" + dirname);
                Copydirectory(Rundir + "\\world", Rundir + "\\Map_Backup\\" + dirname);
                if (SkinMessageBox("提示 :", "备份完成，是否打开备份文件夹？", 2) == 2)
                {
                    Process.Start("explorer.exe", Rundir + "\\Map_Backup\\" + dirname);
                }
            }
            else
            {
                Directory.CreateDirectory(Rundir + "\\Map_Backup\\" + dirname);
                Copydirectory(Rundir + "\\world", Rundir + "\\Map_Backup\\" + dirname);
                if (SkinMessageBox("提示 :", "备份完成，是否打开备份文件夹？", 2) == 2)
                {
                    Process.Start("explorer.exe", Rundir + "\\Map_Backup\\" + dirname);
                }
            }
        }//备份地图
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isserverrunning)
            {
                SkinMessageBox("警告 :", "请先关闭服务器", 1);
                return;
            }
            notifyIcon1.Dispose();
            this.Close();
        }
        private void 显示主窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void skinButton5_Click(object sender, EventArgs e)
        {
            //TODO:懒人包
        }
        private void Button_serverstopi_Click(object sender, EventArgs e)
        {
            //TODO:强制关闭
        }
        private void Button_serverrestart_Click(object sender, EventArgs e)
        {
            //TODO:重启服务器
        }
        private void skinButton2_Click(object sender, EventArgs e)
        {
            //TODO:循环任务
        }
        //TODO:更新
    }
}
