using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    public static PlayerData data;

    public static void SaveData(Character player, EnemySpawn spawn) {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.crystal";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerData LoadPlayer() {
        string path = Application.persistentDataPath + "/player.crystal";

        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void DeleteSaveFile() {
        string path = Application.persistentDataPath + "/player.crystal";

        if (File.Exists(path)) {
            File.Delete(path);
            Debug.Log("Save file deleted at " + path);
        } else {
            Debug.Log("No save file to delete at " + path);
        }
    }
}
