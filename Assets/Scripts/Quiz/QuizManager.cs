using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField]
    private QuizData[] questions;
    [SerializeField]
    private Text questionText;
    [SerializeField]
    private Text[] optionTexts;
    [SerializeField]
    private Text resultText;

    private int correctAnswers;
    private int currentQuestionIndex;

    void Start()
    {
        // サンプルデータの設定
        SetupSampleQuestions();

        currentQuestionIndex = 0;
        correctAnswers = 0;
        ShowQuestion();
    }

    void SetupSampleQuestions()
    {
        questions = new QuizData[]
        {
            new QuizData {
                question = "地球上で最も高い山はどれですか？",
                options = new string[] { "エベレスト", "K2", "マッキンリー", "キリマンジャロ" },
                correctAnswerIndex = 0
            },
            new QuizData {
                question = "「鈰」この漢字の読みはどれですか？",
                options = new string[] { "イリジウム", "セリウム", "レニウム", "ランタン" },
                correctAnswerIndex = 1
            },
            new QuizData {
                question = "「誾誾」この漢字の読みはどれですか？",
                options = new string[] { "げんげん", "ぎんぎん", "ごんごん", "がんがん" },
                correctAnswerIndex = 1
            },
        };
    }

    void ShowQuestion()
    {
        QuizData question = questions[currentQuestionIndex];
        questionText.text = question.question;
        for (int i = 0; i < optionTexts.Length; i++)
        {
            optionTexts[i].text = question.options[i];
        }
    }

    public void OnOptionSelected(int index)
    {
        if (index == questions[currentQuestionIndex].correctAnswerIndex)
        {
            correctAnswers++;
            Debug.Log("正解!");
        }
        else
        {
            Debug.Log("不正解!");
        }

        // 次の質問に進むか、結果を表示
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            FinishQuiz();
        }
        void FinishQuiz()
        {
            Debug.Log("終了");
            resultText.text = $"クイズ終了！{questions.Length}問中{correctAnswers}問正解しました。";
        }
    }

}