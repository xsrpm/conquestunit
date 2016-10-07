using DataModel;
using System.Collections.Generic;

namespace ConquestUnit
{
    public static class DataInitialize
    {
        static int npregunta;
        static int nopcion;

        public static void Initialize(ConquestUnitContext context)
        {
            npregunta = 0;
            nopcion = 0;

            InsertarPregunta("¿Que ingiere una planta ademas de  luz solar, agua y nutrients de la tierra para usar en la fotosintesis ?",
                "Dioxido de Carbono", "Tierra", "Oxigeno", "Insectos", context);
            InsertarPregunta("En el futbol, ¿Quien puede anotar goles?",
                "Todos ellos", "Los mediocampistas", "Los delanteros", "Los defenzas", context);
            InsertarPregunta("¿Que futbolista es apodado como \"Bad Boy\"?",
                "Wayne Rooney", "Theo Walcott", "Nani", "Ryan Giggs", context);
            InsertarPregunta("¿Quien mató a su padre para casarse con su madre?",
                "Edipo Rey", "Homero", "Aristoteles", "Pericles", context);
            InsertarPregunta("¿Como se le denomina al O3?",
                "Ozono", "Oxigeno molecular", "Monoxido", "Oxigeno", context);
            InsertarPregunta("¿Cual es el lago navegable ubicado a mayor altitud?",
                "El lago Titicana", "El lago Yanganuco", "Ninguno de ellos", "El lago Salado", context);
            InsertarPregunta("¿Quien es la entrenadora de volleyball de Perú que se caracteriza por el uso de palabras soeces a sus deportistas?",
                "Natalia Málaga", "Zenaida Uribe", "Nadine Heredia", "Rocio Miranda", context);
            InsertarPregunta("¿Cual fue la guerra relámpago?",
                "Cuando Hitler invadió Polonia", "La guerra de 1943 en la que hubo un relampago", "Fue un fenomeno natural", "Cuando Stalin invadió Alemania en 2 dias", context);
        }

        private static void InsertarPregunta(string pregunta, string opcioncorrecta, string otraopcion1, string otraopcion2, string otraopcion3, ConquestUnitContext context)
        {
            npregunta++;
            context.conn.InsertOrReplace(new Pregunta() { PreguntaId = npregunta, TextoPregunta = pregunta });
            context.conn.InsertOrReplace(new Opcion() { OpcionId = ++nopcion, TextoOpcion = opcioncorrecta, EsRespuesta = true, PreguntaId = npregunta });
            context.conn.InsertOrReplace(new Opcion() { OpcionId = ++nopcion, TextoOpcion = otraopcion1, EsRespuesta = false, PreguntaId = npregunta });
            context.conn.InsertOrReplace(new Opcion() { OpcionId = ++nopcion, TextoOpcion = otraopcion2, EsRespuesta = false, PreguntaId = npregunta });
            context.conn.InsertOrReplace(new Opcion() { OpcionId = ++nopcion, TextoOpcion = otraopcion3, EsRespuesta = false, PreguntaId = npregunta });
        }
    }
    
}
