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
            ingredients: [{ ingredient: "" }],
            stringifyIngredients: '',
            recipes: [],
            toasts: [],
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
            return (input !== 'undefined' || input != null || input.length > 0) ? true : false;
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
                    throw new Error("We have encountered an error, this may be because we are over the quota");
                }
                return response.json();
            }).then(data => {
                this.recipes = data;
                this.loading = false;
            }).catch(error => {
                alert(error);
                this.loading = false;
            });

            this.submitted = true;
        },

        getURL(id) {
            fetch(`${window.location.origin}/spoonacular/geturl/${id}`
            ).then(response => {
                if (!response.ok) {
                    throw new Error("We have encountered an error, this url might not exist");
                }
                return response.text();
            }).then(data => {
                window.open(data);
            }).catch(error => { alert(error); });
        },

        imageRecognition() {
            let file = document.getElementById('imageUpload').files[0];

            const fileReader = new FileReader();
            fileReader.readAsDataURL(file);
            fileReader.onload = () => {
                let fileString = fileReader.result;

                fetch(`${window.location.origin}/mlcontroller/imagerecognition`, {
                    method: 'POST',
                    body: JSON.stringify({ filename: file.name, image: fileString }),
                    headers: { 'Content-Type': 'application/json' }
                }).then(response => {
                    if (!response.ok) {
                        throw new Error("Sorry, we can't read that image, try again");
                    }
                    return response.json();
                }).then(data => {
                    console.log(data);
                    this.ingredients.push(data);
                    this.ingredients.push({ ingredient: "" });
                }).catch(error => { console.log(error); });
            }
        },
    }
});