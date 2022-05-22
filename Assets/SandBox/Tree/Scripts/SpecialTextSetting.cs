using System;
using System.Drawing;
using DG.Tweening;

[Serializable]
public class SpecialTextSetting
{
    public float position;
    public float jumpDuration;
    public float spaceDuration;
    public Ease ease;
    public LoopType loopType;
    public int loops;
    public Color color;
}