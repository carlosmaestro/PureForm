using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using System.Net.Sockets;
using System;
using UnityEngine.UI;




public class Connection : MonoBehaviour
{
	//public Image image;
	private IDbConnection dbcon;
	//		private Button button;
	//public Text path;
    public Text erroText;
    private string databaseName = "PureFormDataB";
    string erro = "";
	void Start ()
	{
        TesDB();
	}

    public void TesDB()
    {
        try
        {
            erro += "1";
            //string conn = "URI=file:" + Application.persistentDataPath + "/" + databaseName; //Path to database.
            //Debug.Log(Application.dataPath + databaseName);
            //IDbConnection dbconn;
            erro += "2";
            string filepath = Application.persistentDataPath + "/" + databaseName;

            if (!File.Exists(filepath))
            {
                erro += "3";
                byte[] bases = new byte[new Byte()];
                File.WriteAllBytes(filepath, bases);
                erro += "4";
                SqliteConnection.CreateFile(filepath);
                
            }

                string conn = "URI=file:" + Application.persistentDataPath + "/" + databaseName; //Path to database.
                //Debug.Log(Application.dataPath + databaseName);
                IDbConnection dbconn;

                dbconn = (IDbConnection)new SqliteConnection(conn);
            erro += "5";

            dbconn.Open();
            erro += "6";
            IDbCommand dbcmd = dbconn.CreateCommand();
            erro += "7";
            //dbcmd.CommandText = "CREATE DATABASE IF NOT EXITS 'PureFormDataBase'";
            //dbcmd.ExecuteNonQuery();
            string createtable = "CREATE TABLE IF NOT EXISTS  `gems` (`id`	INTEGER PRIMARY KEY AUTOINCREMENT,`name`TEXT NOT NULL,`nivel`INTEGER NOT NULL,`combination` INTEGER NOT NULL,`orderItem`INTEGER NOT NULL);";

            dbcmd.CommandText = createtable;
            dbcmd.ExecuteNonQuery();

            dbcmd.Dispose();
            dbconn.Close();
            dbcmd = null;
            erro += "8";
        }
        catch (Exception e)
        {
           // erroText.text = erro + e.Message;
        }
       
    }

    public IDbConnection GetConnection()
    {
        string conn = "URI=file:" + Application.persistentDataPath + "/" + databaseName; //Path to database.
        //Debug.Log(Application.dataPath + databaseName);
        IDbConnection dbconn;

        dbconn = (IDbConnection)new SqliteConnection(conn);
        return dbconn;
    }

    public Item GetItemById(int idItem){
        Item item = new Item();

        IDbConnection dbconn;
        dbconn = GetConnection();

        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM gems WHERE id = " +idItem + ";";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            int nivel = reader.GetInt32(2);

            int combination = reader.GetInt32(3);
            int order = reader.GetInt32(4);

            bool iscombination = false;
            if (combination == 1)
            {
                iscombination = true;
            }
            item = new Item(id, nivel, name, iscombination, order);

            //Debug.Log("value= " + value + "  name =" + name + "  random =" + rand);
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;


        return item;
    }


    public List<Item> GetItens()
    {
        IDbConnection dbconn;
        dbconn = GetConnection();

        List<Item> itens = new List<Item>();

        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM gems";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            int nivel = reader.GetInt32(2);

            int combination = reader.GetInt32(3);
            int order = reader.GetInt32(4);

            bool iscombination = false;
            if(combination == 1){
                iscombination = true;
            }
            itens.Add(new Item(id, nivel, name, iscombination, -1));

            //Debug.Log("value= " + value + "  name =" + name + "  random =" + rand);
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return itens;
    }

    public void CreateItem(Item item)
    {
        IDbConnection dbconn;
        dbconn = GetConnection();

        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        int intCombination = 0;
        if (item.isCombination)
        {
            intCombination = 1;
        }
        string insert = "INSERT INTO gems(name, nivel, combination, orderItem) values('" + item.itemType + "', " + item.itemLevel + ", " + intCombination + ", " + item.orderEquip + ");";

        dbcmd.CommandText = insert;
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbconn.Close();
        dbcmd = null;
    }
    
