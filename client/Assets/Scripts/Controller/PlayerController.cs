using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{

    public BaseController CounterPlayer;


    // Start is called before the first frame update
    void Start()
    {
   
    }

    public override void Damaged(int atk)
    {
        base.Damaged(atk);

        if (Hp <= 0)
        {
            MainSystem.GameManager.Units.Remove(this);
            MainSystem.GameManager.Players.Remove((PlayerController)this);
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (CounterPlayer != null)
        {
            if (CounterPlayer.transform.position.x - this.transform.position.x > 0)
            {
                // 플립
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                // 플립
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }


    public void ColorChange(int colorCode)
    {
        Color color;
        ColorUtility.TryParseHtmlString(colorKind, out color);  // 원래색




        switch (colorCode)
        {
            case 0: // 기본
                ColorUtility.TryParseHtmlString(colorKind, out color);  // 원래색

                if(colorState != colorCode)
                {
                    AnimationController.anim = null;
                    AnimationController.StopAllCoroutines();

                    AnimationController.anim = StartCoroutine(AnimationController.coIdle());
                }

                break;
            case 1: // 공격
                //ColorUtility.TryParseHtmlString("#FE23FF", out color); // 자주색

                ColorUtility.TryParseHtmlString(colorKind, out color);  // 원래색
       

                if (colorState != colorCode)
                {
                    AnimationController.anim = null;
                    AnimationController.StopAllCoroutines();

                    AnimationController.anim = StartCoroutine(AnimationController.coAttack(1.0f / atkSpeed));
                }

                break;
            case 2:// 걷기
                ColorUtility.TryParseHtmlString(colorKind, out color);  // 원래색

                if (colorState != colorCode)
                {
                    AnimationController.anim = null;
                    AnimationController.StopAllCoroutines();

                    AnimationController.anim = StartCoroutine(AnimationController.coWalk());
                }


                break;
        }

        colorState = colorCode;

        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    // 스킬 영역


    public void Action()
    {
        switch (skillId)
        {
            case 1:
                CounterPlayer.Damaged(atk);
                break;

            case 2:

                GameObject Projectile = Instantiate(Resources.Load<GameObject>("Prefab/Projectile"));
                Projectile.transform.GetComponent<ProjectileController>().damage = atk;
                Projectile.transform.GetComponent<ProjectileController>().target = CounterPlayer;
                Projectile.transform.position = this.transform.position;
                Projectile.transform.parent = this.transform;

                Projectile.transform.GetComponent<ProjectileController>().Init();
                // CounterPlayer.Damaged(atk);
                break;



            default:
                break;
        }

    }

    IEnumerator Battle()
    {
        if (CounterPlayer != null)
        {

            Action();
            ColorChange(1);


            yield return new WaitForSeconds(1.0f / atkSpeed);

            if (CounterPlayer.Hp > 0)
            {
                StartCoroutine(Battle());
            }
            
            if(CounterPlayer == null)
            {
                // ColorChange(0);
                StartCoroutine(Search());
       
            }
        }
        else
        {
            StartCoroutine(Search());
            ColorChange(0);
        }

      

    }

    IEnumerator Search()
    {
        int dist = range;

        // 갈 수 있는지 없는지 체크
        BaseController monster = currentCheck(2, dist);

        // 몬스터인지 확인
        if (monster != null && monster.GetType() == typeof(MonsterController) && CounterPlayer == null)
        {
            ColorChange(1);
            CounterPlayer = monster;

            StartCoroutine(Battle());

        }
        else
        {
            ColorChange(0);

            yield return new WaitForSeconds(0.05f);

            StartCoroutine(Search());

        }




    }

    public override void Init()
    {
        base.Init();

        StartCoroutine(Search());
    }


}
