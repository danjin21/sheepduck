using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public int Kind;

    public string Path;

    public SpriteRenderer thisSprite;

    public Coroutine anim;


    public Sprite walk1;
    public Sprite walk2;
    public Sprite Idle1;
    public Sprite Idle2;
    public Sprite Attack;
    public Sprite Hit;


    MonsterController monsterController;

    // Start is called before the first frame update
    void Start()
    {
        thisSprite = this.GetComponent<SpriteRenderer>();

        monsterController = this.GetComponent<MonsterController>();

        if (monsterController != null)
        {
            SetSprite_Monster(10000);
        }
        else if (monsterController == null)
        {
            SetSprite(10000);
        }


        anim = null;
        anim = StartCoroutine(coIdle());



    }

    public void SetSprite(int kind)
    {
        walk1 = Resources.Load<Sprite>($"Texture/Character/Player/{kind}/Walk1");
        walk2 = Resources.Load<Sprite>($"Texture/Character/Player/{kind}/Walk2");

        Idle1 = Resources.Load<Sprite>($"Texture/Character/Player/{kind}/Idle1");
        Idle2 = Resources.Load<Sprite>($"Texture/Character/Player/{kind}/Idle2");

        Attack = Resources.Load<Sprite>($"Texture/Character/Player/{kind}/Attack");
        Hit = Resources.Load<Sprite>($"Texture/Character/Player/{kind}/Hit");
    }

    public void SetSprite_Monster(int kind)
    {

        Sprite[] sprites = Resources.LoadAll<Sprite>($"Texture/Character/Monster/Sheep");

        walk1 = sprites[5];
        walk2 = sprites[6];

        Idle1 = sprites[2];
        Idle2 = sprites[3];

        Attack = sprites[9];
        Hit = sprites[12];
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator coWalk()
    {

        thisSprite = this.GetComponent<SpriteRenderer>();


        thisSprite.sprite = walk1;
        yield return new WaitForSeconds(0.2f);
        thisSprite.sprite = walk2;

        yield return new WaitForSeconds(0.2f);

        anim = null;
        anim = StartCoroutine(coWalk());

    }


    public IEnumerator coIdle()
    {

        thisSprite = this.GetComponent<SpriteRenderer>();


        thisSprite.sprite = Idle1;
        yield return new WaitForSeconds(0.5f);
        thisSprite.sprite = Idle2;

        yield return new WaitForSeconds(0.5f);

        anim = null;
        anim = StartCoroutine(coIdle());

    }


    public IEnumerator coAttack(float time)
    {

        thisSprite = this.GetComponent<SpriteRenderer>();

        thisSprite.sprite = Attack;

        yield return new WaitForSeconds(time/2);

        thisSprite.sprite = Idle1;
        yield return new WaitForSeconds(time/2);

        anim = null;
        anim = StartCoroutine(coAttack(time));

    }

    public IEnumerator coHit()
    {
        thisSprite = this.GetComponent<SpriteRenderer>();



        thisSprite.sprite = Hit;
        yield return new WaitForSeconds(0.5f);
        thisSprite.sprite = Idle1;

        anim = null;
    }
}
