// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

Vue.component('ingredient',{
    template: '\
        <div class="input-group pb-3">\
            <input type="text" class="form-control" placeholder="Add an ingredient">\
        </div>'
})

new Vue({
    el: '#app',
    data: {
        count: 2,
        ingredients: []
    },
    methods: {
        addIngredient: function () {
            this.count += 1;
        }
    }
})
