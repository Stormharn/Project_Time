using UnityEngine;

namespace ProjectTime.HexGrid
{
    public static class Hex
    {
        public const float outerRadius = 10f;
        public const float innerRadius = outerRadius * radiusRatio;

        private const float radiusRatio = 0.866025404f;

        public static Vector3[] corners = {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius)
        };
    }
}