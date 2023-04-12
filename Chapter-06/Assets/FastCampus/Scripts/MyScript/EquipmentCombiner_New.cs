using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentCombiner_New
{
    private readonly Dictionary<int, Transform> _rootBoneDictionary = new Dictionary<int, Transform>();

    private readonly Transform _transform;

    public EquipmentCombiner_New(GameObject rootGO)
    {
        _transform = rootGO.transform;
        TraverseHierarchy(_transform);
    }

    public Transform Addlimb(GameObject itemGO, List<string> boneNames)
    {
        Transform limb = ProcessBoneObject(itemGO.GetComponentInChildren<SkinnedMeshRenderer>(), boneNames);
        limb.SetParent(_transform);

        return limb;
    }

    private Transform ProcessBoneObject(SkinnedMeshRenderer renderer, List<string> boneNames)
    {
        Transform itemTransform = new GameObject().transform;

        SkinnedMeshRenderer meshRenderer = itemTransform.gameObject.AddComponent<SkinnedMeshRenderer>();

        Transform[] boneTransforms = new Transform[boneNames.Count];
        for (int i = 0; i < boneNames.Count; ++i)
        {
            boneTransforms[i] = _rootBoneDictionary[boneNames[i].GetHashCode()];
        }

        meshRenderer.bones = boneTransforms;
        meshRenderer.sharedMesh = renderer.sharedMesh;
        meshRenderer.material = renderer.sharedMaterial;

        return itemTransform;
    }

    public Transform[] AddMesh(GameObject itemGO)
    {
        Transform[] itemTransforms = ProcessMeshObject(itemGO.GetComponentsInChildren<MeshRenderer>());
        return itemTransforms;
    }

    private Transform[] ProcessMeshObject(MeshRenderer[] meshRenderers)
    {
        List<Transform> itemTransforms = new List<Transform>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            if (renderer.transform.parent != null)
            {
                Transform parent = _rootBoneDictionary[renderer.transform.parent.name.GetHashCode()];

                GameObject itemGO = GameObject.Instantiate(renderer.gameObject, parent);

                itemTransforms.Add(itemGO.transform);
            }
        }

        return itemTransforms.ToArray();
    }

    private void TraverseHierarchy(Transform root)
    {
        foreach (Transform child in root) {
            _rootBoneDictionary.Add(child.name.GetHashCode(), child);

            TraverseHierarchy(child);
        }
    }
}
