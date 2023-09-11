using System.Collections.Generic;
using UnityEngine;

public abstract class IInputProvider
{
    protected IInputProvider(List<Platform> platforms)
    {
        Platforms = platforms;
    }

    protected List<Platform> Platforms { get;}
    public abstract void Update();
}

public class KeyboardInput : IInputProvider
{
    private float m_position;

    public override void Update()
    {
        var u = (Input.GetAxis("Horizontal") + 1) / 2;
        for (int i = 0; i < Platforms.Count; i++)
            Platforms[i].SetPosition(u);
    }

    public KeyboardInput(List<Platform> platforms) : base(platforms)
    {
    }
}

public class TouchInput : IInputProvider
{
    public override void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var u = touch.position.x / Screen.width;
            for (int i = 0; i < Platforms.Count; i++)
                Platforms[i].SetPosition(u);
        }
    }

    public TouchInput(List<Platform> platforms) : base(platforms)
    {
    }
}