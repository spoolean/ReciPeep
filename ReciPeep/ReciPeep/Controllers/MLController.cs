using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReciPeep.Controllers
{
    public class MLController : Controller
    {
        [HttpGet("imagerecognition/{blobImage}")]
        public async Task<string[]> RecogniseImage(string blobImage)
        {

            String[] ingredients = ["Eggs","Water"];
            return ingredients;
        }
    }
}
