using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;


public class Compass : MonoBehaviour
{
    [SerializeField] private UIComponentNeeds UIflowerManager;
    private GameObject flower;
    private float timeSince;
    private bool isExiting;
    // Start is called before the first frame update
    void Start()
    {
        flower = UIflowerManager.flowerManager.GetFlower();
    }

    // Update is called once per frame
    void Update()
    {
        if(!flower)
        {
            flower = UIflowerManager.flowerManager.GetFlower();
        }
        if (flower.activeSelf==false)
        {

            try
            {
                flower = UIflowerManager.flowerManager.GetFlower();
                if (flower.activeSelf)
                {
                    float hue = 0f;
                    float sat = 0f;
                    float val = 0f;
                    float hue2 = 0f;
                    float sat2 = 0f;
                    float val2 = 0f;
                    Color.RGBToHSV(flower.GetComponent<SpriteRenderer>().color, out hue, out sat, out val);
                    Color.RGBToHSV(gameObject.GetComponent<Image>().color, out hue2, out sat2, out val2);
                    gameObject.GetComponent<Image>().color = Color.HSVToRGB(math.abs(hue), sat2, val2);
                }
                else
                {
                    gameObject.GetComponent<Image>().color = new Color32(16, 255, 0, 255);
                    flower = UIflowerManager.exit.getExit();
                    isExiting = true;
                }
            }
            catch
            {
                gameObject.GetComponent<Image>().color = new Color32(16, 255, 0, 255);
                flower = UIflowerManager.exit.getExit();
                isExiting = true;
            }
        }
        Vector3 dir = flower.transform.position - UIflowerManager.player.transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, dir);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        if (isExiting)
        {
            timeSince += Time.deltaTime;
            if (timeSince >= 1)
            {
                AudioManager.instance.PlayClip("ExitOpen");
                isExiting = false;
            }
        }
    }
}
