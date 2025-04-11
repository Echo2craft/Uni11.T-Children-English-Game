using System.Collections.Generic;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using static AccountManager;
using UnityEngine.Networking;
using i5.Toolkit.Core.OpenIDConnectClient;
using i5.Toolkit.Core.ServiceCore;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Text = UnityEngine.UI.Text;
using Random = UnityEngine.Random;
public class Spawner : MonoBehaviour
{
    //public string[] easyWords = new string[] { "at", "ant", "eat", "but", "bat", "bat", "dog", "apple", "fish", "bear", "can", "big", "small" }; // 2 - 4 characters
    //public string[] normalWords = new string[] { "scissor", "apollo", "amazing", "mommy", "daddy", "catch", "match", "bitter", "butter", "banner", "attach", "attack" }; // 5 - 8 chars
    //public string[] hardWords = new string[] { "agglutination", "aurantiaceous", "blandiloquence", "blandiloquence", "cephalonomancy", "cheiloproclitic", "dactylopterous", "gastrohysterotomy" }; // > 8 chars
    private List<string> easyWords = new();
    private List<string> normalWords = new();
    private List<string> hardWords = new();

    public GameObject easyPrefab;
    public GameObject normalPrefab;
    public GameObject hardPrefab;
    private bool readyToSpawn = false;
    public float spawnInterval = 5;
    private float _counter = 0;
    [Serializable]
    public class HomeworkQuestion
    {
        public int homework_question_id;
        public string question;
    }

