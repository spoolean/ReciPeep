// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

Vue.component('recipe-card', {
    props: ['recipe'],
    template: '<div class="card" style="width: 18rem;"> \
                    <img src="this.recipe.img"class= "card-img-top" > \
                    <div class="card-body"> \
                        <h5 class="card-title">{{this.recipe.name}}</h5> \
                        <h2>Used Ingredients</h2> \
                        <p v-for="used in this.recipe.used">{{ used }}</p> \
                        <h2>Missing Ingredients</h2> \
                        <p v-for="missing in this.recipe.missing">{{ missing }}</p> \
                        <button href="this.recipe.link" class="btn btn-primary">Open Method</button> \
                    </div>\
               </div>'


})

let vm = new Vue({
    el: '#index',
    data: function () {
        return {
            submitted: false,
            ingredients: [{ ingredient: '' }],
            stringifyIngredients: '',
            recipes: [{
                name: "cheese bites",
                img: "https://www.telegraph.co.uk/content/dam/music/2018/06/08/Kanye-Ye-album-cover_trans_NvBQzQNjv4BqSZCfQn3UNBPwFTCNOaG4Id2-jbwZxVZZoXJ1WwZY6Xk.jpg",
                used: ["eggs", "cheese"],
                missing: ["bacon", "mushrooms", "tagliatelle"],
                link: "https://news.sky.com/"
            }]
        }
    },
    methods: {
        addIngredient: function () {
            this.ingredients.push({ ingredient: '' });
        },

        pushIngredients: function () {
            this.stringifyIngredients = '';

            for (var i = 0; i < this.ingredients.length; i++) {
                this.stringifyIngredients += String(this.ingredients[i].ingredient) + ',';
            }

            fetch(`${window.location.origin}/spoonacular/getrecipies/${this.stringifyIngredients}`)
                .then(({ data }) => { this.recipes = data; })
                .catch(() => { alert("fail"); });

            this.submitted = true;

        }
    }
});




