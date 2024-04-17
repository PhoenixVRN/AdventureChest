using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLine : MonoBehaviour
{
    [SerializeField] private LineRenderer _trajectoryLine;
    [SerializeField] private Transform _arrow;
    void Start()
    {
        
    }

  
    void Update()
    {
        ShowTrajectoryLine();
    }

    private void ShowTrajectoryLine()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _arrow.gameObject.SetActive(true);
            _arrow.position = mousePos;
            _trajectoryLine.enabled = true;
            _trajectoryLine.positionCount = 2;
            _trajectoryLine.SetPosition(0, Vector3.zero);
            _trajectoryLine.SetPosition(1, mousePos);
        }
        else
        {
            _trajectoryLine.enabled = false;
            _arrow.gameObject.SetActive(false);
        }
    }
}
