#pragma checksum "C:\Users\jamie\Source\Repos\ReciPeep\ReciPeep\ReciPeep\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c8aad3a8b6a0e67eccb54a76e1217fa030cf53d2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\jamie\Source\Repos\ReciPeep\ReciPeep\ReciPeep\Views\_ViewImports.cshtml"
using ReciPeep;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\jamie\Source\Repos\ReciPeep\ReciPeep\ReciPeep\Views\_ViewImports.cshtml"
using ReciPeep.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c8aad3a8b6a0e67eccb54a76e1217fa030cf53d2", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5730bcd59f6cd02ac2f40719541b95d425e2233b", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\jamie\Source\Repos\ReciPeep\ReciPeep\ReciPeep\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<script src=\"https://cdn.jsdelivr.net/npm/vue@2.6.12/dist/vue.js\"></script>\r\n\r\n");
            WriteLiteral("<div id=\"index\">\r\n");
            WriteLiteral(@"    <div v-if=""!submitted"" class=""container bg-light rounded"">

        <div class=""text-center"">
            <h3 class=""display-3"">Welcome</h3>
            <p>This website allows you to tell us what ingredients you have in your house and we'll find you some really good reciepes you can make with those ingredients.</p>
            <p>Just input the ingredients below or send us a picture of the products (with the camera icon) and we'll try and give you some amazing meals</p>
        </div>

");
            WriteLiteral(@"        <div class=""d-flex flex-row-reverse pt-3 mb-3 sticky-top"">
            <button class=""btn btn-secondary ml-1"" v-on:click=""addIngredient"">Add Ingredient</button>
            <button class=""btn btn-danger ml-1"">
                <svg xmlns=""http://www.w3.org/2000/svg"" width=""16"" height=""16"" fill=""currentColor"" class=""bi bi-camera-fill"" viewBox=""0 0 16 16"">
                    <path d=""M10.5 8.5a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0z"" />
                    <path d=""M2 4a2 2 0 0 0-2 2v6a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V6a2 2 0 0 0-2-2h-1.172a2 2 0 0 1-1.414-.586l-.828-.828A2 2 0 0 0 9.172 2H6.828a2 2 0 0 0-1.414.586l-.828.828A2 2 0 0 1 3.172 4H2zm.5 2a.5.5 0 1 1 0-1 .5.5 0 0 1 0 1zm9 2.5a3.5 3.5 0 1 1-7 0 3.5 3.5 0 0 1 7 0z"" />
                </svg>
            </button>
        </div>

        <div class=""input-group pb-3"" v-for=""ingredient in ingredients"">
            <input v-model=""ingredient.ingredient"" v-on:keyup.enter=""addIngredient"" type=""text"" class=""form-control"" placeholder=""Add an ingr");
            WriteLiteral("edient\">\r\n        </div>\r\n\r\n        <div class=\"row\">\r\n            <button class=\"btn btn-success flex-fill ml-3 mr-3 mb-3\" v-on:click=\"pushIngredients\">Get Recipes</button>\r\n        </div>\r\n    </div>\r\n\r\n");
            WriteLiteral(@"    <div class=""container"" v-if=""submitted"" v-cloak>

        <div class=""d-flex flex-row-reverse pt-3 mb-3 sticky-top"">
            <button class=""btn btn-secondary ml-1"" v-on:click=""changeIngredients"">Add Ingredient</button>
            <button class=""btn btn-danger ml-1"">
                <svg xmlns=""http://www.w3.org/2000/svg"" width=""16"" height=""16"" fill=""currentColor"" class=""bi bi-camera-fill"" viewBox=""0 0 16 16"">
                    <path d=""M10.5 8.5a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0z"" />
                    <path d=""M2 4a2 2 0 0 0-2 2v6a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V6a2 2 0 0 0-2-2h-1.172a2 2 0 0 1-1.414-.586l-.828-.828A2 2 0 0 0 9.172 2H6.828a2 2 0 0 0-1.414.586l-.828.828A2 2 0 0 1 3.172 4H2zm.5 2a.5.5 0 1 1 0-1 .5.5 0 0 1 0 1zm9 2.5a3.5 3.5 0 1 1-7 0 3.5 3.5 0 0 1 7 0z"" />
                </svg>
            </button>
        </div>

        <div class=""row card-group mt-3"">
            <div class=""col-md-4"" v-for=""recipe in recipes"">
                <div class=""card mb-3"">
         ");
            WriteLiteral(@"           <img :src=""recipe.image"" class=""card-img-top"">
                    <div class=""card-body"">
                        <h5 class=""card-title"">{{recipe.name}}</h5>
                        <h6 class=""card-subtitle mb-2 text-muted"">Used Ingredients</h6>
                        <ul>
                            <li v-for=""used in recipe.used"">{{ used }}</li>
                        </ul>
                        <h6 class=""card-subtitle mb-2 text-muted"">Missing Ingredients</h6>
                        <ul>
                            <li v-for=""missing in recipe.missing"">{{ missing }}</li>
                        </ul>
                        <div class=""row"">
                            <a :href=""recipe.link"" class=""btn btn-primary flex-fill ml-3 mr-3"">Open Method</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</div>



");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
