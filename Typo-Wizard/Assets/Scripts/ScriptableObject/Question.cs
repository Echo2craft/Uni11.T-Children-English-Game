using System;
using System.Collections.Generic;
using UnityEngine;
using static AccountManager;
using UnityEngine.Networking;
using System.Threading.Tasks;
using UnityEditor;

[Serializable()]
public struct Answer
{
    [SerializeField] public string _info;
    public string Info { get { return _info; } }

    [SerializeField] public bool _isCorrect;
    public bool IsCorrect { get { return _isCorrect; } }
}
[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/new Question")]
public class Question : ScriptableObject {

    public enum                 AnswerType                  { Multi, Single }

    [SerializeField] private    String      _info           = String.Empty;
    public                      String      Info            { get { return _info; } }

    [SerializeField]            Answer[]    _answers        = null;
    public                      Answer[]    Answers         { get { return _answers; } }

    //Parameters

    [SerializeField] private    bool        _useTimer       = false;
    public                      bool        UseTimer        { get { return _useTimer; } }

    [SerializeField] private    int         _timer          = 0;
    public                      int         Timer           { get { return _timer; } }

    [SerializeField] private    AnswerType  _answerType     = AnswerType.Multi;
    public                      AnswerType  GetAnswerType   { get { return _answerType; } }

    [SerializeField] private    int         _addScore       = 10;

    public                      int         AddScore        { get { return _addScore; } }
    public int homeworkQuestionId;
    public string question;
    public object homeworkStatus;
    public object courseStatus;
    public int answersAmount;
    public object homework;
    public object[] homeworkAnswers;
    [Serializable]
    public class HomeworkQuestion
    {
        public int homework_question_id;
        public string question;
        public string type; // Multi or Single
        public List<HomeworkAnswer> answers;
    }

    [Serializable]
    public class HomeworkAnswer
    {
        public int homework_answer_id;
        public string answer;
        public bool is_correct;
    }
    private readonly string _baseUrl = "https://cegwebapi-bsamgfdjgqbyg2fr.eastus-01.azurewebsites.net";
    public async Task FetchHomeworkQuestionsAndCreateAssets(int sessionId)
    {
        string url = $"{_baseUrl}/api/Question/All/BySession/{1}";
        Debug.Log($"Fetching questions from: {url}");

        UnityWebRequest request = UnityWebRequest.Get(url);
        var operation = request.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error fetching questions: {request.error}");
            return;
        }

        string jsonResponse = request.downloadHandler.text;
        List<HomeworkQuestion> questions = JsonUtilityHelper.FromJsonList<HomeworkQuestion>(jsonResponse);

        foreach (var questionData in questions)
        {
            CreateQuestionAsset(questionData);
        }

        Debug.Log("Questions created successfully!");
    }
    private void CreateQuestionAsset(HomeworkQuestion questionData)
    {
        Question newQuestion = ScriptableObject.CreateInstance<Question>();

        // Populate fields
        newQuestion.name = $"Question_{questionData.homework_question_id}";
        newQuestion.question = questionData.question;
        newQuestion.homeworkQuestionId = questionData.homework_question_id;
        newQuestion.answersAmount = questionData.answers.Count;

        // Set answers
        Answer[] answers = new Answer[questionData.answers.Count];
        for (int i = 0; i < questionData.answers.Count; i++)
        {
            answers[i] = new Answer
            {
                _info = questionData.answers[i].answer,
                _isCorrect = questionData.answers[i].is_correct
            };
        }
        newQuestion.SetAnswers(answers);

        // Save the asset
        string path = $"Assets/Questions/Question_{questionData.homework_question_id}.asset";
        //AssetDatabase.CreateAsset(newQuestion, path);
        //AssetDatabase.SaveAssets();

        Debug.Log($"Created Question asset at {path}");
    }
    public void SetAnswers(Answer[] answers)
    {
        _answers = answers;
    }
    /// <summary>
    /// Function that is called to collect and return correct answers indexes.
    /// </summary>
    public List<int> GetCorrectAnswers ()
    {
        List<int> CorrectAnswers = new List<int>();
        for (int i = 0; i < Answers.Length; i++)
        {
            if (Answers[i].IsCorrect)
            {
                CorrectAnswers.Add(i);
            }
        }
        return CorrectAnswers;
    }
}