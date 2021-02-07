using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace SimbirSoftTestConsole
{
    class Word
    {
        WebClient WebClient = new WebClient();
        public string namefile = " ";

        public async Task GetText(string website)
        {
            byte[] raw= { };
                raw = WebClient.DownloadData(website);
            Console.WriteLine("Analyze");

            string webData = System.Text.Encoding.UTF8.GetString(raw);

            var pageDoc = new HtmlAgilityPack.HtmlDocument();

            pageDoc.LoadHtml(webData);

            var pageText = pageDoc.DocumentNode.InnerText;

            var Data = HttpUtility.HtmlDecode(pageText);

            string[] text = Data.Split(',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t', ' ');

            namefile = $"{Environment.CurrentDirectory}\\{website.Replace("://","").Replace("/","")}.txt";

            if (File.Exists(namefile))
            {
                File.Delete(namefile);
            }

            foreach (var word in text)
            {
                if (!String.IsNullOrEmpty(word) && !String.IsNullOrWhiteSpace(word))
                {
                    File.AppendAllText(namefile, word + '\n');
                    await Task.Delay(10);
                }
            }

            List<string> lines = File.ReadAllLines(namefile).ToList();
            Counting(lines);

        }

        private static void Counting(List<string> lines)
        {
            try
            {
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                var wordCount = from w in lines group w by w;
                foreach (var words in wordCount)
                {
                    foreach (var wordsCount in words)
                    {
                        keyValues.Add(words.Key, wordsCount.Count().ToString());
                        break;
                    }
                }
                Console.WriteLine("\tAnalysis completed!\n");

                foreach (KeyValuePair<string, string> kvp in keyValues)
                {
                    Console.WriteLine($"Word= {kvp.Key}\t\t\t Count= {kvp.Value}");
                }

            }
            catch (Exception e) { }
        }

    }
}
