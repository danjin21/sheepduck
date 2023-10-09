using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{

    GameObject Nexus;


    public void SaveAndLoadAndUserDataSync()
    {
        SaveDataSystem.Instance.SaveGameData();

        // 유저 정보 불러오기
        SaveDataSystem.Instance.LoadGameData();
        // 유저 정보 모두 넣기
        Managers.UserDataManager.UserDataSync(SaveDataSystem.Instance.data);
    }

    public void GetGold()
    {

        SaveDataSystem.Instance.data.gold += 10;
        SaveAndLoadAndUserDataSync();

    }

    public void GetLevel()
    {
        SaveDataSystem.Instance.data.level += 1;
        SaveAndLoadAndUserDataSync();
    }

    public void GetMana()
    {
        SaveDataSystem.Instance.data.mana += 1;
        SaveAndLoadAndUserDataSync();
    }

    public void GetStage()
    {
        int currentStage = SaveDataSystem.Instance.data.currentStage;
        int maxStage = SaveDataSystem.Instance.data.maxStage;

        if (maxStage == currentStage)
        {
            SaveDataSystem.Instance.data.maxStage += 1;
        }

        if (maxStage >= 10)
        {

            // World 넘어가기      
            SaveDataSystem.Instance.data.maxWorld = SaveDataSystem.Instance.data.currentWorld+1;
            SaveDataSystem.Instance.data.currentWorld = SaveDataSystem.Instance.data.currentWorld + 1;
            SaveDataSystem.Instance.data.maxStage = 1;
        }

        

        SaveAndLoadAndUserDataSync();
    }
}

