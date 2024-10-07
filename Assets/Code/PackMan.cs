using UnityEditor;
using UnityEngine;

public class PackMan : MonoBehaviour
{
    public TypePackMan typePackMan;
    bool pushed=false;
    public float speed = 5f;
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
                        Debug.Log("a");
                        pushed = true;
                    }
                }
            }
        }

        if (pushed)
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

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        pushed = true;


    }
}
