using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class BlazeSave {

	static DESCryptoServiceProvider des = new DESCryptoServiceProvider();

    static string FolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games";
    static string FolderName = Application.productName;
    static string FullFolderPath = FolderPath + "/" + FolderName;
    
    private static string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
		
		// Encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
		
		return hashString.PadLeft(32, '0');
	}

	public static void SaveData<T>(string dataName, T objectToWrite , string cryptoKey = null, bool obfName = false)
	{
		//CryptoKeys
		byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8 };
		byte[] iv = { 1, 2, 3, 4, 5, 6, 7, 8 };
		if (cryptoKey != null) {
			key = Encoding.ASCII.GetBytes (cryptoKey.Substring(0,8));
			iv = Encoding.ASCII.GetBytes (cryptoKey.Substring((cryptoKey.Length-8), 8));
		}

		//Path and name obfuscation

		if (obfName)
            dataName = Md5Sum(dataName);

		if (!Directory.Exists (FullFolderPath))
            Directory.CreateDirectory(FullFolderPath);

		string fullPath = FullFolderPath + "/" + dataName;

		CryptoStream cryptoStream;

		try{
			using (Stream stream = File.Open(fullPath, FileMode.Create))
			using (cryptoKey == null ? null : (cryptoStream = new CryptoStream(stream, des.CreateEncryptor(key, iv), CryptoStreamMode.Write)) )
			{
				var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				if(cryptoKey == null){
					binaryFormatter.Serialize(stream, objectToWrite);
				}else{
					binaryFormatter.Serialize(cryptoStream, objectToWrite);
				}
				stream.Close();
				if(cryptoKey != null){
					cryptoStream.Close();
				}
			}
		}catch(IOException e){
			Debug.Log("ERROR: "+e.Message);
		}
	}

	public static T LoadData<T>(string dataName, string cryptoKey = null, bool obfName = false)
	{
		//CryptoKeys
		byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8 };
		byte[] iv = { 1, 2, 3, 4, 5, 6, 7, 8 };
		if (cryptoKey != null) {
			key = Encoding.ASCII.GetBytes (cryptoKey.Substring(0,8));
			iv = Encoding.ASCII.GetBytes (cryptoKey.Substring((cryptoKey.Length-8), 8));
		}

		//Path and name obfuscation

		if (obfName)
            dataName = Md5Sum(dataName);

		string fullPath = FullFolderPath + "/" + dataName;

		CryptoStream cryptoStream;

		try{
			using (Stream stream = File.Open(fullPath, FileMode.Open))
			using (cryptoKey == null ? null : cryptoStream = new CryptoStream(stream, des.CreateDecryptor(key, iv), CryptoStreamMode.Read))
			{
				var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				if(cryptoKey == null){
					return (T)binaryFormatter.Deserialize(stream);
				}else{
					return (T)binaryFormatter.Deserialize(cryptoStream);
				}
				stream.Close();
				if(cryptoKey != null){
					cryptoStream.Close();
				}
			}
		}catch(IOException e){
			Debug.Log("ERROR: "+e.Message);
			return default(T);
		}
	}

	public static bool Exists(string dataName, bool obfName = false)
    {
		if (obfName)
            dataName = Md5Sum(dataName);

		string fullPath = FullFolderPath + "/" + dataName;

		return File.Exists(fullPath);
	}

}
