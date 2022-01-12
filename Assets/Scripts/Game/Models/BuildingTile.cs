using Game.Common;

namespace Game.Models
{
    public class BuildingTile : Tile
    {
        public override void OnStartCollision(CollisionDetector collisionDetector)
        {
            collisionDetector.collisionStatus = CollisionStatus.BuildingTile;
        }

        public override void OnEndCollision(CollisionDetector collisionDetector)
        {
            collisionDetector.collisionStatus = CollisionStatus.Null;
        }
    }
}

