using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public List<ButtonController> buttons;
    public float timeBetweenNotes = 1f;
    private List<int> sequence;
    private int sequenceIndex;
    private bool isSequencePlaying;
    private bool isPlayerTurn;
    private bool isSequenceCompleted = false;
    public Move playerMovement;

    public float originalSpeed;
    public float slowSpeed = 3f;

    public CinemachineVirtualCamera cinemachineCamera;
    public float targetSize = 18f;
    public float zoomDuration = 2f;
    public float originalSize = 20f;

    public GameObject[] buttonUI;

    public static bool canTravel = false;

    public string travelSceneStringName = "";

    private void Awake()
    {
        originalSpeed = playerMovement.maxSpeed;
        SetButtonUIVisibility(false);
        
    }

    private void Start()
    {
        canTravel = false;
    }

    IEnumerator PlaySequence()
    {
        isSequencePlaying = true;
        UpdateSpeed(slowSpeed);
        StartZoomIn();
        isPlayerTurn = false;
        sequence = GenerateRandomSequence(4);

        float lastTime = Time.realtimeSinceStartup;
        foreach (int buttonIndex in sequence)
        {
            buttons[buttonIndex].PressButton();
            yield return new WaitForSecondsRealtime(timeBetweenNotes);
            buttons[buttonIndex].ReleaseButton();
            // Wait for an additional period accounting for realtime
            while (Time.realtimeSinceStartup < lastTime + timeBetweenNotes * 2) yield return null;
            lastTime = Time.realtimeSinceStartup;
        }

        isSequencePlaying = false;
        isPlayerTurn = true;
        sequenceIndex = 0;
    }


    List<int> GenerateRandomSequence(int length)
    {
        List<int> newSequence = new List<int>();
        for (int i = 0; i < length; i++)
        {
            newSequence.Add(Random.Range(0, buttons.Count));
        }
        return newSequence;
    }

    void Update()
    {
        if (isSequenceCompleted)
        {
            return;
            
        }

        if (canTravel && Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                SetButtonUIVisibility(true);
                if (!isSequencePlaying && !isPlayerTurn && !isSequenceCompleted)
                    StartCoroutine(PlaySequence());
            }
            else
            {
                if ((isPlayerTurn || isSequencePlaying) && !isSequenceCompleted)
                {
                    Debug.Log("D key released. Ending the current sequence.");
                    StopAllCoroutines();
                    StopAllBlinking();
                    StartZoomOut();
                    ResetAllButtons();
                    isPlayerTurn = false;
                    isSequencePlaying = false;
                    UpdateSpeed(originalSpeed);
                    sequenceIndex = 0;
                    SetButtonUIVisibility(false);
                }
                return;
            }
        }
       

        if (isSequencePlaying)
        {
            Debug.Log("Sequence is playing, wait for your turn.");
            return;
        }

        if (isPlayerTurn)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (Input.GetKeyDown(buttons[i].keyToPress))
                {
                    Debug.Log("You pressed a key corresponding to a button.");

                    buttons[i].PressButton();

                    if (sequence[sequenceIndex] == i)
                    {
                        Debug.Log($"Correct button! You pressed button {i}.");
                        sequenceIndex++;
                        if (sequenceIndex >= sequence.Count)
                        {
                            Debug.Log("Sequence complete. Travel Sequence Intitated");
                            isPlayerTurn = false;
                            isSequenceCompleted = true;
                            UpdateSpeed(originalSpeed);
                            ResetAllButtons();
                            StartZoomOut();
                            SetButtonUIVisibility(false);
                            StartCoroutine(ChangeSceneWithAnimation(travelSceneStringName));
                        }
                    }
                    else
                    {
                        Debug.Log($"Wrong button! Reminding you to press the correct button.");
                        // Highlight the correct button for the player
                        buttons[sequence[sequenceIndex]].HighlightButton();
                    }

                    StartCoroutine(ReleaseButtonAfterDelay(buttons[i]));
                }
            }
        }
    }

    private void SetButtonUIVisibility(bool isVisible)
{
    foreach (var uiElement in buttonUI)
    {
        if (uiElement != null)
        {
            uiElement.SetActive(isVisible);
        }
    }
}

    private void StopAllBlinking()
    {
        foreach (var button in buttons)
        {
            if (button.IsBlinking)
            {
                button.UnhighlightButton();
            }
        }
    }

    IEnumerator ReleaseButtonAfterDelay(ButtonController button)
    {
        yield return new WaitForSeconds(timeBetweenNotes);
        button.ReleaseButton();
    }

    public void UpdateSpeed(float newSpeed)
    {
        playerMovement.maxSpeed = newSpeed;
    }

    private void ResetAllButtons()
    {
        foreach (var button in buttons)
        {
            button.ResetToDefaultState();
        }
    }

    IEnumerator ZoomCameraIn()
    {
        float currentTime = 0f;
        float startSize = cinemachineCamera.m_Lens.OrthographicSize;


        while (currentTime < zoomDuration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / zoomDuration;
            cinemachineCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }
    }

    IEnumerator ZoomCameraOut()
    {
        float currentTime = 0f;
        float startSize = cinemachineCamera.m_Lens.OrthographicSize;
        
        while (currentTime < zoomDuration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / zoomDuration;
            cinemachineCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, originalSize, t);
            yield return null;
        }
    }


    public void StartZoomIn()
    {
        StartCoroutine(ZoomCameraIn());
    }

    public void StartZoomOut()
    {
        StartCoroutine(ZoomCameraOut());
    }

    IEnumerator ChangeSceneWithAnimation(string sceneName)
    {
        // Start the teleportation or transition animation here

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(sceneName);
    }

}
