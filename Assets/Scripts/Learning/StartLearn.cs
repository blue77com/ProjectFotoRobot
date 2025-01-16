using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartLearn : MonoBehaviour
{
    public CanvasController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller.ShowCanvas();
    }
}
