using UnityEngine;

public class Flower : MonoBehaviour
{
    private ScoreManager ScoreManager;
    private void Start()
    {
        ScoreManager = ScoreManager.instanceScore;
    }
    public void DestroyMe()
    {
        gameObject.GetComponent<Animator>().Play("Daisy - Collect");
    }

}
