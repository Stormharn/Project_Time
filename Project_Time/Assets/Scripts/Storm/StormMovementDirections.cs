using System.Collections.Generic;
using UnityEngine;

namespace ProjectTime.Storm
{
    public sealed class StormMovementDirections
    {
        private static readonly StormMovementDirections instance = new StormMovementDirections();
        private static StormDirections stormDirections;

        static StormMovementDirections()
        {
            stormDirections = new StormDirections();
        }

        private StormMovementDirections() { }

        public static StormMovementDirections Instance
        {
            get { return instance; }
        }

        public StormDirections GetDirections()
        {
            return stormDirections;
        }
    }

    public enum MovementDirections
    {
        TopRight,
        TopLeft,
        Right,
        Left,
        BottomRight,
        BottomLeft
    }

    public class StormDirections
    {
        public Dictionary<MovementDirections, Vector3> directions;

        public StormDirections()
        {
            directions = new Dictionary<MovementDirections, Vector3>();
            directions.Add(MovementDirections.TopRight, new Vector3(8.66025404f, 0.0f, 15.0f));
            directions.Add(MovementDirections.TopLeft, new Vector3(-8.66025404f, 0.0f, 15.0f));
            directions.Add(MovementDirections.Right, new Vector3(17.32050808f, 0.0f, 0.0f));
            directions.Add(MovementDirections.Left, new Vector3(-17.32050808f, 0.0f, 0.0f));
            directions.Add(MovementDirections.BottomRight, new Vector3(8.66025404f, 0.0f, -15.0f));
            directions.Add(MovementDirections.BottomLeft, new Vector3(-8.66025404f, 0.0f, -15.0f));
        }

        public Vector3 GetVector(MovementDirections direction)
        {
            if (directions.ContainsKey(direction))
                return directions[direction];
            return Vector3.zero;
        }
    }
}