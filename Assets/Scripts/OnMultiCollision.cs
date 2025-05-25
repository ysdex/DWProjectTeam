using UnityEngine;

public class OnMultiCollision : MonoBehaviour
{
    public string targetObjectName;
    public string showObjectName;

    GameObject showObject;
    float orgY = 0;
    float ofsetY = 10000;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showObject = GameObject.Find(showObjectName);

        orgY = showObject.transform.position.y;
        if (orgY > ofsetY)
        {
            orgY -= ofsetY;
        }
        Vector3 pos = showObject.transform.position;
        pos.y = orgY + ofsetY;
        showObject.transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == targetObjectName)
        {
            Vector3 pos = showObject.transform.position;

            pos.y = orgY;
            showObject.transform.position = pos;
        }
    }
}
