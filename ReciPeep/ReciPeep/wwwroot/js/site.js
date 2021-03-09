// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let vm = new Vue({
    el: '#index',
    data: () => {
        return {
            loading: false,
            imfeelinglucky: false,
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
        addIngredient: function (index) {
            this.ingredients.push({ ingredient: "" });

            this.$nextTick(() => {
                let nextElement = this.$refs.ingredientsElements[index + 1];

                nextElement.focus();
            })
        },

        isNotNullOrWhitespace(input) {
            if (input !== 'undefined' || input != null || input.length > 0) { return true; }
            else { return false; }
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

        imFeelingLucky() {
            this.loading = true;
            this.imfeelinglucky = true;

            this.recipes = [];

            fetch(`${window.location.origin}/spoonacular/feelinglucky`
            ).then(response => {
                if (!response.ok) {
                    throw new Error("This dont work");
                }
                return response.json();
            }).then(data => {
                this.recipes = data;
                this.loading = false;
            }).catch(error => { alert(error); });

            this.submitted = true;
        },

        pushIngredients: function () {
            this.loading = true;
            this.imfeelinglucky = false;

            this.recipes = [];
            this.stringifyIngredients = '';

            for (var i = 0; i < this.ingredients.length; i++) {
                this.stringifyIngredients += String(this.ingredients[i].ingredient) + ',';
            }
            this.stringifyIngredients = this.stringifyIngredients.substring(0, this.stringifyIngredients.length - 1);

            fetch(`${window.location.origin}/spoonacular/getrecipes/${this.stringifyIngredients}`
            ).then(response => {
                if (!response.ok) {
                    throw new Error("We have encountered an error, this may be because we ar eover the quota");
                }
                return response.json();
            }).then(data => {
                this.recipes = data;
                this.loading = false;
            }).catch(error => { alert(error); });

            this.submitted = true;
        },

        getURL(id) {
            fetch(`${window.origin.location}/geturl/${id}`
            ).then(response => {
                if (!response.ok) {
                    throw new Error("We have encountered an error, this url might not exist");
                }
                return response.json();
            }).then(data => {
                window.open(data);
            }).catch(error => { alert(error); });
        },

        imageRecognition() {
            alert(document.getElementById("imageUpload").innerHTML);
        },
    }
});