// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

new Vue({
    el: '#index',
    data: function data() {
        return {
            ingredients: [{ ingredient: '' }],
            stringifyIngredients: '',
            recipes: []
        };
    },
    methods: {
        addIngredient: function addIngredient() {
            this.ingredients.push({ ingredient: '' });
        },

        pushIngredients: function pushIngredients() {
            this.stringifyIngredients = '';
            for (var i = 0; i < this.ingredients.length; i++) {
                this.stringifyIngredients += String(this.ingredients[i].ingredient) + ',';
            }

            alert(this.stringifyIngredients);
        }
    }
});