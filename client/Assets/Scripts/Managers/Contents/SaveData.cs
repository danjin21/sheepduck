using System;
using System.Collections.Generic;


// List 도 저장할 수 있게 함
[Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}



[Serializable] // 직렬화
public class SaveData
{
    // 각 챕터의 잠금여부를 저장할 배열
    public bool[] isUnlock = new bool[5];

    public int gold = 10;
    public int mana = 10;
    public int level = 1;
    public int tree = 0;
    public int cash = 0;


    public int nexusHp = 100;
    public int nexusMaxHp = 100;
    public int nexusKind = 1;
    public int nexusPosX = 0;
    public int nexusPosY = 0;

    public int maxStage = 1;
    public int currentStage = 1;
    public int maxWorld = 1;
    public int currentWorld = 1;


}

[Serializable]
public class Duck
{
    public Duck(string _name, int _type, int _hp, int _maxHp, int _dp, int _atkRange, int _range, int _atk, int _mtk, int _delay, int _desc, int _mp, int _maxMp)
    { name = _name; type = _type; hp = _hp; maxHp = _maxHp; dp = _dp; atkRange = _atkRange; range = _range;
        atk = _atk;mtk = _mtk;delay = _delay; desc = _desc;mp = _mp;maxMp = _maxMp; }

    public string name;
    public int type;
    public int hp;
    public int maxHp;
    public int dp;
    public int atkRange;
    public int range;
    public int atk;
    public int mtk;
    public int delay;
    public int desc;
    public int mp;
    public int maxMp;

}


