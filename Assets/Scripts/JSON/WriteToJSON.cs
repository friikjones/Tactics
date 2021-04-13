using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WriteToJSON : MonoBehaviour
{

    public CharacterHolder dummy_char = new CharacterHolder();
    public List<CharacterHolder> characterList;

    [ContextMenu("Reset List")]
    void DropList()
    {
        characterList = new List<CharacterHolder>();
    }

    [ContextMenu("Populate List")]
    void AddtoList()
    {
        AddtoList(dummy_char);
    }

    void AddtoList(CharacterHolder target_char)
    {
        CharacterHolder temp_char = new CharacterHolder();
        temp_char.Clone(target_char);
        characterList.Add(temp_char);
    }

    [ContextMenu("Write to JSON")]
    void WritetoJSON()
    {
        foreach (CharacterHolder character in characterList)
        {
            string json = JsonUtility.ToJson(character);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/" + character.ID + ".json", json);
            Debug.Log(character.ID + " saved at " + Application.persistentDataPath);
        }
    }

    [ContextMenu("Read from JSON")]
    void ReadfromJSON()
    {
        DropList();
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("*.json");
        foreach (FileInfo f in info)
        {
            string json = System.IO.File.ReadAllText(f.FullName);
            CharacterHolder temp_char = new CharacterHolder();
            JsonUtility.FromJsonOverwrite(json, temp_char);
            AddtoList(temp_char);
        }
    }
}
