<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>Nuevo Registro</title>
</head>
<body>
<div class="container mt-5">
    <div class="row">
        <div class="col-5 mx-auto">
            <div class="form-Registro">
                <p class="title">Nuevo Usuario</p>

                <form class="form" action="InsertarDatos" method="post">                    
                    
                <input type="text" id="nombre" name="nombre" class="input" placeholder="Nombre "required>
                <input type="text" id="apellido" name="apellido" class="input" placeholder="Apellidos" required>                    
                <input type="text" id="dni" name="dni" class="input" placeholder="DNI" required>                    
                <input type="text" id="telefono" name="telefono" class="input" placeholder="Telefono" required>                   
                <input type="text" id="email" name="email" class="input" placeholder="Email" required>                    
                <input type="text" id="clave" name="clave" class="input" placeholder="Clave" required>                    
                <input type="password" id="contraseña" name="contraseña" class="input" placeholder="Contraseña">
                <input type="password" id="confContraseña" name="confContraseña" class="input" placeholder="Confirmar Contraseña">
                <button type="submit" class="form-btn">Registrarse</button>                 
                </form>
        </div>
        
    </div>
<!-- <div class="row">
        <div class="col-1">
            <div class="buttons-container">
                <div class="apple-login-button">                        
                    <a href="index.html"><span>Back</span></a>   
                </div>
        </div>
    </div>  -->
    
    
</div>
</body>
</html>