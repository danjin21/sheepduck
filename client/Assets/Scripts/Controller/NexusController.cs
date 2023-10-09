using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NexusController : MonoBehaviour
{

    public MainSystem MainSystem;
    public GameObject GameOverPopup;


    public Text HpText;

    public int Hp;
    public int MaxHp;


    // Start is called before the first frame update
    void Start()
    {
        Hp = 5000;
        MaxHp = 5000;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Init()
    {
        MainSystem = GameObject.Find("MainSystem").gameObject.GetComponent<MainSystem>();
        GameOverPopup = GameObject.Find("HighCanvas/GameOver");


        HpText = gameObject.transform.GetChild(1).GetComponent<Text>();
        HpText.text = $"HP : {Hp} / {MaxHp} ";

        GameOverPopup.transform.gameObject.SetActive(false);
    }

    public virtual void RefreshHp()
    {
        HpText.text = $"HP : {Hp} / {MaxHp} ";

        if(Hp <= 0)
        {
            GameOverPopup.transform.gameObject.SetActive(true);
        }
    }

    public void GotoTown()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Replay()
    {
        SceneManager.LoadScene("Game");
    }


    public virtual void Damaged(int atk)
    {
        Hp -= atk;
        Debug.Log($"{gameObject.name} Hp : " + Hp);



        GameObject hudText = Instantiate(Resources.Load("Prefab/DamageText")) as GameObject;

        GameObject DamageObject = GameObject.Find("Canvas/DamageText");

        hudText.transform.SetParent(DamageObject.transform);

        hudText.GetComponent<DamageText>().damage = atk; // 데미지 전달
        hudText.GetComponent<DamageText>().ChangeText();


        if (this.GetType() == typeof(NexusController))
        {

            hudText.GetComponent<DamageText>().alpha = new Color32(255, 52, 52, 255);

        }

        int randomPos = Random.Range(0, 20);

        hudText.transform.position = transform.position + new Vector3(0, randomPos*0.05f, 0);

        RefreshHp();


    }





}
