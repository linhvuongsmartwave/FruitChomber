using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackMan : MonoBehaviour
{
    public TypePackMan typePackMan;
    bool pushed = false;
    public float speed;
    public float timeDistance = 0.2f;
    public float distanceBack;
    public string tag;
    public bool back = false;
    bool moving;
    private static bool clickPackman = false;
    public CircleCollider2D circleCollider;
    Animator animator;
    public enum TypePackMan
    {
        colum,
        row
    }
    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        animator=GetComponent<Animator>();
        speed = 2f;
        distanceBack = GameManager.Instance.spacing/2;
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
                        if (GameManager.Instance.isHint && clickPackman) return;
                        if (GameManager.Instance.isHint)
                        {
                            clickPackman = true;
                        }
                        pushed = true;
                        moving = true;
                        back = true;
                        GameManager.Instance.btnHint.interactable = false;

                    }
                }
            }
        }

        if (pushed && moving)
        {
            if (typePackMan == TypePackMan.colum)
            {
                transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
            }

        }
        if (GameManager.Instance.win)
        {
            circleCollider.enabled = false;
            pushed=true;
            moving = true;
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.isHint && clickPackman) return;
        if (GameManager.Instance.isHint)
        {
            clickPackman = true;
        }
        pushed = true;
        moving = true;
        back = true;
        GameManager.Instance.btnHint.interactable = false;

    }
    public IEnumerator WaitPackMove()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.isHint = false;
        clickPackman = false;
        GameManager.Instance.UnFade();
        GameManager.Instance.txtHint.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.isHint && collision != null)
        {
            Destroy(collision.gameObject);
            if (typePackMan == TypePackMan.colum)
                StartCoroutine(WaitPackMove());
            else
                StartCoroutine(WaitPackMove());
            return;
        }
        if (collision.gameObject.CompareTag(tag))
        {
            moving = true;
            animator.SetBool("isMoving",true);
        }


        else
        {
            animator.SetBool("isMoving", false);

            Vector3 newPos = transform.position;
            moving = false;
            if (back)
            {
                if (typePackMan == TypePackMan.colum)
                    newPos.y += distanceBack;
                else
                    newPos.x += distanceBack;
                transform.DOMove(newPos, timeDistance).SetEase(Ease.Linear);
                back = false;
            }
        }
    }

}
