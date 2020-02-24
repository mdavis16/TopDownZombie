using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PickUpItemDisplay : MonoBehaviour
{
    private string message;
    PickUpItemDisplayManager displayManager;

    // Start is called before the first frame update
    void Start()
    {
        displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<PickUpItemDisplayManager>();
        StartCoroutine("DestroyMessage");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMessage(string m)
    {
        GetComponent<TextMeshProUGUI>().text = m;
    }

    public IEnumerator DestroyMessage()
    {
        yield return new WaitForSeconds(3);
        displayManager.RemoveMessage(this.gameObject);
        Destroy(gameObject);
    }

}
