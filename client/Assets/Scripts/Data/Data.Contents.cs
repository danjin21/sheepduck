using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{

    public class StatInfo
    {
        public int level;
        public int maxHp;
        public int attack;
        public float speed;
        public float atkSpeed;
        public int range;
        public int def;
        public int skill;
    }


    #region Player

    [Serializable]
    public class PlayerData
    {
        public int id;
        public string name;
        public StatInfo stat; // 나중에 지워도됌
        public string prefabPath;
        public string HitSoundPath;



        public int level;
        public int maxHp;
        public int attack;
        public float speed1;
        public float speed2;
        public float speed3;
        public float atkSpeed;
        public int range;
        public int def;
        public int cost;
        public int cooltime;
        public int skill;
        public string hitSoundPath;
        public string color;
        public string engName;
        public string desc;




    }

    [Serializable]
    public class PlayerLoader : ILoader<int, PlayerData>
    {
        public List<PlayerData> players = new List<PlayerData>();

        public Dictionary<int, PlayerData> MakeDict()
        {
            Dictionary<int, PlayerData> dict = new Dictionary<int, PlayerData>();
            foreach (PlayerData player in players)
            {
                dict.Add(player.id, player);
            }

            return dict;
        }
    }
    #endregion

    #region Monster

    [Serializable]
    public class MonsterData
    {
        public int id;
        public string name;
        public StatInfo stat; // 나중에 지워도됌
        public string prefabPath;
        public string HitSoundPath;


        public int level;
        public int maxHp;
        public int attack;
        public float speed;
        public float atkSpeed;
        public int range;
        public int def;
        public int cost;
        public int probability;
        public int itemId;
        public int count;
        public string hitSoundPath;
        public string color;
        public string engName;
        public string desc;
    }

    [Serializable]
    public class MonsterLoader : ILoader<int, MonsterData>
    {
        public List<MonsterData> monsters = new List<MonsterData>();

        public Dictionary<int, MonsterData> MakeDict()
        {
            Dictionary<int, MonsterData> dict = new Dictionary<int, MonsterData>();
            foreach (MonsterData monster in monsters)
            {
                dict.Add(monster.id, monster);
            }

            return dict;
        }
    }
    #endregion

}
