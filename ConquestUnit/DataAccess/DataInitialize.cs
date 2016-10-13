using DataModel;
using System;
using System.Collections.Generic;

namespace ConquestUnit
{
    public static class DataInitialize
    {
        static int npregunta;
        static int nopcion;
        static List<Pregunta> preguntas;
        static List<Opcion> opciones;

        public static void Initialize(ConquestUnitContext context)
        {
            npregunta = 0;
            nopcion = 0;
            preguntas = new List<Pregunta>();
            opciones = new List<Opcion>();

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

            #region Geografia
            InsertarPregunta("¿Cuál es el país menos turístico de Europa?",
                            "Liechtenstein", "Armenia", "Moldavia", "Hungría", context);
            InsertarPregunta("¿A qué país pertenece la isla de Tasmania?",
                            "Australia", "Estados Unidos", "Portugal", "Ninguna es correcta", context);
            InsertarPregunta("¿En cuál de los siguientes países NO hay ningún desierto?",
                            "Alemania", "España", "Chile", "Mongolia", context);
            InsertarPregunta("¿Cuál es el código internacional para Cuba?",
                            "CU", "CA", "CB", "Ninguna es correcta", context);
            InsertarPregunta("¿Cuál es la capital del estado de Arkansas?",
                            "Little Rock", "Kansas", "Hot Springs", "Washington", context);
            InsertarPregunta("¿En qué país situarías la ciudad de Cali?",
                            "Colombia", "Venezuela", "Costa Rica", "Chile", context);
            InsertarPregunta("¿Cuál de estas características no pertenece al clima Mediterráneo?",
                            "Lluvias muy abundantes", "Veranos secos y calurosos", "Es un tipo de clima templado", "Variables temperaturas en primavera", context);
            InsertarPregunta("¿Qué es el Cabo de Creus?",
                            "El punto más oriental de la Península", "El punto más oriental de España", "El punto más oriental de Cataluña", "Ninguna es correcta", context);
            InsertarPregunta("¿Cuál es principal celebración holandesa?",
                            "El día de la Reina", "Navidad", "La llegada del verano", "Hallowen", context);
            InsertarPregunta("¿Cuál de las siguientes especialidades NO es típica de la cocina estadounidense?",
                            "Todas son típicas", "La hamburguesa", "El pastel de cangrejo", "La tarta de manzana", context);
            InsertarPregunta("¿Con cuántos países limita Argentina?",
                            "Cinco", "Tres", "Cuatro", "Seis", context);
            InsertarPregunta("¿Qué es la UA?",
                            "Unión Africana", "Unión Austrohúngara", "Unión Americana", "Unión Afroamericana", context);
            InsertarPregunta("¿Cuál es la capital de Suiza?",
                            "Berna", "Zurich", "Ginebra", "Basilea", context);
            InsertarPregunta("¿Qué separa las franjas de Gaza y Cisjordania?",
                            "Israel", "Un muro", "Nada", "Un río", context);
            InsertarPregunta("¿En qué país está Ushuaia, la ciudad más al sur del mundo?",
                            "Argentina", "Chile", "Sudáfrica", "Nueva Zelanda", context);
            InsertarPregunta("¿Cuál de estos países africanos no tiene costa?",
                            "Todas tienen costa", " Mauritania", "Senegal", "Gambia", context);
            InsertarPregunta("¿En qué se dividen las rupias indias y pakistanís?",
                            "En paisas", "En centavos", "En céntimos", "En peniques", context);
            InsertarPregunta("¿Cuál de las siguientes islas está más al sur?",
                            "Malta", "Sicilia", "Córcega", "Cerdeña", context);
            InsertarPregunta("¿Cuál de las siguientes ciudades no es una ciudad santa?",
                            "Burgos", "Santiago de Compostela", "Hebrón", "Asís", context);
            InsertarPregunta("¿Cuál es la capital de Baréin? ",
                            "Manama", "Baréin", "Ammán", "Riad", context);
            InsertarPregunta("¿En qué país está la Laguna Verde? ",
                            "Bolivia", "Chile", "Argentina", "Todas son correctas", context);
            InsertarPregunta("¿En qué cordillera están la mayoría de las grandes montañas?",
                            "En el Himalaya", "En el Karakórum", "En los Andes", "En las Montañas Rocosas", context);
            InsertarPregunta("¿Qué ciudad europea es famosa por la belleza de su parlamento?",
                            "Budapest", "París", "Madrid", "Praga", context);
            InsertarPregunta("¿Cuál es la capital de Libia?",
                            "Trípoli", "Beirut", "El Cairo", "Riad", context);
            InsertarPregunta("¿Entre qué dos países está el lago Titicaca?",
                            "Bolivia y Perú", "Bolivia y Paraguay", "Bolivia y Brasil", "Bolivia y Argentina", context);
            InsertarPregunta("¿Cuál es la capital de la república de Macedonia?",
                            "Skopje", "La Valeta", "Chisinau", "Sarajevo", context);
            InsertarPregunta("¿Qué accidente geográfico se define como un conjunto de islas, islotes y diminutas masas de tierra cercanas entre sí?",
                            "Archipiélago", "Islotes", "Península", "Meandro", context);
            InsertarPregunta("¿A qué país pertenece la isla Mujeres?",
                            "México", "Argentina", "Colombia", "Chile", context);
            InsertarPregunta("¿Qué país es bañado por la Bahía de Hudson?",
                            "Canadá", "Estados Unidos", "Reino Unido", "Rep. de Irlanda", context);
            InsertarPregunta("¿Qué país africano fue antigua colonia española?",
                            "Guinea Ecuatorial", "R.D. Congo", "Sudáfrica", "Egipto", context);
            InsertarPregunta("¿En qué continente/s se encuentra Turquía?",
                            "Asia y Europa", "América", "África y Europa", "Oceania", context);
            InsertarPregunta("¿Cuál es la religión mayoritaria en China?",
                            "Budismo", "Taoísmo", "Confucianismo", "Cristianismo", context);
            InsertarPregunta("¿En qué mar desemboca el rio Segura?",
                            "Mar Mediterráneo", "Mar Cantábrico", "Mar Báltico", "Ninguna es correcta", context);
            InsertarPregunta("¿Qué accidente geográfico se define como una pendiente vertical abrupta?",
                            "Un acantilado", "Un barranco", "Una montaña", "Un puente", context);
            InsertarPregunta("¿Cuál es el edificio más famoso en Dubai?",
                            "Burjal Arab", "Torre Eiffel", "Crystal Island", "Crosgrove", context);
            InsertarPregunta("¿Con cuántos países limita la República Argentina?",
                            "5", "6", "4", "3", context);
            InsertarPregunta("¿Dónde se encuentra el Cerro Uritorco, conocido por sus avistamientos a O.V.N.I.S.? ",
                            "Capilla del Monte, Córdoba", "La Banda, Santiago del Estero", "Logroño, España", "Santiago, Chile", context);
            InsertarPregunta("¿Dónde está el desierto del Gobi?",
                            "Al sur de Mongolia", "Al sur de China", "Al sur de Japón", "Ninguna es correcta", context);
            InsertarPregunta("¿Dónde se encuentra Baiona?",
                            "Pontevedra", "Francia", "País Vasco", "Andalucia", context);
            InsertarPregunta("¿A qué país pertenece la Isla de Pascua?",
                            "Chile", "Australia", "Indonesia", "Es un país", context);
            InsertarPregunta("¿En qué país de África es el español el idioma oficial?",
                            "Ghana", "Camerún", "Gabón", "Guinea Ecuatorial", context);
            InsertarPregunta("¿Cuál es la capital de Argelia?",
                            "Argel", "Argel", "Arabia", "Colombo", context);
            InsertarPregunta("¿Dónde está la isla más joven del mundo, nacida en 2011 producto de una erupción volcánica?",
                            "En Yemen", "En Omán", "En la Polinesia Francesa", "En Australia", context);
            InsertarPregunta("¿En qué continente está la India?",
                            "Asia", "Oceanía", "América", "Antártida", context);
            InsertarPregunta("¿Cuál es la capital del estado de California?",
                            "Sacramento", "San Francisco", "Los Ángeles", "San Diego", context);
            InsertarPregunta("¿Cuál es el mayor golfo de Africa?",
                            "El de Guinea", "El de Gabés", "El de Sirte", "Ninguna es correcta", context);
            InsertarPregunta("¿Cúales son las ciudades autónomas de España?",
                            "Ceuta y Melilla", "Melilla y Alhucemas", "Melilla y Nador", "Ceuta y Villa Sanjurjo", context);
            InsertarPregunta("¿Cúal es la capital de Alemania?",
                            "Berlín", "Múnich", "Múnich", "Dublin", context);
            InsertarPregunta("¿Dónde está la ciudad de Mostar?",
                            "Bosnia y Herzegovina", "Serbia", "Montenegro", "Croacia", context);
            InsertarPregunta("¿De dónde es capital Thimbu?",
                            "De Bután", "De Thimbu", "De Nepal", "Ninguna es correcta", context);
            InsertarPregunta("¿Cuál es la capital de Ecuador?",
                            "Quito", "Lima", "Bogota", "Guayaquil", context);
            InsertarPregunta("¿Cuál de estos países no está en una isla?",
                            "Ghana", "Japón", "Jamaica", "Madagascar", context);
            InsertarPregunta("¿Cuál de las siguientes ciudades sudafricanas es la más poblada?",
                            "Johannesburgo", "Ciudad del Cabo", "Pretoria", "East London", context);
            InsertarPregunta("¿Cuál de las grandes montañas ha sido estalada más veces?",
                            "El Everest", "El K2", "El Annapurna I", "El Annapurna II", context);
            InsertarPregunta("¿Dónde queda el monumento a la mitad del mundo?",
                            "Quito", "Bogota", "Lima", "Manaos", context);
            InsertarPregunta("¿Dónde se encuentra la cordillera de Los Andes?",
                            "América", "Europa", "Asia", "Oceania", context);
            InsertarPregunta("¿A qué localidad manchega irías a por ‘berenjenas’?",
                            "a Almagro", "al Toboso", "a Orejilla del Sordete", "al Viso del Marqués", context);
            InsertarPregunta(" ¿En qué ciudad española encontrarás los barrios de la Trinidad, la Palmilla y Carranque?",
                            "Málaga", "Barcelona", "Sevilla", "Las Palmas", context);
            InsertarPregunta("¿Dónde vivía Pablo Escobar?",
                            "Colombia", "Ecuador", "Argentina", "España", context);
            InsertarPregunta(" ¿A qué archipiélago pertenece la isla de Gran Canaria?",
                            "Canario", "Chinijo", "Isleño", "Atlántico", context);
            InsertarPregunta("¿Cuál es la capital de Corea Del Sur?",
                            "Seúl", "Busan", "Pyongyang", "Tegucigalpa", context);
            InsertarPregunta("¿Dónde se encuentra la ciudad de Bahía Blanca?",
                            "Argentina", "Uruguay", "Chile", "Brasil", context);
            InsertarPregunta(" Una península es una parte del territorio… ",
                            "Rodeada de agua menos por una parte", "Muy montañosa", "Rodeada totalmente de agua", "Ninguna es correcta", context);
            InsertarPregunta("¿A qué ciudad costera europea se la conoce como la Perla del Adriático?",
                            "Dubrovnik", "Nápoles", "Barcelona", "Atenas", context);
            InsertarPregunta("¿En qué continente se encuentra el país Mexico?",
                            "América", "Europa", "Asia", "Oceania", context);
            InsertarPregunta("¿De qué colores es la bandera de Bélgica?",
                            "Negro, amarillo y rojo", "Negro, amarillo y verde", "Negro y amarillo", "Amarillo y marrón", context);
            InsertarPregunta("¿Cuál es la capital de Angola?",
                            "Luanda", "Kinasasa", "Zuberoa", "Brazzabille", context);
            InsertarPregunta("¿Dónde están las torres Petronas?",
                            "En Malasia", "En Indonesia", "En Singapur", "En Tailandia", context);
            InsertarPregunta("¿Dónde se celebra el Oktoberfest?",
                            "Alemania", "Rusia", "Ucrania", "Reino Unido", context);
            InsertarPregunta("¿Cuál es el plato típico de Mallorca?",
                            "Tumbet", "Paella", "Marisco", "Merchines", context);
            InsertarPregunta("¿Dónde nació el Hip-Hop? ",
                            "Nueva York", "San Francisco", "Chicago", "Philadelphia", context);
            InsertarPregunta("¿Cuál es el idioma oficial de Uruguay?",
                            "El español", "El uruguayo", "El uruguayo y el español", "El español y el guaraní", context);
            InsertarPregunta("¿Conocida por su barrio rojo y sus cafeterías, en las que se vende cannabis, ¿qué ciudad tiene como símbolo ‘xxx’?",
                            "Ámsterdam", "París", "Praga", "Berlín", context);
            InsertarPregunta("¿Cuáles son los ingblackientes exclusivos de la paella valenciana son?",
                            "Arroz, judías verdes, carne de pollo y conejo", "Arroz, mariscos, guisantes", "Arroz, garbanzos, costillas, morcillas", "Arroz, chorizo y mucho limón", context);
            InsertarPregunta("¿Cuál es la capital de Kosovo?",
                            "Prístina", "Kosovo", "Tenerife", "Malí", context);
            InsertarPregunta("¿Cuándo es verano en el hemisferio sur?",
                            "De diciembre a marzo", "De marzo a junio", "De junio a septiembre", "De septiembre a diciembre", context);
            InsertarPregunta("¿Qué país NO se encuentra en Asia?",
                            "Comores", "Indonesia", "Sri Lanka", "Brunei", context);
            InsertarPregunta("¿Cuál es la capital de Japón?",
                            "Tokio", "Kyoto", "Pekin", "Ninguna es correcta", context);
            InsertarPregunta("¿Cuál de estos países africanos NO tiene costa?",
                            "Chad", "Sudáfrica ", "Túnez", "Marruecos", context);
            InsertarPregunta("¿Cuál es la capital de Belice?",
                            "Belmopan", "Kingston", "Managua", "Saint John", context);
            InsertarPregunta("¿A qué país tienes que ir para visitar el turístico pueblo costero de Sidi Bou Said?",
                            "Túnez", "Marruecos ", "Libia", "Egipto", context);
            InsertarPregunta("¿En qué provincia desemboca el Ebro en el Mediterráneo?",
                            "Tarragona", "Castellón", "Barcelona", "Zaragoza", context);
            InsertarPregunta("¿Dónde creció Sigmund Freud?",
                            "Viena", "París", "Munich", "Kiev", context);
            InsertarPregunta("¿En qué continente queda Chile?",
                            "América", "Asia", "Europa", "Africa", context);
            InsertarPregunta("¿Cuántas estrellas tiene la bandera de China?",
                            "5", "6", "0", "Ninguna es correcta", context);
            InsertarPregunta("¿Cuál es la comunidad autónoma más grande de España?",
                            "Andalucía", "Extremadura", "Comunidad de Madrid", "País Vasco", context);
            InsertarPregunta("¿Dónde se encuentra Manresa?",
                            "Centro de Cataluña", "Norte de Cataluña", "Sur de Cataluña", "Oeste de Cataluña", context);
            InsertarPregunta("¿Cuál es la capital de Islandia?",
                            "Reikjavik", "Riga", "Dublín", "Minsk", context);
            InsertarPregunta("¿En qué provincia está Valdelinares el pueblo más alto de España?",
                            "Teruel", "Granada", "Gran Canaria", "Zaragoza", context);
            InsertarPregunta("¿De que tiene forma Italia?",
                            "Bota", "Gorro", "Submarino", "Nube", context);
            InsertarPregunta("¿Cuál es la capital de Luxemburgo?",
                            "Luxemburgo", "Berlín", "Berna", "La Valeta", context);
            InsertarPregunta("¿Cuál es el océano más profundo?",
                            "Pacífico", "Atlántico", "Ártico", "Índico", context);
            InsertarPregunta("¿Dónde se encuentra la Comisión y el Consejo Europeo?",
                            "Bruselas", "Estrasburgo", "Luxemburgo", "Londres", context);
            InsertarPregunta("¿En qué provincia vasca se situa el pueblo de Soraluze?",
                            "Gipuzkoa", "Iparralde", "Álava", "Vizkaia", context);
            InsertarPregunta("¿Cuál es la capital de Vizcaia, País Vasco?",
                            "Bilbao", "Sopuerta", "Donostia", "Vitoria", context);
            InsertarPregunta("¿Cuál de las siguientes ciudades es la capital autonómica de Extremadura?",
                            "Mérida", "Badajoz", "Zafra", "Cáceres", context);
            InsertarPregunta("¿De dónde eran los vikingos?",
                            "Noruega", "Suiza", "Italia", "U.S.A", context);
            InsertarPregunta("¿Dónde están las líneas de Nazca?",
                            "En Perú", "En Chile", "En Colombia", "En Paraguay", context);
            InsertarPregunta(" ¿Qué países integran la alianza centroeuropea llamada Grupo Visegrad?",
                            "Hungría,Polonia,Rep. Checa y Eslovaquia", "Hungría,Polonia,Bosnia y Eslovenia", "Hungría,Polonia,Rep. Checa y Bosnia", "Hungría,Polonia,Eslovequia,Eslovenia", context);
            InsertarPregunta("¿A qué país pertenece el código internacional PA?",
                            "Panamá", "Portugal", "Palestina", "Palaos", context);
            InsertarPregunta("¿En qué pais situarías a la bahia Hudson?",
                            "Canadá", "Estados Unidos", "Irlanda", "Australia", context);
            InsertarPregunta("¿Qué ciudad es conocida por su carnaval,considerado el segundo más importante del mundo?",
                            "Santa Cruz de Tenerife", "Cádiz", "Las Palmas de Gran Canaria", "Sitges", context);
            InsertarPregunta("¿En qué ciudad de Andalucía es típico el carnaval?",
                            "Cádiz", "Sevilla", "Granada", "Málaga", context);
            InsertarPregunta("¿Dónde se encuentran las Lagunas de Ruidera?",
                            "Comunidad Valenciana", "Castilla la Mancha", "Galicia", "Castilla y Leon", context);
            InsertarPregunta("¿Dónde está la montaña de Jade?",
                            "Taiwan", "Taiwan", "Corea del Sur", "China", context);
            InsertarPregunta("¿Cuáles son los colores de la bandera de Bolivia?",
                            "Rojo,amarillo y verde", "Verde,azul y amarillo", "Rojo,naranja y verde", "Amarillo,verde y blanco", context);
            InsertarPregunta("¿Dónde están las líneas de Nazca?",
                            "En Perú", "En Chile", "En Colombia", "En Paraguay", context);
            InsertarPregunta("¿Cuál es el país más pequeño de Asia?",
                            "Maldivas", "Nauru", "Laos", "Japón", context);
            InsertarPregunta("¿Cuantas islas tienen las Islas Canarias y cuantos islotes?",
                            "7 islas y 6 islotes ", "7 islas y 5 islotes", "6 islas y 6 islotes", "7 islas y 7 islotes", context);
            InsertarPregunta("¿En qué comunidad autónoma se encuentra la población de Torrijos?",
                            "Toledo", "Cádiz", "Burgos", "Lleida", context);
            InsertarPregunta("¿Dónde se encuentra el Cabo de Gata?",
                            "En Almería", "En Cádiz", "En Alicante", "En Huelva", context);
            InsertarPregunta("¿De qué país es el panda el animal nacional?",
                            "China", "Canadá", "Tanzania", "Australia", context);
            InsertarPregunta("¿Dónde está Pernambuco?",
                            "En Brasil", "En Chile", "En Sudáfrica", "En India", context);
            #endregion

            #region Deportes
            InsertarPregunta("¿De que deporte es el kemari uno de los principales antecesores?",
                "Fútbol", "Ténis", "Rugby", "Karate", context);
            InsertarPregunta("¿Cuál es el clásico rival del Flamengo (BRA)?",
                "Corinthians", "Palmeiras", "Cruzeiro", "São Paulo", context);
            InsertarPregunta("¿Qué jugadora de hockey sobre césped ha ganado 7 veces el premio a la mejor jugadora del mundo de 2013?",
                            "Luciana Aymar", "Natascha Keller", "Alyson Annan", "Maartje Paumen", context);
            InsertarPregunta("¿Cuántas finales del mundo jugó la Selección Argentina de fútbol?",
                            "Cuatro", "Cinco", "Seis", "Tres", context);
            InsertarPregunta("¿Quién marcó el gol 3.500 en Liga del Atlético de Madrid?",
                            "Christian Vieri", "Zinedine Zidane", "Luka Modric", "Adrián Escudero", context);
            InsertarPregunta("¿Cuántos mangos por lado tiene el futbolín?",
                            "Cuatro", "Dos", "Tres", "Cinco", context);
            InsertarPregunta("¿Cuál es el estilo de natación más rápido?",
                            "Crol", "Espalda", "Mariposa", "Pecho", context);
            InsertarPregunta("¿Cuántos jugadores componen un equipo de rugby?",
                            "15", "11", "12", "21", context);
            InsertarPregunta("¿Cuántas puntas de cada color hay en un tablero de backgammon?",
                            "Doce", "Ocho", "Catorce", "Dieciséis", context);
            InsertarPregunta("¿En qué país se inventó el voleibol?",
                            "Estados Unidos", "Gran Bretaña", "Francia", "Rusia", context);
            InsertarPregunta("¿Cuál de las siguientes modalidades no forma parte del deporte rural vasco?",
                            "Desintegramiento de piedra", "Arrastre de piedra", "Lanzamiento de fardo", "Corte de troncos", context);
            InsertarPregunta("¿Qué tienen en común Ty Cobb, Cy Young y Roberto Clemente?",
                            "Son jugadores de béisbol", "Son jugadores de fútbol", "Son jugadores de baloncesto", "Son jugadores de golf", context);
            InsertarPregunta("¿Dónde se jugará la Copa Mundial de Rugby en 2015?",
                            "Inglaterra", "Australia", "Escocia", "Gales", context);
            InsertarPregunta("¿Qué selección acumula mayor cantidad de expulsados en  mundiales de fútbol?",
                            "Argentina", "Brasil", "Italia", "Italia", context);
            InsertarPregunta("¿Quién inventó el arte marcial llamado Jeet Kune Do?",
                            "Bruce Lee", "David Carradine", "Kato Mimoko", "Ninguna es correcta", context);
            InsertarPregunta("¿De qué deporte es el longboard una de las modalidades?",
                            "Skateboard", "Snowboard", "Alpinismo", "Surf", context);
            InsertarPregunta("¿En qué deporte se destaca Jonny Wilkinson?",
                            "Rugby", "Fútbol", "Atletismo", "Remo", context);
            InsertarPregunta("¿De qué color es el cero en el cilindro de la ruleta?",
                            "Verde", "Blanco", "Negro", "Rojo", context);
            InsertarPregunta("¿A qué barrio porteño pertenece el club de fútbol San Lorenzo?",
                            "Boedo", "La Boca", "Liniers", "Paternal", context);
            InsertarPregunta("¿Cuánto pesa aproximadamente una bola de bolera?",
                            "Siete kilos y cuarto", "5 kilos y cuarto", "Cuatro kilos y medio", "Ocho kilos y cuarto", context);
            InsertarPregunta("¿Cuántos puntos vale un tiro libre ensectado en baloncesto?",
                            "Uno", "Dos", "Tres", "Depende", context);
            InsertarPregunta("¿Cuál es el estadio de fútbol con mayor capacidad en América?",
                            "Estadio Azteca (México)", "Estadio Maracaná (Brasil)", "Estadio Monumentar (Argentina)", "Estadio Morumbi (Brasil)", context);
            InsertarPregunta("¿Qué tipo de competición es el Giro de Italia?",
                            "Una carrera ciclista", "Una competición de vela", "Un maratón", "Una carrera automovilística", context);
            InsertarPregunta("¿Cuánto dura un partido de fútbol?",
                            "90 minutos", "45 minutos", "75 minutos", "80 minutos", context);
            InsertarPregunta("¿Cuál es el derbi más esperado en Andalucía?",
                            "Betis vs Sevilla", "Real Madrid vs At. Madrid", "Real Madrid vs Sevilla", "Granada vs Málaga", context);
            InsertarPregunta("¿Cómo se llama la zona de hierba rasa donde está ubicado el hoyo en golf?",
                            "Green", "Esplanada", "Campo", "Terreno", context);
            InsertarPregunta("¿Cuál de estas frases NO fue dicha por Diego Armando Maradona?",
                            "La pelota no dobla", "Pelé, debutó con un pibe", "Segurola y La Habana 4310 7mo piso", "La pelota no se mancha", context);
            InsertarPregunta("¿Cuál de estos pilotos no es de F1?",
                            "Richard Petty", "Fernando Alonso", "Sebastian Vettel", "Mark Webber", context);
            InsertarPregunta("¿A qué tipo de billa se juega con más bolas?",
                            "Snooker", "Bola 9", "Billar español", "Billar italiano", context);
            InsertarPregunta("¿De qué nacionalidad es el entrenador de fútbol Tata Martino?",
                            "Argentina", "Italia", "España", "Brasil", context);
            InsertarPregunta(" ¿En cuál de estos deportes no hay árbitro?",
                            "Ultimate", "Ultimate", "Basketball", "Volleyball", context);
            InsertarPregunta("¿Cuál es el país de origen del futbolista Lionel Messi?",
                            "Argentina", "España", "Brasil", "Chile", context);
            InsertarPregunta("¿Cómo se llama la cantera del FC Barcelona?",
                            "La Masía", "El Chalet", "La Fábrica", "La Depuradora", context);
            InsertarPregunta("¿Cuál de los siguientes es un truco de skate?",
                            "Ollie", "Mollie", "Clop", "Superclop", context);
            InsertarPregunta("Cuándo el árbitro de fútbol hace sonar su silbato a los 90 minutos ¿Qué significa?",
                            "Finalización del partido", "Medio tiempo", "Falta", "Tarjeta", context);
            InsertarPregunta("¿Quién ganó el mundial de fútbol del año 2002?",
                            "Brasil", "Italia", "España", "Francia", context);
            InsertarPregunta("¿A la selección de rugby de qué país se los conoce como Los Pumas?",
                            "Argentina", "Sudáfrica", "Sudáfrica", "Gales", context);
            InsertarPregunta("¿Cómo se llama el estadio del Universidad de Las Palmas?",
                            "Maspalomas", "Pedro Escartín", "Monumental", "Los pajaricos", context);
            InsertarPregunta("¿En cuál de estas situaciones se sacará una bandera amarilla, o incluso roja, en las carreras de coches?",
                            "Un peligro en la pista", "Se ha declarado un ganador", "Queda una vuelta", "La carrera va a empezar", context);
            InsertarPregunta("¿Qué país ganó el Mundial de balonmano en el año 2013?",
                            "España", "Francia", "Croacia", "Serbia", context);
            InsertarPregunta("¿En cuántos mundiales de fútbol participó la selección de Canadá?",
                            "1", "0", "3", "5", context);
            InsertarPregunta("¿Dónde se inventó el tenis de mesa?",
                            "Inglaterra", "China", "Japón", "Korea del Sur", context);
            InsertarPregunta("¿Qué jugador francés ha ganado un balón de oro mientras jugaba en Italia?",
                            "Zidane", "Zidane", "Ribery", "Maradona", context);
            InsertarPregunta("Qué país es campeón del mundo de balonmano masculino del 2013?",
                            "España", "Hungria", "Alemania", "Francia", context);
            InsertarPregunta("¿Cómo se llama el primer sueco que jugó en el FC Barcelona?",
                            "Henrik Larsson", "Zlatan Larsson", "Henrik Ibrahimovic", "Zlatan Ibrahimovic", context);
            InsertarPregunta("¿De dónde viene el jugador Alexis Sánchez?",
                            "Chile", "Chile", "España", "Moscú", context);
            InsertarPregunta("¿Con qué sobrenomnre apodó el Presidente de la FIFA a Cristiano Ronaldo?",
                            "El Comandante", "El Coloso", "El Chulito", "O Rei", context);
            InsertarPregunta("¿Quién quedó número 1 del mundo de tenis en 2008?",
                            "Rafael Nadal", "Roger Federer", "Novak Djokovic", "Carlos Moyá", context);
            InsertarPregunta("¿Cómo se llama el estadio del FCBarcelona?",
                            "Camp Nou", "Santiago Bernabeu", "La Masia", "Barcelona Camp", context);
            InsertarPregunta("¿Quién es el máximo goleador de la selección argentina de fútbol?",
                            "Batistuta", "Maradona", "Kempes", "Caniggia", context);
            InsertarPregunta("¿Cuál es el club de fútbol más antiguo de España?",
                            "Recreativo de Huelva", "Athletic Club de Bilbao", "F.C. Barcelona", "Real Madrid", context);
            InsertarPregunta("¿Cuánto dura un partido de balonmano?",
                            "60 minutos", "30 minutos", "45 minutos", "90 minutos", context);
            InsertarPregunta("Si durante una mano de Póker me tocan 3 cartas del mismo valor, ¿qué jugada tengo?",
                            "Trio", "Doble Par", "Full", "Par", context);
            InsertarPregunta("¿Con qué apodo fue conocido el velocista Carl Lewis?",
                            "El hijo del viento", "El rayo", "La chispa de Birmingham", "La gacela negra", context);
            InsertarPregunta("¿Dónde apoyan los jinetes sus pies?",
                            "Estribos", "Montura", "Riendas", "Baste", context);
            InsertarPregunta("¿Con qué deporte relacionarías a Juan Antonio Flecha?",
                            "Ciclismo", "Piragüismo", "Fútbol", "Atletismo", context);
            InsertarPregunta("¿Qué gimnasta española ha disputado cuatro juegos Olímpicos?",
                            "Almudena Cid", "Carolina Rodríguez", "Natalia García", "Julia Usón", context);
            InsertarPregunta("¿Qué equipo de fútbol es de Cataluña?",
                            "Todos son correctos", "CE Europa", "FC Barcelona", "RCD Español", context);
            InsertarPregunta("¿Cómo apodaban a Emilio Butragueño?",
                            "El buitre", "El mago", "El halcón", "El picha", context);
            InsertarPregunta("¿En qué deporte destacó Joe DiMaggio?",
                            "Béisbol", "Fútbol", "Boxeo", "Esquí", context);
            InsertarPregunta("¿Quién ganó cuatro mundiales consecutivos de Fórmula 1?",
                            "Sebastian Vettel", "Fernando Alonso", "Lewis Hamilton", "Kimi Raikkonen", context);
            InsertarPregunta("¿Quién preside la UEFA actualmente?",
                            "Michel Platini", "Alex Ferguson", "David Beckham", "Ángel María Villar", context);
            InsertarPregunta("- ¿Cuántos cuadros tiene un tablero de ajedres?",
                            "64", "36", "54", "81", context);
            InsertarPregunta("¿Cómo se llama la liga española de balonmano?",
                            "Asobal", "Liga Balonmano", "Balonbal", "Abobal", context);
            InsertarPregunta("¿A qué jugador de fútbol le apodan ‘El Apache’?",
                            "Carlos Tévez", "Lionel Messi", "Cristiano Ronaldo", "Sergio Ramos", context);
            InsertarPregunta("¿Qué país fue el ganador de baloncesto en los Juegos Olímpicos de Londres 2012?",
                            "Estados Unidos", "España", "Japón", "Italia", context);
            InsertarPregunta(" ¿Cuál fue el Mundial de fútbol que consagró a Diego Armando Maradona?",
                            "México’86", "Argentina’76", "España’82", "Italia’90", context);
            InsertarPregunta("¿A qué edad ganó su primer mundial de Fórmula 1 el automovilista español Fernando Alonso?",
                            "21", "38", "22", "20", context);
            InsertarPregunta("¿De qué tipo eran las 3 medallas conseguidas por Usain Bolt en los Juegos Olimpicos de Londres 2012?",
                            "Oro", "Plata", "Bronce", "No consiguió ninguna", context);
            InsertarPregunta("¿Qué numero llevó Raúl en el Real Madrid?",
                            "7", "5", "10", "98", context);
            InsertarPregunta("¿Cuál es el segundo apellido de Gerad Piqué?",
                            "Bernabeu", "Ninguna es correcta", "Vázquez", "Sibina", context);
            InsertarPregunta("¿Qué clase de peso en el boxeo está entre el peso gallo y el peso ligero?",
                            "Peso pluma", "Peso ideal", "Peso mosca", "Sobrepeso", context);
            InsertarPregunta("¿Cuántas cartas tiene una baraja inglesa?",
                            "52", "40", "48 ", "50", context);
            InsertarPregunta("¿Dónde nació el Rugby?",
                            "Reino Unido", "Australia", "Canadá", "Alemania", context);
            InsertarPregunta("¿Quién es el máximo goleador del FC Barcelona, hasta el 2013?",
                            "Lionel Messi", "César Rodríguez", "Ladislao Kubala", "Samuel Eto’o", context);
            InsertarPregunta("¿En qué ciudad sudafricana ganó España su único mundial de fútbol?",
                            "Johanesburgo", "Ciudad del Cabo", "Pretoria", "Bloemfontein", context);
            InsertarPregunta("¿En qué año se celebraron los primeros juegos olímpicos de verano , en Atenas?",
                            "1896", "1888", "1904", "1932", context);
            InsertarPregunta("¿Cuántos títulos mundiales posee el motociclista Valentino Rossi?",
                            "Nueve", "Dos", "Cinco", "Siete", context);
            InsertarPregunta("¿Qué piloto español de Fórmula 1 tiene 2 mundiales ganados en su haber?",
                            "Fernando Alonso", "Pedro Martínez de la Rosa", "Felipe Massa", "Luis Hamilton", context);
            InsertarPregunta("¿A qué ex-jugador del Valencia c.f. se le conocía como ” El Matador”?",
                            "Mario Alberto Kempes", "Gaizka Mendieta", "Claudio Lopez", "Francisco Camarasa", context);
            InsertarPregunta("¿Cuáles son los equipos de fútbol de Sevilla?",
                            "R.Betis Balompié y Sevilla F.C", "Selliva F.C y Málaga C.F", "Cádiz C.F y R.Betis Balompié", "Málaga C.F y Cádiz C.F", context);
            InsertarPregunta("¿Quién tiró el segundo disparo más veloz del mundo?",
                            "Zlatan Ibrahimovic", "Miroslav Klose", "Roberto Carlos", "Cristiano Ronaldo", context);
            InsertarPregunta("¿En la década de los 90, la leyenda del fútbol Pelé fue ministro de deportes. ¿En qué país?",
                            "Brasil", "Francia", "Italia", "México", context);
            InsertarPregunta("¿Dónde se disputará el próximo Mundial de Fútbol 2014?",
                            "Brasil", "Japón", "Chile", "Portugal", context);
            InsertarPregunta("¿Cómo se llama el ex-jugador del F.C Barcelona apellidado Stoichkov?",
                            "Hristo", "Risto", "Cristo", "Bristo", context);
            InsertarPregunta("¿Cómo apodaban a Alfblacko Di Stéfano?",
                            "La saeta rubia", "La pulga", "El minero", "El crack", context);
            InsertarPregunta("¿Qué arte marcial utiliza mayor porcentaje de extremidades inferiores?",
                            "Taekwondo", "Judo", "Karate", "Boxeo", context);
            InsertarPregunta("¿Cuál es la última cinta en las artes marciales?",
                            "Negra", "Blanca", "Roja", "Café", context);
            InsertarPregunta("¿Quién ganó dos veces tres campeonatos consecutivamente en la NBA?",
                            "Michael Jordan", "Lebron James", "Brian Scalabrine", "Bill Russel", context);
            #endregion

            #region Arte y Literatura
            InsertarPregunta("¿Cuál era la nacionalidad de Heri Cartier-Bresson, considerado por muchos el padre del fotoreportaje?",
                "Francesa", "Británica", "Española", "Alemana", context);
            InsertarPregunta("¿Quién pintó el cuadro “El jardín de las delicias”?",
                            "El Bosco", "Carvaggio", "Carvaggio", "Arcimboldo", context);
            InsertarPregunta("¿Qué ciudades describe Italo Calvino en “Las ciudades invisibles”?",
                            "Ciudades imaginadas por Marco Polo", "Ciudades asiáticas", "Las que Marco Polo visitó en sus viajes", "Ciudades europeas", context);
            InsertarPregunta("¿Cuál es la ciudad fetiche del escritor Paul Auster?",
                            "Nueva York", "Londres", "París", "Chicago", context);
            InsertarPregunta("¿Quién escribió “El viejo y el mar?",
                            "Ernest Hemingway", "Norman Mailer", "Gabriel García Márquez", "Truman Capote", context);
            InsertarPregunta("¿Quién vivía en el 221B de Backer Street?",
                            "Sherlock Holmes", "Truman Capote", "Philip Marlowe", "Arthur Conan Doyle", context);
            InsertarPregunta("¿Qué fotografía, sobre todo, Anne Geddes?",
                            "Bebés", "Famosos", "Farolas", "Bebés", context);
            InsertarPregunta("¿A qué género pertenece la novela “La historia del ojo” de George Bataille?",
                            "Erótico", "Ciencia-ficción", "Negro", "Romántico", context);
            InsertarPregunta("¿Cuál de los siguientes artistas es una figura clave del dadaísmo y el surrealismo?",
                            "Max Ernst", "Claude Monet", "Vincent Van Gogh", "Rafael", context);
            InsertarPregunta("¿Qué profesión tenía Hércules Poiriot en las novelas de Agatha Christie?",
                            "Detective", "Policía", "Médico", "Paul Verlaine", context);
            InsertarPregunta("¿Quién es el autor de la obra teatral “Casa de Muñecas”?",
                            "Henrik Ibsen", "Isaac Asimov", "Émile Zola", "Sigrid Undset", context);
            InsertarPregunta("¿Qué cosa perdida buscaba Marcel Proust?",
                            "El tiempo", "El amor", "La fortuna", "La existencia", context);
            InsertarPregunta("¿Dónde muere el padre del niño protagonista de la novela “Tan fuerte, tan cerca” de Jonathan Safran-Foer?",
                            "En los atentados del World Trade Center", "En la guerra de Vietnam", "En la guerra de Irak", "En un accidente de coche", context);
            InsertarPregunta("¿Qué era Le Chat Noir, el establecimiento que anunciaban los carteles el famoso gato negro de París?",
                            "Un cabaret", "Un restaurante", "Un cine", "Una biblioteca", context);
            InsertarPregunta("Los amantes de Teruel…",
                            "Tonta ella y tonto él", "Viajaban en carrusel", "Vivían en un burdel", "Existieron en papel", context);
            InsertarPregunta("¿Quién escribió “El túnel”?",
                            "Ernesto Sabato", "Gabrial García Márquez", "Mario Vargas Llosa", "Jorge Luis Borges", context);
            InsertarPregunta("¿Quién es el autor de “El retrato de Dorian Gray”?",
                            "Oscar Wilde", "Charles Dickens", "Arthur Conan Doyle", "George Orwell", context);
            InsertarPregunta("¿Quién es el autor de “Moby Dick”?",
                            "Herman Melville", "Henry David Thoreau", "Ralph Waldo Emerson", "Henry James", context);
            InsertarPregunta("¿Qué editorial ha publicado los dos últimos libros del humorista y escritor Miguel Noguera?",
                            "Blackie Books", "Mondadori", "Alpha Decay", "Planeta", context);
            InsertarPregunta("¿Quién introdujo la lira en la lírica española?",
                            "Garcilaso de la Vega", "Luis de Góngora", "Lope de Vega", "Miguel de Cervantes", context);
            InsertarPregunta("¿Cómo se llama el protagonista de “El código Da Vinci” de Dan Brown? ",
                            "Robert Langdom", "Tom Hanks", "Mr. White", "Robert Black", context);
            InsertarPregunta("¿Cuál fue el género más cultivado por los autores de la generación del 27?",
                            "Poesía", "Ensayo", "Teatro", "Novela", context);
            InsertarPregunta("¿Qué describe una prosopografía?",
                            "El físico de una persona", "El carácter de una persona", "El fisico y el carácter de una persona", "Caricaturiza a una persona", context);
            InsertarPregunta("¿Qué escritor español firmaba como Fígaro?",
                            "Mariano José de Larra", "Federico García Lorca", "Antonio Machado", "Francisco de Quevedo", context);
            InsertarPregunta("¿A dónde pertenece la colección de relatos folklóricos llamada Mabinogion?",
                            "Gales", "Inglaterra", "Irlanda", "Escocia", context);
            InsertarPregunta("¿Quién escribió la música del himno de la Comunidad de Valencia?",
                            "José Serrano Simeón", "Maximiliano Thous Orts", "Manuel de Falla Matheu", "José Iturbi Baguena", context);
            InsertarPregunta("¿A qué obra pertenece este fragmento: ‘No es verdad, ángel de amor, que en esta apartada orilla…’?",
                            "Don Juan Tenorio", "Fígaro", "Fausto", "Quijote", context);
            InsertarPregunta("¿En qué siglo nació el artista conocido como Caravaggio?",
                            "XVI", "XIV", "XII", "XVIII", context);
            InsertarPregunta("¿Cómo se llama a la gente que no posee magia en la saga de Harry Potter?",
                            "Muggles", "Humano", "Simplón", "Impuro", context);
            InsertarPregunta("¿Qué obra de mármol de Miguel Ángel se encuentra en la Basílica de San Pedro?",
                            "La Piedad", "Moisés", "Capilla Sixtina", "David", context);
            InsertarPregunta("Gato con guantes…",
                            "No caza ratones", "Y con botas", "No rasca bigotes", "No corre al trote", context);
            InsertarPregunta("¿Qué forma es característica de las plantas de las iglesias románicas?",
                            "Cruz", "Óvalo", "Rectángulo", "Cuadrado", context);
            InsertarPregunta("La autoría de la pintua ‘El Coloso’ se ha puesto muy en duda en los últimos años. ¿A quién se le atribuye tradicionalmente?",
                            "Goya", "Sorolla", "Quevedo", "Velázquez", context);
            InsertarPregunta("¿Qué odia Mafalda?",
                            "La sopa", "El Pájaro Loco", "Los panqueques", "A Manolito", context);
            InsertarPregunta("¿A qué pintor pertenece la obra ‘Serpientes de agua’?",
                            "Gustave Klimt", "Vicent Van Gogh", "Pablo Picasso", "Roger Van der Weiden", context);
            InsertarPregunta(" Arroz con leche me quiero…",
                            "Casar", "Escapar", "Matar", "Cazar", context);
            InsertarPregunta("¿En qué está esculpida la Venus de Milo?",
                            "Mármol", "Bronce", "Piedra", "Madera", context);
            InsertarPregunta("¿Quién fue Antonio Lucio Vivaldi?",
                            "Violinista y Compositor del Barroco", "Guitarrista Clásico", "Tenor de Opera", "Clavesista Italiano", context);
            InsertarPregunta("¿Quién hizo la obra “La vida es sueño”?",
                            "Calderón de la Barca", "Miguel de Cervantes", "Feliz Lope de Vega", "Tirso de Molina", context);
            InsertarPregunta("¿Quién pintó La Capilla Sixtina?",
                            "Miguel Ángel", "Leonardo Da Vinci", "Giorgio Vasari", "Tiziano", context);
            InsertarPregunta("¿Qué filósofo dijo ‘solo sé que no sé nada’?",
                            "Sócrates", "Heráclito", "Platón", "Tales de Mileto", context);
            InsertarPregunta("¿Quién es la autora de “Los Juegos del Hambre”?",
                            "Suzanne Collins", "Blue Jeans", "Jordi Sierra i Fabra", "Bono Bidari", context);
            InsertarPregunta("¿Quién es la autor del libro “El gato negro y otros cuentos de terror”?",
                            "Edgar Allan Poe", "Manuel Broncano", "Julio-César Santoyo", "Jesús Gabán", context);
            InsertarPregunta("¿De qué siglo es representativo el arte mozárabe?",
                            "X", "IIX", "V", "XIX", context);
            InsertarPregunta("¿A quién está dedicada la torre más alta de la Sagrada Familia de Gaudí, aún pendiente de construcción?",
                            "Jesucristo", "La Virgen María", "El Espiritú Santo", "La Sagrada Familia", context);
            InsertarPregunta("¿Cómo despertó el príncipe a la Bella Durmiente?",
                            "Besándola", "Tocando palmas", "Acariciándola", "Gritándole", context);
            InsertarPregunta("¿Quién compuso la famosa canción “Bohemian Rhapsody?",
                            "Fblackdie Mercury", "John Lennon", "Elton John", "David Bowie", context);
            InsertarPregunta("¿Quién pintó ‘Idas y venidas. La Martinica’? ",
                            "Paul Gauguin", "Paul Cézanne", "Pablo Picasso", "Velázquez", context);
            InsertarPregunta("¿Qué famosa artista estaba casada con su colega, Diego Rivera?",
                            "Frida Kahlo", "María Antonieta", "Frida Alejandra", "Antonieta", context);
            InsertarPregunta("¿Qué artista expuso un orinal con el título ‘La fuente’?",
                            "Marcel Duchamp", "Andy Warhol", "Salvador Dalí", "Pablo Picasso ", context);
            InsertarPregunta("¿Qué poeta escribió ‘A galopar’?",
                            "Rafael Alberti", "Lope de Vega", "Antonio Machado", "Federico García Lorca", context);
            InsertarPregunta("¿Con que filme el director Alfonso Cuarón ganó un premio Oscar 2014?",
                            "Gravity", "Brave", "Iron Man 3", "The Lady in Number 6", context);
            InsertarPregunta("¿Quién es el autor de “El principito”?",
                            "Antoine de Saint-Exupery", "Beatriz Espejo", "Cuahutemoc Sanchez", "Ninguna es correcta", context);
            InsertarPregunta("¿De qué estilo artístico es la catedral de Santa María de Toledo?",
                            "Gótico", "Románico", "Barroco", "Ninguno", context);
            InsertarPregunta("¿Quién escribió ‘Don Juan Tenorio’?",
                            "José Zorrilla", "Camilo José Cela", "Espronceda", "García-Lorca", context);
            InsertarPregunta("¿Con qué pseudónimo se conocía al escritor José Martínez Ruiz?",
                            "Azorín", "Unamuno", "Ruiseñor", "Carrillo", context);
            InsertarPregunta("¿Quién escribió el poema cuyo inicio es “Pegasos, lindos Pegasos, caballitos de madera”?",
                            "Antonio Machado", "Rafael Alberti ", "Pablo Neruda", "Federico García Lorca", context);
            InsertarPregunta("¿Dónde nació Shakespeare?",
                            "Stratford-upon-Avon", "Albacete", "Surrey", "New Castle", context);
            InsertarPregunta("¿Cuál es el género teatral intermedio entre la comedia y la tragedia?",
                            "Drama", "Zarzuela", "Entremés", "Farsa", context);
            InsertarPregunta("¿Qué ingeniero y arquitecto valenciano construyó el puente del Alamillo de Sevilla?",
                            "Calatrava", "Gaudí", "Moneo", "Villanueva", context);
            InsertarPregunta("¿Quién es el autor del libro “Once minutos”?",
                            "Paulo Coelho", "Pablo Neruda", "Ana María Matute", "Gabriel Garcia Marquez", context);
            InsertarPregunta("¿De qué material está hecho un saxofón?",
                            "Latón", "Cobre", "Hierro", "Acero", context);
            InsertarPregunta("¿Quién pintó el famoso cuadro de pop art 'Latas de sopa Campbell'?",
                            "Andy Warhol", "Kurt Schwitters", "Laurence Mills", "Vicent van Gogh", context);
            InsertarPregunta("¿Quién es el autor de la novela ‘Crimen y Castigo’?",
                            "Fiódor Dostoyevski", "Antón Chéjov", "Vladimir Navokov", "León Tolstói", context);
            InsertarPregunta("¿Quién es el autor de “La Celestina”?",
                            "Fernando de Rojas", "Jorge Manrique", "Miguel de Cervantes", "Anónima", context);
            InsertarPregunta("¿Qué profesión tenía Hércules Poirot en las novelas de Agatha Christie?",
                            "Detective", "Policía", "Médico", "Bombero", context);
            InsertarPregunta("¿Qué animal son los polacos en la novela gráfica sobre el Holocausto nazi ‘Maus’?",
                            "Cerdos", "Ratones", "Gatos", "Perros", context);
            #endregion

            GuardarPreguntasOpciones(context);
        }

