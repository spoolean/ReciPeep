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
            recipes: []
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

            fetch(`${window.location.origin}/spoonacular/getrecipes/${this.stringifyIngredients}`)
                .then(({ data }) => { this.recipes = data; })
                .catch(() => { alert("fail"); });

            this.submitted = true;

        }
    }
});




