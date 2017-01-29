using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Linq;
using System.Xml;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections;


public static class Test
{

    public static void ParseRssFile()
    {
        String[] se = new String[] {
           "http://feeds.reuters.com/reuters/INentertainmentNews",
        "http://feeds.reuters.com/reuters/INworldNews",
    "http://feeds.reuters.com/reuters/INtechnologyNews",
    "http://feeds.reuters.com/reuters/INhealth",
    "http://feeds.reuters.com/reuters/INlifestyle",
    "http://feeds.reuters.com/reuters/INhollywood",
    
        };
        /* "http://www.news18.com/rss/india.xml",
             "http://feeds.reuters.com/reuters/INtopNews",
             "http://timesofindia.indiatimes.com/rssfeeds/-2128936835.cms",
             "http://feeds.feedburner.com/ndtvnews-india-news","http://zeenews.india.com/rss/india-national-news.xml",
             "http://economictimes.indiatimes.com/rssfeedstopstories.cms",
             "http://www.oneindia.com/rss/news-india-fb.xml",
             "http://www.hindustantimes.com/rss/india/rssfeed.xml",
             "http://www.rediff.com/rss/inrss.xml",
          
               */



        string path = @"D:\hello3.txt";
        string path2 = @"D:\hello4.txt";
        try
        {

            if (File.Exists(path))
            {

                File.Delete(path);
            }
            if (File.Exists(path2))
            {

                File.Delete(path2);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        foreach (String s in se)
        {
            XmlDocument rssXmlDoc = new XmlDocument();

            // Load the RSS file from the RSS URL
            rssXmlDoc.Load(s);

            // Parse the Items in the RSS file
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");



            // Iterate through the items in the RSS file
            foreach (XmlNode rssNode in rssNodes)
            {

                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("link");
                string link = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("description");
                string description = (rssSubNode != null ? rssSubNode.InnerText : "");
                description = description.Replace("  ", " ");
                string output;
                if (description == "" || title == "")
                {
                    continue;

                }
                //get rid of HTML tags
                output = Regex.Replace(description, "<[^>]*>", string.Empty);
                output = Regex.Replace(output, ",", string.Empty);
                output = Regex.Replace(output, @"^\s*$\n", string.Empty, RegexOptions.Multiline);
                File.AppendAllText(path, output + Environment.NewLine);
                output = Regex.Replace(title, "<[^>]*>", string.Empty);
                output = Regex.Replace(output, ",", string.Empty);
                output = Regex.Replace(output, @"^\s*$\n", string.Empty, RegexOptions.Multiline);
                File.AppendAllText(path2, output + Environment.NewLine);
            }
        }
        var tempFileName = Path.GetTempFileName();
        try
        {
            using (var streamReader = new StreamReader(path))
            using (var streamWriter = new StreamWriter(tempFileName))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        streamWriter.WriteLine(line);
                }
            }
            File.Copy(tempFileName, path, true);
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }
}

    /*public static void Main(ArrayList arrdes, ArrayList arrtitle)
    {
        string path = @"D:\hello.txt";
        string path2= @"D:\hello2.txt";


        try
        {
                
               if (File.Exists(path))
                {
                    
                    File.Delete(path);
                }
                if (File.Exists(path2))
               {
                
                File.Delete(path2);
               }

            FileStream fs1 = File.Create(path2);
            using (FileStream fs = File.Create(path))
                {
                foreach (Object obj in arrdes)
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(obj.ToString());
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                        info = new UTF8Encoding(true).GetBytes(Environment.NewLine);
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                }
                }

                // Open the stream and read it back.
                /*using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
        

        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public static void Main2()
    {
        string path = @"D:\hello2.txt";


        try
        {

            ArrayList arr = ParseRssFile1();

            // Delete the file if it exists.
            if (File.Exists(path))
            {
                // Note that no lock is put on the
                // file and the possibility exists
                // that another process could do
                // something with it between
                // the calls to Exists and Delete.
                File.Delete(path);
            }

            // Create the file.
            using (FileStream fs = File.Create(path))
            {
                foreach (Object obj in arr)
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(obj.ToString());
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                    info = new UTF8Encoding(true).GetBytes(Environment.NewLine);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);

                }
            }

            // Open the stream and read it back.
            /*using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }


        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

}*/