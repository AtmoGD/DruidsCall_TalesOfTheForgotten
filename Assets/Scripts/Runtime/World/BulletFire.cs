using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BulletFireData
{
    [field: SerializeField] public GameObject Bullet { get; set; } = null;
    [field: SerializeField] public Vector2 Direction { get; set; } = Vector2.zero;
    [field: SerializeField] public float WaitTime { get; set; } = 1f;
}

public class BulletFire : MonoBehaviour
{
    [field: SerializeField] public List<BulletFireData> BulletFireDataList { get; private set; } = new List<BulletFireData>();

    private int currentBulletIndex = 0;
    private float currentWaitTime = 0f;

    private void Update()
    {
        if (BulletFireDataList.Count == 0) return;

        currentWaitTime += Time.deltaTime;
        if (currentWaitTime >= BulletFireDataList[currentBulletIndex].WaitTime)
        {
            currentWaitTime = 0f;
            FireBullet();
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(BulletFireDataList[currentBulletIndex].Bullet, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(BulletFireDataList[currentBulletIndex].Direction);
        currentBulletIndex = (currentBulletIndex + 1) % BulletFireDataList.Count;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (BulletFireData bulletFireData in BulletFireDataList)
        {
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)bulletFireData.Direction);
        }
    }
}
