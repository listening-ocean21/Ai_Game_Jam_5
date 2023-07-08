using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private SceneChangerObject sceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Demo");
            //sceneChanger.ChangeScene("Demo");
        });

        exitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
            //sceneChanger.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
