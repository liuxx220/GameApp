using UnityEngine;
using System.Collections;

public class UICD : MonoBehaviour
{
    UISprite UISpriteCD;
    
    void Start()
    {
        UISpriteCD = GetComponent<UISprite>();


    }


    void FixedUpdate()
    {
        if (UISpriteCD.fillAmount > 0)
        {
            UISpriteCD.fillAmount -= 0.03f;
        }
    }
}
