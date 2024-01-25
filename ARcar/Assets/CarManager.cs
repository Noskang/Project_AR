using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CarManager : MonoBehaviour
{

    public GameObject Indicator;
    ARRaycastManager ARRaycastManager;
    public GameObject myCar;
    GameObject PlacedObject;
    public float relocationDistance = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Indicator.SetActive(false);
        ARRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectGround();

        if(EventSystem.current.currentSelectedGameObject)
        {
            return;
        }
        if (Indicator.activeInHierarchy && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                if(PlacedObject == null)
                {
                    PlacedObject = Instantiate(myCar, Indicator.transform.position, Indicator.transform.rotation);
                }
                else
                {
                    if(Vector3.Distance(PlacedObject.transform.position, Indicator.transform.position) > relocationDistance)
                    {
                        PlacedObject.transform.SetPositionAndRotation(Indicator.transform.position, Indicator.transform.rotation);
                    }                    
                }
            }
        }
    }

    void DetectGround()
    {
        Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();

        if(ARRaycastManager.Raycast(screenSize, hitInfos, TrackableType.Planes))
        {
            Indicator.SetActive(true);
            Indicator.transform.position = hitInfos[0].pose.position;
            Indicator.transform.rotation = hitInfos[0].pose.rotation;
            Indicator.transform.position += Indicator.transform.up * 0.1f;
        }
        else
        {
            Indicator.SetActive(false);
        }
    }
}
