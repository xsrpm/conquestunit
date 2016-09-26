

using ConquestUnit.Model;
using System.Collections.Generic;

namespace ConquestUnit
{
    public static class DataInitialize
    {
        public static void Initialize(ConquestUnitContext context)
        {
            //PREGUNTAS
            var pregunta1 = new Pregunta() { PreguntaId = 1, TextoPregunta = "¿Cuál es tu nombre?" };
            var pregunta2 = new Pregunta() { PreguntaId = 2, TextoPregunta = "¿Cuál es tu Edad?" };
            var pregunta3 = new Pregunta() { PreguntaId = 3, TextoPregunta = "¿Cuál es tu Apellido?" };
            //if (context.Listar<Pregunta>().FirstOrDefault() != null)
            //{
            context.conn.InsertOrReplace(pregunta1);
            context.conn.InsertOrReplace(pregunta2);
            context.conn.InsertOrReplace(pregunta3);
            //}
            //OPCIONES
            var opcion1 = new Opcion() { OpcionId = 1, TextoOpcion = "opcion 1", EsRespuesta = false, PreguntaId = 1 };
            var opcion2 = new Opcion() { OpcionId = 2, TextoOpcion = "opcion 2", EsRespuesta = false, PreguntaId = 1 };
            var opcion3 = new Opcion() { OpcionId = 3, TextoOpcion = "opcion 3", EsRespuesta = true, PreguntaId = 1 };
            var opcion4 = new Opcion() { OpcionId = 4, TextoOpcion = "opcion 4", EsRespuesta = false, PreguntaId = 1 };
            var opcion5 = new Opcion() { OpcionId = 5, TextoOpcion = "opcion 5", EsRespuesta = false, PreguntaId = 2 };
            var opcion6 = new Opcion() { OpcionId = 6, TextoOpcion = "opcion 6", EsRespuesta = false, PreguntaId = 2 };
            var opcion7 = new Opcion() { OpcionId = 7, TextoOpcion = "opcion 7", EsRespuesta = true, PreguntaId = 2 };
            var opcion8 = new Opcion() { OpcionId = 8, TextoOpcion = "opcion 8", EsRespuesta = false, PreguntaId = 2 };
            var opcion9 = new Opcion() { OpcionId = 9, TextoOpcion = "opcion 3", EsRespuesta = false, PreguntaId = 3 };
            var opcion10 = new Opcion() { OpcionId = 10, TextoOpcion = "opcion 5", EsRespuesta = false, PreguntaId = 3 };
            var opcion11 = new Opcion() { OpcionId = 11, TextoOpcion = "opcion 8", EsRespuesta = true, PreguntaId = 3 };
            var opcion12 = new Opcion() { OpcionId = 12, TextoOpcion = "opcion 2", EsRespuesta = false, PreguntaId = 3 };
            //if (context.Listar<Opcion>().FirstOrDefault() != null)
            //{
            context.conn.InsertOrReplace(opcion1);
            context.conn.InsertOrReplace(opcion2);
            context.conn.InsertOrReplace(opcion3);
            context.conn.InsertOrReplace(opcion4);
            context.conn.InsertOrReplace(opcion5);
            context.conn.InsertOrReplace(opcion6);
            context.conn.InsertOrReplace(opcion7);
            context.conn.InsertOrReplace(opcion8);
            context.conn.InsertOrReplace(opcion9);
            context.conn.InsertOrReplace(opcion10);
            context.conn.InsertOrReplace(opcion11);
            context.conn.InsertOrReplace(opcion12);

            //pregunta1.Opciones = new List<Opcion>() { opcion1, opcion2, opcion3, opcion4 };
            //pregunta2.Opciones = new List<Opcion>() { opcion5, opcion6, opcion7, opcion8 };
            //pregunta3.Opciones = new List<Opcion>() { opcion9, opcion10, opcion11, opcion12 };

            //context.conn.Update(pregunta1);
            //context.conn.Update(pregunta2);
            //context.conn.Update(pregunta3);
            //}
        }
    }
}
