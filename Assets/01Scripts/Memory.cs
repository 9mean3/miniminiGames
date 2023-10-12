using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    Questioning,
    Listening,
}

public class Memory : MonoBehaviour
{
    State curState;

    [SerializeField] List<GameObject> Obj = new List<GameObject>();
    List<int> objOrder = new List<int>();
    [SerializeField] float qCooltime;
    [SerializeField] float qFlashtime;
    int qCount = 1;
    Camera mainCam;
    bool isWrong = false;


    private void Start()
    {
        mainCam = Camera.main;
        curState = State.Questioning;
        Question();
    }

    void Question()
    {
        Obj.ForEach((GameObject obj) =>
        {
            Color t = obj.GetComponent<SpriteRenderer>().color;
            t.a = 0.1f;
            obj.GetComponent<SpriteRenderer>().color = t;
        });
        StartCoroutine(QCor());
    }

    void Listen()
    {
        curState = State.Listening;
        StartCoroutine(LCor());
    }

    IEnumerator QCor()
    {
        int cCnt = qCount;
        objOrder.Add(Random.Range(0, Obj.Count));

        while (cCnt > 0)
        {
            //Debug.Log(cCnt + ' ' + q);

            yield return new WaitForSeconds(qCooltime);
            int rn = qCount - cCnt;
            Color t = Obj[objOrder[rn]].GetComponent<SpriteRenderer>().color;
            t.a = 1f;
            Obj[objOrder[rn]].GetComponent<SpriteRenderer>().color = t;
            yield return new WaitForSeconds(qFlashtime);
            t.a = 0.1f;
            Obj[objOrder[rn]].GetComponent<SpriteRenderer>().color = t;

            cCnt--;
        }
        qCount++;
        Listen();
    }

    IEnumerator LCor()
    {
        yield return new WaitUntil(CKAnswer);
        print(isWrong);
        Question();
    }

    int cIdx = 0;
    bool CKAnswer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                mainCam.ScreenToWorldPoint(Input.mousePosition),
                mainCam.transform.forward,
                20);
            if (hit.collider != null && hit.transform.CompareTag("Node"))
            {
                foreach (var item in Obj)
                {
                    if(item == hit.transform.gameObject && item == Obj[objOrder[cIdx]])
                    {
                        Color t = Obj[objOrder[cIdx]].GetComponent<SpriteRenderer>().color;
                        t.a = 1f;
                        Obj[objOrder[cIdx]].GetComponent<SpriteRenderer>().color = t;
                        Wait(qFlashtime);
                        t.a = 0.1f;
                        Obj[objOrder[cIdx]].GetComponent<SpriteRenderer>().color = t;
                        break;
                    }
                    else if(item == hit.transform.gameObject && item != Obj[objOrder[cIdx]])
                    {
                        isWrong = true;
                        Debug.Log("Wrong!");
                        return true;
                    }
                }

                cIdx++;
            }
        }

        if (cIdx >= objOrder.Count) {
            cIdx = 0;
            return true;
        }
        else return false;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
