using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSetFlaslight : MonoBehaviour
{
  [SerializeField] private Vector3 Voffset;
  [SerializeField] private GameObject gofollow;
  [SerializeField] private float speed = 3.0f;

    void Start()
    {
            Voffset = transform.position - gofollow.transform.position;
    }

    void Update()
    {
        transform.position = gofollow.transform.position + Voffset;
      transform.rotation = Quaternion.Slerp(transform.rotation, gofollow.transform.rotation, speed * Time.deltaTime); 
    }
}
