using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Librerías que necesitamos
using System.Data;
using System.IO;
using Mono.Data.Sqlite;

public class RankingManager : MonoBehaviour
{
    //Variable para controlar la ruta de la base de datos, constructor de la ruta, y el nombre de la base de datos
    string rutaDB;
    string strConexion;
    string DBFileName = "RankingDB.sqlite";

    //Variable para trabajar con las conexiones
    IDbConnection dbConnection;
    //Para poder ejecutar comandos
    IDbCommand dbCommand;
    //Variable para leer
    IDataReader reader;

    //Lista para el Ranking
    private List<string> rankings = new List<string>();

    public Text text1;
    public Text text2;

    // Start is called before the first frame update
    void Start()
    {
        //InsertarPuntos("Jose", 50);
        //BorrarPuntos(2);
        //ObtenerRanking();
        //BorrarPuntosExtra();
        //MostrarRanking();
    }

    //Método para abrir la DB
    void AbrirDB()
    {
        // Crear y abrir la conexión
        // Comprobar en que plataforma estamos
        // Si es el Editor de Unity mantenemos la ruta
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            rutaDB = Application.dataPath + "/StreamingAssets/" + DBFileName;
        }
        //Si estamos en PC
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            rutaDB = Application.dataPath + "/StreamingAssets/" + DBFileName;
        }
        //Si es Android
        else if (Application.platform == RuntimePlatform.Android)
        {
            rutaDB = Application.persistentDataPath + "/" + DBFileName;
            // Comprobar si el archivo se encuentra almecenado en persistant data
            if (!File.Exists(rutaDB))
            {
                // Almaceno el archivo en load db
                WWW loadDB = new WWW("jar;file://" + Application.dataPath + "!/assets/" + DBFileName);
                while (!loadDB.isDone)
                {

                }
                // Copio el archivo a persistant data
                File.WriteAllBytes(rutaDB, loadDB.bytes);
            }
        }

        strConexion = "URI=file:" + rutaDB;
        dbConnection = new SqliteConnection(strConexion);
        dbConnection.Open();
    }
    //Método para obtener el Ranking de la DB
    void ObtenerRanking()
    {
        //Primero dejamos la lista de Rankings limpia
        rankings.Clear();
        //Abrimos la DB
        AbrirDB();
        // Crear la consulta
        dbCommand = dbConnection.CreateCommand();
        string sqlQuery = "select * from Ranking Order by Score Desc limit 5";
        dbCommand.CommandText = sqlQuery;

        // Leer la base de datos
        reader = dbCommand.ExecuteReader();
        while (reader.Read())
        {
            rankings.Add(reader.GetString(1) + "  " + reader.GetInt32(2));
        }
        reader.Close();
        reader = null;
        //Cerramos la DB
        CerrarDB();
        //Ordenamos la lista
    }
    //Método para insertar puntos en la DB
    public void InsertarPuntos(string n, int s)
    {
        //Abrimos la DB
        AbrirDB();
        // Crear la consulta
        dbCommand = dbConnection.CreateCommand();
        string sqlQuery = String.Format("INSERT INTO Ranking(Name, Score) values(\"{0}\",\"{1}\")", n, s);
        dbCommand.CommandText = sqlQuery;
        dbCommand.ExecuteScalar();
        //Cerramos la DB
        CerrarDB();
    }
    //Método para mostrar el ranking en la UI
    public void MostrarRanking()
    {
        //Obtener el ranking de la DB
        ObtenerRanking();
        //Hacemos una pasada por la lista para ir posicionando los puntos en la UI
        foreach  (string var in rankings)
        {
            text1.text = text1.text + var + "\n";
            text2.text = text2.text + var + "\n";
        }
    }

    //Método para cerrar la DB
    void CerrarDB()
    {
        // Cerrar las conexiones
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
}
