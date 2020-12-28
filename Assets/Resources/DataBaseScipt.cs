using System.Collections.Generic;
using UnityEngine;

public class DataBaseScipt : MonoBehaviour
{
    List<InfoDataBase> info = new List<InfoDataBase>();

    void Start()
    {
        TextAsset dataBase = Resources.Load<TextAsset>("database");

        string[] data = dataBase.text.Split(new char[] { '\n' } );
        
        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ';' });

            InfoDataBase b = new InfoDataBase();

            int.TryParse(row[0], out b.id);
            b.word = row[1];
            b.answer1 = row[2];
            b.answer2 = row[3];
            b.answer3 = row[4];
            b.answer4 = row[5];
            

            info.Add(b);


        }

        foreach (InfoDataBase b in info)
        {
            Debug.Log(b.word);
        }
    }
}