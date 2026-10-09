using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text continuePrompt;

    [Header("Settings")]
    [SerializeField] private string fallbackScene = "Game";
    [SerializeField] private float minDisplayTime = 3f;
    [SerializeField] private bool waitForInput = true;

    private float displayedProgress;

    private IEnumerator Start()
    {
        string target = string.IsNullOrEmpty(SceneLoader.TargetScene)
            ? fallbackScene
            : SceneLoader.TargetScene;

        if (continuePrompt != null)
            continuePrompt.gameObject.SetActive(false);

        SetProgress(0f);

        if (!Application.CanStreamedLevelBeLoaded(target))
        {
            Debug.LogError($"Scene not found: {target}. Add it in Build Profiles > Scene List.");
            yield break;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(target);
        operation.allowSceneActivation = false;

        float timer = 0f;

        while (displayedProgress < 1f)
        {
            timer += Time.deltaTime;

            float loadProgress = Mathf.Clamp01(operation.progress / 0.9f);
            float timeProgress = Mathf.Clamp01(timer / minDisplayTime);
            float targetProgress = Mathf.Min(loadProgress, timeProgress);

            displayedProgress = Mathf.MoveTowards(displayedProgress, targetProgress, Time.deltaTime);
            SetProgress(displayedProgress);

            yield return null;
        }

        if (waitForInput)
        {
            if (continuePrompt != null)
                continuePrompt.gameObject.SetActive(true);

            while (!AnyInputPressed())
                yield return null;
        }

        operation.allowSceneActivation = true;
    }

    private void SetProgress(float value)
    {
        if (progressBar != null)
            progressBar.value = value;

        if (progressText != null)
            progressText.text = $"Loading... {Mathf.RoundToInt(value * 100)}%";
    }

    private static bool AnyInputPressed()
    {
        bool key = Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame;
        bool click = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
        return key || click;
    }
}