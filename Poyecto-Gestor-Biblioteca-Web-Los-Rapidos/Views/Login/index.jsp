<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<link
	href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/css/bootstrap.min.css"
	rel="stylesheet">
<script
	src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/js/bootstrap.bundle.min.js"></script>
<link rel="stylesheet" href="estilo.css">
<title>Welcome</title>

</head>
<body>

	<div id="content">

		<div class="container mt-5">

			<div class="row">
				<div class="col-5 mx-auto">
					<div class="form-container">

						<p class="title">Bienvenido</p>

						<form class="form">
							<input type="email" class="input" placeholder="Email"> 
							<input type="password" class="input" placeholder="Contraseña">

							<button class="col-12 form-btn">Acceder</button>
						</form>					

						<button class="col-12 form-btn"
							onclick="cargarContenido('RegistrarUsuario.jsp')">Registrarse</button>

						<div class="buttons-container">
							<div class="apple-login-button" onclick="cargarContenido('olvidadoContraseñ.jsp')">
								Recuperar contraseña
							</div>

						</div>
					</div>
				</div>

				<script src="tu_script.js"></script>
</body>
</html>