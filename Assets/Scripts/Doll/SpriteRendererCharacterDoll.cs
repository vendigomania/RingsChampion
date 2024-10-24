using UnityEngine;

public class SpriteRendererCharacterDoll : CharacterDoll<SpriteRenderer>
{
    protected override void SetPart(SpriteRenderer part, Sprite _sprite)
    {
        part.sprite = _sprite;
    }
}
