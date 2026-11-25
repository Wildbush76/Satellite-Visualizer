using Assets.Scripts.Satellite;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject categoryPrefab;
        [SerializeField]
        private Transform categories;
        void Start()
        {
            foreach (SatelliteCategory category in SatelliteGroupConfig.ALL_GROUPS.Keys)
            {
                var obj = GameObject.Instantiate(categoryPrefab, categories);
                var uiCategory = obj.GetComponent<UiSatelliteCategory>();
                uiCategory.Initialize(category);
            }
        }
    }
}