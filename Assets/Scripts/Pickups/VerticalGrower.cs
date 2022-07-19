using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalGrower : Deployable
{
    private const int GROUND_LAYER = 6;
    public float growthCount;
    [SerializeField] private int growthLength;
    [SerializeField] private float growthSpeed;

    // Start is called before the first frame update
    void Start()
    {
        growthCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void Deploy()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == GROUND_LAYER)
        {
            // begin vertical growth
            gameObject.layer = GROUND_LAYER;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            StartCoroutine(Grow());
        }
    }

    private IEnumerator Grow()
    {
        while(growthCount < growthLength)
        {
            float toGrow = growthSpeed * Time.deltaTime;
            transform.Translate(Vector3.up * toGrow);
            growthCount += toGrow;

            yield return new WaitForFixedUpdate();
        }
    }
}
