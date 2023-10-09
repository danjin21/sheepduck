using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public BaseController target;
    public float speed;
    public int damage;

    public Vector3 targetPosition;

    // Start is called before the first frame update
    void init()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            targetPosition = target.transform.position;
            TargetMoving(targetPosition);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }

    public void Init()
    {
        speed = 10.0f;
        targetPosition = target.transform.position;

        // 2초뒤에는 사라지게
        Destroy(this, 2000);

    }

    public void TargetMoving(Vector3 targetPosition)
    {



        Vector3 _dir = (targetPosition - this.transform.position);
        _dir.z = 0;

        _dir = _dir.normalized;


   


        Debug.Log("야!!!!!!!!!!!!" + _dir);


        this.transform.position += _dir * speed * Time.deltaTime;

        float distance = Vector2.Distance(this.transform.position, target.transform.position);


        Debug.Log("거리" + distance + "/" + _dir.magnitude);

        //if (Math.Abs(distance) <= 0.5f)
        //{
        //    target.transform.GetComponent<BaseController>().Damaged(damage);

        //    Destroy(gameObject);
        //}


    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject == target.transform.gameObject)
        {
            target.transform.GetComponent<BaseController>().Damaged(damage);

            Destroy(gameObject);
        }
    }

    public void UnTargetMoving()
    {

    }


}
