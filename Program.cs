using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp2
{
    public class Program
    {
        private static SemaphoreSlim semaphore;
        public delegate void callBack(int state);

        public static void Main(string[] args)
        {
            string generalPath = "C:\\CShap\\namespace1\\";

            /* void processFileProxy()
             {
                 string filePath = "C:\\Users\\Pc\\Downloads\\37269.txt";
                 string filePath2 = "C:\\Users\\Pc\\Downloads\\37268.txt";

                 string[] readFile(string filePath)
                 {
                     string content = "";
                     using (StreamReader sr = new StreamReader(filePath))
                     {
                         content = sr.ReadToEnd();
                     }
                     string[] proxyArr = content.Split("\n");
                     return proxyArr;
                 }

                 string[] proxyArr1 = readFile(filePath);
                 string[] proxyArr2 = readFile(filePath2);
                 List<string> arr = new List<string>() { };
                 int arrLength = proxyArr1.Length + proxyArr2.Length;
                 for (int index = 0; index < arrLength; index++)
                 {
                     if (index % 2 != 0 && index > 2)
                     {
                         arr.Add(proxyArr1[index / 2]);
                     }
                     else if (index % 2 == 0 && index > 2)
                     {
                         arr.Add(proxyArr2[index / 2]);
                     }
                 }
                 string fileS = "C:\\Users\\Pc\\source\\repos\\ConsoleApp1\\ConsoleApp1\\proxyS.txt";

                 using (StreamWriter sw = new StreamWriter(fileS))
                 {
                     foreach (string element in arr)
                     {
                         sw.WriteLine(element.Trim());
                     }
                 }
             }*/
            void processFileData(int count)
            {
                string filePath = $"{generalPath}dataUc2.txt";
                List<string> newArr = new List<string>();
                string[] readFile(string filePath)
                {
                    string content = "";
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        content = sr.ReadToEnd();
                    }
                    string[] dataArr = content.Split("\n");
                    return dataArr;
                }
                string[] data = readFile(filePath);
                int space = data.Length / count;
                int residual = data.Length - (space * count);
                for (int inx = 0; inx < count; inx++)
                {
                    string fileS = $"{generalPath}data{inx}.txt";
                    if (inx < count)
                    {
                        Console.WriteLine(inx);
                        List<string> list = new List<string>();
                        for (int j = (space * inx); j < space * (inx + 1); j++)
                        {
                            list.Add(data[j]);
                        }

                        using (StreamWriter sw = new StreamWriter(fileS))
                        {
                            foreach (string item in list)
                            {
                                sw.WriteLine(item.Trim());
                            }
                        }

                    }
                }

            }
          // processFileData(10);

            void showLength()
            {
                string filePath = $"{generalPath}merged.txt";
                List<string> newArr = new List<string>();
                string[] readFile(string filePath)
                {
                    string content = "";
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        content = sr.ReadToEnd();
                    }
                    string[] dataArr = content.Split("\n");
                    return dataArr;
                }
                string[] data = readFile(filePath);
                Console.WriteLine("length" + data.Length);
            };
            /*async Task start()
            {
                string proxyAddress = "http://5.161.208.175:9495";
                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = new WebProxy(proxyAddress),
                    UseProxy = true,
                    UseDefaultCredentials = true,
                    UseCookies = true
                };

                string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36";
                string[] cookies = { "datr=dV1LZMfVOpE9o19zSgZ309hP", "datr=C15LZOX2iVTi_001xvnooIPQ", "datr=R15LZM6z_vq6kmQEvf0iWP4z", "datr=dV5LZDG_dbIbyuO3nRQtxDR2", "datr=mV5LZNzLf3WemdjBOAxeJIx2", "datr=rl5LZOwhtAXTQiMYKDX7u0hi", "datr=yF5LZKPNpG1NMBfKLyL8zfS_", "datr=7l5LZF1GQxrs6fMuJf1Q_g-U", "datr=EF9LZB9wx8hy1XkAmOW1pfF5", "datr=J19LZD07RgSBOyaJXrT4CB4U", "datr=RV9LZOKCH2B1Ec7jbyulExKC", "datr=fF9LZKjGp8I2sVrTG1t7Z4Kk" };

                Login login = new Login("trove.catbui.1996", "aaaaaa", userAgent, "datr=dV1LZMfVOpE9o19zSgZ309hP", httpClientHandler, 1);
                //   await login.main();

            }*/
            //  Task.WhenAll(Enumerable.Range(0, 1).Select(i => start())).GetAwaiter().GetResult();
            async Task sendError(string message)
            {
                try
                {
                    var botClient = new TelegramBotClient("6108844689:AAGfmjr4pWeggysbB3wI_9uvw3SKi4dwKHU");

                    var chatId = new Telegram.Bot.Types.ChatId("-936160804");

                    await botClient.SendTextMessageAsync(chatId, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("***** Inside mess *****");
                }

            }

            void processP()
            {
                string[] arr = readFile("C:\\Users\\Pc\\source\\repos\\ConsoleApp1\\ConsoleApp1\\newP.txt");
                List<string> list = new List<string>();
                foreach (string item in arr)
                {
                    list.Add(item.Trim().Replace(":voluuesmlMkx9zn:hpL7SP", ""));
                }
                string fileS = $"C:\\Users\\Pc\\source\\repos\\ConsoleApp1\\ConsoleApp1\\proxyX.txt";
                using (StreamWriter sw = new StreamWriter(fileS))
                {
                    foreach (string item in list)
                    {
                        sw.WriteLine(item.Trim());
                    }
                }
            }

            string[] readFile(string filePath)
            {
                string content = "";
                using (StreamReader sr = new StreamReader(filePath))
                {
                    content = sr.ReadToEnd();
                }
                string[] dataArr = content.Split("\n");
                return dataArr;
            }
            string readBan()
            {
                try
                {
                    string content = "";
                    string filePath = $"{generalPath}checkLog.txt";
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        content = sr.ReadToEnd();
                    }
                    return content;
                }
                catch (Exception ex)
                {
                    return "1";
                }

            }
            void writerUnBan()
            {
                string path = $"{generalPath}checkLog.txt";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    writer.Write("0");
                }
            }
            async Task rotationIp(string id, string key)
            {
                try
                {
                    Task.Delay(60000);
                    HttpClient httpClient = new HttpClient();
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    string url = $"https://coldproxy.com/clients/modules/servers/proxypanel/api.php?id={id}&key={key}&a=rotate";

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                    HttpResponseMessage response = httpClient.SendAsync(request).GetAwaiter().GetResult();
                    string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException ex)
                {
                }
                catch (Exception ex)
                {

                }
            }

            async void rotate(Object o)
            {
                    Console.WriteLine("******************* Rotate ******************");
                    await rotationIp("38040", "857PAV8NPWuRW23K1s0Yd8IIPI3FaDJwda3woF6n");
            }
           //Timer timer = new Timer(rotate, null, 0, 2 * 60 * 1000);
            async Task RunAsync(int idThread)
            {

                string pathUserAgent = $"{generalPath}userAgent.txt";
                string[] userAgentList = readFile(pathUserAgent);
                //   string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36";
                string[] cookies = { "datr=0hZZZEb-TX7JJrzcVyl2wcLo", "datr=_xZZZKhSCDMGSpm0x-zuTWJn", "datr=LxdZZNscii9ZKAca56hbka1q", "datr=aBdZZPPNJuprLIQSVFX-pzXG", "datr=kxdZZNpU1zvYV36BnmwZAXDi", "datr=rxdZZERQmczBG5UMy7lRzeUg", "datr=rxdZZERQmczBG5UMy7lRzeUg", "datr=-xdZZLzMwqrivQboOZyirK5M", "-xdZZLzMwqrivQboOZyirK5M", "datr=NhhZZA0Rfo18Yrms7m-LRvIg", "datr=WhhZZP5tLNyXfXVldvUCWfqd", "datr=eBhZZFZyFOyxZBUTy-7r-pf7" };
                string path = $"{generalPath}proxyX.txt";
                string[] proxyList = readFile(path);
                string filePath = $"{generalPath}data{idThread}.txt";
                string[] dataList = readFile(filePath);
                int i = 0;
                int countCookie = idThread;
                int countProxy = idThread;
                int countUserAgent = idThread;
                Console.WriteLine("Thread Start " + idThread);
                foreach (string item in dataList)
                {
                    string ua = "";
                    try
                    {
                        string checkBan = readBan();
                        if (checkBan == "0")
                        {
                            if (countCookie > cookies.Length - 1)
                            {
                                countCookie = idThread;
                            }
                            if (countProxy > proxyList.Length - 1)
                            {
                                countProxy = idThread;
                            }
                            if (countUserAgent > userAgentList.Length - 1)
                            {
                                countUserAgent = idThread;
                            }
                            string userAgent = userAgentList[countUserAgent].Trim();
                            string proxy = proxyList[countProxy].Trim();
                            
                            userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36";
                            ua = userAgent;
                            string str = item.Trim();
                            string[] strL = str.Split(":");
                            string proxyAddress = $"http://{proxy}";
                            var httpClientHandler = new HttpClientHandler()
                            {
                                Proxy = new WebProxy(proxyAddress),
                                UseProxy = true,
                            //    UseDefaultCredentials = true,
                            //    UseCookies = true
                            };
                            Login login = new Login(strL[0], strL[1], userAgent, cookies[countCookie], httpClientHandler, generalPath, idThread);
                            await login.main();
                            countCookie = countCookie + 11;
                            countProxy = countProxy + 11;
                            countUserAgent = countUserAgent + 11;
                            Console.WriteLine("check: " + idThread + " Count: " + i);
                            i++;
                        }
                        else
                        {
                            Thread.Sleep(600000);
                            writerUnBan();
                            if (countCookie > cookies.Length - 1)
                            {
                                countCookie = idThread;
                            }
                            if (countProxy > proxyList.Length - 1)
                            {
                                countProxy = idThread;
                            }
                            if (countUserAgent > userAgentList.Length - 1)
                            {
                                countUserAgent = idThread;
                            }
                            string proxy = proxyList[countProxy].Trim();
                            string userAgent = userAgentList[countUserAgent].Trim();

                            userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36";
                            ua = userAgent;
                            string str = item.Trim();
                            string[] strL = str.Split(":");
                            string proxyAddress = $"http://{proxy}";
                            var httpClientHandler = new HttpClientHandler()
                            {
                                Proxy = new WebProxy(proxyAddress),
                                UseProxy = true,
                            //    UseDefaultCredentials = true,
                            //    UseCookies = true
                            };
                            Login login = new Login(strL[0], strL[1], userAgent, cookies[countCookie], httpClientHandler, generalPath, idThread);
                            await login.main();
                            countCookie = countCookie + 11;
                            countProxy = countProxy + 11;
                            countUserAgent = countUserAgent + 11;
                            Console.WriteLine("check: " + idThread + " Count: " + i);
                            i++;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("End Thread" + ex.Message);
                        string messErr = $"Error Thread :{idThread} ==>> {ua}  =>> PC";
                        await sendError(messErr);
                        Thread.Sleep(2000);
                        continue;
                    };
                }

            }
           Task.WhenAll(Enumerable.Range(0,10).Select(i => RunAsync(i))).GetAwaiter().GetResult();





           ////////////////////////////////////////////////////// file /////////////////////////////////////////////////////
           
            async Task cutData(string pathGeneral,string fileNameLive,string fileNameData,int index){
                try{
                    string pathLive = $"{pathGeneral}{fileNameLive}";
                    string[] liveArr = readFile(pathLive);
                    string itemEndLive = liveArr[liveArr.Length - 1];
                    string pathData = $"{pathGeneral}{fileNameData}";
                    string[] dataArr = readFile(pathData);
                    int posSt =0 ;
                    for(int i = 0;i<dataArr.Length;i++){
                        if(dataArr[i] == itemEndLive){
                            posSt=i;
                        }
                    }
                    using (StreamWriter writer = new StreamWriter(pathGeneral+$"temp{index}.txt"))
                    {
                        for(int i = posSt;i<dataArr.Length;i++){
                        writer.WriteLine(dataArr[i]);
                        }             
                    }
                    File.Delete(pathData);
                    File.Move(pathGeneral + "temp.txt", pathData);
                }catch (Exception ex){
                    Console.WriteLine("Khong co file");
                }
            }

            async void fileMain(int count,string pathGeneral){
                string fileNameData = "data";
                string fileNameLive="live";
                string fileNameBlock="block";
                for(int i=0;i<count;i++){
                    await cutData(pathGeneral,$"{fileNameLive}{i}",$"{fileNameData}{i}",i);
                }
                for(int i =0;i<count;i++){
                    await appendFile(pathGeneral,fileNameBlock,fileNameData,i);
                }
            }

            async Task appendFile(string pathGeneral,string fileNameBlock,string fileNameData,int index){
                try{
                    string pathBlock = $"{pathGeneral}{fileNameBlock}";
                    string[] blockArr = readFile(pathBlock);
                    string pathData = $"{pathGeneral}{fileNameData}";
                    foreach(string item in blockArr){
                        using (StreamWriter writer = System.IO.File.AppendText(pathData))
                        {
                            writer.WriteLine(item);
                        }
                    }
                }catch(Exception ex){}
            }
            //fileMain(10,generalPath)
        }
    }
}
