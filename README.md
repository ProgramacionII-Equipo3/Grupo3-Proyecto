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
        
    > Implementar la serialización de los objetos
        > CompanySerialization              --> Serializa la lista y los usuarios;
        > EntrepreneurSerialization         --> Serializa los usuarios emprendedores;
        > MaterialPublicationSerialization  --> Serializa las publicaciones de materiales;
        > AdminSerialization                --> Serializa los usuarios admin;

    > Modificar MaterialPublication
        > Eliminar el método GetMaterialConstantlyGenerated             --> Martín
        > Agregar en la publicacion si es continuo, puntual, o normal   --> Santiago
