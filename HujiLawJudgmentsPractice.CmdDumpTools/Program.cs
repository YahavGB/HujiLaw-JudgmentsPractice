using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HujiLawJudgmentsPractice.CmdDumpTools
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            List<Entry> entries = new List<Entry>();
            const int NumberOfResources = 10;
            for (int i = 1; i <= NumberOfResources; i++)
            {
                var resourceName = "HujiLawJudgmentsPractice.CmdDumpTools.Data." + i.ToString() + ".txt";
                string content = "";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    content = reader.ReadToEnd();
                }

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                entries.AddRange(
                    doc.DocumentNode.SelectSingleNode("//table").Descendants("tr").Skip(1).Select(tr => new Entry()
                        {
                            Section = doc.DocumentNode.SelectSingleNode("//h1").InnerText.Trim(),
                            Name = tr.ChildNodes[0].InnerText.Trim(),
                            SidesDescription = tr.ChildNodes[1].InnerText.Trim(),
                            LawQuestion = tr.ChildNodes[2].InnerText.Trim(),
                            BeforeSupremeCourt = tr.ChildNodes[3].InnerText.Trim(),
                            Rationale = tr.ChildNodes[4].InnerText.Trim(),
                            LessonRelated = tr.ChildNodes[5].InnerText.Trim()
                        }).ToList());
            }

            Console.WriteLine(entries.Count());

            Console.ReadLine();
            return;
            //Microsoft.Office.Interop.Word.Application han = new Microsoft.Office.Interop.Word.Application();
            ////han.Application.Visible = false;

            //object nullobj = System.Reflection.Missing.Value;

            //Microsoft.Office.Interop.Word.Document document = han.Documents.Open(
            //    @"C:\Users\Yahav Gindi Bar\Documents\law-table.doc");

            //Console.WriteLine("nubmer of tables: {0}", document.Tables.Count);
            //string content = "<table>";
            //foreach (Microsoft.Office.Interop.Word.Table table in document.Tables)
            //{
            //    content += "<tr>";
            //    for (int i = 0; i < table.Columns.Count; i++)
            //    {
            //        for (int j = 0; j < table.Rows.Count; j++)
            //        {
            //            content += "<td>";
            //            content += table.Cell(j, i).Range.Text;
            //            content += "</td>";
            //        }
            //        content += "</tr>";
            //    }
            //    break;
            //}

            //content += "</table>";

            //System.IO.File.WriteAllText(@"C:\Users\Yahav Gindi Bar\Documents\test.txt", content);

            //    document.Close();
            //han.Application.Quit();

            //Console.WriteLine("-- Done --");
            //Console.ReadLine();
        }
    }

    public class Entry
    {
        public string Section { get; set; }
        public string Name { get; set; }
        public string SidesDescription { get; set; }
        public string LawQuestion { get; set; }
        public string BeforeSupremeCourt { get; set; }
        public string Rationale { get; set; }
        public string LessonRelated { get; set; }
    }
}
