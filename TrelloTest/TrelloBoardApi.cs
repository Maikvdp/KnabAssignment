using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloTest
{
    public class TrelloBoardApi
    {
        private RestClient RestClient = new RestClient("https://api.trello.com/1");

        public IRestResponse CreateBoard(TrelloBoardTestData trelloBoardTestData)
        {
            var request = new RestRequest("/boards/", Method.POST);
            string postData = $"{{\"name\": \"{trelloBoardTestData.Name}\", \"key\": \"{trelloBoardTestData.Key}\", \"token\": \"{trelloBoardTestData.Token}\"}}";
            request.AddParameter("application/json", postData, ParameterType.RequestBody);
            IRestResponse response = RestClient.Execute(request);
            return response;
        }

        public IRestResponse GetAllBoards(TrelloBoardTestData trelloBoardTestData)
        {
            var request = new RestRequest($"/members/me/boards?key={trelloBoardTestData.Key}&token={trelloBoardTestData.Token}", Method.GET);
            IRestResponse response = RestClient.Execute(request);
            return response;
        }

        public IRestResponse UpdateBoard(TrelloBoardTestData trelloBoardTestData)
        {
            var request = new RestRequest($"/boards/{trelloBoardTestData.Id}", Method.PUT);
            string postData = $"{{\"name\": \"{trelloBoardTestData.Name}\", \"key\": \"{trelloBoardTestData.Key}\", \"token\": \"{trelloBoardTestData.Token}\", \"closed\": \"{trelloBoardTestData.Closed}\"}}";
            request.AddParameter("application/json", postData, ParameterType.RequestBody);
            IRestResponse response = RestClient.Execute(request);
            return response;
        }

        public IRestResponse GetBoard(TrelloBoardTestData trelloBoardTestData)
        {
            var request = new RestRequest($"/boards/{trelloBoardTestData.Id}?key={trelloBoardTestData.Key}&token={trelloBoardTestData.Token}", Method.GET);
            IRestResponse response = RestClient.Execute(request);
            return response;
        }

        public IRestResponse DeleteBoard(TrelloBoardTestData trelloBoardTestData)
        {
            var request = new RestRequest($"/boards/{trelloBoardTestData.Id}?key={trelloBoardTestData.Key}&token={trelloBoardTestData.Token}", Method.DELETE);
            IRestResponse response = RestClient.Execute(request);
            return response;
        }

    }
}