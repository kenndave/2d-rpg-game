using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class save
{
    public static void saveplayer (int player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/robot.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        playerdata data = new playerdata(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static playerdata loadplayer()
    {
        string path = Application.persistentDataPath + "/robot.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            playerdata data = formatter.Deserialize(stream) as playerdata;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("save not found" + path);
            return null;
        }
    }
}
