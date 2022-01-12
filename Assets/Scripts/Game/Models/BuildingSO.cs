using System;
using UnityEngine;

namespace Game.Models
{
    [CreateAssetMenu(fileName = "New Building", menuName = "Building")]
    public class BuildingSO : ScriptableObject
    {
        public string buildingName;
        public Sprite image;
        public Cost cost;
        public float resourceGenerationDuration;
        public GeneratedResources generatedResources;
        public BuildingShape shape;
    }

    [Serializable]
    public class Cost
    {
        public int goldCost;
        public int gemCost;
    }

    [Serializable]
    public class GeneratedResources
    {
        public int goldsGenerated;
        public int gemsGenerated;
    }
}

