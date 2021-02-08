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

        private string apiKey = "19276f6da3644e54981cc0d2dea5c426";

        [HttpGet("FeelingLucky")]
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
                Console.WriteLine("loaded random recipe");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return Ok(responseBody);
        }


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
                Console.WriteLine("loaded response");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return Ok(CutObject(JArray.Parse(responseBody)).ToString());
        }

        private JObject CutObject(JArray recipie){
            JObject outputRecipie = new JObject
            {
                { "title", recipie[0]["title"] },
                { "image", recipie[0]["image"] },
                { "missedIngredients", recipie[0]["missedIngredients"] },
                { "usedIngredients", recipie[0]["usedIngredients"] }
            };
            return outputRecipie;
        }

        
    }
}
