using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase;
//using Firebase.Database;
using System;

public class DBTest : MonoBehaviour
{
   /* public string databaseUrl = "https://arproject-22c5f-default-rtdb.asia-southeast1.firebasedatabase.app/";
    // Start is called before the first frame update
    void Start()
    {
        print("연결시작?");
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(databaseUrl);
        print("연결됐나?");
        SaveData();
    }

    // Update is called once per frame
    void SaveData()
    {
        Mans Data1 = new Mans("루시안", 35, 70.0f, false);
        Mans Data2 = new Mans("세나", 35, 70.0f, false);

        string _json_Data1 = JsonUtility.ToJson(Data1);
        string _json_Data2 = JsonUtility.ToJson(Data2);

        DatabaseReference refData = FirebaseDatabase.DefaultInstance.RootReference;
        refData.Child("LOL").Child("data1").SetRawJsonValueAsync(_json_Data1);
        refData.Child("LOL").Child("data2").SetRawJsonValueAsync(_json_Data2);

        print("데이터 저장 시작!");
    }
}

public class Mans
{
    public string name;
    public int age;
    public float weight;
    public bool isMarry;

    public Mans(string _name, int _age, float _weight, bool _isMarry)
    {
        name = _name;
        age = _age;
        weight = _weight;
        isMarry = _isMarry;
    }*/
}
