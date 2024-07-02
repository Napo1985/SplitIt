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

            var nodes = document.DocumentNode.SelectNodes("//li[@class='ipc-metadata-list-summary-item']");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var fullName = node.SelectSingleNode(".//h3[@class='ipc-title__text']").InnerText.Trim();
                    var rankAndName = fullName.Split(new[] { ". " }, 2, StringSplitOptions.None);

                    var detailsNode = node.SelectSingleNode(".//div[@data-testid='dli-bio']");
                    var details = detailsNode?.InnerText.Trim();

                    var actor = new Actor
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = rankAndName[1],
                        Details = details,
                        Type = "Actor", // Adjust as per your need
                        Rank = new Rank(int.Parse(rankAndName[0])), // Assuming Rank is an enum or similar
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

