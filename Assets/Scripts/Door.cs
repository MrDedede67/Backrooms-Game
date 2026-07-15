using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    public float openRotation = 90f;
    public float speed = 2f;

    public AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Audio Settings")]
    public AudioClip openCreakSound;
    public AudioClip slamSound;

    private Quaternion closedRotation;
    private Quaternion targetRotation;
    private AudioSource audioSource;

    void Start()
    {
        closedRotation = transform.localRotation;
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact(float customSpeed = -1f)
    {
        isOpen = !isOpen;
        targetRotation = isOpen ?
            closedRotation * Quaternion.Euler(0, openRotation, 0) : closedRotation;

        float effectiveSpeed = (customSpeed > 0) ? customSpeed : speed;

        if (openCreakSound != null) audioSource.PlayOneShot(openCreakSound);

        StopAllCoroutines();
        StartCoroutine(MoveDoor(effectiveSpeed));
    }

    IEnumerator MoveDoor(float currentSpeed)
    {
        float elapsed = 0f;
        float duration = 1f / currentSpeed; 
        Quaternion startRotation = transform.localRotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            float curveValue = movementCurve.Evaluate(t);

            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, curveValue);
            yield return null;
        }

        transform.localRotation = targetRotation;
        if (slamSound != null && isOpen == false) audioSource.PlayOneShot(slamSound);
    }
}