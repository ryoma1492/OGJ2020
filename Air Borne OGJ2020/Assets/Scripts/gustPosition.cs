using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gustPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public void setGustPositon ()
    {
        Debug.Log("Moved");

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
