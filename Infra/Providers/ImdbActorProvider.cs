using System;
using HtmlAgilityPack;
using Splitit.Splitit.Entities;
using Splitit.Splitit.Repositories;
using Splitit.Splitit.ValueObjects;

namespace Splitit.Infra.Providers
{
    public class ImdbActorProvider : IActorProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IActorRepository _actorRepository;

        public ImdbActorProvider(HttpClient httpClient, IActorRepository actorRepository)
        {
            _httpClient = httpClient;
            _actorRepository = actorRepository;
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
                    var actor = new Actor
                    {
                        Id = System.Guid.NewGuid().ToString(),
                        Name = name,
                        Rank = rank,
                        Source = "IMDb"
                    };
                    actors.Add(actor);
                    _actorRepository.Add(actor);
                }
            }
            return actors;
        }
    }
}

