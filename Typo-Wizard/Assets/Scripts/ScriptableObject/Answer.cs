//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//    public class Answer : ScriptableObject
//    {
//    [SerializeField] private string _info;
//    public string Info { get { return _info; } }

//    [SerializeField] private bool _isCorrect;
//    public bool IsCorrect { get { return _isCorrect; } }

//    public List<int> GetCorrectAnswers()
//    {
//        List<int> CorrectAnswers = new List<int>();
//        for (int i = 0; i < Answers.Length; i++)
//        {
//            if (Answers[i].IsCorrect)
//            {
//                CorrectAnswers.Add(i);
//            }
//        }
//        return CorrectAnswers;
//    }
//}
