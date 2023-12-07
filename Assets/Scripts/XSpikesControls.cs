using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XSpikesControls : MonoBehaviour
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
        float newX = Mathf.Sin(Time.time * retractSpeed) * spikeHeight + pos.x;

        transform.position = new Vector3(newX, pos.y, pos.z);
        transform.Rotate(new Vector3(0, 30, 0) * (Time.deltaTime * rotateSpeed));
    }
}
