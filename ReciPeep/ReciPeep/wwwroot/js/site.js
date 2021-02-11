// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



let vm = new Vue({
    el: '#index',
    data: function () {
        return {
            submitted: false,
            show: true,
            ingredients: [{ ingredient: '' }],
            stringifyIngredients: '',
            recipes: [{
                name: "cheese bites",
                image: "https://www.telegraph.co.uk/content/dam/music/2018/06/08/Kanye-Ye-album-cover_trans_NvBQzQNjv4BqSZCfQn3UNBPwFTCNOaG4Id2-jbwZxVZZoXJ1WwZY6Xk.jpg",
                used: ["eggs", "cheese"],
                missing: ["bacon", "mushrooms", "tagliatelle"],
                link: "https://news.sky.com/"
            },
            {
                name: "cheese bites",
                image: "https://www.telegraph.co.uk/content/dam/music/2018/06/08/Kanye-Ye-album-cover_trans_NvBQzQNjv4BqSZCfQn3UNBPwFTCNOaG4Id2-jbwZxVZZoXJ1WwZY6Xk.jpg",
                used: ["eggs", "cheese"],
                missing: ["bacon", "mushrooms", "tagliatelle"],
                link: "https://news.sky.com/"
            },
            {
                name: "cheese bites",
                image: "https://www.telegraph.co.uk/content/dam/music/2018/06/08/Kanye-Ye-album-cover_trans_NvBQzQNjv4BqSZCfQn3UNBPwFTCNOaG4Id2-jbwZxVZZoXJ1WwZY6Xk.jpg",
                used: ["eggs", "cheese"],
                missing: ["bacon", "mushrooms", "tagliatelle"],
                link: "https://news.sky.com/"
            }
            ,
            {
                name: "cheese bites",
                image: "https://www.telegraph.co.uk/content/dam/music/2018/06/08/Kanye-Ye-album-cover_trans_NvBQzQNjv4BqSZCfQn3UNBPwFTCNOaG4Id2-jbwZxVZZoXJ1WwZY6Xk.jpg",
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

        changeIngredients() {
            this.submitted = false;
        },

        pushIngredients: function () {
            this.stringifyIngredients = '';

            for (var i = 0; i < this.ingredients.length; i++) {
                this.stringifyIngredients += String(this.ingredients[i].ingredient) + ',';
            }

            //fetch(`${window.location.origin}/spoonacular/getrecipies/${this.stringifyIngredients}`)
            //    .then(({ data }) => { this.recipes = data; })
            //    .catch(() => { alert("fail"); });

            this.submitted = true;

        }
    }
});




