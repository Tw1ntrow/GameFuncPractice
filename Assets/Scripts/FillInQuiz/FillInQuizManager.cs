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
        new QuizQuestion { question = "�u�T�v���̊����̓ǂ݂́H", correctAnswer = "����" },
        new QuizQuestion { question = "�u�S�O�v���̊����̓ǂ݂́H", correctAnswer = "���イ����" },
        new QuizQuestion { question = "�u���r�v���̊����̓ǂ݂́H", correctAnswer = "����" },
        new QuizQuestion { question = "�u��ࣁv���̊����̓ǂ݂́H", correctAnswer = "������" },
        new QuizQuestion { question = "�u�N�W�v���̊����̓ǂ݂́H", correctAnswer = "���イ���イ" },
        new QuizQuestion { question = "�u�A�]�v���̊����̓ǂ݂́H", correctAnswer = "����ڂ�" },
        new QuizQuestion { question = "�u�R���v���̊����̓ǂ݂́H", correctAnswer = "���イ���イ" },
        new QuizQuestion { question = "�u�]�~�v���̊����̓ǂ݂́H", correctAnswer = "���񂿂�" },
        new QuizQuestion { question = "�u�`��v���̊����̓ǂ݂́H", correctAnswer = "���傤����" },
        new QuizQuestion { question = "�u�U�P�v���̊����̓ǂ݂́H", correctAnswer = "�Ă����傭" },
        new QuizQuestion { question = "�u��v���̊����̓ǂ݂́H", correctAnswer = "�͂�" },
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
            Debug.Log("�����I");
        }
        else
        {
            Debug.Log("�s�����I");
        }

        currentQuestionIndex++;
        ShowQuestion();
    }

    private void EndQuiz()
    {
        submitButton.enabled = false;
        resultText.text = $"�N�C�Y�I���I{correctAnswersCount} / {questions.Length} �␳�����܂����B";
    }
}