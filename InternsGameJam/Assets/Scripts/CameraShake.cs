using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform camTransform;

    // How long the object should shake for.
    private float shake = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    private float shakeMagnitude = 0.01f;

    private float decreaseFactor = 1.0f;

    Vector3 originalPos;

    // Getters and setters
    // Use these in other scripts to activate and modify the camera shake.
    public float ShakeMagnitude { get => shakeMagnitude; set => shakeMagnitude = value; }
    public float DecreaseFactor { get => decreaseFactor; set => decreaseFactor = value; }

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    // Get the camera's local position.
    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        shake = 0f;
    }

    // If the shake variable is changed by another script,
    // move the camera to a random position within a sphere.
    // Return to it's original local position afterwards.
    void Update()
    {
        if (shake > 0f)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * ShakeMagnitude;

            shake -= Time.deltaTime * DecreaseFactor;
        }
        else
        {
            shake = 0f;
            camTransform.localPosition = originalPos;

            ResetDecrease();
            ResetMagnitude();
        }
    }

    public void Shake(float value)
    {
        if (shake <= 0f)
        {
            shake = value;
        }
    }

    void ResetDecrease()
    {
        DecreaseFactor = 1f;
    }

    void ResetMagnitude()
    {
        ShakeMagnitude = 0.05f;
    }
}