    [System.Serializable]
    public class HomeworkAnswer
    {
        public int homeworkAnswerId;
        public int answerNumber;
        public string answer;
        public string type;
    }
    [System.Serializable]
    private class Wrapper<T>
    {
        public bool status;
        public List<T> data;
    }
    public static class JsonUtilityHelper
    {
        public static List<T> FromJsonList<T>(string json)
        {
            var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            if (wrapper == null || wrapper.data == null)
            {
                Debug.LogError("Wrapper or data is null. JSON: " + json);
                return new List<T>();
            }
            return wrapper.data;
        }
    }
    private readonly string _baseUrl = "https://cegwebapi-bsamgfdjgqbyg2fr.eastus-01.azurewebsites.net";
    // Start is called before the first frame update
    async void Start()
    {
        //await FetchHomeworkQuestions();
        await FetchHomeworkAnswer();

        Debug.Log($"Easy: {easyWords.Count}, Normal: {normalWords.Count}, Hard: {hardWords.Count}");

        if (easyWords.Count > 0 || normalWords.Count > 0 || hardWords.Count > 0)
        {
            readyToSpawn = true;
            Debug.Log("Ready to spawn!");
        }
        else
        {
            Debug.LogWarning("No words found to spawn.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!readyToSpawn) return;

        _counter += Time.deltaTime;

        if (_counter >= spawnInterval)
        {
            _counter = 0 + Random.Range(0, Time.deltaTime * 10);
            Spawn();
        }
    }
    public async Task FetchHomeworkAnswer()
    {
        string url = $"{_baseUrl}/api/Answer/All";
        Debug.Log($"[Answer] Sending request to: {url}");

        UnityWebRequest request = UnityWebRequest.Get(url);
        var operation = request.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[Answer] Error: {request.error}");
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log($"[Answer] Response: {jsonResponse}");

            List<HomeworkAnswer> answers = JsonUtilityHelper.FromJsonList<HomeworkAnswer>(jsonResponse);
            Debug.Log("Easy: " + answers.Count);

            foreach (var answer in answers)
            {
                Debug.Log($"[Answer] ID: {answer.homeworkAnswerId}, Text: {answer.answer}");

                if (answer.answer.Length <= 2)
                {
                    easyWords.Add(answer.answer);
                }if (answer.answer.Length <= 4)
                {
                    normalWords.Add(answer.answer);
                }
                else
                {
                    hardWords.Add(answer.answer);
                }
            }

            Debug.Log($"[After Parse] easyWords: {easyWords.Count}, normalWords: {normalWords.Count}");
            foreach (var word in easyWords) Debug.Log($"Easy: {word}");
            foreach (var word in normalWords) Debug.Log($"Normal: {word}");
            foreach (var word in hardWords) Debug.Log($"Normal: {word}");
        }
    }
    //public async Task FetchHomeworkQuestions()
    //{
    //    string url = $"{_baseUrl}/api/Question/All";
    //    Debug.Log($"[Question] Sending request to: {url}");

    //    UnityWebRequest request = UnityWebRequest.Get(url);
    //    var operation = request.SendWebRequest();

    //    while (!operation.isDone)
    //    {
    //        await Task.Yield();
    //    }

    //    if (request.result != UnityWebRequest.Result.Success)
    //    {
    //        Debug.LogError($"[Question] Error: {request.error}");
    //    }
    //    else
    //    {
    //        string jsonResponse = request.downloadHandler.text;
    //        Debug.Log($"[Question] Response: {jsonResponse}");

    //        List<HomeworkQuestion> questions = JsonUtilityHelper.FromJsonList<HomeworkQuestion>(jsonResponse);
    //        Debug.Log($"[Question] Parsed {questions.Count} items");

    //        foreach (var question in questions)
    //        {
    //            Debug.Log($"[Question] ID: {question.homework_question_id}, Text: {question.question}");
    //            hardWords.Add(question.question);
    //        }

    //        Debug.Log($"[After Parse] hardWords: {hardWords.Count}");
    //        foreach (var word in hardWords) Debug.Log($"Hard: {word}");
    //    }
    //}
    string FixJson(string value)
    {
        return "{\"data\":" + value + "}";
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
    private GameObject Spawn()
    {
        var random = Random.Range(0, 10);

        GameObject prefab;
        List<string> words;
        float boardScale = 1;

        if (random < 6 && easyWords.Count > 0)
        {
            prefab = easyPrefab;
            words = easyWords;
        }
        else if (random < 9 && normalWords.Count > 0)
        {
            prefab = normalPrefab;
            words = normalWords;
            boardScale = 2f;
        }
        else if (hardWords.Count > 0)
        {
            prefab = hardPrefab;
            words = hardWords;
            boardScale = 3f;
        }
        else return null; // skip spawn if no words available

        var go = Instantiate(prefab);
        var board = go.transform.GetChild(1).transform.GetChild(1);
        board.transform.localScale = new Vector3(board.transform.localScale.x * boardScale, board.transform.localScale.y, 1);
        var boardSize = board.GetComponent<SpriteRenderer>().bounds.size;
        var spawnY = Screen.height + boardSize.y;
        var spawnX = Random.Range(200, Screen.width - 200);
        go.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(spawnX, spawnY, -Camera.main.transform.position.z));

        var textMesh = go.GetComponentInChildren<TextMeshPro>();
        textMesh.text = words[Random.Range(0, words.Count)];

        Controller.INSTANCE.Register(textMesh.text, go);

        return go;
    }
    //private GameObject Spawn()
    //{
    //    var random = Random.Range(0, 10); // 0 - 5 Easy; 6 - 8 Normal; 9 Hard

    //    GameObject prefab;
    //    string[] words;
    //    float boardScale = 1;

    //    if (random < 6)
    //    {
    //        prefab = easyPrefab;
    //        words = easyWords;
    //    } else if (random < 9)
    //    {
    //        prefab = normalPrefab;
    //        words = normalWords;
    //        boardScale = 2f;
    //    } else
    //    {
    //        prefab = hardPrefab;
    //        words = hardWords;
    //        boardScale = 3f;
    //    }

    //    var go = Instantiate(prefab);

    //    // Get a random spawn position
    //    var board = go.transform.GetChild(1).transform.GetChild(1);
    //    board.transform.localScale = new Vector3(board.transform.localScale.x * boardScale, board.transform.localScale.y, 1);
    //    var boardSize = board.GetComponent<SpriteRenderer>().bounds.size;
    //    var spawnY = Screen.height + boardSize.y;
    //    var spawnX = Random.Range(200, Screen.width - 200);
    //    go.transform.position = Camera.main!.ScreenToWorldPoint(new Vector3(spawnX, spawnY, -Camera.main.transform.position.z));

    //    // Assign random text to object
    //    var textMesh = go.GetComponentInChildren<TextMeshPro>();
    //    textMesh.text = words[Random.Range(0, words.Count())];

    //    // Associate this object with the text
    //    Controller.INSTANCE.Register(textMesh.text, go);

    //    return go;
    //}
}
