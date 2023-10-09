﻿//using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LobbyScene : UI_Scene
{
    public UI_Stat StatUI { get; private set; }
    //public UI_Inventory InvenUI { get; private set; }
    //public UI_KeySetting KeySettingUI { get; private set; }
    //public UI_Skill SkillUI { get; private set; }

    public Text Hp;
    public Text Mp;
    public Text Exp;

    public GameObject MapAlert;

    public GameObject SelectStage;

    // 스테이지 버튼들
    public List<GameObject> StageButtons = new List<GameObject>();

    enum Buttons
    {
        //ChangeButton,
    }


    public override void Init()
    {
        base.Init();

        // 유저 정보 불러오기
        SaveDataSystem.Instance.LoadGameData();
        // 유저 정보 모두 넣기
        Managers.UserDataManager.UserDataSync(SaveDataSystem.Instance.data);

        // 유저 정보 저장하기
        SaveDataSystem.Instance.SaveGameData();

        StatUI = GetComponentInChildren<UI_Stat>();
        //InvenUI = GetComponentInChildren<UI_Inventory>();
        //KeySettingUI = GetComponentInChildren<UI_KeySetting>();
        //SkillUI = GetComponentInChildren<UI_Skill>();

        //Hp = GameObject.Find("Hp/HpText").GetComponent<Text>();
        //Mp = GameObject.Find("Mp/MpText").GetComponent<Text>();
        //Exp = GameObject.Find("Exp/ExpText").GetComponent<Text>();


        //MapAlert = GameObject.Find("MapAlert");
        //MapAlert.SetActive(false);

        //SkillUI.gameObject.SetActive(false);

        Bind<Image>(typeof(Buttons));

        //GetImage((int)Buttons.ChangeButton).gameObject.BindEvent(Change);

        //처음에 UI 안보이게 하는거
        //StatUI.gameObject.SetActive(false);
        //InvenUI.gameObject.SetActive(false);


        //UI_GameScene gameSceneUI = Managers.UI.SceneUI as UI_GameScene;
        //UI_Inventory invenUI = gameSceneUI.InvenUI;

        //invenUI.gameObject.SetActive(true);
        //invenUI.RefreshUI();

        SelectStage = GameObject.Find("SelectStage").gameObject;

        SelectStage.SetActive(false);

        for (int i = 0; i < 10; i++)
        {
            GameObject stageButton = Instantiate(Resources.Load<GameObject>("Prefab/UI/Button/Stage"));
            stageButton.GetComponent<UI_Stage_Buttons>().stage = i + 1;
            stageButton.transform.parent = SelectStage.transform.GetChild(1);
            stageButton.transform.GetChild(0).GetComponent<Text>().text = "Stage " + (i + 1);

            stageButton.transform.GetComponent<Button>().interactable = false;

            StageButtons.Add(stageButton);
            
        }


        for (int i = 0; i< SaveDataSystem.Instance.data.maxStage; i++)
        {
            StageButtons[i].transform.GetComponent<Button>().interactable = true;
        }


    }


    public void BackButton()
    {
        Managers.Scene.LoadScene(Define.Scene.Login);
    }

    public void PlayButtonOpen()
    {
        SelectStage.SetActive(true);
    }

    public void PlayButtonClose()
    {
        SelectStage.SetActive(false);
    }

    //public void StageButton(int stage)
    //{
    //    SaveDataSystem.Instance.data.currentStage = stage;
    //    // 유저 정보 저장하기
    //    SaveDataSystem.Instance.SaveGameData();

    //    Managers.Scene.LoadScene(Define.Scene.Game);
    //}

    public void OnClickedExit()
    {

        //Managers.Network.DisconnectFromGame();
        //Managers.Scene.LoadScene(Define.Scene.Login);

        //#if UNITY_EDITOR
        //        UnityEditor.EditorApplication.isPlaying = false;

        //        Managers.Network.DisconnectFromGame();

        //#else
        //                Application.Quit(); // 어플리케이션 종료
        //#endif

        // Sound
        //Managers.Sound.Play("UI/Button/Slick Button", Define.Sound.Effect);

        // 서버한테 로그아웃 하겠다고 하는 곳
        //C_Logout logout = new C_Logout();
        //Managers.Network.Send(logout);

        //Managers.s_instance = null;

        // 매니저 모든 값들 초기화
        // 임시방편임..
        GameObject.Destroy(GameObject.Find("@Managers"));
        GameObject.Destroy(GameObject.Find("@Pool_Root"));
        GameObject.Destroy(GameObject.Find("@Sound"));

        // 로그인 창으로 가게 해준다.
        Managers.Scene.LoadScene(Define.Scene.Login);

    }

    public void OnClickUsers()
    {
        // Sound
        //Managers.Sound.Play("UI/Button/Slick Button", Define.Sound.Effect);

        // User 창이 이미 켜져있는지 확인한다.

        GameObject UserList = GameObject.Find("UI_UserListPopup");

        //if (UserList != null)
        //{           
        //    Managers.UI.ClosePopupUI(UserList.GetComponent<UI_UserListPopup>());
        //    return;
        //}


        // 서버한테 접속 유저를 물어본다.

        //C_Users users = new C_Users();
        //Managers.Network.Send(users);
    }


    public void Change(PointerEventData evt)
    {
        // Sound
        //Managers.Sound.Play("UI/Button/Slick Button", Define.Sound.Effect);

        // 스위칭

        //UI_GameScene gameSceneUI = Managers.UI.SceneUI as UI_GameScene;
        //UI_Inventory invenUI = gameSceneUI.InvenUI;

        //if (invenUI.gameObject.activeSelf)
        //{
        //    invenUI.gameObject.SetActive(false);
        //}
        //else
        //{
        //    invenUI.gameObject.SetActive(true);
        //    invenUI.RefreshUI();
        //}

        //// 스위칭

        //UI_Stat statUI = gameSceneUI.StatUI;

        //if (statUI.gameObject.activeSelf)
        //{
        //    statUI.gameObject.SetActive(false);
        //}
        //else
        //{
        //    statUI.gameObject.SetActive(true);
        //    statUI.RefreshUI();
        //}

        // 스위칭

        //UI_Skill skillUI = gameSceneUI.SkillUI;

        //if (skillUI.gameObject.activeSelf)
        //{
        //    skillUI.gameObject.SetActive(false);
        //    GetImage((int)Buttons.ChangeButton).gameObject.GetComponentInChildren<Text>().text = "↔ Skill";
        //}
        //else
        //{
        //    skillUI.gameObject.SetActive(true);
        //    GetImage((int)Buttons.ChangeButton).gameObject.GetComponentInChildren<Text>().text = "↔ Inven";
        //    skillUI.RefreshUI();
        //}

    }

}
