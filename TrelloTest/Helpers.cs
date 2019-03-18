using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloTest
{
    public class Helpers
    {
        public static string GetValueFromResponseContent(string fieldName, string responseContent)
        {
            JObject jObject = JObject.Parse(responseContent);
            string value = jObject[fieldName].Value<string>();
            return value;
        }
    }
}
