using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PackMan : MonoBehaviour
{
    public TypePackMan typePackMan;
    bool pushed = false;
    public float speed = 5f;
    bool moving;
    public float timeDistance=0.2f;
    public float distanceBack=0.2f;
    public string tag;
    public bool back= false;
    public enum TypePackMan
    {
        colum,
        row
    }
    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        pushed = true;
                    }
                }
            }
        }

        if (pushed)
        {
            if (moving)
            {
                if (typePackMan == TypePackMan.colum)
                {
                    transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
                }
            }


        }
    }

    private void OnMouseDown()
    {
        pushed = true;
        moving = true;
        back = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            moving = true;
        }
        else
        {
            Vector3 newPos= transform.position;
            moving = false;
            if (back)
            {
                if (typePackMan == TypePackMan.colum)
                {
                    newPos.y += distanceBack;

                }
                else
                {
                    newPos.x += distanceBack;

                }
                transform.DOMove(newPos, timeDistance).SetEase(Ease.Linear);
                back = false;
            }
        }
    }

}
