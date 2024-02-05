using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;

public class FileDataHandler: MonoBehaviour
{
    private string dataDirPath = "";
    private string dataFileName = "";
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                
                    Debug.LogError("Error occured when trying to load file at path: "
                        + fullPath + " and backup did not work.\n" + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        //Debug.Log(data.Records[0].Count);
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // create the directory the file will be written to if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            // serialize the C# game data object into Json
            string dataToStore = "";
            for(int i=0;i<7;i++)
            {
                for (int j = 0; j < data.Records[0].Count; j++)
                {
                    dataToStore += JsonUtility.ToJson(data.Records[i][j], true);
                } 
            }
            Debug.Log(dataToStore);
            // write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
            //StartCoroutine(Example());
            //StartCoroutine(Upload());
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }
    IEnumerator Upload()
    {
        using (UnityWebRequest www = UnityWebRequest.Post("https://iitkgpacin-my.sharepoint.com/:f:/g/personal/sreejanshivam04_kgpian_iitkgp_ac_in/Etwdw2orG-hHsGtWYHRrgJEBN8KYsPd-7iDP9mbNQlm29g?e=QSBzLL", "{ \"field1\": 1, \"field2\": 2 }", "data.json"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}