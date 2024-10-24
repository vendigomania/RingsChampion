using Data;
using UnityEngine;

public abstract class CharacterDoll<T> : MonoBehaviour
{
    [SerializeField] private T[] parts;

    public void SetSkin(CharacterSkinData _skin)
    {
        for(var i = 0; i < parts.Length; i++)
        {
            SetPart(parts[i], _skin.SkinElements[i]);
        }
    }

    protected abstract void SetPart(T part, Sprite _sprite);
}
