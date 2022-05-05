using System.Diagnostics;
using System.Text;

namespace AsyncExample
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient;
        public Form1()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private void SyncButton_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            var stopWatch = Stopwatch.StartNew();

            DownloadWebsitesSync();
            txtResult.Text += $"Elapsed time:{stopWatch.Elapsed}->{Environment.NewLine}";
        }

        private void DownloadWebsitesSync()
        {
            foreach (var item in Contents.WebSites)
            {
                var result = DownloadWebsiteSync(item);
                ReportResult(result);
            }
        }

        private void ReportResult(string result)
        {
            txtResult.Text += result;
        }

        private string DownloadWebsiteSync(string url)
        {
            var response = _httpClient.GetAsync(url).GetAwaiter().GetResult();
            var responsePayloadBytes = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            return $"finish downloading data from {url}.Total bytes returned {responsePayloadBytes.Length}.{Environment.NewLine}";
        }

        private async void AsyncButton_Click(object sender, EventArgs e)
        {
            AsyncButton.Enabled = false;
            txtResult.Text = "";
            var stopWatch = Stopwatch.StartNew();

            await DownloadWebsitesASync();
            txtResult.Text += $"Elapsed time:{stopWatch.Elapsed}->{Environment.NewLine}";
            this.AsyncButton.Enabled = true;
        }
        private async Task DownloadWebsitesASync()
        {
            #region 逐一执行
            /*      foreach (var item in Contents.WebSites)
                  {
                      //var result = await DownloadWebsiteASync(item);
                      var result = await Task.Run(() => DownloadWebsiteSync(item));
                      ReportResult(result);
                  }*/
            #endregion
            #region 并行执行
            List<Task<string>> list = new List<Task<string>>();
            foreach (var item in Contents.WebSites)
            {
                list.Add(DownloadWebsiteASync(item));
            }
            var results = await Task.WhenAll(list);
            foreach (var item in results)
            {
                ReportResult(item);
            }
            #endregion
        }
        private async Task<string> DownloadWebsiteASync(string url)
        {
            var sw = Stopwatch.StartNew();
            var response = await _httpClient.GetAsync(url);
            var responsePayloadBytes = await response.Content.ReadAsByteArrayAsync();
            sw.Stop();
            return $"finish downloading data from {url}.Total bytes returned {responsePayloadBytes.Length},Elapsed time:{sw.Elapsed}.{Environment.NewLine}";
        }
        Dictionary<int, string> lst = new Dictionary<int, string>();
        private void button1_Click(object sender, EventArgs e)
        {
            lst.Clear();
            var t = lst.FirstOrDefault(a => a.Key == 1);
            var defaultDay = default(KeyValuePair<int, string>);
            if (t.Equals(defaultDay))
            {
                lst.Add(1, "第一个按钮");
            }
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var t = lst.FirstOrDefault(a => a.Key == 2);
            var defaultDay = default(KeyValuePair<int, string>);
            if (t.Equals(defaultDay))
            {
                lst.Add(2, "第一个按钮");
            }
            Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var t = lst.FirstOrDefault(a => a.Key == 3);
            var defaultDay = default(KeyValuePair<int, string>);
            if (t.Equals(defaultDay))
            {
                lst.Add(3, "第一个按钮");
            }
            Show();
        }
        private void Show()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in lst)
            {
                sb.Append($"{item.Key.ToString()}->{item.Value}");
            }
            tsl.Text =sb.ToString();
        }
    }
}