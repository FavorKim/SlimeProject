using System;
using UnityEngine;

public class CheckPointManager : SingletonMono<CheckPointManager>
{
    CheckPoint[] checkPoints;

    private void Start()
    {
        if (instance != null && instance.gameObject != gameObject)
        {
            DestroyImmediate(instance.gameObject);
        }
        instance = this;

        checkPoints = FindObjectsOfType<CheckPoint>();
        Array.Sort(checkPoints, SortByPositionXUp);
    }

    public Vector3 GetNearestCheckPoint()
    {
        Vector3 nearestCheckPoint = default;
        foreach (var checkPoint in checkPoints)
        {
            if (checkPoint.isChecked)
            {
                nearestCheckPoint = checkPoint.transform.position;
                break;
            }
        }
        if(nearestCheckPoint == null)
        {
            Debug.Log("체크포인트가 저장되지 않았습니다.");
            return default;
        }
        else
        {
            return nearestCheckPoint;
        }
    }

    private int SortByPositionXUp(CheckPoint a, CheckPoint b)
    {
        if (a.transform.position.x > b.transform.position.x)
            return -1;
        if (a.transform.position.x < b.transform.position.x)
            return 1;
        return 0;
    }

}
