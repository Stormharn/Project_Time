using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoom : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] float leftRightBoundaryPercent = 5;
    [SerializeField] float topBottomBoundaryPercent = 5;
    [SerializeField] float leftClampValue;
    [SerializeField] float rightClampValue;
    [SerializeField] float topClampValue;
    [SerializeField] float bottomClampValue;
    int width;
    int height;
    int xBoundary;
    int yBoundary;
    Transform myTransform;

    void Start()
    {
        myTransform = transform;
        width = Screen.width;
        height = Screen.height;
        xBoundary = Mathf.RoundToInt(width * (leftRightBoundaryPercent / 100));
        yBoundary = Mathf.RoundToInt(height * (topBottomBoundaryPercent / 100));
    }

    void Update()
    {
        if (Input.mousePosition.x > width - xBoundary)
        {
            var newPosition = transform.position + new Vector3(Time.deltaTime * speed, 0.0f, 0.0f);
            if (newPosition.x > rightClampValue)
                newPosition.x = rightClampValue;
            transform.position = newPosition;
        }

        if (Input.mousePosition.x < 0 + xBoundary)
        {
            var newPosition = transform.position - new Vector3(Time.deltaTime * speed, 0.0f, 0.0f);
            if (newPosition.x < leftClampValue)
                newPosition.x = leftClampValue;
            transform.position = newPosition;
        }

        if (Input.mousePosition.y > height - yBoundary)
        {
            var newPosition = transform.position + new Vector3(0.0f, 0.0f, Time.deltaTime * speed);
            if (newPosition.z > topClampValue)
                newPosition.z = topClampValue;
            transform.position = newPosition;
        }

        if (Input.mousePosition.y < 0 + yBoundary)
        {
            var newPosition = transform.position - new Vector3(0.0f, 0.0f, Time.deltaTime * speed);
            if (newPosition.z < bottomClampValue)
                newPosition.z = bottomClampValue;
            transform.position = newPosition;
        }

    }
}
