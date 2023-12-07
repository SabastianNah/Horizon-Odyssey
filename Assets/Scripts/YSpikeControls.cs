using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSpikeControls : MonoBehaviour
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
        float newY = Mathf.Sin(Time.time * retractSpeed) * spikeHeight + pos.y;
        transform.position = new Vector3(pos.x, newY, pos.z);
        transform.Rotate(new Vector3(0, 30, 0) * (Time.deltaTime * rotateSpeed));
    }       
}
