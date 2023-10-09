using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Stat : UI_Base
{
    bool _init = false;


    enum Texts
    {
        TreeText,
        GoldText,
        CashText,
        LevelText
    }

    enum Buttons
    {
        BackButton,
        PlayButton
    }


    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Buttons));

        //GetImage((int)Buttons.Stats01Button).gameObject.BindEvent(Str_Up);
        //GetImage((int)Buttons.Stats02Button).gameObject.BindEvent(Dex_Up);
        //GetImage((int)Buttons.Stats03Button).gameObject.BindEvent(Int_Up);
        //GetImage((int)Buttons.Stats04Button).gameObject.BindEvent(Luk_Up);


        _init = true;
        RefreshUI();
    }

    public void RefreshUI()
    {

        Get<Text>((int)Texts.GoldText).text = $"Gold : {Managers.UserDataManager.Gold}";
        Get<Text>((int)Texts.TreeText).text = $"Tree : {Managers.UserDataManager.Tree}";
        Get<Text>((int)Texts.CashText).text = $"Cash : {Managers.UserDataManager.Cash}";
        Get<Text>((int)Texts.LevelText).text = $"Level : {Managers.UserDataManager.Level}";
    }
}
