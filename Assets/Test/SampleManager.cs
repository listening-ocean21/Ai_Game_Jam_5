using AnttiStarterKit.Animations;
using AnttiStarterKit.Events;
using AnttiStarterKit.Utils;
using AnttiStarterKit.Visuals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleManager : MonoBehaviour
{
    [SerializeField] private Appearer appearer;
    [SerializeField] private Pulsater pulsater;
    [SerializeField] private Shaker shaker;
    [SerializeField] private SpeechBubble speechBubble;
    [SerializeField] private NumberScroller numberScroller;
    [SerializeField] private EffectCamera effectCamera;
    [SerializeField] private GameEvent gameEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            TestAppearer();
            TestPulsater();
            TestShaker();
            TestSpeech();
            TestNumberScroller();
            TestEffectCamera();
            gameEvent.Trigger();
        }
    }

    void TestAppearer()
    {
        if (!appearer.IsShown)
            appearer.Show();
        else
            appearer.Hide();
    }

    void TestPulsater()
    {
        pulsater.Pulsate();
    }

    void TestShaker()
    {
        shaker.Shake();
    }

    void TestSpeech()
    {
        speechBubble.Show("Hello world!");
    }

    void TestNumberScroller()
    {
        numberScroller.Add(1);
    }

    void TestEffectCamera()
    {
        effectCamera.BaseEffect();
    }

    public void TestBottonStyleClick()
    {
        Debug.Log("TestBottonStyleClick");
    }

}
