using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 2;
    public GameObject menu;
    public GameObject options;
    public InputActionProperty showButton;
    public static GameMenuManager Instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        menu.SetActive(!menu.activeSelf);
        options.SetActive(!options.activeSelf);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame()) 
        {
            
            menu.SetActive(!menu.activeSelf);
            options.SetActive(false);
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance; 
            options.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance; 
        }
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z + 180));
        options.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z + 180));
    }


}
