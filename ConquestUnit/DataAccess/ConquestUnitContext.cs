﻿using SQLite.Net;
using System.IO;
using ConquestUnit.Model;

namespace ConquestUnit
{
    public class ConquestUnitContext
    {
        public string ruta;
        public SQLiteConnection conn;
        public void InitDatabase()
        {
            ruta = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "ConquestUnit.sqlite");
            CreateDatabase();
            DataInitialize.Initialize(this);
        }

        public void InitDatabase(string path)
        {
            ruta = Path.Combine(path, "ConquestUnit.sqlite");
            CreateDatabase();
            DataInitialize.Initialize(this);
        }

        private void CreateDatabase()
        {
            conn =new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), ruta);

            if (!TableExists<Pregunta>(conn))
            {
                conn.CreateTable<Pregunta>();
            }
            if (!TableExists<Opcion>(conn))
            {
                conn.CreateTable<Opcion>();
            }
        }

        public int Insertar<T>(T obj)
        {
            return conn.InsertOrReplace(obj, typeof(T));
        }

        public int Eliminar<T>(int objId)
        {
            return conn.Delete<T>(objId);
        }

        public T Obtener<T>(int objId) where T : class
        {
            return conn.Get<T>(objId);
        }

        public T Encontrar<T>(int objId) where T : class
        {
            return conn.Find<T>(objId);
        }

        public TableQuery<T> Listar<T>() where T : class
        {
            return conn.Table<T>();
        }

        private static bool TableExists<T>(SQLiteConnection connection)
        {
            //var cmd = connection.CreateCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='" + typeof(T).Name + "'");
            var info = connection.GetTableInfo(typeof(T).Name);
            return info.Count != 0;
        }
    }
}
