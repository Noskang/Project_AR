using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Markerscript : MonoBehaviour
{
    // ARTrackedImageManager�� AR Ʈ��ŷ �̹����� ���¸� �����ϴ� ARFoundation ������Ʈ�Դϴ�.
    ARTrackedImageManager imageManager;

    // Start �Լ��� ��ũ��Ʈ�� ó�� ����� �� ȣ��˴ϴ�.
    void Start()
    {
        // ARTrackedImageManager ������Ʈ�� ��������, �̺�Ʈ �ڵ鷯�� �����մϴ�.
        imageManager = GetComponent<ARTrackedImageManager>();
        imageManager.trackedImagesChanged += OnTrackedImage;
    }

    // AR Ʈ��ŷ �̹����� ���°� ����� �� ȣ��Ǵ� �̺�Ʈ �ڵ鷯 �Լ��Դϴ�.
    public void OnTrackedImage(ARTrackedImagesChangedEventArgs args)
    {
        // �߰��� �̹����� ���� ó��
        foreach (ARTrackedImage trackedImage in args.added)
        {
            // Ʈ��ŷ�� �̹����� �̸��� �����ɴϴ�.
            string imageName = trackedImage.referenceImage.name;

            // Resources �������� �̹��� �������� �ε��մϴ�.
            GameObject imagePrefab = Resources.Load<GameObject>(imageName);

            // ���� �ε�� �������� �����ϸ�
            if (imagePrefab != null)
            {
                // �̹����� �ڽ� ������Ʈ ������ 1���� ������ (��, �ڽ��� ������)
                if (trackedImage.transform.childCount < 1)
                {
                    // �̹����� ��ġ�� ȸ���� ���ο� �������� �����ϰ�, �θ�� �����մϴ�.
                    GameObject go = Instantiate(imagePrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    go.transform.SetParent(trackedImage.transform);
                }
            }
        }

        // ������Ʈ�� �̹����� ���� ó��
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            // �̹����� �ڽ� ������Ʈ�� 1�� �̻��̸� (�ڽ��� �ִ� ���)
            if (trackedImage.transform.childCount > 0)
            {
                // �ڽ� ������Ʈ�� ��ġ�� ȸ���� �̹����� ��ġ�� ȸ������ ������Ʈ�մϴ�.
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
            }
        }
    }
}