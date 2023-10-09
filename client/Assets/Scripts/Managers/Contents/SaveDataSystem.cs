using System.IO;
using UnityEngine;
using System;
using System.Collections.Generic;

public class SaveDataSystem : MonoBehaviour
{
    static GameObject container;

    public List<Duck> DuckData = new List<Duck>();

    // ---싱글톤으로 선언--- //
    static SaveDataSystem instance;
    public static SaveDataSystem Instance
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "SaveDataSystem";
                instance = container.AddComponent(typeof(SaveDataSystem)) as SaveDataSystem;
                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }

    // --- 게임 데이터 파일이름 설정 ("원하는 이름(영문).json") --- //
    string GameDataFileName = "SaveData.json";

    // --- 저장용 클래스 변수 --- //
    public SaveData data = new SaveData();



    // 불러오기
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        {
            // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<SaveData>(FromJsonData);
            print("불러오기 완료");
        }

        // 오리 데이터 불러오기

        

        string filePath_Duck = Application.persistentDataPath + "/" + "DuckData.json";

        if(File.Exists(filePath_Duck))
        {
            string jdata = File.ReadAllText(filePath_Duck);
            DuckData = JsonUtility.FromJson<Serialization<Duck>>(jdata).target;

            // 복호화 하는법

            //string code = File.ReadAllText(filePath_Duck);
            //byte[] bytes = System.Convert.FromBase64String(code);
            //string jdata = System.Text.Encoding.UTF8.GetString(bytes);
            //DuckData = JsonUtility.FromJson<Serialization<Duck>>(jdata).target;
        }


        foreach (Duck t in DuckData)
        {
            Debug.Log($"오리정보 불러오기 {t.name }/{t.hp }/{t.maxHp }/{t.atk }");
        }

     
        // 리스트에서 조건 값 찾을때
        // DuckData.FindAll(x=>x.Type == tabName);
       
   
        // 오리 데이터 불러오기 끝
    }


    // 저장하기
    public void SaveGameData()
    {

        // 오리 데이터 넣기

        //List<Duck> DuckData = new List<Duck>();

        //Duck ADuck = new Duck("A", 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
        //Duck BDuck = new Duck("B", 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2);
        //Duck CDuck = new Duck("B", 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3);
        //Duck DDuck = new Duck("B", 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3);

        //DuckData.Add(ADuck);
        //DuckData.Add(BDuck);
        //DuckData.Add(CDuck);
        //DuckData.Add(DDuck);


        // List도 저장할 수 있게함 
        string jdata = JsonUtility.ToJson(new Serialization<Duck>(DuckData));
        string filePath_Duck = Application.persistentDataPath + "/" + "DuckData.json";


        // 암호화 하는 방법
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        //string code = System.Convert.ToBase64String(bytes);
        // File에 jdata 대신 code를 넣으면 됨


        File.WriteAllText(filePath_Duck, jdata);

        // 오리 데이터 넣기 끝


        // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, ToJsonData);

        // 올바르게 저장됐는지 확인 (자유롭게 변형)
        print("저장 완료");
        for (int i = 0; i < data.isUnlock.Length; i++)
        {
            print($"{i}번 챕터 잠금 해제 여부 : " + data.isUnlock[i]);
        }

        print($"골드 : {data.gold}");


    }
}