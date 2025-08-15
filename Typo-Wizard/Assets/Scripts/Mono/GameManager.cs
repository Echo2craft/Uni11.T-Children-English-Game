using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using static AccountManager;

public class GameManager : MonoBehaviour {

    #region Variables
    private DateTime gameStartTime;
    private             Question[]          _questions              = null;
    public              Question[]          Questions               { get { return _questions; } }

    [SerializeField]    GameEvents2          events                  = null;

    [SerializeField]    Animator            timerAnimtor            = null;
    [SerializeField]    TextMeshProUGUI     timerText               = null;
    [SerializeField]    Color               timerHalfWayOutColor    = Color.yellow;
    [SerializeField]    Color               timerAlmostOutColor     = Color.red;
    private             Color               timerDefaultColor       = Color.white;

    private             List<AnswerData>    PickedAnswers           = new List<AnswerData>();
    private             List<int>           FinishedQuestions       = new List<int>();
    private             int                 currentQuestion         = 0;

    private             int                 timerStateParaHash      = 0;

    private             IEnumerator         IE_WaitTillNextRound    = null;
    private             IEnumerator         IE_StartTimer           = null;
    private             int                 rawScore                = 0;
    [SerializeField]    private int         globalTimerDuration     = 90; // 1m30s for all questions
    private             bool                isDataLoaded            = false;
    private             bool                IsFinished
    {
        get
        {
            return (FinishedQuestions.Count < Questions.Length) ? false : true;
        }
    }
    #endregion

    #region Default Unity methods

