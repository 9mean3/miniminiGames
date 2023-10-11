using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField] List<GameObject> Obj;
    Stack<int> callIdx;

    [SerializeField]



    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    IEnumerator Setc()
    {
        if(callIdx.Count != 0)
        {
            for (int i = 0; i < callIdx.Count; i++)
            {
                Obj[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                yield return new WaitForSeconds(0.1f);
                Obj[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
        int r = Random.Range(0, Obj.Count - 1);
        callIdx.Push(r);
        Obj[r].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.1f);
        Obj[r].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

    }
}