	public void OpenDB ()
	{
		try {

			string conn = "URI=file:" + Application.persistentDataPath + "/" + databaseName; //Path to database.
			Debug.Log (Application.dataPath + databaseName);
			IDbConnection dbconn;

            string filepath = Application.persistentDataPath + "/" + databaseName;

			if (!File.Exists (filepath)) {

				SqliteConnection.CreateFile (filepath);
			} 

			dbconn = (IDbConnection)new SqliteConnection (conn);
			dbconn.Open (); //Open connection to the database.
			IDbCommand dbcmd = dbconn.CreateCommand ();

            string createtable = "CREATE TABLE IF NOT EXISTS  `gems` (`id`	INTEGER PRIMARY KEY AUTOINCREMENT,`name`TEXT NOT NULL,`nivel`INTEGER NOT NULL,`combination` INTEGER NOT NULL,`order`	INTEGER);";

			dbcmd.CommandText = createtable;
			dbcmd.ExecuteNonQuery ();


			string sqlQuery = "SELECT * FROM gems";
			dbcmd.CommandText = sqlQuery;
			IDataReader reader = dbcmd.ExecuteReader ();


			while (reader.Read()) {
				int value = reader.GetInt32 (0);
				string name = reader.GetString (1);
				int rand = reader.GetInt32 (2);

				Debug.Log ("value= " + value + "  name =" + name + "  random =" + rand);
			}

			reader.Close ();
			reader = null;
			dbcmd.Dispose ();
			dbcmd = null;
			dbconn.Close ();
			dbconn = null;
		} catch (Exception e) {
			Debug.Log( e.Message);
		}

	}


	static byte[] GetBytes (SqliteDataReader reader)
	{
		const int CHUNK_SIZE = 2 * 1024;
		byte[] buffer = new byte[CHUNK_SIZE];
		long bytesRead;
		long fieldOffset = 0;
		using (MemoryStream stream = new MemoryStream()) {
			while ((bytesRead = reader.GetBytes(2, fieldOffset, buffer, 0, buffer.Length)) > 0) {
				stream.Write (buffer, 0, (int)bytesRead);
				fieldOffset += bytesRead;
			}
			return stream.ToArray ();
		}
	}

	public byte[] getImage (IDataReader dr)
	{
		try {
			object image = dr [2];
			if (!Convert.IsDBNull (image))
				return (byte[])image;
			else
				return null;
		} catch (Exception) {
			return null;
		}
	}

	private void CreateIfMissing (string path)
	{
		bool folderExists = Directory.Exists (path);
		if (!folderExists)
			Directory.CreateDirectory (path);
	}
	IEnumerator CopyFileAsyncOnAndroid ()
	{
		string fromPath = Application.streamingAssetsPath + "/";
		//In Android = "jar:file://" + Application.dataPath + "!/assets/" 
		string toPath = Application.persistentDataPath + "/";

		string[] filesNamesToCopy = new string[] { "coress.sqlite3" };
		foreach (string fileName in filesNamesToCopy) {
			Debug.Log ("copying from " + fromPath + fileName + " to " + toPath);
			WWW www1 = new WWW (fromPath + fileName);
			yield return www1;
			Debug.Log ("yield done");
			File.WriteAllBytes (toPath + fileName, www1.bytes);
			Debug.Log ("file copy done");
		}
		//ADD YOUR CALL TO NATIVE CODE HERE
		//Note: 4 small files can take a second to finish copy. so do it once and
		//set a persistent flag to know you don`t need to call it again
	}


}


//		//string filepath = "URI=file:" + Application.dataPath + "/cores"; // we set the connection to our database
//		
//		string filepath = Application.persistentDataPath + "/" + p;
//		//if (!File.Exists (filepath)) {
//		
//		// if it doesn't ->
//		
//		// open StreamingAssets directory and load the db ->
//		
//		//WWW loadDB = new WWW ("file:///" + Application.dataPath + "!/assets/" + p);  // this is the path to your StreamingAssets in android
//		byte[] file = File.ReadAllBytes (Application.dataPath + "/Bases/" + p);
//		
//		//				while (!loadDB.isDone) {
//		//				}  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
//		
//		// then save to Application.persistentDataPath
//		
//		File.WriteAllBytes (filepath, file);
//		path.text =  allfiles + "  - caminho nao exite";
//		
//		//} else {
//		//						path.text =  allfiles + "  - caminho exite";
//        //				}
//        
//        //connection = "URI=file:" + p; // like this will NOT work on android
//        
//  
//void OpenDatabase(string dbFilePath)
//    {
//        try
//        {
//            dbConnection = new SqliteConnection(dbFilePath);
//            if(dbConnection != null) {
//                dbConnection.Open();
 
//                using (dbCommand = new SqliteCommand(dbConnection))
//                {
//                    dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS PlayedGames (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Score INTEGER NOT NULL DEFAULT 0)";
//                    dbCommand.Prepare();
 
//                    dbCommand.ExecuteNonQuery();
//                }
//            }
//        } catch (SqliteException ex) {
//            WriteToFile(ex.StackTrace);
//        } catch (System.Exception ex) {
//            WriteToFile(ex.Message);
//        } finally {
//            if(reader != null) {            
//                reader.Close();
//                reader = null;
//            }
         
//            if(dbCommand != null) {
//                dbCommand.Dispose();
//                dbCommand = null;
//            }
         
//            if(dbConnection != null) {            
//                dbConnection.Close();
//                dbConnection = null;
//            }
//        }
//    }