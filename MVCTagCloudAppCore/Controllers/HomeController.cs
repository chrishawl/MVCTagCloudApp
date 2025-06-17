using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCTagCloudApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetWordCloud(string url)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var html = await httpClient.GetStringAsync(url);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                htmlDoc.DocumentNode.Descendants()
                    .Where(n => n.Name == "script" || n.Name == "style" || n.Name == "#comment" || n.Name == "meta" || n.Name == "pre" || n.Name == "img" || n.Name == "link" || n.Name == "footer" || n.Name == "nav")
                    .ToList().ForEach(n => n.Remove());

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
                var GroupedWords = wordCollection.Where(word => word.Length > 2)
                    .GroupBy(value => value.ToLower())
                    .OrderByDescending(a => a.Count())
                    .Take(30)
                    .Select(kv => new { text = kv.Key, weight = kv.Count() })
                    .ToList();

                var json = JsonConvert.SerializeObject(new { result = "Success", data = GroupedWords });

                return Content(json, "application/json");
            }
            catch (Exception e)
            {
                var error = JsonConvert.SerializeObject(new { result = "Error", data = "An error has occurred : " + e.ToString() });
                return Content(error, "application/json");
            }
        }
    }
}