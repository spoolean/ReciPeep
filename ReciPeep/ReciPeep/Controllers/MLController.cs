using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using ReciPeep.Models;

namespace ReciPeep.Controllers
{
    [Route("MLController")]
    public class MLController : Controller
    {
        [HttpPost]
        [Route("imagerecognition")]
        public async Task<IActionResult> RecogniseImage([FromBody] ImagePayloadModel imageModel)
        {
            string image = imageModel.Image.Substring(imageModel.Image.IndexOf(',')+1); // removes the intital headers that defines the image 64
            Console.WriteLine(image);
            //convert to blob
            var imageDataByteArray = Convert.FromBase64String(image);
            //set up array of ingredients to be recieved

            JObject ingredients = new JObject();//delete contents when ready
            ingredients.Add("ingredient", "Eggs");

            //send to AI
            //creates the images file as jpg
            System.IO.File.WriteAllBytes("Models\\pic.jpg", imageDataByteArray);
            Process runPythonScrypt = new Process
            {
                StartInfo = new ProcessStartInfo("python.exe", "Models\\TestPythonScrypt.py Models\\pic.jpg")
                {
                    RedirectStandardOutput = true
                }
            };

            try
            {
                runPythonScrypt.Start();
                string ingredientsString = runPythonScrypt.StandardOutput.ReadToEnd();
                runPythonScrypt.WaitForExit();
                Console.WriteLine(ingredientsString);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }


            return Ok(ingredients.ToString());
        }
    }
}
