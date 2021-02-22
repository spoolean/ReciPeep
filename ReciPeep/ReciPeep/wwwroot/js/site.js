﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function enterKeyPressed(event) {
    if (event.keyCode == 13) {
        console.log("Enter key is pressed");
        return true;
    } else {
        return false;
    }
}

let vm = new Vue({
    el: '#index',
    data: function () {
        return {
            submitted: false,
            vegetarian: false,
            vegan: false,
            show: true,
            ingredients: [{ingredient:""}],
            stringifyIngredients: '',
            recipes: []
        }
    },
    methods: {
        addIngredient: function () {
            this.ingredients.push({ ingredient: "" });            
        },

        changeIngredients() {
            this.submitted = false;
        },

        setVegetarian() {
            let temp = !this.vegetarian
            this.vegetarian = temp;
        },

        setVegan() {
            let temp = !this.vegan
            this.vegan = temp;
        },

        pushIngredients: function () {
            this.recipes = [];
            this.stringifyIngredients = '';

            for (var i = 0; i < this.ingredients.length; i++) {
                this.stringifyIngredients += String(this.ingredients[i].ingredient) + ',';
            }
            this.stringifyIngredients = this.stringifyIngredients.substring(0, this.stringifyIngredients.length - 1);

            fetch(`${window.location.origin}/spoonacular/getrecipes/${this.stringifyIngredients}`
            ).then(response => {
                if (!response.ok) {
                    throw new Error("This dont work");
                }
                return response.json();
            }).then(data => {
                this.recipes = data;
            }).catch(error => { alert(error); });

            this.submitted = true;
        }
    }
});