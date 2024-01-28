using System.Collections;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minY, maxY, angleOffset;
    private float x, y, z, targetY;

    private void Start()
    {
        targetY = 10;
    }

    private void Update()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        targetY += Input.GetAxis("Mouse ScrollWheel") * 10;
        targetY = Mathf.Clamp(targetY, minY, maxY);
        y = Mathf.MoveTowards(y, targetY, zoomSpeed * Time.deltaTime);

        z += Input.GetAxis("Vertical") * speed * Time.deltaTime;
        
        transform.position = new Vector3(x, y, z);
        //transform.rotation = Quaternion.AngleAxis((y * 2) + angleOffset, Vector3.right);
    }
}