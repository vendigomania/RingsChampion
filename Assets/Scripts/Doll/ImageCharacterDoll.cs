using UnityEngine;
using UnityEngine.UI;

public class ImageCharacterDoll : CharacterDoll<Image>
{
    protected override void SetPart(Image part, Sprite _sprite)
    {
        part.sprite = _sprite;
    }
}
