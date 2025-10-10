using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Question
{
    public string question;
    public string[] answers;
    public int correct;
}
public class QuizManager : MonoBehaviour
{
    [SerializeField] Question[] questions;
    [SerializeField] GameObject startButton;
    [SerializeField] Button[] answerButton;
    [SerializeField] TextMeshPro bubbleTxt;
    int corrects;
    int q;
    int btns;
    [SerializeField] GameObject[] quizIcons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HideQuizIcons()
    {

    }
    public void StartQuiz()
    {
        UIManager.instance.SetMissionPopup(false);
        GameManager.instance.HideBubble();
        FormatQuestion();
        StartCoroutine(EnableAnswerButtons());
    }
    IEnumerator EnableAnswerButtons()
    {
        btns = 0;
        startButton.SetActive(false);
        for(int i = 0; i < questions[q].answers.Length; i++)
        {
            if (!String.IsNullOrEmpty(questions[q].answers[i]))
            {
                yield return new WaitForSeconds(0.2f);
                answerButton[i].gameObject.SetActive(true);
                answerButton[i].interactable = false;
                answerButton[i].GetComponent<Animator>().SetBool("Visible", true);
                btns++;
            }
        }
        for(int i = 0; i < btns; i++)
        {
            answerButton[i].interactable = true;
        }
    }
    IEnumerator DisableAnswerButtons(bool next)
    {
        for (int i = 0; i < btns; i++)
        {
            yield return new WaitForSeconds(0.2f);
            answerButton[i].GetComponent<Animator>().SetBool("Visible", false);
        }
        if(next)
        {
            StartCoroutine(NextQuestion());
        }
        else
        {
            yield return new WaitForSeconds(2f);
            Finish();
        }
    }
    public void Answer(int i)
    {
        for(int x = 0; x < btns; x++)
        {
            answerButton[x].interactable = false;
        }
        bool next = q + 1 < questions.Length;
        StartCoroutine(DisableAnswerButtons(next));
        if (i == questions[q].correct)
        {
            bubbleTxt.text = "Correct!";
            corrects++;
        }
        else
        {
            bubbleTxt.text = "Nope! The right answer is: " + questions[q].answers[questions[q].correct];
        }
    }
    void Finish()
    {
        string not = corrects >= questions.Length / 2 ? " " : " NOT ";
        string txt = $"You answered {corrects}/{questions.Length} correctly. You shall{not}pass";
        if(corrects >= questions.Length / 2)
        {
            Trophy.instance.Toggle(true);
        }
        bubbleTxt.text = txt;
    }
    IEnumerator NextQuestion()
    {
        q++;
        yield return new WaitForSeconds(2f);
        FormatQuestion();
        StartCoroutine(EnableAnswerButtons());
    }
    void FormatQuestion()
    {
        bubbleTxt.text = questions[q].question;
        for(int i = 0; i < questions[q].answers.Length; i++)
        {
            answerButton[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questions[q].answers[i];
        }
    }
}
