using UnityEngine;
using Game.Common;

namespace Game.Models
{
    public abstract class Tile : MonoBehaviour
    {
        public int x;
        public int y;

        public abstract void OnStartCollision(CollisionDetector collisionDetector);
        public abstract void OnEndCollision(CollisionDetector collisionDetector);
    }
}

