using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private SpaceShipScript playerRef;
    private void Start()
    {
        playerRef = Object.FindObjectOfType<SpaceShipScript>();
    }

    private void Update()
    {
        transform.position = playerRef.transform.position;
        transform.rotation = playerRef.transform.rotation;
    }
}
