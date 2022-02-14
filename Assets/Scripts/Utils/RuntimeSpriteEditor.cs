using UnityEngine;

namespace Utils
{
    public static class RuntimeSpriteEditor
    {
        public static (Sprite, Sprite) SpriteSlice(Sprite sprite, Vector2 secant)
        {
            var spriteA = Sprite.Create(sprite.texture, sprite.rect, sprite.pivot, sprite.pixelsPerUnit);
            var spriteB = Sprite.Create(sprite.texture, sprite.rect, sprite.pivot, sprite.pixelsPerUnit);

            var vertices = new Vector2[]
            {
                
            };

            var triangles = new ushort[] { 0, 1, 2, 1, 3, 2 };

            spriteA.OverrideGeometry(vertices, triangles);
            
            // TODO: invert vertices/triangles
            
            spriteB.OverrideGeometry(vertices, triangles);
           
            return (sprite, sprite);
        }

        public static Vector2 SpritePivotUV(Sprite sprite)
        {
            return (new Vector2(sprite.rect.x, sprite.rect.y) + sprite.pivot) / sprite.rect.max;
        }

        public static Vector2 WorldPointToSpriteUV(Sprite sprite, Vector3 worldPoint, Transform transform)
        {
            return SpritePivotUV(sprite) + (Vector2) transform.InverseTransformPoint(worldPoint) * sprite.pixelsPerUnit;
        }
    }
}