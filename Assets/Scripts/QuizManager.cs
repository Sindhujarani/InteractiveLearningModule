using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;

    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] options = new string[4];
        public int correctIndex;
    }

    private List<Question> questions = new List<Question>();

    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public TextMeshProUGUI quizFeedbackText;
    public TextMeshProUGUI scoreText;
    public Button nextButton;

    public GameObject quizPanel;
    public GameObject resultPanel;
    public TextMeshProUGUI finalScoreText;

    public GameObject[] draggableObjects;
    public GameObject[] slotObjects;
    public GameObject taskTwoUI;

    private int currentIndex = 0;
    private int score = 0;
   

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetupQuestions();
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
        nextButton.gameObject.SetActive(false);
    }

    void SetupQuestions()
    {
        questions.Add(CreateQ("Which object is a fruit?",
            new string[] { "Apple", "Stone", "Chair", "Bottle" }, 0));

        questions.Add(CreateQ("Which object can hold liquid?",
            new string[] { "Chair", "Stone", "Apple", "Bottle" }, 3));

        questions.Add(CreateQ("Which object is used for sitting?",
            new string[] { "Apple", "Chair", "Bottle", "Stone" }, 1));

        questions.Add(CreateQ("Which object is a heavy natural rock?",
            new string[] { "Box", "Chair", "Stone", "Bottle" }, 2));

        questions.Add(CreateQ("Which object is used for storage?",
            new string[] { "Apple", "Stone", "Chair", "Box" }, 3));
    }

    Question CreateQ(string q, string[] opts, int correct)
    {
        return new Question { questionText = q, options = opts, correctIndex = correct };
    }

    public void StartQuiz()
    {
        // Hide draggable objects
        foreach (GameObject obj in draggableObjects)
            obj.SetActive(false);

        // Hide slot objects
        foreach (GameObject slot in slotObjects)
            slot.SetActive(false);

        // Hide Task 2 UI
        if (taskTwoUI != null)
            taskTwoUI.SetActive(false);

        // Show quiz panel
        quizPanel.SetActive(true);
        currentIndex = 0;
        score = 0;
        ShowQuestion();
       
    }

    void ShowQuestion()
    {
        quizFeedbackText.text = "";
        nextButton.gameObject.SetActive(false);

        Question q = questions[currentIndex];
        questionText.text = "Q" + (currentIndex + 1) + ": " + q.questionText;
        scoreText.text = "Score: " + score + "/" + questions.Count;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int idx = i;
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.options[i];
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnAnswerSelected(idx));
            optionButtons[i].interactable = true;
        }
    }

    void OnAnswerSelected(int selected)
    {
        foreach (Button b in optionButtons) b.interactable = false;

        if (selected == questions[currentIndex].correctIndex)
        {
            score++;
            quizFeedbackText.text = "Correct!";
            quizFeedbackText.color = Color.green;
        }
        else
        {
            string correctAnswer = questions[currentIndex].options[questions[currentIndex].correctIndex];
            quizFeedbackText.text = "Wrong! Answer: " + correctAnswer;
            quizFeedbackText.color = Color.red;
        }

        scoreText.text = "Score: " + score + "/" + questions.Count;
        nextButton.gameObject.SetActive(true);
    }

    public void OnNextButton()
    {
        currentIndex++;
        if (currentIndex < questions.Count)
            ShowQuestion();
        else
            ShowResult();
    }

    void ShowResult()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(true);

        string message = score >= 4 ? "Excellent!" : score >= 3 ? "Good job!" : "Keep practicing!";
        finalScoreText.text = "Your Score: " + score + "/" + questions.Count + "\n" + message;
    }

    public void RestartQuiz()
    {
        resultPanel.SetActive(false);
        currentIndex = 0;
        score = 0;
        quizPanel.SetActive(true);
        ShowQuestion();
    }
}