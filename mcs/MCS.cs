using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections;

namespace mcs
{
    class MCS
    {
        string ServerPath;
        public delegate void serverEventHandler(object sender, mcs.MCSEvent e); //委托
        public event serverEventHandler serverMessage;//回显文本事件
        Process ps; //启动进程
        private Thread th_output;//监控OutPut输出线程
        private Thread th_errorput;//监控ErrorPut输出线程
        private Thread th_ps;//监控进程运行状态线程
        private bool serverIsRun;//服务端是否在运行
        private string serverVer;//服务端版本
        public MCS(string runDir)
        {
            ServerPath = runDir;
            serverIsRun = false;
        }//构造函数
        public static bool WriteText(string Path, string value)
        {
            try
            {
                StreamWriter sw = File.CreateText(Path);
                sw.Write(value);
                sw.Flush();
                sw.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }//写出文本
        public static string ReadText(string Path)
        {
            try
            {
                StreamReader sr = new StreamReader(Path);
                string str = "";
                while (!sr.EndOfStream)
                {
                    str += sr.ReadLine() + "\r\n";
                }
                return str;
            }
            catch
            {
                return "";
            }
        }//读取文件
        public void changerundir(string Rundir)
        {
            ServerPath = Rundir;
        }
        public bool Instal(string jarPath)
        {
            if (Directory.Exists(ServerPath))
                Directory.Delete(ServerPath,true);
            try
            {
                Directory.CreateDirectory(ServerPath);
                File.Copy(jarPath, ServerPath + "\\Server.jar");
                WriteText(ServerPath + "\\eula.txt", Properties.Resources.Eula);//写出协议
                WriteText(ServerPath + "\\server.properties", System.Text.Encoding.Default.GetString(Properties.Resources.server));//写出默认配置
            }
            catch
            {
                return false;
            }
            return true;
        }//安装服务端
        public string[,] GetConf()
        {
             StreamReader sr = new StreamReader(ServerPath + "\\server.properties",false);
             string conft;
            ArrayList List = new ArrayList();
            //string[,] conftt = new string[36,2];
            int i = 0;
             while ((conft = sr.ReadLine()) != null)
             {
                if (conft.Substring(0,1) != "#")
                {
                    List.Add(conft);
                    i++;
                }

                /*
                string[] s = conft.Split('=');
                 conftt[i, 0] = s[0];
                 conftt[i, 1] = s[1];
                 i++;
                 */
             }
            string[,] conftt = new string[i, 2];
            for (int x = 0; x < i; x++)
            {
                string[] s = List[x].ToString().Split('=');
                conftt[x, 0] = s[0];
                conftt[x, 1] = s[1];
            }

            return conftt;
        }//读取server.properties 返回二维数组
        public bool addPlus(string Path)
        {
            try
            {
                string[] FileName = Path.Split('\\');
                /*
                if (File.Exists(ServerPath + "\\Plugins\\" + FileName[FileName.Length - 1]))
                {
                    return false;
                }
                else
                {
                */
                    File.Copy(Path, ServerPath + "\\Plugins");
                    /*
                }
                */
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool addMod(string Path)
        {
            try
            {
                string[] FileName = Path.Split('\\');
                File.Copy(Path, ServerPath + "\\Mods");
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool Run(string javaPath,string cmd)
        {
            ps = new Process();
            ps.StartInfo.FileName = javaPath;
            ps.StartInfo.Arguments = cmd;
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.RedirectStandardError = true;
            ps.StartInfo.RedirectStandardInput = true;
            ps.StartInfo.RedirectStandardOutput = true;
            ps.StartInfo.WorkingDirectory = ServerPath;
            ps.StartInfo.CreateNoWindow = true;
            try
            {
                ps.Start();
            }
            catch (Exception e)
            {
                
                ps = null;
                return false;
            }
            th_output = new Thread(Th_Output);
            th_output.Start();
            return true;

        }
        public void Stop()
        {
            ps.StandardInput.WriteLine("stop");
        }
        private void Th_Output()
        {
            string line;
            while ((line = ps.StandardOutput.ReadLine()) != null)
            {
                line = line.Replace("\b", "");
                if (!serverIsRun)
                {
                    int a = line.IndexOf("Starting minecraft server");
                    if (a != -1)
                    {
                        a = a + 33;
                        serverVer = line.Substring(a).Trim();
                    }
                    if (line.IndexOf("Done") != -1)
                    {
                        line = "[提醒] 服务端已成功运行，您可以进入服务器了。";
                        serverIsRun = true;
                    }
                }
                if (serverMessage != null)
                    serverMessage(this, new MCSEvent(line, 0));
            }
        }//文本回显
        public void sendMessage(string cmd)
        {
            ps.StandardInput.WriteLine(cmd);
        }//发送指令
    }
}