    /// <summary>
    /// Function that is called when the object becomes enabled and active
    /// </summary>
    void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswers;
    }
    /// <summary>
    /// Function that is called when the behaviour becomes disabled
    /// </summary>
    void OnDisable()
    {
        events.UpdateQuestionAnswer -= UpdateAnswers;
    }

    /// <summary>
    /// Function that is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    void Awake()
    {
        events.CurrentFinalScore = 0;
    }
    /// <summary>
    /// Function that is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        Debug.Log("GameManager Start method called.");
        gameStartTime = DateTime.Now;
        events.StartupHighscore = PlayerPrefs.GetInt(GameUtility2.SavePrefKey);

        timerDefaultColor = timerText.color;
        LoadQuestions();
        //StartCoroutine(LoadQuestionsFromAPI());
        
        timerStateParaHash = Animator.StringToHash("TimerState");

        var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);

        Debug.Log("Calling Display()...");
        
        Display();
    }
    //IEnumerator WaitForQuestionsToLoad()
    //{
    //    // Wait until the data is loaded from the API
    //    while (!isDataLoaded)
    //    {
    //        Debug.Log("Waiting for questions to load...");
    //        yield return null; // Wait one frame and check again
    //    }

    //    // Now that the questions are loaded, display them
    //    Debug.Log("Questions are now loaded. Proceeding...");
    //    Display();  // Call Display when data is ready
    //}

    #endregion
    private readonly string _baseUrl = "https://cegwebapi-bsamgfdjgqbyg2fr.eastus-01.azurewebsites.net";
    /// <summary>
    /// Function that is called to update new selected answer.
    /// </summary>
    public void UpdateAnswers(AnswerData newAnswer)
    {
        if (Questions[currentQuestion].GetAnswerType == Question.AnswerType.Single)
        {
            foreach (var answer in PickedAnswers)
            {
                if (answer != newAnswer)
                {
                    answer.Reset();
                }
            }
            PickedAnswers.Clear();
            PickedAnswers.Add(newAnswer);
        }
        else
        {
            bool alreadyPicked = PickedAnswers.Exists(x => x == newAnswer);
            if (alreadyPicked)
            {
                PickedAnswers.Remove(newAnswer);
            }
            else
            {
                PickedAnswers.Add(newAnswer);
            }
        }
    }

    /// <summary>
    /// Function that is called to clear PickedAnswers list.
    /// </summary>
    public void EraseAnswers()
    {
        PickedAnswers = new List<AnswerData>();
    }

    /// <summary>
    /// Function that is called to display new question.
    /// </summary>
    void Display()
    {
        Debug.Log("Display function called.");

        //StartCoroutine(LoadQuestionsFromAPI()); // Ensure this is called correctly.
        if (_questions == null || _questions.Length == 0)
        {
            Debug.LogError("Questions array is not populated.");
            return;
        }
        EraseAnswers();
        var question = GetRandomQuestion();

        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(question);
        }
        else
        {
            Debug.LogWarning("GameEvents.UpdateQuestionUI is null. Issue occurred in GameManager.Display() method.");
        }

        if (question.UseTimer)
        {
            UpdateTimer(true); // Pass true to start the timer
        }
    }


    /// <summary>
    /// Function that is called to accept picked answers and check/display the result.
    /// </summary>
    //public void Accept()
    //{
    //    UpdateTimer(false);
    //    bool isCorrect = CheckAnswers();
    //    FinishedQuestions.Add(currentQuestion);

    //    UpdateScore((isCorrect) ? Questions[currentQuestion].AddScore : -Questions[currentQuestion].AddScore);

    //    if (IsFinished)
    //    {
    //        SetHighscore();
    //    }

    //    var type 
    //        = (IsFinished) 
    //        ? UIManager2.ResolutionScreenType.Finish 
    //        : (isCorrect) ? UIManager2.ResolutionScreenType.Correct 
    //        : UIManager2.ResolutionScreenType.Incorrect;

    //    if (events.DisplayResolutionScreen != null)
    //    {
    //        events.DisplayResolutionScreen(type, Questions[currentQuestion].AddScore);
    //    }

    //    AudioManager.Instance.PlaySound((isCorrect) ? "CorrectSFX" : "IncorrectSFX");

    //    if (type != UIManager2.ResolutionScreenType.Finish)
    //    {
    //        if (IE_WaitTillNextRound != null)
    //        {
    //            StopCoroutine(IE_WaitTillNextRound);
    //        }
    //        IE_WaitTillNextRound = WaitTillNextRound();
    //        StartCoroutine(IE_WaitTillNextRound);
    //    }
    //}
    public void Accept()
    {
        UpdateTimer(false); // Stop the timer
        bool isCorrect = CheckAnswers(); // Check if the answer is correct
        FinishedQuestions.Add(currentQuestion); // Mark the question as finished

        // Update score only if the answer is correct
        if (isCorrect)
        {
            UpdateScore(Questions[currentQuestion].AddScore);
        }

        // Display the resolution screen based on correctness or finish
        var type = (IsFinished)
            ? UIManager2.ResolutionScreenType.Finish
            : (isCorrect) ? UIManager2.ResolutionScreenType.Correct
            : UIManager2.ResolutionScreenType.Incorrect;

        if (events.DisplayResolutionScreen != null)
        {
            events.DisplayResolutionScreen(type, Questions[currentQuestion].AddScore);
        }

        AudioManager.Instance.PlaySound(isCorrect ? "CorrectSFX" : "IncorrectSFX");

        // Wait for the next round if not finished
        if (type != UIManager2.ResolutionScreenType.Finish)
        {
            if (IE_WaitTillNextRound != null)
            {
                StopCoroutine(IE_WaitTillNextRound);
            }
            IE_WaitTillNextRound = WaitTillNextRound();
            StartCoroutine(IE_WaitTillNextRound);
        }
    }
    #region Timer Methods

    //void UpdateTimer(bool state)
    //{
    //    switch (state)
    //    {
    //        case true:
    //            IE_StartTimer = StartTimer();
    //            StartCoroutine(IE_StartTimer);

    //            timerAnimtor.SetInteger(timerStateParaHash, 2);
    //            break;
    //        case false:
    //            if (IE_StartTimer != null)
    //            {
    //                StopCoroutine(IE_StartTimer);
    //            }

    //            timerAnimtor.SetInteger(timerStateParaHash, 1);
    //            break;
    //    }
    //}
    //IEnumerator StartTimer()
    //{
    //    var totalTime = Questions[currentQuestion].Timer;
    //    var timeLeft = totalTime;

    //    timerText.color = timerDefaultColor;
    //    while (timeLeft > 0)
    //    {
    //        timeLeft--;

    //        AudioManager.Instance.PlaySound("CountdownSFX");

    //        if (timeLeft < totalTime / 2 && timeLeft > totalTime / 4)
    //        {
    //            timerText.color = timerHalfWayOutColor;
    //        }
    //        if (timeLeft < totalTime / 4)
    //        {
    //            timerText.color = timerAlmostOutColor;
    //        }

    //        timerText.text = timeLeft.ToString();
    //        yield return new WaitForSeconds(1.0f);
    //    }
    //    Accept();
    //}
    void UpdateTimer(bool state)
    {
        switch (state)
        {
            case true:
                IE_StartTimer = StartTimer(globalTimerDuration); // Use global duration
                StartCoroutine(IE_StartTimer);

                timerAnimtor.SetInteger(timerStateParaHash, 2);
                break;
            case false:
                if (IE_StartTimer != null)
                {
                    StopCoroutine(IE_StartTimer);
                }

                timerAnimtor.SetInteger(timerStateParaHash, 1);
                break;
        }
    }

    IEnumerator StartTimer(int duration)
    {
        int timeLeft = duration; // Use the passed duration

        timerText.color = timerDefaultColor; // Reset timer color

        while (timeLeft > 0)
        {
            timeLeft--;

            // Play countdown sound (optional)
            AudioManager.Instance.PlaySound("CountdownSFX");

            // Update the timer color based on remaining time
            if (timeLeft < duration / 2 && timeLeft > duration / 4)
            {
                timerText.color = timerHalfWayOutColor;
            }
            if (timeLeft < duration / 4)
            {
                timerText.color = timerAlmostOutColor;
            }

            // Update the timer text display
            int minutes = timeLeft / 60;
            int seconds = timeLeft % 60;
            timerText.text = $"{minutes:00}:{seconds:00}"; // Format MM:SS

            yield return new WaitForSeconds(1.0f);
        }

        Accept(); // When time is up, proceed to accept answers
    }
    IEnumerator WaitTillNextRound()
    {
        yield return new WaitForSeconds(GameUtility2.ResolutionDelayTime);
        Display();
    }

    #endregion

    /// <summary>
    /// Function that is called to check currently picked answers and return the result.
    /// </summary>
    bool CheckAnswers()
    {
        if (!CompareAnswers())
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// Function that is called to compare picked answers with question correct answers.
    /// </summary>
    bool CompareAnswers()
    {
        if (PickedAnswers.Count > 0)
        {
            List<int> c = Questions[currentQuestion].GetCorrectAnswers();
            List<int> p = PickedAnswers.Select(x => x.AnswerIndex).ToList();

            var f = c.Except(p).ToList();
            var s = p.Except(c).ToList();

            return !f.Any() && !s.Any();
        }
        return false;
    }

    /// <summary>
    /// Function that is called to load all questions from the Resource folder.
    /// </summary>
    void LoadQuestions()
    {
        UnityEngine.Object[] objs = Resources.LoadAll("Questions", typeof(Question));
        _questions = new Question[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _questions[i] = (Question)objs[i];
        }
    }
    //private IEnumerator LoadQuestionsFromAPI()
    //{
    //    Debug.Log("Starting to load questions from API...");
    //    string url = "https://cegwebapi-bsamgfdjgqbyg2fr.eastus-01.azurewebsites.net/api/Question/All";
    //    UnityWebRequest request = UnityWebRequest.Get(url);

    //    // Start the request
    //    yield return request.SendWebRequest();

    //    // Check for errors in the response
    //    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    //    {
    //        Debug.LogError("Failed to load questions: " + request.error);
    //    }
    //    else
    //    {
    //        Debug.Log("Successfully received response from API.");

    //        // Parse the response (assuming JSON)
    //        string json = request.downloadHandler.text;
    //        Debug.Log("Received JSON: " + json); // Log the JSON string

    //        try
    //        {
    //            // Check if JSON is empty or null before parsing
    //            if (string.IsNullOrEmpty(json))
    //            {
    //                Debug.LogError("Received an empty or null JSON response.");
    //                yield break; // Exit the method early if JSON is invalid
    //            }

    //            // Deserialize the response into an object containing the 'data' field
    //            ApiResponse response = JsonUtility.FromJson<ApiResponse>(json);

    //            // Check if the response object is null
    //            if (response == null)
    //            {
    //                Debug.LogError("Failed to deserialize JSON response into ApiResponse object.");
    //                yield break;
    //            }

    //            // Check if the data field is null or empty
    //            if (response.data == null)
    //            {
    //                Debug.LogError("The 'data' field in the API response is null.");
    //                yield break; // Exit early if 'data' is null
    //            }

    //            if (response.data.Length == 0)
    //            {
    //                Debug.LogError("The 'data' array is empty.");
    //                yield break; // Exit early if the data array is empty
    //            }

    //            // Log the received data to check if it's properly populated
    //            Debug.Log("Data received: " + response.data.Length + " questions.");

    //            // Assign the questions to _questions
    //            _questions = response.data;

    //            // Log the length of _questions
    //            Debug.Log($"Number of questions loaded: {_questions.Length}");

    //            // Log the actual content of _questions
    //            for (int i = 0; i < _questions.Length; i++)
    //            {
    //                Debug.Log($"Question {i + 1}: {_questions[i].question}"); // Adjust field names as necessary
    //            }

    //            // Set the flag to true once data is loaded
    //            isDataLoaded = true;
    //            Debug.Log($"Successfully parsed {_questions.Length} questions from API.");
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.LogError("Failed to parse questions JSON: " + ex.Message);
    //        }
    //    }
    //}

    // Helper class to represent the response structure
    [System.Serializable]
    public class ApiResponse
    {
        public bool status;
        public Question[] data;
    }


    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string wrappedJson = "{\"Items\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(wrappedJson);
            return wrapper.Items;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
    /// <summary>
    /// Function that is called restart the game.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Function that is called to quit the application.
    /// </summary>
    public void QuitGame(string sceneName)
    {
        QuitGamePressed();
        SceneManager.LoadScene(sceneName); ;
    }
    /// <summary>
    /// Function that is called to set new highscore if game score is higher.
    /// </summary>
    private void SetHighscore()
    {
        var highscore = PlayerPrefs.GetInt(GameUtility2.SavePrefKey);
        if (highscore < events.CurrentFinalScore)
        {
            PlayerPrefs.SetInt(GameUtility2.SavePrefKey, events.CurrentFinalScore);
        }
    }
    /// <summary>
    /// Function that is called update the score and update the UI.
    /// </summary>
    private void UpdateScore(int add)
    {
        // Update the score but prevent it from going below 0
        events.CurrentFinalScore = Mathf.Max(0, events.CurrentFinalScore + add);

        // Trigger the ScoreUpdated event if it is not null
        if (events.ScoreUpdated != null)
        {
            events.ScoreUpdated();
        }
    }

    #region Getters

    Question GetRandomQuestion()
    {
        var randomIndex = GetRandomQuestionIndex();
        currentQuestion = randomIndex;

        return Questions[currentQuestion];
    }
    int GetRandomQuestionIndex()
    {
        var random = 0;
        if (FinishedQuestions.Count < Questions.Length)
        {
            do
            {
                random = UnityEngine.Random.Range(0, Questions.Length);
            } while (FinishedQuestions.Contains(random) || random == currentQuestion);
        }
        return random;
    }
    public async void SendStudentAnswer(int studentanswerId, int gameId, int studentHomeworkId, string answer, string type)
    {
        // Create a new StudentAnswer object
        var studentAnswerRequest = new StudentAnswerRequest()
        {
            student_answer_id = studentanswerId,
            game_id = 1,
            student_homework_id = studentHomeworkId,
            answer = answer,
            type = type
        };

        // Convert the object to JSON
        string jsonRequestBody = JsonUtility.ToJson(studentAnswerRequest);
        Debug.Log("Send StudentAnswer: " + jsonRequestBody);

        // Define the URL for StudentAnswer API
        string url = $"{_baseUrl}/api/StudentAnswer/Create";

        await SendPostRequest(url, jsonRequestBody);
    }
    public async void SendStudentHomework(int studentHomeworkId, int homeworkId, int studentProgressId, int homeworkResultId, int point, TimeSpan playtime, string status, int correctAnswers)
    {
        // Create a new StudentHomework object
        var studentHomeworkRequest = new StudentHomeworkRequest()
        {
            student_homework_id = studentHomeworkId,
            homework_id = homeworkId,
            student_progress_id = studentProgressId,
            homework_result_id = homeworkResultId,
            point = point,
            playtime = playtime.ToString(@"hh\:mm\:ss"),
            status = status,
            correct_answers = correctAnswers
        };

        // Convert the object to JSON
        string jsonRequestBody = JsonUtility.ToJson(studentHomeworkRequest);
        Debug.Log("Send StudentHomework: " + jsonRequestBody);

        // Define the URL for StudentHomework API
        string url = $"{_baseUrl}/api/StudentHomework/Create";

        await SendPostRequest(url, jsonRequestBody);
    }
    private async Task SendPostRequest(string url, string jsonRequestBody)
    {
        // Create a UnityWebRequest POST object
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        // Set the request headers
        request.SetRequestHeader("Content-Type", "application/json");

        // Attach the JSON data to the request
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Send the request
        var operation = request.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error sending POST request to {url}: {request.error}");
        }
        else
        {
            Debug.Log($"POST request to {url} successful! Response: {request.downloadHandler.text}");
        }
    }
    public async void SendHomeworkResult(int homeworkResultId, int totalPoint, int totalCorrectAnswers, TimeSpan playtime)
    {
        var homeworkResultRequest = new HomeworkResultRequest()
        {
            homework_result_id = homeworkResultId,
            total_point = totalPoint,
            total_correct_answers = totalCorrectAnswers,
            playtime = playtime.ToString(@"hh\:mm\:ss")
        };

        string jsonRequestBody = JsonUtility.ToJson(homeworkResultRequest);
        Debug.Log("Send HomeworkResult: " + jsonRequestBody);

        string url = $"{_baseUrl}/api/HomeworkResult/Create";
        await SendPostRequest(url, jsonRequestBody);
    }
    public async void SendStudentProgress(int studentProgressId, int studentId, int classId, int totalPoint, TimeSpan playtime)
    {
        var studentProgressRequest = new StudentProgressRequest()
        {
            student_progress_id = studentProgressId,
            student_id = studentId,
            class_id = classId,
            total_point = totalPoint,
            playtime = playtime.ToString(@"hh\:mm\:ss")
        };

        string jsonRequestBody = JsonUtility.ToJson(studentProgressRequest);
        Debug.Log("Send StudentProgress: " + jsonRequestBody);

        string url = $"{_baseUrl}/api/StudentProgress/Create";
        await SendPostRequest(url, jsonRequestBody);
    }
    public void QuitGamePressed()
    {
        TimeSpan playtime = DateTime.Now - gameStartTime;
        string formattedPlaytime = playtime.ToString(@"hh\:mm\:ss\.fffffff");
        SendStudentProgress(90, 1, 1, 80, playtime);
        SendStudentHomework(69, 1, 90, 1, 80, playtime, "Submitted", 8);
        SendStudentAnswer(22, 1, 1, "to", "Correct");
        SendStudentAnswer(23, 1, 1, "at", "Correct");
        SendStudentAnswer(24, 1, 1, "of", "Correct");
        SendStudentAnswer(25, 1, 1, "in", "Correct");
        SendStudentAnswer(26, 1, 1, "of", "Correct");
        SendStudentAnswer(27, 1, 1, "about", "Correct");
        SendStudentAnswer(28, 1, 1, "for", "Correct");
        SendStudentAnswer(29, 1, 1, "for", "Correct");
        SendStudentAnswer(30, 1, 1, "to", "Incorrect");
        SendStudentAnswer(31, 1, 1, "off", "Incorrect");
        
        
        SendHomeworkResult(55, 80, 8, playtime);
        
    }
    //Question GetRandomQuestion()
    //{
    //    var randomID = GetRandomQuestionID();
    //    currentQuestion = Array.FindIndex(Questions, q => q.homeworkQuestionId == randomID); // Find index by ID

    //    return Questions[currentQuestion];
    //}

    //int GetRandomQuestionID()
    //{
    //    if (Questions == null || Questions.Length == 0)
    //    {
    //        Debug.LogError("Questions array is null or empty.");
    //        return -1; // Return an invalid ID
    //    }

    //    List<int> unfinishedQuestionIDs = Questions
    //        .Where(q => q != null && !FinishedQuestions.Contains(q.homeworkQuestionId)) // Add null check
    //        .Select(q => q.homeworkQuestionId)
    //        .ToList();

    //    if (unfinishedQuestionIDs == null || unfinishedQuestionIDs.Count == 0)
    //    {
    //        Debug.LogError("No unfinished questions found.");
    //        return -1; // Return an invalid ID
    //    }

    //    int randomID = unfinishedQuestionIDs[UnityEngine.Random.Range(0, unfinishedQuestionIDs.Count)];
    //    Debug.Log($"Random question ID selected: {randomID}");
    //    return randomID;
    //}
    #endregion
}