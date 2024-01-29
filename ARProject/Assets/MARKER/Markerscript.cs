using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Markerscript : MonoBehaviour
{
    // ARTrackedImageManager는 AR 트래킹 이미지의 상태를 관리하는 ARFoundation 컴포넌트입니다.
    ARTrackedImageManager imageManager;

    // Start 함수는 스크립트가 처음 실행될 때 호출됩니다.
    void Start()
    {
        // ARTrackedImageManager 컴포넌트를 가져오고, 이벤트 핸들러를 연결합니다.
        imageManager = GetComponent<ARTrackedImageManager>();
        imageManager.trackedImagesChanged += OnTrackedImage;
    }

    // AR 트래킹 이미지의 상태가 변경될 때 호출되는 이벤트 핸들러 함수입니다.
    public void OnTrackedImage(ARTrackedImagesChangedEventArgs args)
    {
        // 추가된 이미지에 대한 처리
        foreach (ARTrackedImage trackedImage in args.added)
        {
            // 트래킹된 이미지의 이름을 가져옵니다.
            string imageName = trackedImage.referenceImage.name;

            // Resources 폴더에서 이미지 프리팹을 로드합니다.
            GameObject imagePrefab = Resources.Load<GameObject>(imageName);

            // 만약 로드된 프리팹이 존재하면
            if (imagePrefab != null)
            {
                // 이미지의 자식 오브젝트 개수가 1보다 작으면 (즉, 자식이 없으면)
                if (trackedImage.transform.childCount < 1)
                {
                    // 이미지의 위치와 회전에 새로운 프리팹을 생성하고, 부모로 설정합니다.
                    GameObject go = Instantiate(imagePrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    go.transform.SetParent(trackedImage.transform);
                }
            }
        }

        // 업데이트된 이미지에 대한 처리
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            // 이미지의 자식 오브젝트가 1개 이상이면 (자식이 있는 경우)
            if (trackedImage.transform.childCount > 0)
            {
                // 자식 오브젝트의 위치와 회전을 이미지의 위치와 회전으로 업데이트합니다.
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
            }
        }
    }
}