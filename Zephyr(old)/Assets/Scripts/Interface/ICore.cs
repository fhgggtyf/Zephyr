using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICore
{
    // Start is called before the first frame update
    public abstract void LogicUpdate();

    public abstract void AddComponent(CoreComponent component);

    public abstract T GetCoreComponent<T>() where T : CoreComponent;

    public abstract T GetCoreComponent<T>(ref T value) where T : CoreComponent;
}
