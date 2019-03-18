using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace TrelloTest.FeatureFile
{
    [Binding]
    public sealed class Hooks
    {

        [BeforeScenario(Order = -100)]
        public void DeleteAllBoards()
        {
            TrelloBoardApi _TrelloBoardApi = new TrelloBoardApi();
            TrelloBoardTestData trelloBoard = new TrelloBoardTestData();
            IRestResponse allBoards = _TrelloBoardApi.GetAllBoards(trelloBoard);
            JArray jArray = JArray.Parse(allBoards.Content);
            for (var i = 0; i < jArray.Count; i++)
            {
                trelloBoard.Id = jArray[i]["id"].Value<string>();
                _TrelloBoardApi.DeleteBoard(trelloBoard);
            }
        }


        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}
