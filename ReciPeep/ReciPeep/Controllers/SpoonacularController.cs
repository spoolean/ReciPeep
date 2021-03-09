using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Collections.Generic;

namespace ReciPeep.Controllers
{
    [Route("Spoonacular")]
    public class SpoonacularController : Controller
    {
        //Variable storing the key to the spoonacular account. This should be removed before making it public
        private string apiKey = "19276f6da3644e54981cc0d2dea5c426";
        private Boolean quotaReached = false;  

        //Function for getting a random recipe
        [HttpGet("FeelingLucky")]
        public async Task<IActionResult> IngredientSearch()
        {
            //variables for storing the url to call as well as the string to store the result
            HttpClient client = new HttpClient();
            string responseBody = @"{}";
            string url = "https://api.spoonacular.com/recipes/random?apiKey=" + apiKey + "&number=10";

            if (!quotaReached)
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions.         
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();

                    JObject recipeTemp = JObject.Parse(responseBody);
                    Console.WriteLine("loaded random recipe");

                    var headers = response.Headers;
                    IEnumerable<string> values;
                    if (headers.TryGetValues("X-API-Quota-Used", out values))
                    {
                        string quotaUsed = values.First();
                        CheckQuota(quotaUsed);
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }

            //convert the data to desired json types
            JObject ResponseObject = JObject.Parse(responseBody);
            JArray responseData = JArray.Parse(ResponseObject["recipes"].ToString());
            JArray skinnyRecipes = new JArray();

            //loop through the list of recipes for extra info
            for (int i = 0; i < responseData.Count; i++)
            {
                //create add to an array a cut down version of the recipe based on both sets of data
                skinnyRecipes.Add(CutObject(responseData[i])); //, JObject.Parse(responseBody)

            }
            return Ok(skinnyRecipes.ToString());
        }

        //Perform a recipe search with a given string of ingredients
        [HttpGet("GetRecipes/{ingredientsString}")]
        public async Task<IActionResult> IngredientSearch(string ingredientsString)
        {
            //split up the ingredients string into a list
            string[] ingredients = ingredientsString.Split(",");
            //store the number of wanted recipes 
            int numRecipes = 21;


            //variables for storing the url to call as well as the string to store the result
            HttpClient client = new HttpClient();
            string responseBody = @"{}";
            string url = "https://api.spoonacular.com/recipes/findByIngredients?apiKey=" + apiKey + "&ingredients=";

            //add ingredients to the search url by looping through the list of ingredients
            for (int i = 0; i < ingredients.Length; i++)
            {
                url += ingredients[i];
                if (i < ingredients.Length - 1)
                {
                    url += ",+";
                }
            }
            url += "&number=" + numRecipes.ToString();

            if (!quotaReached)
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions.         
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("loaded list of recipes");
                    
                    var headers = response.Headers;                   
                    IEnumerable<string> values;
                    if (headers.TryGetValues("X-API-Quota-Used", out values))
                    {
                        string quotaUsed = values.First();
                        CheckQuota(quotaUsed);
                    }                   
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }            

            JArray responseData = JArray.Parse(responseBody);
            JArray skinnyRecipes = new JArray();

            for (int i = 0; i < responseData.Count; i++) 
            {
                skinnyRecipes.Add(CutObject(responseData[i]));
            }
            
            JArray sortedSkinnyRecipes = new JArray(skinnyRecipes.OrderBy(obj => (string)obj["missedIngredientCount"]));

            return Ok(sortedSkinnyRecipes.ToString());
        }

        [HttpGet("GetUrl/{recipeID}")]
        public async Task<string> GetUrlFromID(string recipeID)
        {
            string sourceURL = "";

            //variables for storing the url to call as well as the string to store the result
            HttpClient client = new HttpClient();
            string responseBody = @"{}";
            if (!quotaReached)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(GetInfoURL(recipeID));
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("loaded extra info for recipe " + recipeID);

                    var headers = response.Headers;
                    IEnumerable<string> values;
                    if (headers.TryGetValues("X-API-Quota-Used", out values))
                    {
                        string quotaUsed = values.First();
                        CheckQuota(quotaUsed);
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }

            JObject responseData = JObject.Parse(responseBody);
            sourceURL = responseData["sourceUrl"].ToString();

            return sourceURL;
        }

        //create a cutdown version of the recipe 
        private JObject CutObject(JToken recipe)
        {
            //create an object with an ID, title, image and url
            JObject outputRecipe = new JObject
            {
                { "id", recipe["id"] },
                { "title", recipe["title"] },
                { "image", recipe["image"] },
                //{ "sourceUrl", extraInfo["sourceUrl"] },
            };
            //if it's from an ingredients search
            if(recipe["missedIngredientCount"] != null)
            {
                //add the specific attributes
                outputRecipe.Add("missedIngredientCount", recipe["missedIngredientCount"]);
                outputRecipe.Add("missedIngredients", recipe["missedIngredients"]);
                outputRecipe.Add("usedIngredients", recipe["usedIngredients"]);
            }
            //if it's from I'm feeling lucky
            if(recipe["extendedIngredients"] != null)
            {
                //just add the full list of ingredients
                outputRecipe.Add("ingredients", recipe["extendedIngredients"]);
            }
            
            return outputRecipe;
        }

        //create the url for getting more info from a recipe ID
        private string GetInfoURL(string recipeID)
        {
            string searchURL = "https://api.spoonacular.com/recipes/"+ recipeID +"/information?apiKey=" + apiKey + "&includeNutrition=false";
            return searchURL;
            
        }

        private void CheckQuota(string pointsUsed)
        {
            decimal pointsInt = Convert.ToDecimal(pointsUsed);
            if (pointsInt >= 145)
            {
                quotaReached = true;
                Console.WriteLine("Quota reached");
            }
            else
            {
                Console.WriteLine((150 - pointsInt).ToString()+" Points remaining");
            }
        }

        
    }
}
