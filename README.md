Segunda Entrega Correcciones:
    > Agregar Tests para comprobar el funcionamiento de las demás clases que faltaron; ---> Martín --
    > Arreglar documentación de los patrones y principios utilizados; ---> Martín, Bianca, Juan, Santiago --
    > Arreglar warnings;  ---> Martín --
    > Juntar todas las clases de Search; ---> Juan --
    > Cambiar Assert.AreEqual(true, expected) por Assert.That(expected, Is.True); ---> Martín --
    > "La propiedad pública entrepeneurList de la clase Entrepreneur debe comenzar en mayúsculas. Ídem categorySearcher de la clase SearchByCategory." ---> Bianca --
    > "El método EntrepreneurRegister de la clase EntrepreneurRegisterTest tiene comentarios XML dentro del método; los comentarios XML deben estar junto a elementos como clases, métodos, etc. y no dentro de los métodos."   ---> Bianca --
    > Agregar precondiciones; ---> Juan


Correcciones pequeñas:
    > Hacer que el atributo MaterialPublication.Keywords sea una propiedad de solo lectura


Tercera Entrega:
    > Agregar los comandos; 
        > CompanyCommands
        > EntrepreneurCommands
        > AdminCommands
        
    > Implementar la serialización y deserialización de los objetos (carpeta Memory en la raíz del proyecto)
        > CompanySerialization              --> Serializar la lista, las companias y las publicaciones   --> Martín
        > EntrepreneurSerialization         --> Serializar los usuarios emprendedores;                   --> Juan
        > AdminSerialization                --> Serializar los usuarios admin;                           --> Bianca
        > States                            --> Serializar los estados de los mensajes                   --> Santiago

    ID´s Telegram:
        > Santiago: 1883636472
        > Martín:   2066298868
        > Bianca:   2090102457
        > Juan:     2012232708

    > Modificar MaterialPublication
        > Eliminar el método GetMaterialConstantlyGenerated             --> Martín
        > Agregar en la publicacion si es continuo, puntual, o normal   --> Santiago --

-------------------------------------------------------------------------------------------------
Implementar funcionalidad de:
	Comprar (emprendedor) -- Martín
	Borrar publicación (empresa) -- Martín
	Mostrar las publicaciones de una empresa (empresa) -- Martín


Hacer que un emprendedor, cuando compre un material, no pueda:
	Comprar un material totalmente vendido
	Comprar de un material una cantidad superior al stock
		Preferiblemente, cuando la cantidad solicitada sea superior, se podría preguntar al emprendedor si desea comprar lo que queda --Santiago

Chequear ejemplos de materiales y residuos

Add tests to the RuntimeTest for:
	Publishing materials
		Continuous -- Juan
		Scheduled -- Juan
	Searching materials
		By category -- Bianca
		By location -- Bianca
	Buying materials -- Bianca
		Make sure that an entrepreneur can't buy materials whose requirements are not met by his habilitations
	Creating reports
		For entrepreneur -- Juan 
	Removing users
		Company -- Juan 
		Entrepreneur -- Juan 
		Admin -- Bianca


Change the input processing amounts and prices to single strings -- Martín

Añadir atributo "vendido" a MaterialPublication, definido por su Amount -- Martín

Evitar que los emprendedores puedan ver publicaciones de materiales vendidos -- Martín

-----------------------------------------------------------------------------------------------------

Reflexiones sobre las entregas:
    Segunda Entrega:
En nuestro caso, lo más dificil de esta entrega fue conseguir dividirnos el trabajo de una manera equitativa; pero luego lo pudimos conversar y ahora tenemos mejor resuelto el tema de la división de las tareas restantes. 
Esta entrega nos deja como reflexión que el diálogo entre nosotros es sumamente importante para lograr un buen trabajo y que todos estemos contentos con el mismo, y, lo más importante, que todos podamos ampliar nuestros conocimientos con el desarrollo del proyecto.
En general, la documentación de Microsoft, Refactoring Gurú, Stack Overflow fueron páginas de las que pudimos tener una guía para resolver errores o aplicar conocimientos nuevos.

    Tercera Entrega:
En esta entrega mejoramos deficiencias de la anterior, diviendo tareas según las áreas trabajadas por la persona. Trabajamos la parte del procesamiento de mensajes y respuestas a los mismos. Implementamos tests para comprobar el funcionamiento y también creamos empresas y emprendedores en la plataforma de  telegram para asegurarnos de un óptimo funcionamiento. Cada usuario, empresa, material comprado, reporte de venta y compra es almacenado en archivos json.
Logramos manejar bien los tiempos y proponer reuniones diarias en la que discutimos acerca de las diferentes ideas que fueron surgiendo en este proceso. Cada cambio hecho por un integrante es revisado y aceptado por otro, de esta forma, nos garantizamos de que cada cambio se realice dependiendo de al menos un integrante del grupo.
