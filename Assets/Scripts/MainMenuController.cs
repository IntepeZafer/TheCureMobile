using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
public class MainMenuController : MonoBehaviour
{
    [Header("Kamera Ayarları")]
    public Camera mainCamera;
    public Transform targetPosition;
    public float transitionSpeed = 0f;

    [Header("UI")]
    public GameObject menuCanvas;

    [Header("Oyun Kontrolü")]
    public MonoBehaviour playerMovementScript;

    public void StartGameSequence()
    {
        menuCanvas.SetActive(false);
        StartCoroutine(MoveCameraToShoulder());
    }
    IEnumerator MoveCameraToShoulder()
    {
        float timeElapse = 0;
        float duration = transitionSpeed;
        Vector3 startPos = mainCamera.transform.position;
        Quaternion startRot = mainCamera.transform.rotation;
        while(timeElapse < duration)
        {
            timeElapse += Time.deltaTime;
            float t = timeElapse / duration;
            t = t * t * (3f - 2f * t);
            mainCamera.transform.position = Vector3.Lerp(startPos , targetPosition.position , t);
            mainCamera.transform.rotation = Quaternion.Lerp(startRot , targetPosition.rotation , t);
            yield return null;
        }
        mainCamera.transform.position = targetPosition.position;
        mainCamera.transform.rotation = targetPosition.rotation;
        mainCamera.transform.SetParent(targetPosition.parent);
        Debug.Log("Oyun Başladı");
        if(playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
    }
    public void QuitGame(){Application.Quit();}
}
