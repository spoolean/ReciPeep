using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReciPeep.Controllers
{
    [Route("MLController")]
    public class MLController : Controller
    {
        [HttpGet("imagerecognition/{blobImage}")]
        public async Task<string[]> RecogniseImage(string blobString)
        {
            //remove below line when front end is set up
            blobString = ""; 
            //convert to blob
            byte[] blob = Convert.FromBase64String(blobString);
            //set up array of ingredients to be recieved
            String[] ingredients = { "Eggs", "Water" };//delete contents when ready

            //send to AI
            System.IO.File.WriteAllBytes(@"C:\Users\isaac\Desktop\pic.jpg", blob);

            //receive list of ingredients from AI
            
            return ingredients;
        }
    }
}
