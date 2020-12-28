using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WritingGameManager : MonoBehaviour
{
    public WritingQuestions[] WritingQuestions;
    private static List<WritingQuestions> unansweredQuestions;

    public Text[] answersText;
    private WritingQuestions currentQuestion;

    [SerializeField]
    private InputField answerField;
    [SerializeField]
    private Text factText;
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
            unansweredQuestions = WritingQuestions.ToList<WritingQuestions>();
        }

        SerCurrentQuestion();

    }

    void SerCurrentQuestion()
    {
        int randomQustionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQustionIndex];
        List<string> answers = new List<string>(currentQuestion.answer);

        factText.text = currentQuestion.fact;

        Debug.Log("answer: " + currentQuestion.answer[0]);

        for (int i = 0; i < currentQuestion.answer.Length; i++)
        {
            int rand = Random.Range(0, answers.Count);
            answersText[i].text = answers[rand];
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

    public void CheckAnswer()
    {

        //if(answerField.text.Trim().ToLower() == currentQuestion.answer.Trim().ToLower())
        //{
        //
        //}

        if (answerField.text.Trim() == currentQuestion.answer[0] || answerField.text.Trim() == currentQuestion.answer[1])
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
