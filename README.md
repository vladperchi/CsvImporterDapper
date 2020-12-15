
# CsvImporter ASP.NET Core API
 
Elementos a tener en cuenta:

Arquitectura orientada al domino segura y escalable utilizando Net Core.

Herramientas como:

# 1.	Micro ORM Dapper
# 2.	AutoMapper
# 3.	JSON
# 5.	Postman
# 6.	Open API
# 7.	Swagger

Entre otras se utilizaron para garantizar un buen desarrollo y poder cumplir con los requerimientos.

Entrando en materia vamos a la API ==> appsettings.json, modificamos la cadena de conexión y compilamos. Establecemos como proyecto principal la API e iniciamos el proyecto consola como una nueva instancia. De esta manera entrariamos de alleno al proceso en cuestion. Otra manera seria iniciado las API sin depurar desde el Panel Vinculo del Explorados y podrán observar un poco de swagger, de cierta manera, veriemos una corta documentación y algo de testing.
 
El tema principal dado los requerimientos, aparentemente no tenía mayor complicación, pero si tenía un nivel de complejidad por lo cual tome una serie de decisiones en aras de lograr el objetivo o reto impuesto.

Inicialmente se tomó la decisión de crear un servicio, garantizando   de esta manera, sostenibilidad, mantenimiento, usabilidad y ciclo de vida
En un inicio fue un tanto demorado, se refactorizo una y otro vez implementando un bucle Parallel.ForEach para habilitar el paralelismo de datos sobre cualquier fuente de la mano con Range pero no se logro el resultado esperado por falta de tiempo para investigar varias referencias al respecto
 
Tener en consideración que este proceso se realiza en un Pc con un mejor rendimiento, mas núcleos y RAM , y el resultado puede ser sorprendentemente increíble.

Bueno, espero que sea positivo y provecho como lo fue para mi. 

# Gracias por su atención!!!
