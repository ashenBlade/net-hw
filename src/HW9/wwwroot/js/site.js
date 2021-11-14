// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let btn = document.getElementById("calc-btn");
btn.onclick(function (e) {
    let expression = document.getElementById("expression").value;
    if (!expression)
        return;
    let response = await fetch(`https://localhost:5001/Calculate`,
        {
            method: "post",
            headers: {
                'Content-Type': 'text/plain'
            },
            body: expression
        });

});