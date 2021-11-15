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
        > Agregar en la publicacion si es continuo, puntual, o normal   --> Santiago
