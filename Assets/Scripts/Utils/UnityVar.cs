using UnityEngine;

public abstract class UnityVar<T> : ScriptableObject
{
    public T value;

    public T GetValue()
    {
        return value;
    }

    public void SetValue(T newValue)
    {
        value = newValue;
    }

}
