using System.Collections;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

	//static string path = "/Saves/";
	static string filename = "/data";
	static string ext = ".sav";
	static int[] defaultGameInfo = {1,0,0,0,0,0,0,0};
	static bool[] defaultFirstTimeObjs = {false,false,false,true};
	static float[] defaultUpgradeStats = {50,10,100};
	static int[] defaultShopVars = {0,0,0,0,75,125,200,500};

	public static void SaveGame (GameData data)
	{
		BinaryFormatter bf = new BinaryFormatter();
		//FileStream stream = new FileStream(Application.persistentDataPath + "/game.sav", FileMode.Create);
		FileStream file = File.Create(Application.persistentDataPath + filename + ext);

		//GameData data = new GameData();

		bf.Serialize(file, data);
		file.Close();
	}

	public static GameData LoadGame ()
	{
		if (File.Exists(Application.persistentDataPath + filename + ext))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = new FileStream(Application.persistentDataPath + filename + ext, FileMode.Open);

			GameData data = (GameData)bf.Deserialize(file);

			
			file.Close();
			return data;
		}
		else
		{
			return new GameData(0, defaultGameInfo, defaultFirstTimeObjs, defaultUpgradeStats, 1, defaultShopVars);
		}
	}
}

[Serializable]
public class GameData {
	public float playerHealth;
	public int[] gameInfo;
	public bool[] firstTimeObjs;
	public float[] upgradeableStats;
	public int upgradeableCannon;
	public int[] shopVars;

	public GameData (float playerHealth, int[] gameInfo, bool[] firstTimeObjs, float[] upgradeableStats, int upgradeableCannon, int[] shopVars)
	{
		this.playerHealth = playerHealth;
		this.gameInfo = gameInfo;
		this.firstTimeObjs = firstTimeObjs;
		this.upgradeableStats = upgradeableStats;
		this.upgradeableCannon = upgradeableCannon;
		this.shopVars = shopVars;
	}
}
