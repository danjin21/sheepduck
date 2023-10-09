using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{

    int gold;
    int mana;
    int level;
    int tree;
    int cash;

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
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
        }
    }

    public int Tree
    {
        get
        {
            return tree;
        }
        set
        {
            tree = value;
        }
    }

    public int Cash
    {
        get
        {
            return cash;
        }
        set
        {
            cash = value;
        }
    }

    public void UserDataSync(SaveData saveData)
    {

        Gold = saveData.gold;
        Mana = saveData.mana;
        Level = saveData.level;
        Tree = saveData.tree;
        Cash = saveData.cash;
        // UI 변동되게해주기
        //MainSystem.UISystem.GameUserData_UI.ChangeUI();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
