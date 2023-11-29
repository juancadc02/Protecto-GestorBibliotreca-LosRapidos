// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function revisarContraseña() {
    var contraseña = document.getElementById('contrasenya').value;
    var confContraseña = document.getElementById('confContrasenya').value;
    var mensajeContraseña = document.getElementById('mensajeContrasenya');

    if (contraseña === confContraseña) {
        mensajeContraseña.textContent = 'Las contraseñas coinciden';
        mensajeContraseña.style.color = 'green';
        document.getElementById("btnRegistro").disabled = false;//habilita el boton
    } else {
        mensajeContraseña.textContent = 'Las contraseñas no coinciden';
        mensajeContraseña.style.color = 'red';
        document.getElementById("btnRegistro").disabled = true;//deshabilita el boton
    }
}