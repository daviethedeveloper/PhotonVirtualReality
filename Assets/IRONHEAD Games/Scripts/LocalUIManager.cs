using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LocalUIManager : MonoBehaviour
{

    [SerializeField] private GameObject GoHome_Button;
    // Start is called before the first frame update
    void Start()
    {
        GoHome_Button.GetComponent<Button>().onClick.AddListener(VirtualManager.Instance.LeaveRoomAndLoadHomeScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
