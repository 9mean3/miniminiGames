using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacle : MonoBehaviour
{
    [SerializeField] GameObject obstacle;

    [SerializeField] Transform genTopPos;
    [SerializeField] Transform genBotPos;

    [SerializeField] float minGenTime = 1;
    [SerializeField] float maxGenTime = 4;

    float genTime = 0;

    void Update()
    {
        genTime -= Time.deltaTime;
        if(genTime < 0)
        {
            genTime = Random.Range(minGenTime, maxGenTime);
            GameObject prefab = Instantiate(obstacle, new Vector3(genTopPos.position.x,
                Random.Range(genTopPos.position.y, genBotPos.position.y), 0), Quaternion.identity);
            if(Random.value < 0.8)
            {
                prefab.transform.localScale =
                    new Vector3(0.5f, Random.Range(1, 2));
            }
            else
            {
                prefab.transform.localScale =
                    new Vector3(Random.Range(7,12), Random.Range(1.2f, 2));
                prefab.transform.position += Vector3.right * 2;
                genTime = 3;
            }

            Destroy(prefab, 5);
        }
    }
}
