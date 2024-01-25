using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using TMPro;

public class GPS_Manager : MonoBehaviour
{

    public TextMeshProUGUI latitude_text;
    public TextMeshProUGUI longitude_text;

    public float latitude = 0;
    public float longitude = 0;

    public float maxWaitTime = 10.0f;
    float waitTIme = 0;

    public float resendTime = 1.0f;
    public bool receiveGPS = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPS_On());
    }

    // Update is called once per frame
    public IEnumerator GPS_On()
    {
        if(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            while(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }//1.�㰡
        
        if(!Input.location.isEnabledByUser)
        {
            latitude_text.text = "GPS Off";
            longitude_text.text = "GPS OFF";
            yield break;
        }//GPS ��ġ

        Input.location.Start(); // 3.��û
        
        while(Input.location.status == LocationServiceStatus.Initializing && waitTIme < maxWaitTime)
        {
            yield return new WaitForSeconds(1.0f);
            waitTIme++;
        }// 4.����ϴ� ����

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            latitude_text.text = "Whichi jungbo failed";
            longitude_text.text = "Whichi jungbo failed";
        }// 5.���Ž���

        if(waitTIme >= maxWaitTime)
        {
            latitude_text.text = "time chogwa";
            longitude_text.text = "time chogwa";
        }// 6.Ÿ�Ӿƿ�

        LocationInfo li = Input.location.lastData;
        latitude = li.latitude;
        longitude = li.longitude;
        latitude_text.text = "whido : " + latitude.ToString();
        longitude_text.text = "gyungdo : " + longitude.ToString();
        // 7.GPS ���� ���

        receiveGPS = true;

        while (receiveGPS)
        {
            yield return new WaitForSeconds(resendTime);
            li = Input.location.lastData;
            latitude = li.latitude;
            longitude = li.longitude;
            latitude_text.text = "whido : " + latitude.ToString();
            longitude_text.text = "gyungdo : " + longitude.ToString();
        }// 8. ���� �����ϰ� ��� �ݺ�
    }
}