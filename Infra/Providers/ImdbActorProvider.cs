using System;
using HtmlAgilityPack;
using Splitit.Splitit.Entities;
using Splitit.Splitit.ValueObjects;

namespace Splitit.Infra.Providers
{
    public class ImdbActorProvider : IActorProvider
    {
        private readonly HttpClient _httpClient;

        public ImdbActorProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Actor>> GetActorsAsync()
        {
            var actors = new List<Actor>();
            var response = await _httpClient.GetStringAsync("https://www.imdb.com/list/ls054840033/");
            var document = new HtmlDocument();
            document.LoadHtml(response);

            var nodes = document.DocumentNode.SelectNodes("//div[@class='lister-item mode-detail']");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var name = node.SelectSingleNode(".//h3[@class='lister-item-header']/a").InnerText.Trim();
                    var rankStr = node.SelectSingleNode(".//span[@class='lister-item-index unbold text-primary']").InnerText.Trim().Replace(".", "");
                    int rank = int.Parse(rankStr);
                    actors.Add(new Actor(0, name, new Rank(rank)));
                }
            }
            return actors;
        }
    }
}

