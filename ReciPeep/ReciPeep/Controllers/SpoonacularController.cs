using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;


namespace ReciPeep.Controllers
{
    [Route("Spoonacular")]
    public class SpoonacularController : Controller
    {
        //public JArray recipeStoreArray = new JArray();
        //public JObject recipeStore = new JObject();

        private string apiKey = "19276f6da3644e54981cc0d2dea5c426";

        /*[HttpPost("GetRecipies")]
        public async Task<IActionResult> IngredientSearch()
        {
            HttpClient client = new HttpClient();
            string responseBody = @"{}";
            string url = "https://api.spoonacular.com/recipes/random?apiKey=" + apiKey + "&number=1";

            // Call asynchronous network methods in a try/catch block to handle exceptions.         
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();

                JObject recipeTemp = JObject.Parse(responseBody);
                //recipeStoreArray.Add(recipeTemp["recipes"][0]);
                Console.WriteLine("loaded random recipe");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return responseBody;
        }*/






        [HttpGet("GetRecipies/{ingredientsString}")]
        public async Task<IActionResult> IngredientSearch(string ingredientsString)
        {
            string[] ingredients = ingredientsString.Split(",");

            HttpClient client = new HttpClient();
            string responseBody = @"{}";
            string url = "https://api.spoonacular.com/recipes/findByIngredients?apiKey=" + apiKey + "&ingredients=";

            //add ingredients to the search
            for (int i = 0; i < ingredients.Length; i++)
            {
                url += ingredients[i];
                if (i < ingredients.Length - 1)
                {
                    url += ",+";
                }
            }
            url += "&number=2";

            // Call asynchronous network methods in a try/catch block to handle exceptions.         
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();

                //recipeStoreArray = JArray.Parse(responseBody);
                Console.WriteLine("loaded response");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return Ok(responseBody);
        }

        /*[HttpPost("ToString")]
        public override string ToString()
        {
            string beautifyString = "";
            if (recipeStoreArray.Count > 0)
            {

                beautifyString += "title: " + recipeStoreArray[0]["title"] + "\n";
                beautifyString += "image: " + recipeStoreArray[0]["image"] + "\n";

                int numIngredients = 0;

                //add ingredients you own           
                if (!(recipeStoreArray[0]["usedIngredientCount"] == null))
                {
                    beautifyString += "Owned ingredients : " + "\n";
                    numIngredients = recipeStoreArray[0]["usedIngredientCount"].ToObject<int>();
                    for (int i = 0; i < numIngredients; i++)
                    {
                        beautifyString += "-" + recipeStoreArray[0]["usedIngredients"][i]["name"] + "\n";
                    }
                }

                //add ingredients you don't own            
                if (!(recipeStoreArray[0]["missedIngredientCount"] == null))
                {
                    beautifyString += "Unowned ingredients : " + "\n";
                    numIngredients = recipeStoreArray[0]["missedIngredientCount"].ToObject<int>();
                    for (int i = 0; i < numIngredients; i++)
                    {
                        beautifyString += "-" + recipeStoreArray[0]["missedIngredients"][i]["name"] + "\n";
                    }
                }

                //handle random
                if (!(recipeStoreArray[0]["extendedIngredients"] == null))
                {
                    JArray ingredients = (JArray)recipeStoreArray[0]["extendedIngredients"];
                    numIngredients = ingredients.Count;
                    for (int i = 0; i < numIngredients; i++)
                    {
                        beautifyString += "-" + recipeStoreArray[0]["extendedIngredients"][i]["name"] + "\n";
                    }
                }

                return beautifyString;
            }
            else
            {
                return "No recipies have been found";
            }
        }*/
    }
}
