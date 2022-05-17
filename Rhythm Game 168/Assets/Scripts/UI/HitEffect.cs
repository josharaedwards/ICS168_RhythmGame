using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public float lifetime = 1f;

    public float speed = .75f;

    public Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        newPosition = new Vector3 (transform.position.x, transform.position.y + .5f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        Destroy(gameObject,lifetime);
    }
}
