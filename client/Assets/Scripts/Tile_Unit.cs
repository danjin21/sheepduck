using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile_Unit : UI_Base
{

    [SerializeField]
    Image _icon = null;

    public MainSystem MainSystem;
    public CreateUnit CreateUnit;
    public int Tile_ID;

    public int Player_ID = -1;


    public override void Init()
    {
        _icon = GetComponent<Image>();

        _icon.gameObject.BindEvent((e) =>
        {
            // 왼쪽 클릭이 아니면 리턴
            if (e.button == PointerEventData.InputButton.Left)
            {
                GameObject Unit_Popup;
                Unit_Popup = MainSystem.GameManager.Unit_Popup;

                if(CreateUnit.CurrentTile == Tile_ID)
                {
                    Unit_Popup.SetActive(false);
                    CreateUnit.CurrentTile = -1;
                }
                else
                {
                    Unit_Popup.SetActive(true);
                    CreateUnit.CurrentTile = Tile_ID;
                }



                Color color;

                // 색 초기화
                foreach (Tile_Unit A in CreateUnit.Tile_Units)
                {
                    //if (A.Player_ID == -1)
                    //{
                    //    ColorUtility.TryParseHtmlString("#FFFFFF", out color);
                    //    A.gameObject.GetComponent<Image>().color = color;
                    //}
                    ColorUtility.TryParseHtmlString("#FFFFFF", out color);
                    A.gameObject.GetComponent<Image>().color = color;
                }

         

                ColorUtility.TryParseHtmlString("#F7EE66", out color); // 노란색
                gameObject.GetComponent<Image>().color = color;

            }
            else if (e.button == PointerEventData.InputButton.Right)
            {



            }
        }, Define.UIEvent.Click);


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

    }
}
