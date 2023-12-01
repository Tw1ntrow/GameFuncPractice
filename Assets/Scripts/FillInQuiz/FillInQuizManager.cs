using UnityEngine;
using UnityEngine.UI;

public class FillInQuizManager : MonoBehaviour
{
    [SerializeField]
    private QuizQuestion[] questions;
    [SerializeField]
    private Text questionText;
    [SerializeField]
    private InputField answerInputField;
    [SerializeField]
    private Text resultText;
    [SerializeField]
    private Button submitButton;

    private int currentQuestionIndex = 0;
    private int correctAnswersCount = 0;

    void Start()
    {
        questions = new QuizQuestion[]
        {
        new QuizQuestion { question = "「鬱」この漢字の読みは？", correctAnswer = "うつ" },
        new QuizQuestion { question = "「躊躇」この漢字の読みは？", correctAnswer = "ちゅうちょ" },
        new QuizQuestion { question = "「瑕疵」この漢字の読みは？", correctAnswer = "かし" },
        new QuizQuestion { question = "「絢爛」この漢字の読みは？", correctAnswer = "けんらん" },
        new QuizQuestion { question = "「蒐集」この漢字の読みは？", correctAnswer = "しゅうしゅう" },
        new QuizQuestion { question = "「羨望」この漢字の読みは？", correctAnswer = "せんぼう" },
        new QuizQuestion { question = "「蹴球」この漢字の読みは？", correctAnswer = "しゅうきゅう" },
        new QuizQuestion { question = "「蘊蓄」この漢字の読みは？", correctAnswer = "うんちく" },
        new QuizQuestion { question = "「饒舌」この漢字の読みは？", correctAnswer = "じょうぜつ" },
        new QuizQuestion { question = "「躑躅」この漢字の読みは？", correctAnswer = "てきちょく" },
        new QuizQuestion { question = "「鱧」この漢字の読みは？", correctAnswer = "はも" },
        };
        ShowQuestion();
    }

    public void ShowQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            questionText.text = questions[currentQuestionIndex].question;
            answerInputField.text = "";
        }
        else
        {
            EndQuiz();
        }
    }

    public void OnSubmitAnswer()
    {
        string userAnswer = answerInputField.text;
        string correctAnswer = questions[currentQuestionIndex].correctAnswer;

        if (userAnswer.Equals(correctAnswer, System.StringComparison.OrdinalIgnoreCase))
        {
            correctAnswersCount++;
            Debug.Log("正解！");
        }
        else
        {
            Debug.Log("不正解！");
        }

        currentQuestionIndex++;
        ShowQuestion();
    }

    private void EndQuiz()
    {
        submitButton.enabled = false;
        resultText.text = $"クイズ終了！{correctAnswersCount} / {questions.Length} 問正解しました。";
    }
}