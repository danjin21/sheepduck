using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateUnit : UI_Base
{

    public int currentTile;

    public int CurrentTile
    {
        get { return currentTile; }   // get method

        set
        {
            currentTile = value;
            RefreshUI();
        }  // set method
    }


    


    public List<Tile_Unit> Tile_Units = new List<Tile_Unit>();

    public Text CurrentTileText;


    public override void Init()
    {
        GameObject Tile = GameObject.Find("Tile").gameObject;


        CurrentTileText = GameObject.Find("CurrentTile_Text").gameObject.GetComponent<Text>();

        for (int i = 0; i < 50; i++)
        {
            GameObject Units = Instantiate(Resources.Load<GameObject>("Prefab/Tile_Unit"), Tile.transform);

            Tile_Units.Add(Units.transform.GetComponent<Tile_Unit>());
            Units.GetComponent<Tile_Unit>().CreateUnit = this;
            Units.GetComponent<Tile_Unit>().Tile_ID = i;

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshUI()
    {
        CurrentTileText.text = "CurrentTile : "+CurrentTile.ToString();
    }
}
