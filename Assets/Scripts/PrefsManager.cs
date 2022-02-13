using UnityEngine;

public static class PrefsManager
{
    private const string CHARACTER_KEY = "Character";
    public static void SaveLastSelectedCharacter(string name)
    {
        PlayerPrefs.SetString(CHARACTER_KEY, name);
        PlayerPrefs.Save();
    }
    
    public static string GetLastSelectedCharacter()
    {
        return PlayerPrefs.GetString(CHARACTER_KEY);
    }
}