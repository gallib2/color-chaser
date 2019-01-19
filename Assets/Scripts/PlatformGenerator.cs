using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;
    private float[] platformWidths;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public GameObject[] thePlatforms;
    private int platformSelector;

    public SpriteRenderer[] spriteRenderers;

    public ObjectPooler[] theObjectPools;

    private float minHeight;
    public Transform maxHieghtPoint;
    private float maxHeight;
    public float maxHeightChange;
    public float heightChange;

    private coinGenerator theCoinGenerator;

    public float randomCoinThreshhold;

    public bool toPaintInBlack;

    void Start()
    {
        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
        toPaintInBlack = false;

        platformWidths = new float[theObjectPools.Length];

        for(int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHieghtPoint.position.y;

        theCoinGenerator = FindObjectOfType<coinGenerator>();

    }

    private void OnEnable()
    {
        PlayerControll.OnTouchBlackPlatform += paintPlatformsInBlack;
        PlayerControll.OnStopPainting += stopPaintPlatforms;
    }

    private void OnDisable()
    {
        PlayerControll.OnTouchBlackPlatform -= paintPlatformsInBlack;
        PlayerControll.OnStopPainting -= stopPaintPlatforms;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            heightChange = Mathf.Clamp(heightChange, minHeight, maxHeight);

            //if (heightChange > maxHeight)
            //{
            //    heightChange = maxHeight;
            //}
            //else if(heightChange < minHeight)
            //{
            //    heightChange = minHeight;
            //}

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            //Instantiate(/*thePlatform*/ thePlatforms[platformSelector], transform.position, transform.rotation);

            GameObject newPlatform = theObjectPools[platformSelector].GetPooledobject();
            spriteRenderers = newPlatform.GetComponentsInChildren<SpriteRenderer>();

            PaintPlatformByType(newPlatform);

            if (toPaintInBlack == true)
            {
                PaintPlatforms(Color.black);
            }

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);



            if (Random.Range(0f, 100f) < randomCoinThreshhold)
                {
                    theCoinGenerator.spownCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
                }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);
        }
    }

    private void PaintPlatformByType(GameObject newPlatform)
    {

        switch (newPlatform.tag)
        {
            case "WhitePlatform":
                {
                    PaintPlatforms(Color.white);
                    break;
                }
            case "RedPlatform":
                {
                    PaintPlatforms(Color.red);
                    break;
                }
            case "GreenPlatform":
                {
                    PaintPlatforms(Color.green);
                    break;
                }
            case "BlackPlatform":
                {
                    PaintPlatforms(Color.black);
                    break;
                }
            default:
                break;
        }
    }

    private void PaintPlatforms(Color color)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = color;
        }
    }

    private void paintPlatformsInBlack()
    {
        toPaintInBlack = true;
        Debug.Log("Got here - paintPlatformsInBlack");
    }

    public void stopPaintPlatforms()
    {
        toPaintInBlack = false;
        Debug.Log("Got here - stopPaintPlatforms");
    }
}
