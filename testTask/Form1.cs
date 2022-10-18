using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Binance.Spot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace testTask
{
    public partial class Form1 : Form
    {
        private string nums = "1000";
        private int limit = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void loadTickers()
        {
            var json = new WebClient().DownloadString("https://api.binance.com/api/v3/ticker/price");
            var info = JsonConvert.DeserializeObject<List<Ticker>>(json);
            comboBox1.DataSource = info.Select(s => s.symbol).ToList();
        }

        private void loadDataGrid()
        {
            dataGridView1.Visible = true;
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Green;
            dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.Green;
            dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.Red;
            dataGridView1.Columns[3].DefaultCellStyle.BackColor = Color.Red;
        }

        public async void getCurrency()
        {
            while (true)
            {
                string json = new WebClient().DownloadString("https://api.binance.com/api/v3/depth?symbol=" + comboBox1.Text + "&limit=" + nums);
                Trade info = JsonConvert.DeserializeObject<Trade>(json);

                dataGridView1.Rows.Clear();

                for (int i = 0; i < limit; ++i)
                    dataGridView1.Rows.Add(info.bids[i][1], info.bids[i][0], info.asks[i][0], info.asks[i][1]);

                dataGridView1.Update();
                dataGridView1.Refresh();

                await Task.Delay(200);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loadTickers();
            loadDataGrid();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.Visible = false;
            getCurrency();
        }
    }
}
