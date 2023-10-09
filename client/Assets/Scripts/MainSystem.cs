using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainSystem : MonoBehaviour
{


    public sceneManager SceneManager;
    public GameManager GameManager;
   

    public Image BackGroundImage;



    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("dsadas");
        SceneManager = GetComponent<sceneManager>();
        GameManager = GetComponent<GameManager>();

        Debug.Log("dsadas@@");


        CreateAccountPacketReq packet = new CreateAccountPacketReq()
        {
            AccountName = "@",
            Password = "@"
        };


        Managers.Web.SendPostRequest<CreateAccountPacketRes>("account/create", packet, (res) =>
        {
            // 응답이오면 처리하는 부분
            Debug.Log("result = " + res.CreateOk);
            Debug.Log("Info = " + res.Info);

        });



    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
