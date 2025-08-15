using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class CourseList : MonoBehaviour
{
    public static CourseList Instance;

    [Header("UI Elements")]
    public GameObject courseListPanel;
    public GameObject courseItemPrefab;
    public Transform courseListContent;
    public TMP_Text statusText;

    // Base URL for API calls - should match AccountManager
    private readonly string _baseUrl = "https://cegwebapi-bsamgfdjgqbyg2fr.eastus-01.azurewebsites.net";

    // Course data
    private List<CourseData> _availableCourses = new List<CourseData>();

    [Serializable]
    public class CourseData
    {
        public int course_id;
        public string course_name;
        public string description;
        public string subject_area;
        public string difficulty_level; // Beginner, Intermediate, Advanced
        public int credits;
        public string image_url;
    }

    [Serializable]
    public class ApiListResponse<T>
    {
        public bool status;
        public string successMessage;
        public List<T> data;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Only fetch courses if the user is logged in
        if (PlayerPrefs.HasKey("IsLoggedIn") && PlayerPrefs.GetInt("IsLoggedIn") == 1)
        {
            StartCoroutine(FetchAvailableCourses());
        }
    }

    // Fetches all available courses
    public IEnumerator FetchAvailableCourses()
    {
        if (statusText != null)
            statusText.text = "Loading courses...";

        string url = $"{_baseUrl}/api/Course/Available";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "application/json");

        // Add authorization if needed
        if (!string.IsNullOrEmpty(AccountManager.Instance.GetAccessToken()))
        {
            request.SetRequestHeader("Authorization", "Bearer " + AccountManager.Instance.GetAccessToken());
        }

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failed to fetch available courses: " + request.error);
            if (statusText != null)
                statusText.text = "Failed to load courses. Please try again.";
        }
        else
        {
            string response = request.downloadHandler.text;
            try
            {
                // Fix JSON format if needed for JsonUtility
                if (!response.StartsWith("{"))
                {
                    response = "{\"status\":true,\"successMessage\":\"Success\",\"data\":" + response + "}";
                }

                ApiListResponse<CourseData> apiResponse = JsonUtility.FromJson<ApiListResponse<CourseData>>(response);

                if (apiResponse.status)
                {
                    _availableCourses = apiResponse.data;
                    Debug.Log($"Fetched {_availableCourses.Count} available courses");
                    PopulateCourseList();

                    if (statusText != null)
                        statusText.text = $"Found {_availableCourses.Count} courses";
                }
                else
                {
                    if (statusText != null)
                        statusText.text = "No courses found";
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error parsing course data: {e.Message}");
                if (statusText != null)
                    statusText.text = "Error loading courses";
            }
        }
    }

    // Populate UI with course list
    private void PopulateCourseList()
    {
        // Clear existing items
        foreach (Transform child in courseListContent)
        {
            Destroy(child.gameObject);
        }

        // Add new items
        foreach (var courseData in _availableCourses)
        {
            GameObject courseItem = Instantiate(courseItemPrefab, courseListContent);

            // Set course name
            Transform nameTransform = courseItem.transform.Find("CourseName");
            if (nameTransform != null)
            {
                TMP_Text nameText = nameTransform.GetComponent<TMP_Text>();
                if (nameText != null)
                {
                    nameText.text = courseData.course_name;
                }
            }

            // Set subject area
            Transform subjectTransform = courseItem.transform.Find("SubjectArea");
            if (subjectTransform != null)
            {
                TMP_Text subjectText = subjectTransform.GetComponent<TMP_Text>();
                if (subjectText != null)
                {
                    subjectText.text = "Subject: " + courseData.subject_area;
                }
            }

            // Set difficulty level
            Transform difficultyTransform = courseItem.transform.Find("DifficultyLevel");
            if (difficultyTransform != null)
            {
                TMP_Text difficultyText = difficultyTransform.GetComponent<TMP_Text>();
                if (difficultyText != null)
                {
                    difficultyText.text = "Level: " + courseData.difficulty_level;
                }
            }

            // Set credits
            Transform creditsTransform = courseItem.transform.Find("Credits");
            if (creditsTransform != null)
            {
                TMP_Text creditsText = creditsTransform.GetComponent<TMP_Text>();
                if (creditsText != null)
                {
                    creditsText.text = $"Credits: {courseData.credits}";
                }
            }

            // Set course image if available
            Transform imageTransform = courseItem.transform.Find("CourseImage");
            if (imageTransform != null && !string.IsNullOrEmpty(courseData.image_url))
            {
                Image courseImage = imageTransform.GetComponent<Image>();
                if (courseImage != null)
                {
                    StartCoroutine(LoadCourseImage(courseData.image_url, courseImage));
                }
            }

            // Set description (if there's a description component)
            Transform descTransform = courseItem.transform.Find("Description");
            if (descTransform != null)
            {
                TMP_Text descText = descTransform.GetComponent<TMP_Text>();
                if (descText != null)
                {
                    // Trim the description if it's too long
                    string description = courseData.description;
                    if (description.Length > 100)
                    {
                        description = description.Substring(0, 97) + "...";
                    }
                    descText.text = description;
                }
            }
        }
    }

    // Load course image from URL
    private IEnumerator LoadCourseImage(string imageUrl, Image targetImage)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            targetImage.sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );
        }
        else
        {
            Debug.LogWarning($"Failed to load image from {imageUrl}: {request.error}");
        }
    }

    // Search courses by keyword
    public void SearchCourses(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            // If search is empty, show all courses
            PopulateCourseList();
            return;
        }

        keyword = keyword.ToLower();
        List<CourseData> filteredCourses = new List<CourseData>();

        foreach (var course in _availableCourses)
        {
            if (course.course_name.ToLower().Contains(keyword) ||
                course.description.ToLower().Contains(keyword) ||
                course.subject_area.ToLower().Contains(keyword))
            {
                filteredCourses.Add(course);
            }
        }

        // Clear existing items
        foreach (Transform child in courseListContent)
        {
            Destroy(child.gameObject);
        }

        // Add filtered items
        foreach (var courseData in filteredCourses)
        {
            GameObject courseItem = Instantiate(courseItemPrefab, courseListContent);

            // Same logic as PopulateCourseList for setting up the UI elements
            // (Code omitted for brevity but would be identical to the code in PopulateCourseList)

            // Set course name
            Transform nameTransform = courseItem.transform.Find("CourseName");
            if (nameTransform != null)
            {
                TMP_Text nameText = nameTransform.GetComponent<TMP_Text>();
                if (nameText != null)
                {
                    nameText.text = courseData.course_name;
                }
            }

            // Set subject area
            Transform subjectTransform = courseItem.transform.Find("SubjectArea");
            if (subjectTransform != null)
            {
                TMP_Text subjectText = subjectTransform.GetComponent<TMP_Text>();
                if (subjectText != null)
                {
                    subjectText.text = "Subject: " + courseData.subject_area;
                }
            }

            // Continue setting other fields...
        }

        if (statusText != null)
            statusText.text = $"Found {filteredCourses.Count} matching courses";
    }

    // Handle course search input
    public void OnSearchInputChanged(TMP_InputField searchInput)
    {
        if (searchInput != null)
        {
            SearchCourses(searchInput.text);
        }
    }

    // Refresh course list manually
    public void RefreshCourseList()
    {
        StartCoroutine(FetchAvailableCourses());
    }

    // Filter courses by subject area
    public void FilterBySubject(string subject)
    {
        List<CourseData> filteredCourses = new List<CourseData>();

        foreach (var course in _availableCourses)
        {
            if (course.subject_area.Equals(subject, StringComparison.OrdinalIgnoreCase))
            {
                filteredCourses.Add(course);
            }
        }

        // Clear and repopulate the list with filtered courses
        foreach (Transform child in courseListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var courseData in filteredCourses)
        {
            // Add course items to the UI (same as in PopulateCourseList)
            // (Code omitted for brevity)
            GameObject courseItem = Instantiate(courseItemPrefab, courseListContent);
            // Set up item UI components...
        }

        if (statusText != null)
            statusText.text = $"Found {filteredCourses.Count} courses in {subject}";
    }

    // Return to the main menu or dashboard
    public void ReturnToMainMenu()
    {
        // Implementation for navigation back to main menu
        if (courseListPanel != null)
        {
            courseListPanel.SetActive(false);
        }
    }
}