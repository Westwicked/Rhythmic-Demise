﻿using UnityEngine;
using System.Collections;

public class MouthEventHandler : MonoBehaviour {

    public RuntimePlatform platform;
    public Ray ray;
    public RaycastHit2D rayHit;
    private GameObject obj;

    // Use this for initialization
    void Start()
    {

        platform = Application.platform;
    }

    // Update is called once per frame
    void Update()
    {

        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    DetermineTouchPosition(Input.GetTouch(0).position);
                }
            }
        }
        else if (platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButtonDown(0))
                DetermineTouchPosition(Input.mousePosition);
        }
    }

    public void DetermineTouchPosition(Vector2 touchPosition)
    {
        ray = Camera.main.ScreenPointToRay(touchPosition);
        rayHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << 15);
        if (rayHit.collider != null)
        {
            switch (rayHit.collider.gameObject.tag)
            {
                case "Mouth_1":
                    //go into troop selection screen
                    Application.LoadLevel("Resource Management");
                    break;
            }

        }
    }

    public void Map_ReturnClick()
    {
        Application.LoadLevel("MainMapOverview");
    }
}