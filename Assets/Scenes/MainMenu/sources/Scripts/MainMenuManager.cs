using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Exact scene names (see Build Profiles)")]
    [SerializeField] private string loadingScene = "LoadScene";
    [SerializeField] private string gameScene = "Game";
    [SerializeField] private string optionsScene = "Options";
    [SerializeField] private string saveScene = "Save";

    public void Play() => SceneLoader.LoadWithLoadingScreen(loadingScene, gameScene);

    public void Options() => SceneLoader.Load(optionsScene);

    public void LoadOrSave() => SceneLoader.Load(saveScene);

    public void Quit() => GameQuitter.Quit();
}