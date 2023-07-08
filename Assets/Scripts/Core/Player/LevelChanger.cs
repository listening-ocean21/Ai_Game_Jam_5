using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private Animator Animator1;
    [SerializeField] private Animator Animator2;

    [SerializeField] private SpriteRenderer spriteRenderer1;
    [SerializeField] private SpriteRenderer spriteRenderer2;

    // Start is called before the first frame update
    void Start()
    {
        Animator1.Play("LevelAnim1");
        Animator2.Play("LevelAnim1");
        spriteRenderer2.color = new Color(1, 1, 1, 0);
        PlayerController.Instance.OnLevelUp += ShowLevel;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowLevel()
    {
        //prevAnimator.Play("LevelAnim" + Mathf.Max(PlayerController.Instance.Player.Attributes.Level - 1, 1));
        Animator2.Play("LevelAnim" + PlayerController.Instance.Player.Attributes.Level);
        spriteRenderer1.DOColor(new Color(1, 1, 1, 0), 0.5f);
        spriteRenderer2.DOColor(new Color(1, 1, 1, 1), 0.5f);
        SwapAnimator();
        SwapSpriteRenderer();
    }

    private void SwapAnimator()
    {
        Animator temp = Animator1;
        Animator1 = Animator2;
        Animator2 = temp;
    }

    private void SwapSpriteRenderer()
    {
        SpriteRenderer temp = spriteRenderer1;
        spriteRenderer1 = spriteRenderer2;
        spriteRenderer2 = temp;
    }

}
