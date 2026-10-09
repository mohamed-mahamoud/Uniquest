using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static string TargetScene { get; private set; }

    public static void Load(string sceneName)
    {
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"Scene not found: {sceneName}. Add it in Build Profiles > Scene List.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public static void LoadWithLoadingScreen(string loadingScene, string targetScene)
    {
        TargetScene = targetScene;
        Load(loadingScene);
    }
}