using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator nextLevelAnimator;
    // [SerializeField] AudioClip menuSelect;
    // [SerializeField] AudioClip levelComplete;

    public float transitionTime = 1f;
    AudioSource audioSource;
    MusicManager musicManager;
    int lastSceneIdx;
    int currentSceneIdx;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        lastSceneIdx = SceneManager.GetSceneByName("GameOver").buildIndex;
        currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        var musicManagerObj = GameObject.Find("MusicManager");
        if (musicManagerObj != null)
        {
            musicManager = musicManagerObj.GetComponent<MusicManager>();
            if (currentSceneIdx == 0 || currentSceneIdx == lastSceneIdx)
            {
                musicManager.SwitchMusic("Menu");
            }
            else
            {
                musicManager.SwitchMusic("Game");
            }
        }
    }

    public void LoadNextScene()
    {
        // audioSource.PlayOneShot(menuSelect);
        StartCoroutine(LoadLevel(currentSceneIdx + 1));
    }

    public void LoadMainMenu()
    {
        // audioSource.PlayOneShot(menuSelect);
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        nextLevelAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
    }
}
