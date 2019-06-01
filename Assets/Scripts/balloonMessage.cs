using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class balloonMessage : MonoBehaviour
{
    private Text msgText;
    private void Awake()
    {
        msgText = transform.Find("Message").GetComponent<Text>();
    }
    private void Update()
    {
        msgText.text = BalloonController.getMsg();
    }
    public void setText(string newMsg)
    {
        msgText.text = newMsg;
    }
}
