using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace HujiLawJudgmentsPractice.FormsUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                const int NumberOfResources = 10;
                for (int i = 1; i <= NumberOfResources; i++)
                {
                    var resourceName = "HujiLawJudgmentsPractice.FormsUI.Data." + i.ToString() + ".txt";
                    string content = "";
                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(content);
                    checkedListBox1.Items.Add(doc.DocumentNode.SelectSingleNode("//h1").InnerText.Trim(), true);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
