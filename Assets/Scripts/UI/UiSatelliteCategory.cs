using System.Collections.Generic;
using Assets.Scripts.Satellite;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
namespace Assets.Scripts.UI
{
    public class UiSatelliteCategory : MonoBehaviour
    {

        [SerializeField]
        private Transform groupsTransform;

        [SerializeField]
        private GameObject groupPrefab;

        [SerializeField]
        private TextMeshProUGUI categoryTitle;


        private readonly List<UiSatelliteGroup> groups = new();

        public void Initialize(SatelliteCategory category)
        {
            categoryTitle.text = category.ToString();
            if (SatelliteGroupConfig.ALL_GROUPS.TryGetValue(category, out var groupList))
            {
                groupList.ForEach(CreateGroup);
            }
        }

        private void CreateGroup(SatelliteGroupConfig groupConfig)
        {
            var obj = GameObject.Instantiate(groupPrefab, groupsTransform);
            var group = obj.GetComponent<UiSatelliteGroup>();
            group.Initialize(groupConfig);
            groups.Add(group);
        }

        public void SetAllGroupActive(bool active)
        {
            groups.ForEach(g => g.SatelliteActive = active);
        }

    }
}