using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomQuestionText : MonoBehaviour
{
    [System.Serializable]
    public struct Question
    {
        public string SummaryName;
        public string firstLine;
        public string secondLine;
    }
    [SerializeField] private float timeBeforeScene = 3f;
    [SerializeField] private bool isEndQuestions;
    [SerializeField] private Question[] questions;
    [SerializeField] private Question[] endQuestions;
    private List<string> endQuestionsFormatted;
    private float elapsedTime;
    private int currentItem = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        endQuestionsFormatted = new List<string>();
        if (isEndQuestions)
        {
            foreach (Question q in endQuestions)
            {
                string qFormatted = q.firstLine + "\n" + q.secondLine;
                endQuestionsFormatted.Add(qFormatted);
            }
        }
        else
        {
            Question chosen = questions[Random.Range(0, 12)];
            string concat = chosen.firstLine + "\n" + chosen.secondLine;
            gameObject.GetComponent<TextMeshProUGUI>().text = concat;

        }
        if (isEndQuestions)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = endQuestionsFormatted[currentItem];
        }
    }

        // Update is called once per frame
        void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeBeforeScene && !isEndQuestions)
        {
            SceneManage.instance.LoadNextScene();
        }
        if (elapsedTime >= timeBeforeScene && isEndQuestions)
        {

            gameObject.GetComponent<TextMeshProUGUI>().text = endQuestionsFormatted[currentItem];
            currentItem++;
            Debug.Log("Current - " + currentItem + "   ----   of Count - " + endQuestionsFormatted.Count);
            if (currentItem == endQuestionsFormatted.Count-1)
            {
                SceneManage.instance.LoadTitleScene();
            }
            elapsedTime = 0f;
        }
    }
}
