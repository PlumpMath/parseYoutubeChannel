using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace parseYoutubeChannel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] channels = youtube.Channels(textBox1.Text);
            foreach(string s in channels)
            {
                ListViewItem i = new ListViewItem(s.Split('/')[4]);
                i.SubItems.Add(youtube.Subscribe(s));
                i.SubItems.Add(youtube.Pageview(s));
                i.SubItems.Add(s);
                this.listView1.Items.Add(i);
            }
            
        }
    }
}
