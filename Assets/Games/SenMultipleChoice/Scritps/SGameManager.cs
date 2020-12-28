using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SGameManager : MonoBehaviour
{
    public SQuestions[] question;
    private SQuestions currenQuestion;
    private static List<SQuestions> unansweredQuestions;

    public Text[] answerText;
    [SerializeField] Text questionText;

    [SerializeField]
    private float time = 1f;

    public GameObject Game;
    public GameObject TrueAnswer;
    public GameObject FalseAnswer;

    int score = 0;
    [SerializeField] Text scoreText;

    void Update()
    {
        scoreText.text = score.ToString();
    }

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = question.ToList<SQuestions>();
        }

        SerCurrentQuestion();
    }
    void SerCurrentQuestion()
    {
        int randomQustionIndex = Random.Range(0, unansweredQuestions.Count);
        currenQuestion = unansweredQuestions[randomQustionIndex];
        List<string> answers = new List<string>(currenQuestion.answerText);

        questionText.text = currenQuestion.question;

        for (int i = 0; i < currenQuestion.answerText.Length; i++)
        {
            int rand = Random.Range(0, answers.Count);
            answerText[i].text = answers[rand];
            answers.RemoveAt(rand);
        }

        unansweredQuestions.RemoveAt(randomQustionIndex);
    }

    IEnumerator CorrectAnswer()
    {
        Game.gameObject.SetActive(false);
        TrueAnswer.gameObject.SetActive(true);

        yield return new WaitForSeconds(time);

        TrueAnswer.gameObject.SetActive(false);
        Game.gameObject.SetActive(true);
    }

    IEnumerator IncorrectAnswer()
    {
        Game.gameObject.SetActive(false);
        FalseAnswer.gameObject.SetActive(true);

        yield return new WaitForSeconds(time);

        FalseAnswer.gameObject.SetActive(false);
        Game.gameObject.SetActive(true);
    }

    IEnumerator TransitionToNextQuestion()
    {
        yield return new WaitForSeconds(time);

        SerCurrentQuestion();
    }

    public void AnswerButton(int index)
    {
        if(answerText[index].text.ToString() == currenQuestion.answerText[0])
        {
            StartCoroutine(CorrectAnswer());

            StartCoroutine(TransitionToNextQuestion());
        }
        else
        {
            StartCoroutine(IncorrectAnswer());

            score++;
        }
    }
}
