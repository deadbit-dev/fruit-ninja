using UnityEngine;

namespace Utils
{
    public static class RuntimeSpriteEditor
    {
        public static (Sprite, Sprite) SliceSprite(Sprite sprite, Vector2 direction)
        {
            var spriteA = Sprite.Create(sprite.texture, sprite.rect, sprite.pivot, sprite.pixelsPerUnit);
            var spriteB = Sprite.Create(sprite.texture, sprite.rect, sprite.pivot, sprite.pixelsPerUnit);

            var vertices = new Vector2[3];
            
            // TODO: vertices by direction

            var triangles = new ushort[] {0, 1, 2};

            spriteA.OverrideGeometry(vertices, triangles);
            
            // TODO: invert vertices/triangles
            
            spriteB.OverrideGeometry(vertices, triangles);
           
            // TODO: right return value
            return (sprite, sprite);
        }
    }
}