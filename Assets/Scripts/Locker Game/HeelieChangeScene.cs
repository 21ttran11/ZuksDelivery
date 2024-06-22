using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class HeelieChangeScene : MonoBehaviour
{
    public Animator heelieFall;
    public Animator cameraPan;


    private Camera mainCamera;

    [SerializeField]
    private GameObject heelies;

    [SerializeField]
    private GameObject roaches;
    private GameObject itemClicked;

    [SerializeField]
    private GameObject droppedDialogue;

    [SerializeField]
    private GameObject arm;
    [SerializeField]
    private GameObject foot;

    private bool fell = false;

    public bool heeliesClicked = false;
    bool roachesKilled = false;

    public Animator animator;
    public string animationStringName;
    public float delayInSeconds = 1f;

    private void OnEnable()
    {
        Debug.Log("Heelies are now available");
    }
    private void Update()
    {
        mainCamera = Camera.main;
        Debug.Log(heelies.activeSelf);

        if (roachesKilled)
        {
            heelies.SetActive(true);
            return;
        }

        if (heeliesClicked)
            heelies.SetActive(false);
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        itemClicked = rayHit.collider.gameObject;
        CheckClicked();
    }

    private void CheckClicked()
    {
        Debug.Log("Item clicked: " + itemClicked.name); // Log the name of the clicked item

        if (itemClicked.CompareTag("heelies"))
        {
            Debug.Log("Heelies clicked. Current active state: " + heelies.activeSelf);
            roaches.SetActive(true);
            heelies.SetActive(false);
            heeliesClicked = true;
            Debug.Log("Heelies new active state: " + heelies.activeSelf);
            Fall();
        }
        else
        {
            Debug.Log("Clicked item is not heelies. It is: " + itemClicked.tag);
        }
    }


    private void Fall()
    {
        if (fell == false)
        {
            heelieFall.SetBool("Falling", true);
            droppedDialogue.SetActive(true);
            fell = true;
            arm.SetActive(false);
            foot.SetActive(true);
            CameraPan();
        }

        else if (fell == true) 
        {
            StartCoroutine(DelayedSceneLoad("PostOffice2", delayInSeconds));
        }
    }

    private void CameraPan()
    {
        cameraPan.SetBool("Panning", true);
    }

    public void AllRoachesKilled()
    {
        roachesKilled = true;
    }

    private IEnumerator DelayedSceneLoad(string sceneName, float delay)
    {
        if (animator != null)
            animator.Play(animationStringName);

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
