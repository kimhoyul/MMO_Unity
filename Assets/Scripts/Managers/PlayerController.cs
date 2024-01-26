using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
			transform.position += new Vector3(0, 0, 1) * Time.deltaTime * 5f;
		}
        if (Input.GetKey(KeyCode.S))
        {
			transform.position -= new Vector3(0, 0, 1) * Time.deltaTime * 5f;
		}
        if (Input.GetKey(KeyCode.A))
        {
			transform.position += new Vector3(1, 0, 0) * Time.deltaTime * 5f;
		}
        if (Input.GetKey(KeyCode.D))
        {
			transform.position -= new Vector3(1, 0, 0) * Time.deltaTime * 5f;
		}
    }
}
