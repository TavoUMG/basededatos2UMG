// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function ValidaSoloNumerosPunto(e) {
    var charCode = (e.which) ? e.which : e.keyCode
    if ((charCode < 46) || (charCode > 57) && (charCode = 47))
        e.returnValue = false;
}

function ValidaSoloNumeros(e) {
    var charCode = (e.which) ? e.which : e.keyCode
    if ((charCode < 47) || (charCode > 57) && (charCode = 47))
        e.returnValue = false;
}