        private static void InsertarPregunta(string pregunta, string opcioncorrecta, string otraopcion1, string otraopcion2, string otraopcion3, ConquestUnitContext context)
        {
            npregunta++;
            preguntas.Add(new Pregunta() { PreguntaId = npregunta, TextoPregunta = pregunta });
            opciones.Add(new Opcion() { OpcionId = ++nopcion, TextoOpcion = opcioncorrecta, EsRespuesta = true, PreguntaId = npregunta });
            opciones.Add(new Opcion() { OpcionId = ++nopcion, TextoOpcion = otraopcion1, EsRespuesta = false, PreguntaId = npregunta });
            opciones.Add(new Opcion() { OpcionId = ++nopcion, TextoOpcion = otraopcion2, EsRespuesta = false, PreguntaId = npregunta });
            opciones.Add(new Opcion() { OpcionId = ++nopcion, TextoOpcion = otraopcion3, EsRespuesta = false, PreguntaId = npregunta });
        }

        private static void GuardarPreguntasOpciones(ConquestUnitContext context)
        {
            try
            {
                var queryPreguntas = @"INSERT INTO Pregunta (PreguntaId,TextoPregunta) VALUES (@0,@1);";
                var queryOpciones = @"INSERT INTO Opcion (OpcionId,TextoOpcion,EsRespuesta,PreguntaId) VALUES (@0,@1,@2,@3);";
                // Start transaction
                context.conn.BeginTransaction();
                try
                {
                    // Execute commands...
                    foreach (var pregunta in preguntas)
                    {
                        context.conn.CreateCommand(queryPreguntas, pregunta.PreguntaId, pregunta.TextoPregunta).ExecuteNonQuery();
                    }
                    foreach (var opcion in opciones)
                    {
                        context.conn.CreateCommand(queryOpciones, opcion.OpcionId, opcion.TextoOpcion, opcion.EsRespuesta, opcion.PreguntaId).ExecuteNonQuery();
                    }
                    // Commit transaction
                    context.conn.Commit();
                }
                catch (Exception)
                {
                    // Rollback transaction
                    context.conn.Rollback();
                }
            }
            finally
            {
                // Close connection
                //context.conn.Close();
            }
        }
    }
}
