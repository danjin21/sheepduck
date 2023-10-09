using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    public TMP_Text text;
    public Color alpha;
    public int damage;



    // Start is called before the first frame update
    void Awake()
    {
        moveSpeed = 5.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;

        text = GetComponent<TMP_Text>();
        alpha = text.color;

        Invoke("DestroyObject", destroyTime);
    }

    public void ChangeText()
    {
        if(damage == -9999999)
        {
            text.text = "Dead";
        }
        else
        {
            text.text = damage.ToString();
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
