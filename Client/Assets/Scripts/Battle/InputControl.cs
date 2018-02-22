using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    public float MoveSpeed = 3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ControlMove();
    }

    void ControlMove()
    {
#if UNITY_EDITOR
        KeyControl();
#else
		GravityControl();
#endif
    }

    private void KeyControl()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * MoveSpeed * Time.deltaTime;
        float deltaY = Input.GetAxisRaw("Vertical") * MoveSpeed * Time.deltaTime;
        Vector3 movement = new Vector3(deltaX, deltaY ,0);
        transform.position = transform.position + movement;
    }
    private void GravityControl()
    {
        float deltaX = Input.acceleration.x * MoveSpeed * Time.deltaTime;
        float deltaY = Input.acceleration.y * MoveSpeed * Time.deltaTime;
        Vector3 movement = new Vector3(deltaX, deltaY , 0);
        transform.position = transform.position + movement;
    }

}
