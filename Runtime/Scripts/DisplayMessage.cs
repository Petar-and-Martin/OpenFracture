using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMessage : MonoBehaviour
{
    public string message = '';
    public TextMeshPro mText;

    // Update is called once per frame
    void Update()
    {
        mText.text = message;
    }


}
