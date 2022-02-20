using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public static class RuntimeSpriteEditor
    {
        public static float SmartSliceAngleBySliceDirection(Sprite sprite, Vector2 sliceDirection)
        {
             var sliceAngle = Vector2.Angle((new Vector2(sprite.pivot.x + sprite.rect.xMax, sprite.pivot.y) - sprite.pivot).normalized, sliceDirection);
                        
             if (sliceDirection.y < 0)
             {
                 sliceAngle = 180 - sliceAngle;
             }
                        
             var angles = new List<float> {0f, 45f, 90f, 135f, 180f};
             return angles[Math.ClampRangeWeight(angles, sliceAngle / 180f)];
        }
        
        public static (Sprite, Sprite) SpriteSliceBySmartAngle(Sprite sprite, float smartSliceAngle)
        { 
            // TODO: move this data inside method
            
            var vertices = new Tuple<Vector2[], Vector2[]>[]
            {
                new Tuple<Vector2[], Vector2[]>(
                    // bottom rect
                    new Vector2[]
                    {
                        new Vector2(0f,0f), 
                        new Vector2(sprite.rect.size.x,0f), 
                        new Vector2(0f, sprite.rect.size.y * 0.5f), 
                        new Vector2(sprite.rect.size.x, sprite.rect.size.y * 0.5f)
                    },
                    // top rect
                    new Vector2[]
                    {
                        new Vector2(0f, sprite.rect.size.y * 0.5f), 
                        new Vector2(sprite.rect.size.x, sprite.rect.size.y * 0.5f),
                        new Vector2(0f,sprite.rect.size.y), 
                        new Vector2(sprite.rect.size.x,sprite.rect.size.y)
                    }
                ),
                new Tuple<Vector2[], Vector2[]>(
                    // left rect
                    new Vector2[]
                    {
                        new Vector2(0f,0f), 
                        new Vector2(sprite.rect.size.x * 0.5f,0f), 
                        new Vector2(0f, sprite.rect.size.y), 
                        new Vector2(sprite.rect.size.x * 0.5f, sprite.rect.size.y)
                    },
                    // right rect
                    new Vector2[]
                    {
                        new Vector2(sprite.rect.size.x * 0.5f,0f), 
                        new Vector2(sprite.rect.size.x,0f),
                        new Vector2(sprite.rect.size.x * 0.5f, sprite.rect.size.y), 
                        new Vector2(sprite.rect.size.x, sprite.rect.size.y)
                    }
                ),
                new Tuple<Vector2[], Vector2[]>(
                    // top left triangle
                    new Vector2[]
                    {
                        new Vector2(0f,0f), 
                        new Vector2(sprite.rect.size.x, sprite.rect.size.y), 
                        new Vector2(0f, sprite.rect.size.y)
                    },
                    // bottom right triangle
                    new Vector2[]
                    {
                        new Vector2(0f,0f), 
                        new Vector2(sprite.rect.size.x, sprite.rect.size.y), 
                        new Vector2(sprite.rect.size.x, 0f)
                    }
                ),
                new Tuple<Vector2[], Vector2[]>(
                    // bottom left triangle
                    new Vector2[]
                    {
                        new Vector2(0f,0f), 
                        new Vector2(sprite.rect.size.x,0f), 
                        new Vector2(0f,sprite.rect.size.y)
                    },
                    // top right triangle
                    new Vector2[]
                    {
                        new Vector2(sprite.rect.size.x, sprite.rect.size.y),
                        new Vector2(sprite.rect.size.x,0f), 
                        new Vector2(0f,sprite.rect.size.y)
                    }
                )
            };

            var triangles = new ushort[][]
            {
                new ushort[] {0, 1, 2, 2, 1, 3},
                new ushort[] {0, 1, 2, 2, 1, 3},
                new ushort[] {0, 1, 2},
                new ushort[] {0, 1, 2},
            };
            
            var angleToIndex = new Dictionary<float, int> { {0f, 0}, {90f, 1}, {45f, 2}, {135f, 3}, {180f, 0} };

            var spriteA = Object.Instantiate(sprite);
            var spriteB = Object.Instantiate(sprite);

            spriteA.OverrideGeometry(vertices[angleToIndex[smartSliceAngle]].Item1, triangles[angleToIndex[smartSliceAngle]]);
            spriteB.OverrideGeometry(vertices[angleToIndex[smartSliceAngle]].Item2, triangles[angleToIndex[smartSliceAngle]]);
            
            return (spriteA, spriteB);
        }

        public static Vector2 SliceDirectionByContact(Sprite sprite, Vector2 contactUV)
        {
            return (contactUV - SpritePivotUV(sprite)).normalized;
        }

        public static Vector2 SpritePivotUV(Sprite sprite)
        {
            return (new Vector2(sprite.rect.x, sprite.rect.y) + sprite.pivot) / sprite.rect.max;
        }

        public static Vector2 WorldPointToSpriteUV(Sprite sprite, Vector3 worldPoint, Transform spriteTransform)
        {
            return SpritePivotUV(sprite) + (Vector2) spriteTransform.InverseTransformPoint(worldPoint) * sprite.pixelsPerUnit;
        }
    }
}