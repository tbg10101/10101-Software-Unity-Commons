using System.Collections.Generic;
using UnityEngine;

namespace Software10101.EngineExtensions {
    public static class GameObjectExtensions {
        public static void AddMaterialIfNotPresentToAllChildren(this GameObject gameObject, Material material) {
            List<Material> materials = new List<Material>();
            MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer meshRenderer in meshRenderers) {
                meshRenderer.GetSharedMaterials(materials);
                if (!materials.Contains(material)) {
                    materials.Add(material);
                    meshRenderer.sharedMaterials = materials.ToArray();
                }
            }
        }

        public static void RemoveMaterialFromAllChildren(this GameObject gameObject, Material material) {
            List<Material> materials = new List<Material>();
            MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer meshRenderer in meshRenderers) {
                meshRenderer.GetSharedMaterials(materials);
                if (materials.Contains(material)) {
                    materials.Remove(material);
                    meshRenderer.sharedMaterials = materials.ToArray();
                }
            }
        }
    }
}
