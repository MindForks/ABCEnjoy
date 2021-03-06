﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABCEnjoy;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using SQLite;

namespace Parser.Core.Habra
{
    class HabraParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            // SQLiteConnection database = new SQLiteConnection("/Users/macbook/Desktop/Items.db");
            // SQLiteCommand cm = database.CreateCommand("select * from Itm");

          
            using (var conn = new SQLite.SQLiteConnection("/Users/macbook/Desktop/Items.db"))
            {
                //  var cmd = conn.CreateCommand("select * from Itm");
                //  var r = cmd.ExecuteQuery<ItemOfCategory>();
               // conn.Insert(itm);
                //foreach (var res in r)
                  //  Console.WriteLine(res.Id);

            }
            int count = 20;
            var list  = new List<string>();
            var prev = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("dark3") && item.TextContent !="");
            var datetime = document.QuerySelectorAll("h3").Where(item => item.ClassName != null && item.ClassName.Contains("title19"));
            var tags = document.QuerySelectorAll("b");
           


            List<string> hrefTags = new List<string>();
            foreach (IElement element in document.QuerySelectorAll("b").Where(item => item.ClassName != null ))
            {
                hrefTags.Add(element.GetAttribute("src"));
            }
           
            ItemOfCategory[] NewNode = new ItemOfCategory[count];


            int tmp = 0;
            foreach (var it in prev)
            {
                list.Add(it.TextContent);
                NewNode[tmp] = new ItemOfCategory();
                NewNode[tmp].Preview = it.TextContent;
                tmp++;
            }

            tmp = 0;
            foreach (var it in datetime)
            {
                list.Add(it.TextContent);
                NewNode[tmp].Date = it.TextContent;
                if (it.TextContent == "\n") 
                    NewNode[tmp].Date = "-1";
                else
                    NewNode[tmp].Date = NewNode[tmp].Date.Remove(0, 2);
                tmp++;
            }


            tmp = 0;
            foreach (var it in datetime)
            {
                list.Add(it.TextContent);
                NewNode[tmp].Tags = it.TextContent;
                tmp++;
            }

            tmp = 0;
          



            return list.ToArray();
        }
    }
}
