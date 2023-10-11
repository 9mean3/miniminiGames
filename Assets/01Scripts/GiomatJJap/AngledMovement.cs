using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AngledMovement : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float upSpeed = 3;

    CircleCollider2D col;

    Vector3 camPos;

    private void Start()
    {
    }

    void Update()
    {
        camPos = Camera.main.transform.position;
        camPos.x = transform.position.x;
        Camera.main.transform.position = camPos;

        transform.position += Vector3.right * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * upSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.down * upSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
