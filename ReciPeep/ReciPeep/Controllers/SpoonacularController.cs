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


        [HttpGet("GetRecipes/{ingredientsString}")]
        public async Task<IActionResult> IngredientSearch(string ingredientsString)
        {
            string[] ingredients = ingredientsString.Split(",");
            int numRecipes = 10;

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
            url += "&number=" + numRecipes.ToString();

            // Call asynchronous network methods in a try/catch block to handle exceptions.         
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("loaded list of recipes");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            JArray responseData = JArray.Parse(responseBody);
            JArray skinnyRecipes = new JArray();

            for (int i = 0; i < responseData.Count; i++) 
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(GetInfoURL(responseData[i]["id"].ToString()));
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("loaded extra info for recipe "+(i+1).ToString());
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
                skinnyRecipes.Add(CutObject(responseData[i],JObject.Parse(responseBody)));

            }
            
            JArray sortedSkinnyRecipes = new JArray(skinnyRecipes.OrderBy(obj => (string)obj["missedIngredientCount"]));

            return Ok(sortedSkinnyRecipes.ToString());
        }

        private JObject CutObject(JToken recipe,JObject extraInfo)
        {
            JObject outputRecipe = new JObject
            {
                { "id", recipe["id"] },
                { "title", recipe["title"] },
                { "image", recipe["image"] },
                { "sourceUrl", extraInfo["sourceUrl"] },
                { "missedIngredientCount", recipe["missedIngredientCount"] },
                { "missedIngredients", recipe["missedIngredients"] },
                { "usedIngredients", recipe["usedIngredients"] }
            };
            return outputRecipe;
        }

        private string GetInfoURL(string recipeID)
        {
            string searchURL = "https://api.spoonacular.com/recipes/"+ recipeID +"/information?apiKey=" + apiKey + "&includeNutrition=false";
            return searchURL;
            
        }

        
    }
}
