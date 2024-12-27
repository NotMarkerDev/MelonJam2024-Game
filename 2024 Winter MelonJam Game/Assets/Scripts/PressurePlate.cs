using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] private float openTime = 1;
    [SerializeField] private float plateTime = 0.5f;
    [SerializeField] private float pressedHeight = -0.1f;

    private bool doorIsOpened = false;
    private float timer = 0f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private Vector3 platePos;
    private Vector3 plateTarget;

    private void Start()
    {
        platePos = transform.position;
        plateTarget = transform.position + new Vector3(0, pressedHeight, 0);

        initialPosition = door.transform.position;
        targetPosition = initialPosition + new Vector3(0, 4f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!doorIsOpened)
        {
            doorIsOpened = true;
            
            timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (doorIsOpened)
        {
            doorIsOpened = false;
            
            timer = 0f;
        }
    }

    private void Update()
    {
        if (doorIsOpened)
        {
            // open door
            timer += Time.deltaTime;
            door.transform.position = Vector3.Lerp(initialPosition, targetPosition, timer / openTime);
            gameObject.transform.position = Vector3.Lerp(platePos, plateTarget, timer / plateTime);

            if (timer >= openTime)
            {
                door.transform.position = targetPosition;

                transform.position = plateTarget;
            }
        }
        else
        {
            // close door
            timer += Time.deltaTime;
            door.transform.position = Vector3.Lerp(door.transform.position, initialPosition, timer / openTime);

            gameObject.transform.position = Vector3.Lerp(transform.position, platePos, timer / plateTime);

            if (timer >= openTime)
            {
                door.transform.position = initialPosition;

                transform.position = platePos;
            }
        }
    }
}
