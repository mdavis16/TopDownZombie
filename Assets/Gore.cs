using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gore : MonoBehaviour
{
    public GameObject BlooSplatter;

    [SerializeField]
    private List<GameObject> bloodSplatList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBloodSplatter(Vector3 pos, float zRot, Color bloodColor)
    {
        var b= Instantiate(BlooSplatter, new Vector3(pos.x,pos.y,5), Quaternion.identity,transform);     
        b.transform.eulerAngles = new Vector3(0, 0, Random.Range(0,360));

        var ranScale = b.transform.localScale;

        ranScale = new Vector3(Random.Range(ranScale.x - 0.5f, ranScale.x + 05f), Random.Range(ranScale.y - 0.5f, ranScale.y + 0.5f), transform.localScale.z);

        b.GetComponent<SpriteRenderer>().color = bloodColor;

        bloodSplatList.Add(b);
    }


    public void FadeGore()
    {
        for (int i = 0; i < bloodSplatList.Count; i++)
        {
            var s = bloodSplatList[i].GetComponent<SpriteRenderer>();
            StartCoroutine(FadeTo(0, 2, s));
        }

    }

    IEnumerator FadeTo(float aValue, float aTime, SpriteRenderer s)
    {
        float alpha = s.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(s.color.r, s.color.g, s.color.b, Mathf.Lerp(alpha, aValue, t));
            s.color = newColor;
            yield return null;
        }

        Destroy(s.gameObject);
        bloodSplatList.Remove(s.gameObject);
    }
}
