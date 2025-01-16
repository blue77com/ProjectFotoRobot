using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class InfiniteScrollFrameAnimation : MonoBehaviour
{
    public Sprite[] frames;
    public Image targetImage;
    public PostProcessVolume postProcessVolume;
    public float minFocusDistance = 5f;
    public float maxFocusDistance = 20f;
    public float focusChangeRate = 0.1f;
    public float holdDelay = 0.1f;
    public float mouseDragSensitivity = 5f; // ���������������� � �������� ����
    public bool isEnabled = true; // ���������/���������� �����������
    private float dragTimer = 0f; // ������ ��� ����������� ������� ��������
    private float dragSwitchDelay = 0.1f; // ����������� �������� ����� ������� ������

    private DepthOfField depthOfField;
    private int currentFrame = 0;
    private float timer = 0f;
    private Vector3 previousMousePosition;
    private bool isDragging = false;
    private bool canIncreaseFocus = true;
    private bool canDecreaseFocus = true;

    void Start()
    {
        // �������� �� ������� ������
        if (frames == null || frames.Length == 0)
        {
            Debug.LogError("No frames available for animation.");
            isEnabled = false;
            return;
        }

        targetImage.sprite = frames[currentFrame];

        // ������� �������� ��������� DepthOfField
        if (postProcessVolume != null && postProcessVolume.profile.TryGetSettings(out depthOfField))
        {
            depthOfField.focusDistance.value = Mathf.Clamp(depthOfField.focusDistance.value, minFocusDistance, maxFocusDistance);
        }
        else
        {
            Debug.LogWarning("DepthOfField not found in PostProcessVolume.");
        }
    }

    void Update()
    {
        // ���������� ���������, ���� ������ ��������
        if (!isEnabled)
        {
            SetFocusDistance(100f);
            return; // ������������� ���������� ���������
        }

        timer += Time.deltaTime;

        HandleKeyboardInput();
        HandleMouseScroll();
        HandleMouseDrag();
    }

    private void HandleKeyboardInput()
    {
        if (Input.GetKey(KeyCode.RightBracket) && timer >= holdDelay && canIncreaseFocus)
        {
            NextFrame();
            AdjustFocus(focusChangeRate);
            timer = 0f;
        }
        else if (Input.GetKey(KeyCode.LeftBracket) && timer >= holdDelay && canDecreaseFocus)
        {
            PreviousFrame();
            AdjustFocus(-focusChangeRate);
            timer = 0f;
        }
    }

    private void HandleMouseScroll()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scrollInput) > 0.01f) // ���������� ����� ��������
        {
            if (scrollInput > 0f && canDecreaseFocus)
            {
                PreviousFrame();
                AdjustFocus(-focusChangeRate);
            }
            else if (scrollInput < 0f && canIncreaseFocus)
            {
                NextFrame();
                AdjustFocus(focusChangeRate);
            }
        }
    }

    private void HandleMouseDrag()
    {
        if (!isDragging) return;

        // ����������� ������
        dragTimer += Time.deltaTime;

        // ���� �������� ��� �� �������, ���������� ���������
        if (dragTimer < dragSwitchDelay) return;

        // ������� � ��������� ���� �� �����������
        Vector3 mouseDelta = Input.mousePosition - previousMousePosition;

        if (Mathf.Abs(mouseDelta.x) > mouseDragSensitivity) // ��������� �� ��� X
        {
            float dragAmount = mouseDelta.x * 0.01f; // ���������� �������� ���������
            if (dragAmount > 0 && canIncreaseFocus) // �������� ������
            {
                NextFrame();
                AdjustFocus(focusChangeRate * Mathf.Abs(dragAmount));
            }
            else if (dragAmount < 0 && canDecreaseFocus) // �������� �����
            {
                PreviousFrame();
                AdjustFocus(-focusChangeRate * Mathf.Abs(dragAmount));
            }

            // ���������� ������ ����� ����� �����
            dragTimer = 0f;

            // ��������� ���������� ������� ����
            previousMousePosition = Input.mousePosition;
        }
    }

    private void SetFocusDistance(float value)
    {
        if (depthOfField != null)
        {
            depthOfField.focusDistance.value = value;
        }
    }



    public void OnMouseDown()
    {
        if (!isEnabled) return;
        isDragging = true;
        previousMousePosition = Input.mousePosition;
    }

    public void OnMouseUp()
    {
        if (!isEnabled) return;
        isDragging = false;
    }

    public void NextFrame()
    {
        if (!isEnabled) return;

        if (frames == null || frames.Length == 0) return;

        currentFrame = (currentFrame + 1) % frames.Length;
        targetImage.sprite = frames[currentFrame];
    }

    public void PreviousFrame()
    {
        if (!isEnabled) return;

        if (frames == null || frames.Length == 0) return;

        currentFrame = (currentFrame - 1 + frames.Length) % frames.Length;
        targetImage.sprite = frames[currentFrame];
    }

    private void AdjustFocus(float amount)
    {
        if (depthOfField == null) return;

        float newFocusDistance = Mathf.Clamp(depthOfField.focusDistance.value + amount, minFocusDistance, maxFocusDistance);

        if (newFocusDistance == minFocusDistance)
        {
            canDecreaseFocus = false;
        }
        else if (newFocusDistance == maxFocusDistance)
        {
            canIncreaseFocus = false;
        }
        else
        {
            canDecreaseFocus = true;
            canIncreaseFocus = true;
        }

        depthOfField.focusDistance.value = newFocusDistance;
    }
}
