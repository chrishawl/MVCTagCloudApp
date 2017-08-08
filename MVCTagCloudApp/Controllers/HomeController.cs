using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MVCTagCloudApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {

            return View();
        }

        /// <summary>
        /// Accesses a web page using the url provided and removes non textual content from the web page.
        /// Then groups words whilst ignoring case to return a List of key value pairs, 
        /// where the key is the unique word and the total is the number of occurences of that word.
        /// </summary>
        /// <param name="url">Url of the web page to be accessed</param>
        /// <returns>Returns json object indicating success with the 30 most common words and associated counts if the code completed successfully</returns>
        /// <returns>Returns json object indicating failure with error associated error data if an exception is thrown</returns>
        public ActionResult GetWordCloud(string url)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                var htmlDoc = web.Load(url);
                if (htmlDoc == null) return null;

                var docDesc = htmlDoc.DocumentNode.Descendants();

                htmlDoc.DocumentNode.Descendants().Where(n => n.Name == "script" || n.Name == "style" || n.Name == "#comment" || n.Name == "meta" || n.Name == "pre" || n.Name == "img" || n.Name == "link" || n.Name == "footer" || n.Name == "nav").
                    ToList().ForEach(n => n.Remove());

                string output = "";
                foreach (var node in htmlDoc.DocumentNode.Descendants())
                {
                    output += node.InnerText;
                }

                output = Regex.Replace(output, @"\t|\n|\r|\d|,", " ");

                var result = new StringWriter();
                HttpUtility.HtmlDecode(output, result);
                var resultToReturn = Regex.Replace(result.ToString(), @"\s+", " ");
                resultToReturn = Regex.Replace(resultToReturn, "[^a-zA-Z]", " ");
                List<string> wordCollection = resultToReturn.Split(' ').ToList();
                var GroupedWords = wordCollection.Where(word => word.Length > 2).GroupBy(value => value.ToLower()).OrderByDescending(a => a.Count()).Take(30).Select(kv => new { text = kv.Key, weight = kv.Count() }).ToList();

                var json = JsonConvert.SerializeObject(new { result = "Success", data = GroupedWords});

                return Content(json, "application/json");
            }
            catch(Exception e)
            {
                var error = JsonConvert.SerializeObject(new { result = "Error", data = "An error has occurred : " + e.ToString() });
                return Content(error, "application/json");
            }
        }
    }
}