using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace WebApplication3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public delegate void ExampleCallback2(string lineCount);
        List<string> Urls = new List<string>();
        static string datasource = @"DYDC-D0133-NB\SQLEXPRESS";
        static string database = "Mytest";
        static string connString = @"Data Source=" + datasource + ";Initial Catalog="
                         + database + ";Trusted_Connection=True; ";
        int pageindex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public static void ResultCallback(string lineCount1)
        {
        }
        public void ThreadProcs(List<string> url)
        {
            ThreadWithState T = new ThreadWithState("", 42, new ExampleCallback2(iNsertData2));
            Thread t = new Thread(new ThreadStart(T.w1));
            List<string> _url = new List<string>();
            _url = url;
            _url.ForEach(delegate (string name)
            {
                T = new ThreadWithState(name, 42, new ExampleCallback2(iNsertData2));
                t = new Thread(new ThreadStart(T.w1));
                t.Start();
            });
            t.Join();
        }
        private void iFirst(int indexing)
        {
            //thid ugu ft yf fy y
            string skeyword = txb1.Text.ToString();
            string squery = "https://www.google.com/search?q=" + skeyword + "&start=" + indexing;
            HtmlWeb webClient = new HtmlWeb();

            List<string> lsUrldtl = new List<string>();

            HtmlDocument linedoc = webClient.Load(squery);

            HtmlNodeCollection linelist = linedoc.DocumentNode.SelectNodes("/html/body/div/div/div/div/div/div");


            string a = linedoc.Text.ToString();
            foreach (var item in linelist)
            {
             lsUrldtl.Add(item.InnerText);
            }

            // string a = linedoc.Text.ToString();
            int iresult = iNext(a, lsUrldtl);

            if (iresult >= 5)
            {
             pageindex += 10;
             iFirst(pageindex);
            }
        }
 
        private int iNext(string pageSource, List<string> contents)
        {
            int ifeedback = 0;
            string _pagehere = pageSource;

            int istart = _pagehere.IndexOf("href=\"http");

            if (istart <= -1)
            {
                ifeedback = 0;
            }
            else
            {
                int i = 0;
                while (_pagehere.IndexOf("href=\"http", istart + i) != -1)
                {
                    istart = _pagehere.IndexOf("href=\"http", istart + i);
                    string snext = _pagehere.Substring(istart + 6, 100);
                    int inext = _pagehere.IndexOf("\"", istart + 6);
                    string snext2 = _pagehere.Substring(istart + 6, inext - (istart + 6));
                    iNsertData(snext2, contents[ifeedback]);
                    Urls.Add(snext2);
                    ifeedback += 1;

                    i++;
                }
            }
            return ifeedback;
        }
        private void iNsertData(string link, string content)
        {
            SqlConnection myConn = new SqlConnection(connString);
            string str = "INSERT INTO [dbo].[urlList]" +
            "([url]," +
            "[content])" +
            "VALUES(" +
            "@url, " +
            "@content)";
            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.Parameters.AddWithValue("@url", link);
                myCommand.Parameters.AddWithValue("@content", content);
                myCommand.ExecuteNonQuery();

            }
            catch (System.Exception ex)
            {
                string err = (ex.ToString());
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }
        private void iNsertData2(string content)
        {
            SqlConnection myConn = new SqlConnection(connString);
            string str = "INSERT INTO [dbo].[urlList2]" +
            "([content])" +
            "VALUES(" +
            "@content )";
            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.Parameters.AddWithValue("@content", content);

                myCommand.ExecuteNonQuery();

            }
            catch (System.Exception ex)
            {
                string err = (ex.ToString());
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }
        private static bool deletetable2()
        {
            SqlConnection myConn = new SqlConnection(connString);
            String str = "delete urlList2;";
            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                string abc = (ex.ToString());
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
            return true;
        }
        private static bool deletetable()
        {
            SqlConnection myConn = new SqlConnection(connString);
            String str = "delete urlList;";
            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                string abc = (ex.ToString());
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
            return true;
        }
        protected void Unnamed2_TextChanged(object sender, EventArgs e)
        {

        }
        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            deletetable();
            deletetable2();
            iFirst(0);
            ThreadProcs(Urls); // Urls.ToString();
            SqlConnection myConn = new SqlConnection(connString);
            SqlDataAdapter sda = new SqlDataAdapter("select * from [MyTest].[dbo].[urlList2]", myConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            gv1.DataSource = dt;
            gv1.DataBind();
        }
        protected void gv1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }



    public class ThreadWithState
    {
        private string _url;
        private string _page;
        private WebForm1.ExampleCallback2 callback;
        static List<string> Urls2 = new List<string>();
        public ThreadWithState(string text, int number, WebForm1.ExampleCallback2 callbackDelegate)
        {
            _url = text;
            callback = callbackDelegate;
        }
        public async Task UrlProc2(string urlink)
        {
            string _url = urlink;
            List<string> lsUrl = new List<string>();
            List<string> lsUrldtl = new List<string>();

            if (_url.Contains(".pdf") || _url.Contains(".xls") || _url.Contains(".txt") || _url.Contains(".ppt"))
            {
                callback("pdf things");
            }
            else
            {
                try
                {
                 HtmlWeb webClient = new HtmlWeb();
                 HtmlDocument linedoc = webClient.Load(_url);
                 HtmlNodeCollection linelist = linedoc.DocumentNode.SelectNodes("/html");
                 if (linelist != null)
                    {
                        string tmp = linedoc.Text.ToString();
                        callback(tmp);
                    }
                    else
                      {
                        using (var httpClients = new HttpClient())
                        {
                            httpClients.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36");
                            var response = await httpClients.GetAsync(_url).ConfigureAwait(false);

                            using (var stream = response.Content.ReadAsStringAsync())
                            {
                                response.Dispose();
                                callback("abc" + stream.ToString());
                            }
                        }
                      }
                }

                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }
        public void UrlProc(string urlink)
        {
            if (urlink == "")
            {
                int h = 0;
            }
            HtmlWeb webClient = new HtmlWeb();
            HtmlDocument linedoc = webClient.Load(urlink);
            string wcontent = linedoc.Text.ToString();
            if (wcontent == "")
            {
            }
            string tmp = wcontent.Substring(0, 50000);
            Urls2.Add(tmp);

        }
        public void w1()
        {
            //UrlProc(_url);
            UrlProc2(_url);
        }
    }
}