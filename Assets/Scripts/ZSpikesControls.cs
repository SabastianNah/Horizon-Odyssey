using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSpikesControls : MonoBehaviour
{
    public float rotateSpeed;
    [SerializeField]
    float retractSpeed = 3f;
    [SerializeField]
    float spikeHeight = 1f;
    Vector3 pos;
    private void Start()
    {
        pos = transform.position;
    }
    void Update()
    {
        float newZ = Mathf.Sin(Time.time * retractSpeed) * spikeHeight + pos.z;

        transform.position = new Vector3(pos.x, pos.y, newZ);
        transform.Rotate(new Vector3(0, 30, 0) * (Time.deltaTime * rotateSpeed));
    }
}