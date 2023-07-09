using AnttiStarterKit.Animations;
using AnttiStarterKit.Utils;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int MaxLevel = 5;
    public int MaxDay = 25;

    public Transform m_Body;
    public PlayerAttributes Attributes;



    [SerializeField] private SpriteRenderer playerImage;
    [SerializeField] private SpriteRenderer nextPlayerImage;
    [SerializeField] private Shaker shaker;


    [SerializeField] private Sprite[] playerImages;

    private void Awake()
    {
        Attributes = new PlayerAttributes();
    }

    // Start is called before the first frame update
    public void Start()
    {
        playerImage.sprite = playerImages[0];
        nextPlayerImage.sprite = playerImages[1];
        nextPlayerImage.color = new Color(1, 1, 1, 0);
        PlayerController.Instance.OnPlayerEat.AddListener(PlayEatAnim);
        PlayerController.Instance.OnPlayerDead.AddListener(PlayDeadAnim);
        PlayerController.Instance.OnLevelUp += PlayLevelUpAnim;
    }


    public void Update()
    {

    }
    // 升级后使用
    public void PlayLevelUpAnim()
    {
        nextPlayerImage.transform.localScale = new Vector3(2f, 2f, 1f);
        nextPlayerImage.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        nextPlayerImage.DOColor(new Color(1, 1, 1, 1), 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            nextPlayerImage.sprite = playerImages[Mathf.Min(Attributes.Level, playerImages.Length - 1)];
            nextPlayerImage.color = new Color(1, 1, 1, 0);
        });
        playerImage.DOColor(new Color(1, 1, 1, 0), 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            playerImage.sprite = nextPlayerImage.sprite;
            playerImage.color = new Color(1, 1, 1, 1);
        });
    }

    public void PlayEatAnim()
    {
        shaker.Shake();
    }

    public void PlayDeadAnim()
    {
        playerImage.transform.DOScale(0.5f, 0.5f).SetEase(Ease.OutBack);
        playerImage.DOColor(new Color(1, 1, 1, 0), 0.5f).SetEase(Ease.OutBack);
    }


    public class PlayerAttributes
    {
        public int Level;           // 等级
        public int Health;          // 生命值
        public int San;             // 理智
        public int Strength;    // 适应力
        public int Day;             // 天数


        public PlayerAttributes()
        {
            Level = 1;
            Day = 1;
            Health = 5;
            San = 4;
            Strength = 12;
        }

        public void LevelUp(int val)
        {
            Level += val;
            Health += 2 * val;
            San += 1 * val;
            Strength -= 2 * val;
        }
    }
}

[Serializable]
public class DayCost
{
    public int Health;          // 生命值
    public int San;             // 理智
    public int Strength;    // 适应力
}

[Serializable]
public class DayCosts
{
    public List<DayCost> Costs;
}

