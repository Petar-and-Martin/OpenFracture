using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCollision : MonoBehaviour
{

    public bool active = false;

    // Update is called once per frame
    private void OnTriggerEnter()
    {
        active = true;
    }
}
