using AnttiStarterKit.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubPanel : MonoBehaviour
{
    [SerializeField] private Button eatButton;
    [SerializeField] private Appearer levelUpAppearer;


    // Start is called before the first frame update
    void Start()
    {
        eatButton.onClick.AddListener(() =>
        {
            PlayerController.Instance.PlayerEat();
        });
        PlayerController.Instance.OnLevelUp += () =>
        {
            levelUpAppearer.Show();
            Invoke("HideLevelUp", 1f);
        };
    }

    void HideLevelUp()
    {
        levelUpAppearer.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
