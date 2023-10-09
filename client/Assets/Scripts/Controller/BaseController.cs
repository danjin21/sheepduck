using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{

    HpBar _hpBar;

    public int id;
    public float speed;
    public int atk;
    public int templateId;
    public int hp;
    public float atkSpeed;
    public int maxHp;

    public int kind;
    public string colorKind;

    public int skillId;
    public int range;

    public MainSystem MainSystem;

    public int position;

    public GameObject hudDamageText;

    public float margin;

    public AnimationController AnimationController;

    public int colorState;

    public virtual int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            UpdateHpBar();
        }
    }



    // HpBar 생성
    protected void AddHpBar()
    {

        GameObject go = Instantiate(Resources.Load<GameObject>("Prefab/UI/HpBar"));
        go.transform.SetParent(this.gameObject.transform);
        go.transform.localPosition = new Vector3(0, 0.6f, 0);
        go.name = "HpBar";
        _hpBar = go.GetComponent<HpBar>();
        UpdateHpBar();
    }

    // HpBar 업데이트
    public virtual void UpdateHpBar()
    {
        if (_hpBar == null)
            return;

        float ratio = 0.0f;

        if (Hp > 0)
        {
            // 3 / 2 = 1
            ratio = ((float)Hp / maxHp);
        }

        _hpBar.SetHpBar(ratio);

    }






    public virtual void Damaged(int atk)
    {
        Hp -= atk;
        //Debug.Log($"{gameObject.name} Hp : " + Hp);



        Tile_Unit Tile_Unit = MainSystem.GameManager.CreateUnit.Tile_Units[position];

        //GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트

        GameObject hudText = Instantiate(Resources.Load("Prefab/DamageText")) as GameObject;

        GameObject DamageObject = GameObject.Find("Canvas/DamageText");
        hudText.transform.SetParent(DamageObject.transform);

        hudText.GetComponent<DamageText>().damage = atk; // 데미지 전달
        hudText.GetComponent<DamageText>().ChangeText();

        if(this.GetType() == typeof(PlayerController))
        {

            hudText.GetComponent<DamageText>().alpha = new Color32(255, 50, 225, 255);

        }
        else if(this.GetType() == typeof(MonsterController))
        {
    
            hudText.GetComponent<DamageText>().alpha = new Color32(125, 50, 255, 255);
        }



        if (Hp <= 0)
        {
            GameObject DeadText = Instantiate(Resources.Load("Prefab/DamageText")) as GameObject;

            DeadText.transform.SetParent(DamageObject.transform);

            DeadText.GetComponent<DamageText>().damage = -9999999; // 데미지 전달
            DeadText.GetComponent<DamageText>().ChangeText();
            DeadText.transform.position = Tile_Unit.gameObject.GetComponent<Transform>().position + new Vector3(0, 50.0f, 0);

            //DeadText.GetComponent<DamageText>().moveSpeed = 100.0f;
            Destroy(this.transform.gameObject);

        }




        // 데미지 텍스트 위치 조정

        if (this.GetType() == typeof(MonsterController))
        {
            if(this.GetComponent<MonsterController>().CounterNexus != null)
            {
                hudText.transform.position = Tile_Unit.gameObject.GetComponent<Transform>().position - new Vector3(margin, 0, 0);
            }
            else
            {
                hudText.transform.position = Tile_Unit.gameObject.GetComponent<Transform>().position + new Vector3(margin, 0, 0);
            }
      
        }
        else if (this.GetType() == typeof(PlayerController))
            hudText.transform.position = Tile_Unit.gameObject.GetComponent<Transform>().position;


    }



    // Start is called before the first frame update
    void Start()
    {

  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Init()
    {
        margin = 0.6f;

        MainSystem = GameObject.Find("MainSystem").gameObject.GetComponent<MainSystem>();

        tileMove();
        AddHpBar();

        AnimationController = GetComponent<AnimationController>();
    }

    public BaseController forwardCheck()
    {
        int forwardPosition = position - 1;
        BaseController forwardObject = null;

        foreach (BaseController p in MainSystem.GameManager.Units)
        {
            if(p.position == forwardPosition)
            {
                forwardObject = p;
                break;
            }
        }

        return forwardObject;

    }

    public BaseController currentCheck(int kind, int dist = 0)
    {

        int currentPosition = position;
        BaseController currentObject = null;

        switch (kind)
        {
            // Player
            case 1:
                {
                    foreach (PlayerController p in MainSystem.GameManager.Players)
                    {
                        // 같은 라인에 있고
                        if (currentPosition / 10 == p.position / 10)
                        {
                            if (Math.Abs(p.position - currentPosition) <= dist)
                            {
                                currentObject = p;
                                break;
                            }
                        }
                    }

                }
                break;

            // Monster    
            case 2:
                {
                    foreach (MonsterController p in MainSystem.GameManager.Monsters)
                    {
                        // 같은 라인에 있고
                        if(p.position / 10 == currentPosition /10 )
                        {
                            if (Math.Abs(p.position - currentPosition) <= dist)
                            {
                                currentObject = p;
                                break;
                            }
                        }
                    }

                }
                break;
        }




        return currentObject;

    }



    public void tileMove()
    {

        //Color color;

        Tile_Unit Tile_Unit = MainSystem.GameManager.CreateUnit.Tile_Units[position];


        if(this.GetType() == typeof(MonsterController))
            transform.position = Tile_Unit.transform.position + new Vector3(margin, 0,0) + new Vector3(0,0,id); // 늦게오면 뒤에 오게
        else if (this.GetType() == typeof(PlayerController))
            transform.position = Tile_Unit.transform.position + new Vector3(0, 0, id); // 늦게오면 뒤에 오게

        // 자기 ID에 따라 색깔을 다르게 해준다.
        //switch (templateId)
        //{
        //    case 10:

        //        ColorUtility.TryParseHtmlString("#6679F7", out color);
        //        Tile_Unit.gameObject.GetComponent<Image>().color = color;
        //        break;

        //        // 몬스터는 제외
        //    case 9000:

        //        ColorUtility.TryParseHtmlString("#F76666", out color);
        //        Tile_Unit.gameObject.GetComponent<Image>().color = color;
        //        break;


        //}


        //if(position%10 != 9)
        //{
        //    // 과거 자기가 지나간 곳은 원래 색으로 변경시켜준다.
        //    Tile_Unit Tile_Unit_past = MainSystem.GameManager.CreateUnit.Tile_Units[position + 1];

        //    ColorUtility.TryParseHtmlString("#FFFFFF", out color);
        //    Tile_Unit_past.gameObject.GetComponent<Image>().color = color;
        //}


    }


}
