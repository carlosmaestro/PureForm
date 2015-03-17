using UnityEngine;

using System.Collections;
using Mono.Data.Sqlite; 
using System.IO;
using System.Data; 
using System;
using UnityEngine.UI;


public class Connection : MonoBehaviour
{
		public Image image;
		private IDbConnection dbcon;
		private Button button;
		public Text path;

		// Use this for initialization
		void Start ()
		{
			
				button = GetComponent<Button> ();	
        
        
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		

		
		public void OpenDB (string p)
		{
		
				string connection = "URI=file:" + Application.dataPath + "/bases/" + p; // we set the connection to our database
		
				//connection = "URI=file:" + p; // like this will NOT work on android
		
				Debug.Log (connection);
				path.text = connection;
		
				dbcon = new SqliteConnection (connection);
		
				dbcon.Open ();

				IDbCommand dbcmd = dbcon.CreateCommand ();

				string sqlQuery = "SELECT id,name,image " + "FROM images";
				dbcmd.CommandText = sqlQuery;
				IDataReader reader = dbcmd.ExecuteReader ();
		
		
				while (reader.Read()) {
						int id = reader.GetInt32 (0);
						string nome = reader.GetString (1);
						var tex = new Texture2D (2, 2);
						tex.LoadImage (getImage (reader));
						//Texture2D textura = GetComponent<Renderer> ().material.GetTexture = tex;
//						button.image.overrideSprite = Sprite.Create (tex, new Rect (0, 0, tex.width, tex.height),
//			                                            new Vector2 (0.5f, 0.5f),
//			                                            40);
						image.overrideSprite = Sprite.Create (tex, new Rect (0, 0, tex.width, tex.height),
			                                             new Vector2 (0.5f, 0.5f),
			                                             40);

						//int rand = reader.GetInt32 (2);
			
						Debug.Log ("value= " + id + "  nome =" + nome);
				}
		
				reader.Close ();
				reader = null;
				dbcmd.Dispose ();
				dbcmd = null;
				dbcon.Close ();
        
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
    
    
    
}
