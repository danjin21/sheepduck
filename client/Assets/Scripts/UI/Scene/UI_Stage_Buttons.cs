using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Stage_Buttons : MonoBehaviour
{

    public int stage = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageButton()
    {
        SaveDataSystem.Instance.data.currentStage = stage;
        // 유저 정보 저장하기
        SaveDataSystem.Instance.SaveGameData();

        Managers.Scene.LoadScene(Define.Scene.Game);
    }


}
