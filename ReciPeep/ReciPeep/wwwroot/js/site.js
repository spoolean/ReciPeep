// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Vue.component('ingredient',{

//    props: ['ingredient'],
//    data: function () {
//        return {
            
//        }
//    },
//    template: '\
//        <div class="input-group pb-3">\
//            <input type="text" class="form-control" placeholder="Add an ingredient">\
//        </div>'

//})

new Vue({
    el: '#index',
    data: function() {
        return {
            count: 0,
            ingredients: [{ ingredient: '' }]
        }
    },
    methods: {
        addIngredient: function () {
            this.count += 1;
            this.ingredients.push({ ingredient: '' });
        },

        pushIngredients: function (data) {
            Http.Post(`${window.location.origin}/whatevercontollerthishappenstobe`, data)
                .then(data => console.log(data))
                .catch(err => conso.log(err));
        }
    }
})


class Http {
    async Get(url) {
        var response = await fetch(url);

        var data = await response.json();

        return data;
    }

    async Post(url, data) {
        var response = fetch(url, {
            method: 'POST',
            headers: {
                'Content-type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        var result = await response.json();

        return result;
    }
}

