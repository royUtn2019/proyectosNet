Para clonar un proyecto de GIT

git clone https://github.com/usuarioGit/nombreProyecto



Para agregar algo al git
te ubicas en el directorio donde esta la carpeta (lo que hacemos siempre). Y debes consultar el estado del repositorio usando git status.

Luego tecleas el comando git add (nombre de archivo o carpeta/directorio)

Luego tecleas el comando git commit -m “aqui el msj”

--//Luego tecleas la ruta del repositorio (https://github.com/xxx/xxx.git)

Luego tecleas git remote -u origin master

Luego tecleas git push


Git: borrar archivos/carpetas del repositorio

1.A Si quieres eliminar un archivo:

    git rm miarchivo.php
	
1.B. Si quieres eliminar una carpeta:

    git rm -r micarpeta

2. Creamos el commit

    git commit -m "elimino archivos innecesarios"

3. Subimos los cambios al repositorio

    git push


comitear cambios al git

Te ubicas en la carpeta donde esta el cambio con CMD y verificas el estado de Git
    git status

    git add nombreArchi.

    git commit -m "Comentario del cambio"

    git remote -u origin master ---( para ver el comentario en el master)

    git push
	