using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Deck_Unit : UI_Base
{

    [SerializeField]
    Image _icon = null;

    public MainSystem MainSystem;
    public int Tile_ID;


    public float m_DoubleClickSecond = 0.03f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;

    public int PlayerKind = 0;

    public override void Init()
    {

        m_DoubleClickSecond = 0.2f;
        _icon = GetComponent<Image>();

        _icon.gameObject.BindEvent((e) =>
        {
            // 왼쪽 클릭이 아니면 리턴
            if (e.button == PointerEventData.InputButton.Left)
            {
         
                if (!m_IsOneClick)
                {
                    // 더블클릭 전 한번
                    m_Timer = Time.time;
                    m_IsOneClick = true;
                }
                else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
                {
                    // 더블클릭 되는 순간
                    m_IsOneClick = false;

                    Debug.Log("더블클릭");

                    SetTile(1,10);
                }

            }
            else if (e.button == PointerEventData.InputButton.Right)
            {



            }
        }, Define.UIEvent.Click);


    }


    public void SetTile(int state, int playerID ) // 0:ready 
    {
        int CurrentTile = MainSystem.GameManager.CreateUnit.CurrentTile;
        Tile_Unit Tile_Unit = MainSystem.GameManager.CreateUnit.Tile_Units[CurrentTile];



        Color color;



        // 타일에 PlayerID 전수
        Tile_Unit.Player_ID = playerID;


        // 색 초기화
        foreach (Tile_Unit A in MainSystem.GameManager.CreateUnit.Tile_Units)
        {
            if (A.Player_ID == -1)
            {
                ColorUtility.TryParseHtmlString("#FFFFFF", out color);
                A.gameObject.GetComponent<Image>().color = color;
            }
        }


        switch (state)
        {
            case 0:
                ColorUtility.TryParseHtmlString("#50CA64", out color); // 초록색
                Tile_Unit.gameObject.GetComponent<Image>().color = color;
                break;

            case 1:


                ColorUtility.TryParseHtmlString("#ffffff", out color);
                Tile_Unit.gameObject.GetComponent<Image>().color = color;

                // 마나가 없으면 리턴한다
                if (MainSystem.GameManager.Mana < 1)
                    break;

                GameObject Player = Instantiate(Resources.Load<GameObject>("Prefab/Player"));
                PlayerController A = Player.GetComponent<PlayerController>();

                Data.PlayerData playerData = null;
                Managers.Data.PlayerDict.TryGetValue(PlayerKind, out playerData);

                A.templateId = playerID;
                A.speed = playerData.speed1;
                A.atk = playerData.attack;
                A.id = MainSystem.GameManager.Units.Count;
                A.position = CurrentTile;
                A.Hp = playerData.maxHp;
                A.maxHp = playerData.maxHp;
                A.atkSpeed = playerData.atkSpeed;
                A.kind = playerData.id;
                A.colorKind = playerData.color;
                A.skillId = playerData.skill;
                A.range = playerData.range;

                MainSystem.GameManager.Units.Add(A);
                MainSystem.GameManager.Players.Add(A);

                A.Init();
                MainSystem.GameManager.Mana -= 1;

                //ColorUtility.TryParseHtmlString("#6B66F7", out color);
                //Tile_Unit.gameObject.GetComponent<Image>().color = color;
                break;

            default:
                break;
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        MainSystem = GameObject.Find("MainSystem").gameObject.GetComponent<MainSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //  한번만 클릭했을 때 KeySetting
        //  0.25 초 이상되면 원상태로
        if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
        {

            m_IsOneClick = false;
            Debug.Log("덱");

            SetTile(0,-1);
            //C_SlotChange slotchangePacket = new C_SlotChange();
            //slotchangePacket.ItemDbId = ItemDbId;
            //slotchangePacket.Slot = 0;

            //Managers.Network.Send(slotchangePacket);

        }
    }
}
