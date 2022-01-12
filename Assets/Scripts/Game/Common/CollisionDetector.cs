using UnityEngine;
using Game.Models;

namespace Game.Common
{
    public class CollisionDetector : MonoBehaviour
    {
        public CollisionStatus collisionStatus;
        public GameObject colliderObject;

        private void Start()
        {
            collisionStatus = CollisionStatus.Null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            colliderObject = collision.gameObject;
            collision.GetComponent<EmptyTile>()?.OnStartCollision(this);
            collision.GetComponent<BuildingTile>()?.OnStartCollision(this);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            colliderObject = collision.gameObject;
            collision.GetComponent<EmptyTile>()?.OnStartCollision(this);
            collision.GetComponent<BuildingTile>()?.OnStartCollision(this);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            colliderObject = collision.gameObject;
            collision.GetComponent<EmptyTile>()?.OnEndCollision(this);
            collision.GetComponent<BuildingTile>()?.OnEndCollision(this);
        }
    }
}

