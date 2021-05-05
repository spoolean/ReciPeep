using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ReciPeep.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ReciPeep.Controllers
{
    [Route("MLController")]
    public class MLController : Controller
    {
        [HttpPost]
        [Route("imagerecognition")]
        public async Task<IActionResult> RecogniseImage([FromBody] ImagePayloadModel imageModel)
        {
            string image = imageModel.Image.Substring(imageModel.Image.IndexOf(',') + 1); // removes the intital headers that defines the image 64
            Console.WriteLine(image);
            //convert to blob
            var imageDataByteArray = Convert.FromBase64String(image);
            //set up array of ingredients to be recieved

            JObject ingredients = new JObject();//delete contents when ready
            //ingredients.Add("ingredient", "Eggs");

            //creates the images file as jpg
            System.IO.File.WriteAllBytes($"Models\\{imageModel.FileName}", imageDataByteArray);

            //send to AI
            Process runPythonScrypt = new Process
            {
                StartInfo = new ProcessStartInfo("python.exe", $"Models\\predictionscript.py Models\\{imageModel.FileName}")
                {
                    RedirectStandardOutput = true
                }
            };

            string ingredientsString = "";
            try
            {
                runPythonScrypt.Start();
                ingredientsString = runPythonScrypt.StandardOutput.ReadToEnd();
                runPythonScrypt.WaitForExit();
                Console.WriteLine(ingredientsString);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return Ok(ingredientsString.ToString());
        }
    }
}