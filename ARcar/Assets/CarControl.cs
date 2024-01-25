using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    public GameObject[] bodyObject;
    public Color32[] colors;

    public float rotSpeed;
    Material[] CarMats;
    private bool going = false;
    // Start is called before the first frame update
    void Start()
    {
        going = false;
        CarMats = new Material[bodyObject.Length];
        for (int i = 0; i < CarMats.Length; i++)
        {
            CarMats[i] = bodyObject[i].GetComponent<MeshRenderer>().material;
        }

        colors[0] = CarMats[0].color;
    }

    public void ChangeColor(int num)
    {
        for(int i = 0; i < CarMats.Length; i++)
        {
            CarMats[i].color = colors[num];
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hitinfo;
                if(Physics.Raycast(ray, out hitinfo, Mathf.Infinity, 1 << 8))
                {
                    Vector3 deltaPos = touch.deltaPosition;
                    transform.Rotate(transform.up, deltaPos.x * -1.0f * rotSpeed);
                }
            }
        }

        if(going)
        {
            GetComponent<Transform>().Translate(gameObject.transform.forward * 5 * Time.deltaTime);
        }
    }

    public void GoStop()
    {
        going = !going;
    }
}
