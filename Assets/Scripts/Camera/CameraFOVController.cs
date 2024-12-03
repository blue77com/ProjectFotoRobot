using UnityEngine;
using UnityEngine.UI;

public class CameraFOVController : MonoBehaviour
{
    public float fovChangeSpeed = 2.0f; // Speed of FOV change with keyboard input
    public Camera m; // Camera reference
    public Scrollbar fovScrollbar; // Scrollbar reference
    public bool isEnabled = true; // Flag to enable/disable functionality

    void Start()
    {
        m = GetComponent<Camera>();

        // Set the scrollbar's initial value based on the camera's current FOV
        if (fovScrollbar != null)
        {
            fovScrollbar.value = (m.fieldOfView - 15.0f) / (90.0f - 15.0f);
            fovScrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
        }
    }

    void Update()
    {
        // If script is disabled, skip processing
        if (!isEnabled)
        {
            if (fovScrollbar != null) fovScrollbar.interactable = false;
            return;
        }

        if (fovScrollbar != null) fovScrollbar.interactable = true;

        // Keyboard input for FOV control
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            m.fieldOfView -= fovChangeSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.KeypadMinus))
        {
            m.fieldOfView += fovChangeSpeed * Time.deltaTime;
        }

        // Clamp FOV to reasonable limits
        m.fieldOfView = Mathf.Clamp(m.fieldOfView, 15.0f, 90.0f);

        // Update scrollbar value to match FOV
        if (fovScrollbar != null)
        {
            fovScrollbar.value = (m.fieldOfView - 15.0f) / (90.0f - 15.0f);
        }
    }

    // Method called when scrollbar value changes
    public void OnScrollbarValueChanged(float value)
    {
        // If script is disabled, do nothing
        if (!isEnabled)
        {
            return;
        }

        // Update camera's FOV based on scrollbar value
        m.fieldOfView = Mathf.Lerp(15.0f, 90.0f, value);
    }
}
