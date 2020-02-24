using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PickUpItemDisplayManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> displayMessages = new List<GameObject>();

    public float shiftHeight;
    public GameObject pickUpItemMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMessage(string message,Color color)
    {
       
        if (displayMessages.Count > 0)
        {
            for (int i = 0; i < displayMessages.Count; i++)
            {
               Vector2 pos = displayMessages[i].GetComponent<RectTransform>().anchoredPosition;
                displayMessages[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x,pos.y-shiftHeight);

            }
        }

        var p = Instantiate(pickUpItemMessage, transform.position, Quaternion.identity, this.transform);
        p.GetComponent<PickUpItemDisplay>().SetMessage(message);
        p.GetComponent<TextMeshProUGUI>().color = color;
        displayMessages.Add(p);

    }

    public void RemoveMessage(GameObject message)
    {
        if (displayMessages.Contains(message))
        {
            displayMessages.Remove(message);
        }
       
    }
}
