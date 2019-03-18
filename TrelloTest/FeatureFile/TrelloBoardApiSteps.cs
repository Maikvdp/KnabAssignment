using FluentAssertions;
using RestSharp;
using TechTalk.SpecFlow;
using Newtonsoft.Json.Linq;

namespace TrelloTest.FeatureFile
{
    [Binding]
    internal class TrelloBoardApiSteps
    {

        TrelloBoardApi _TrelloBoardApi = new TrelloBoardApi();
        TrelloBoardTestData _ClosedTrelloBoard = new TrelloBoardTestData();
        TrelloBoardTestData _ActiveTrelloBoard = new TrelloBoardTestData();
        IRestResponse _Response;

        [BeforeScenario]
        public void Reset_Response()
        {
            _Response = null;
        }


        [Given(@"board name is (.*)")]
        public void GivenBoardNameIsKnabHomeAssignment(string name)
        {
            _ActiveTrelloBoard.Name = name;
        }

        [When(@"I create a new Board")]
        public void WhenICreateANewBoard()
        {
            _Response = _TrelloBoardApi.CreateBoard(_ActiveTrelloBoard);
        }

        [Then(@"the statuscode should be (.*)")]
        public void ThenTheStatuscodeShouldBe(int statusCode)
        {
            _Response.StatusCode.Should().BeEquivalentTo(statusCode);
        }

        [Then(@"the name should be (.*)")]
        public void ThenTheNameShouldBe(string expectedname)
        {
            string actualName = Helpers.GetValueFromResponseContent("name", _Response.Content);
            actualName.Should().BeEquivalentTo(expectedname);
        }

        [Given(@"key is incorrect")]
        public void GivenKeyIsIncorrect()
        {
            _ActiveTrelloBoard.Key = "InCorrectkey";
        }

        [Then(@"the message should be (.*)")]
        public void ThenTheMessageShouldBeInvalidKey(string message)
        {
            _Response.Content.Should().BeEquivalentTo(message);
        }

        [Given(@"a new board")]
        public void GivenANewBoard()
        {
            _Response = _TrelloBoardApi.CreateBoard(_ActiveTrelloBoard);
            _ActiveTrelloBoard.Id = Helpers.GetValueFromResponseContent("id", _Response.Content);
        }


        [Given(@"I close the trello board")]
        public void GivenICloseTheTrelloBoard()
        {
            _ActiveTrelloBoard.Closed = "true";
            _Response = _TrelloBoardApi.UpdateBoard(_ActiveTrelloBoard);
        }


        [When(@"I close the trello board")]
        public void WhenICloseTheTrelloBoard()
        {
            _ActiveTrelloBoard.Closed = "true";
            _Response = _TrelloBoardApi.UpdateBoard(_ActiveTrelloBoard);
        }

        [Then(@"Closed should be true")]
        public void ThenClosedShouldBeTrue()
        {
            string actualClosed = Helpers.GetValueFromResponseContent("closed", _Response.Content); 
            actualClosed.Should().BeEquivalentTo("true");
        }

        [Given(@"boardId is (.*)")]
        public void GivenBoardIdIs(string boardId)
        {
            _ActiveTrelloBoard.Id = boardId;
        }


        [When(@"I get the Trello board")]
        public void WhenIGetTheTrelloBoard()
        {
            _Response = _TrelloBoardApi.GetBoard(_ActiveTrelloBoard);
        }


        [Given(@"a new Trello board with name (.*)")]
        public void GivenANewTrelloBoardWithAName(string name)
        {
            _ActiveTrelloBoard.Name = name;
            _Response = _TrelloBoardApi.CreateBoard(_ActiveTrelloBoard);
            _ActiveTrelloBoard.Id = Helpers.GetValueFromResponseContent("id", _Response.Content);
        }

        [Given(@"a closed Trello board with the name (.*)")]
        public void GivenAClosedTrelloBoardWithTheNameClosedBoard(string name)
        {
            _ClosedTrelloBoard.Name = name;
            _Response = _TrelloBoardApi.CreateBoard(_ClosedTrelloBoard);
            _ClosedTrelloBoard.Id = Helpers.GetValueFromResponseContent("id", _Response.Content);
            _ClosedTrelloBoard.Closed = "true";
            _Response = _TrelloBoardApi.UpdateBoard(_ClosedTrelloBoard);
        }

        [When(@"I get all the Trello boards")]
        public void WhenIGetAllTheTrelloBoards()
        {
            _Response = _TrelloBoardApi.GetAllBoards(_ActiveTrelloBoard);
        }


        [Then(@"the response should be an empty array")]
        public void ThenTheResponseShouldBeAnEmptyArray()
        {
            _Response.Content.Should().BeEquivalentTo("[]");
        }

        [When(@"I delete the trello board")]
        public void WhenIDeleteTheTrelloBoard()
        {
            _Response = _TrelloBoardApi.DeleteBoard(_ActiveTrelloBoard);
        }

        [Then(@"(.*) boards should be returned")]
        public void ThenBoardsShouldBeReturned(int expectedNumberOfBoards)
        {
            JArray jArray = JArray.Parse(_Response.Content);
            jArray.Count.Should().Be(expectedNumberOfBoards);
        }

    }
}