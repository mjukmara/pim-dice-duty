using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;

    public enum ItemType { DICE, DOT };

    public enum ItemColor { WHITE, YELLOW, ORANGE, RED, VIOLET, BLUE, GREEN };

    public enum ItemNumber { ONE, TWO, THREE, FOUR, FIVE, SIX };

    public static readonly Dictionary<ItemNumber, int> numberMap = new Dictionary<ItemNumber, int>
    {
        { ItemNumber.ONE, 1 },
        { ItemNumber.TWO, 2 },
        { ItemNumber.THREE, 3 },
        { ItemNumber.FOUR, 4 },
        { ItemNumber.FIVE, 5 },
        { ItemNumber.SIX, 6 },
    };

    public static readonly Dictionary<ItemColor, Color> colorMap = new Dictionary<ItemColor, Color>
    {
        { ItemColor.WHITE, new Color(255f/255f, 255f/255f, 255f/255f) },
        { ItemColor.YELLOW, new Color(254f/255f, 243f/255f, 5f/255f) },
        { ItemColor.ORANGE, new Color(254f/255f, 144f/255f, 3f/255f) },
        { ItemColor.RED, new Color(254f/255f, 4f/255f, 5f/255f) },
        { ItemColor.VIOLET, new Color(123f/255f, 32f/255f, 161f/255f) },
        { ItemColor.BLUE, new Color(17f/255f, 97f/255f, 255f/255f) },
        { ItemColor.GREEN, new Color(38f/255f, 180f/255f, 80f/255f) },
    };

    public List<Sprite> diceSprites;
    public Sprite dotSprite;
    public ItemType type;
    public ItemNumber number;
    public ItemColor color;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        _spriteRenderer.color = colorMap[color];
        if (type == ItemType.DICE)
        {
            _spriteRenderer.sprite = diceSprites[(int)number];
        }
        if (type == ItemType.DOT)
        {
            _spriteRenderer.sprite = dotSprite;
        }
    }
}
