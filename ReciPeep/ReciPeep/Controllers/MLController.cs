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

namespace ReciPeep.Controllers
{
    [Route("MLController")]
    public class MLController : Controller
    {
        [HttpGet("imagerecognition/{blobImage}")]
        public async Task<IActionResult> RecogniseImage(string blobString)
        {
            //remove below line when front end is set up
            blobString = "";
            //convert to blob
            byte[] blob = Convert.FromBase64String(blobString);
            //set up array of ingredients to be recieved
            JObject ingredients = new JObject();//delete contents when ready
            ingredients.Add("Ingredient 1", "Eggs");

            //send to AI
            System.IO.File.WriteAllBytes("Models\\pic.jpg", blob);
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
