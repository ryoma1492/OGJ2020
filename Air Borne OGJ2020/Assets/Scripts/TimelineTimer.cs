using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineTimer : MonoBehaviour
{
    private float timeSpent;
    private float totalTime = 5f;
    [SerializeField] private Button button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime;
        if (timeSpent>=totalTime)
        {
            button.enabled = true;
        }
    }
}
