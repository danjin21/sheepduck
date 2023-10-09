using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int stage = 1;

    public GameObject Unit_Popup;

    public CreateUnit CreateUnit;


    public List<BaseController> Units = new List<BaseController>();

    public List<PlayerController> Players = new List<PlayerController>();
    public List<MonsterController> Monsters = new List<MonsterController>();

    public NexusController Nexus;

    public GameObject PlayButton;

    public bool checkClear = false;

    public GameObject GameClearPopup;

    public GameObject DeckUnits;

    public int gold = 0;
    public int count = 0;
    public int Time = 0;
    public int mana = 10;
    public int level = 1;


    public Text GoldText;
    public Text WaveText;
    public Text TimeText;
    public Text ManaText;
    public Text LevelText;
    public Text StageText;

    public List<PlayerController> PlayerDecks = new List<PlayerController>();


    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
            RefreshSource();
        }
    }

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            RefreshSource();
        }
    }

    public int Mana
    {
        get
        {
            return mana;
        }
        set
        {
            mana = value;
            RefreshSource();
        }
    }

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            RefreshSource();
        }
    }

    int TotalCount = 3;

    public void CheckClear()
    {


        if(Monsters.Count ==0 && Count == TotalCount)
        {
            checkClear = true;
            GameClearPopup.gameObject.SetActive(true);


            GameObject Parent = GameClearPopup.gameObject.transform.GetChild(5).gameObject;


            Managers.GameSystem.GetGold();
            GameObject RewardContent = Instantiate(Resources.Load<GameObject>("Prefab/RewardContent"));
            RewardContent.GetComponent<Text>().text = "Gold + 10";       
            RewardContent.transform.SetParent(Parent.transform);
            RewardContent.transform.localScale = new Vector3(1, 1, 1);

            Managers.GameSystem.GetLevel();
            GameObject RewardContent_2 = Instantiate(Resources.Load<GameObject>("Prefab/RewardContent"));
            RewardContent_2.GetComponent<Text>().text = "Level + 1";          
            RewardContent_2.transform.SetParent(Parent.transform);
            RewardContent_2.transform.localScale = new Vector3(1, 1, 1);

            Managers.GameSystem.GetStage();


            //UI_GameScene gameSceneUI = Managers.UI.SceneUI as UI_GameScene;
            //gameSceneUI.RefreshUI();

            RefreshSource();
        }
        else
        {

        }


    }

    public void CreateDeck()
    {

        //Data.PlayerData playerData = null;
        //Managers.Data.PlayerDict.TryGetValue(1, out playerData);

        int PlayerCount = Managers.Data.PlayerDict.Count;

        PlayerCount = SaveDataSystem.Instance.DuckData.Count;

        for (int i = 0; i < PlayerCount; i++)
        {
            GameObject PlayerDeck = Instantiate(Resources.Load<GameObject>("Prefab/Deck_Unit"));
            PlayerDeck.transform.SetParent(DeckUnits.transform);
            PlayerDeck.transform.localScale = new Vector3(1, 1, 1);

            int kind = SaveDataSystem.Instance.DuckData[i].type;

            PlayerDeck.GetComponent<Deck_Unit>().PlayerKind = kind;

            Data.PlayerData playerData = null;
            Managers.Data.PlayerDict.TryGetValue(kind, out playerData);

            Color color;
            ColorUtility.TryParseHtmlString(playerData.color, out color);  // 원래색

            PlayerDeck.GetComponent<Image>().color = color;



            PlayerDeck.gameObject.transform.GetChild(0).transform.GetComponent<Text>().text = playerData.name;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        DeckUnits = GameObject.Find("Deck_Units").gameObject;

        stage = SaveDataSystem.Instance.data.currentStage;

        Unit_Popup = GameObject.Find("Canvas/Unit_Popup").gameObject;
        Unit_Popup.SetActive(false);

        CreateUnit = GetComponent<CreateUnit>();

        Nexus = GameObject.Find("Canvas/Nexus").GetComponent<NexusController>();

        PlayButton = GameObject.Find("Canvas/Bt_Play").gameObject;

        GameClearPopup = GameObject.Find("HighCanvas/GameClear");
        GameClearPopup.gameObject.SetActive(false);

        GoldText = GameObject.Find("Canvas/Info/Src_Gold").GetComponent<Text>();
        WaveText = GameObject.Find("Canvas/Info/Src_Wave").GetComponent<Text>();
        TimeText = GameObject.Find("Canvas/Info/Src_Time").GetComponent<Text>();
        ManaText = GameObject.Find("Canvas/Info/Src_Mana").GetComponent<Text>();
        LevelText = GameObject.Find("Canvas/Info/Src_Level").GetComponent<Text>();
        StageText = GameObject.Find("Canvas/Info/Src_Stage").GetComponent<Text>();
        RefreshSource();

        Mana =SaveDataSystem.Instance.data.mana;

        CreateDeck();
    }

    public void RefreshSource()
    {
        //GoldText.text = "$ "+ Gold.ToString();
        GoldText.text = $"Gold : {Managers.UserDataManager.Gold}";
        WaveText.text = "Wave : " + Count.ToString() + "/"+TotalCount;
        TimeText.text = Time.ToString();
        ManaText.text = "Mana : " + Mana.ToString();
        LevelText.text = $"Level : {Managers.UserDataManager.Level}";
        StageText.text = "Stage : 1-"+ stage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public Coroutine A = null;

 

    public void StartWave()
    {

        if (Count == 0)
        {
            checkClear = false;

            PlayButton.GetComponent<Button>().interactable = false;

            Count += 1;
            // StartCoroutine(CoPlayWave_1());

            int currentStage = stage;

            // 4이상부터는 똑같다
            if (currentStage >= 4)
                currentStage = 4;

            StartCoroutine("CoPlayWave_"+ currentStage);
        }

        
     
    }


    public MonsterController MakeMonster(int kind, int position)
    {
        Data.MonsterData monsterData = null;
        Managers.Data.MonsterDict.TryGetValue(kind, out monsterData);

        // 프리팹(조립) 을 갖고 온다.
        GameObject Monster = Instantiate(Resources.Load<GameObject>("Prefab/Monster"));

        // 프리팹(껍질)에 있는 MonsterController 스크립트를 갖고온다.
        // 프리팹 정보를 갖고온거임
        MonsterController M = Monster.GetComponent<MonsterController>();


        // 정보(영혼) 넣어주기
        M.speed = monsterData.speed; // 스피드
        M.id = Units.Count; // 고유 번호
        M.position = position; // 위치
        M.Hp = monsterData.maxHp; // 체력
        M.maxHp = monsterData.maxHp;
        M.atk = monsterData.attack;
        M.atkSpeed = monsterData.atkSpeed;
        M.kind = monsterData.id;
        M.colorKind = monsterData.color;

        // List 에 넣는건데, 관리하기위해서 넣는거임
        Units.Add(M);
        Monsters.Add(M);

        // 각 몬스터들을 깨운다. (영혼을 불어준다.)
        M.Init();

        return M;

    }


    IEnumerator CoPlayWave_1()
    {

        // 몬스터 한마리 데이터(금속활자) 를 갖고옴 (2)
        Data.MonsterData 은주 = null;
        Managers.Data.MonsterDict.TryGetValue(2, out 은주);


        // 반복문 5번 반복 
        for (int i = 0; i < 5; i++)
        {
            // 1 = 종류 , 9+i*10 = 위치
            // 0~9
            // 10~19
            // 20~29
            // 30~39
            // 40~49

            MonsterController monster =  MakeMonster(1, 9 + i * 10);
            // 커스터마이징하고싶으면 이렇게
            //monster.speed = 0;
            //monster.id = 0;
            //monster.position = 0;
            //monster.Hp = 0;
            //monster.maxHp = 0;
            //monster.atk = 0;
            //monster.atkSpeed = 0;
            //monster.kind = 0;
            //monster.colorKind = 0;
        }        

        // 3초 동안 기다린다.
        yield return new WaitForSeconds(3.0f);


        //if (Count < TotalCount)
        //{
        //    Count += 1; // 횟수를 센다.
        //    StartCoroutine(CoPlayWave_1());  // 자기를 다시 시작한다.
        //}

        //// 3초 동안 기다린다.
        //yield return new WaitForSeconds(3.0f);

        
        // 정보를 넣는다.

        // 프리팹(껍질)을 갖고 온다.
        GameObject Monster_2 = Instantiate(Resources.Load<GameObject>("Prefab/Monster"));

        // 프리팹에 있는 MonsterController 스크립트를 갖고온다.
        // 프리팹 정보를 갖고온거임
        MonsterController M2 = Monster_2.GetComponent<MonsterController>();

        // 정보(영혼) 넣어주기
        M2.speed = 은주.speed; // 스피드
        M2.id = Units.Count; // 고유 번호
        M2.position = 25; // 위치

        M2.Hp = 은주.maxHp; // 체력
        M2.maxHp = 은주.maxHp;
        M2.atk = 은주.attack;
        M2.atkSpeed = 은주.atkSpeed;
        M2.kind = 은주.id;
        M2.colorKind = 은주.color;

        // List 에 넣는건데, 관리하기위해서 넣는거임
        Units.Add(M2);
        Monsters.Add(M2);

        // 각 몬스터들을 깨운다. (영혼을 불어준다.)
        M2.Init();


        // 웨이브를 끝낸다.
        Count = TotalCount;
     
    }


    IEnumerator CoPlayWave_2()
    {

        for (int i = 0; i < 5; i++)
        {
            MonsterController monster = MakeMonster(2, 9 + i * 10);
        }

        yield return new WaitForSeconds(3.0f);

        if (Count < TotalCount)
        {
            Count += 1;
            StartCoroutine(CoPlayWave_2());
        }


    }


    IEnumerator CoPlayWave_3()
    {
        for (int i = 0; i < 5; i++)
        {
            MonsterController monster = MakeMonster(3, 9 + i * 10);
        }

        yield return new WaitForSeconds(3.0f);

        if (Count < TotalCount)
        {
            Count += 1;
            StartCoroutine(CoPlayWave_3());
        }
    }


    IEnumerator CoPlayWave_4()
    {

        //for (int i = 0; i < 5; i++)
        //{
        //    MonsterController monster = MakeMonster(1, 9 + i * 10);
        //}

        for(int i=0; i <5; i++)
        {
            MakeMonster(1, 9 );
            MakeMonster(2, 19);
            MakeMonster(1, 29);
            MakeMonster(2, 39);
            MakeMonster(1, 49);

            yield return new WaitForSeconds(0.5f);

            MakeMonster(2, 9);
            MakeMonster(1, 19);
            MakeMonster(2, 29);
            MakeMonster(1, 39);
            MakeMonster(2, 49);

            yield return new WaitForSeconds(0.5f);
        }


        // 웨이브를 끝낸다.
        Count = TotalCount;

        //if (Count < TotalCount)
        //{
        //    Count += 1;
        //    StartCoroutine(CoPlayWave_4());
        //}


    }

}
