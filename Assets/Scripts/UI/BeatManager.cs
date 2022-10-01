using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatManager : ClampedTimer
{
    public static BeatManager instance = null;

    public Slider slider;
    public RectTransform handleSlideArea;

    [SerializeField]
    GameObject shroomIcon;

    Dictionary<Shroom, GameObject> representations;

    protected override void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
            return;
        }
        base.Awake();
    }

    private void LateUpdate()
    {
        slider.value = elapsedTime / timerTick;
    }

    public void registerNewShroom(Shroom shroom)
    {
        GameObject icon = Instantiate(shroomIcon, handleSlideArea);
        var rect = icon.GetComponent<RectTransform>();
        float pos = elapsedTime / timerTick;
        rect.anchorMin = new Vector2(pos, 0f);
        rect.anchorMax = new Vector2(pos, 1f);
    }
}
