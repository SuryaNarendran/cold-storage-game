using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelController : Singleton<LevelController>
{
    [SerializeField] GameObject[] levels;
    [SerializeField] Vector3 originalCamPos;
    int activeLevelindex = 0;

    public static bool transitioningLevel {get; private set;} = false;
    public static bool playingGame { get; private set; }  = false; 


    Door exitDoor;

    public static void Victory()
    {
        if (transitioningLevel) return;

        UI.VictoryMessage(true);
        Timer.Register(1f,()=>Audiomanager.Play("win"));
        Instance.StartCoroutine(Instance.DelayBecauseTimerIsMessedUp(Instance.DropLevel, 2f));
        Instance.StartCoroutine(Instance.DelayBecauseTimerIsMessedUp(NextLevel, 5f));
        transitioningLevel = true;
        Instance.exitDoor.onOpened.RemoveListener(Victory);
    }

    public static void NextLevel()
    {
        UI.VictoryMessage(false);
        UI.DeSelectExisting();
        Chatbox.Clear();
        MouseCast.ClearSelections();
        UI.MainUiSensor.HideValue();

        Instance.levels[Instance.activeLevelindex].SetActive(false);
        Instance.activeLevelindex++;

        if (Instance.levels.Length == Instance.activeLevelindex)
        {
            UI.MainMenuUI.EndOfGame();
            UI.MainMenuUI.DropMenu();
            return;
        }
            

        Instance.levels[Instance.activeLevelindex].SetActive(true);
        Instance.SlideInLevel();

        Instance.SetupExitDoor();
        UI.MainDoorDisplayUI.gameObject.SetActive(true);

        transitioningLevel = false;
    }

    void SetupExitDoor()
    {
        GameObject mainDoorGO = GameObject.FindWithTag("Exit Door");
        if (mainDoorGO == null) Debug.LogError("Exit door not found");
        exitDoor = mainDoorGO.GetComponent<Door>();
        if (exitDoor == null) Debug.LogError("Exit door component not found");
        exitDoor.onOpened.AddListener(Victory);
    }

    public static void Setup(int levelIndex = 0)
    {
        UI.DeSelectExisting();
        Instance.activeLevelindex = levelIndex;
        Instance.levels[levelIndex].SetActive(true);
        Instance.SlideInLevel();
        Instance.SetupExitDoor();
        UI.MainDoorDisplayUI.gameObject.SetActive(true);
        playingGame = true;
    }

    void DropLevel()
    {
        UI.VictoryMessage(false);
        UI.DeSelectExisting();
        Chatbox.Clear();
        MouseCast.ClearSelections();
        UI.MainUiSensor.HideValue();
        UI.MainDoorDisplayUI.gameObject.SetActive(false);

        Camera.main.transform.DOLocalMoveY(20f, 1.5f)
            .SetEase(Ease.InOutCubic);
    }
    void ResetCamera()
    {
        Camera.main.transform.position = originalCamPos;
    }
    void SlideInLevel()
    {
        ResetCamera();

        Camera.main.transform.DOLocalMoveX(-30f, 1.5f)
            .From()
            .SetEase(Ease.InOutCubic);
    }

    private void Awake()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].activeSelf)
            {
                activeLevelindex = i;

                Setup(activeLevelindex);
                return;
            }
        }       
    }

    IEnumerator DelayBecauseTimerIsMessedUp(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
