using Game.Common;

namespace Game.Models
{
    public class EmptyTile : Tile
    {
        public override void OnStartCollision(CollisionDetector collisionDetector)
        {
            collisionDetector.collisionStatus = CollisionStatus.EmptyTile;
        }

        public override void OnEndCollision(CollisionDetector collisionDetector)
        {
            collisionDetector.collisionStatus = CollisionStatus.Null;
        }
    }
}

