using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    private const string HighScoreKey = "HighScore";  
    private const string LeaderboardKey = "Leaderboard";  

    public static void AddScore(float score)  
    {
        float highScore = GetHighScore();  

        if (score > highScore)  
        {
            PlayerPrefs.SetFloat(HighScoreKey, score);  
        }

        List<float> scores = GetLeaderboard();  
        scores.Add(score);  

        scores.Sort((a, b) => b.CompareTo(a));  

        if (scores.Count > 5)  
        {
            scores.RemoveRange(5, scores.Count - 5);  
        }

        SaveLeaderboard(scores);  

        PlayerPrefs.Save();  
    }

    public static float GetHighScore()  
    {
        return PlayerPrefs.GetFloat(HighScoreKey, 0f); 
    }

    public static List<float> GetLeaderboard()  
    {
        string data = PlayerPrefs.GetString(LeaderboardKey, "");  
        List<float> scores = new List<float>();  

        if (string.IsNullOrEmpty(data))  
            return scores;

        string[] split = data.Split(',');  

        foreach (string s in split)  
        {
            if (float.TryParse(s, out float value))  
            {
                scores.Add(value);  
            }
        }

        return scores;
    }

    private static void SaveLeaderboard(List<float> scores)  
    {
        string data = "";  

        for (int i = 0; i < scores.Count; i++)  
        {
            data += scores[i].ToString();  

            if (i < scores.Count - 1)  
            {
                data += ",";  
            }
        }

        PlayerPrefs.SetString(LeaderboardKey, data);  
    }
}
