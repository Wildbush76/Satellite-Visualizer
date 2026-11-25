using System.Text.RegularExpressions;
using Assets.Scripts.Satellite;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UiSatelliteGroup : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI groupTitle;
        [SerializeField]
        private SVGImage icon;

        [SerializeField]
        private Toggle toggle;
        private string groupId;

        public void Initialize(SatelliteGroupConfig groupConfig)
        {
            groupTitle.text = groupConfig.Name;
            icon.color = groupConfig.Color;
            groupId = groupConfig.CelestrakId;
        }


        public bool SatelliteActive
        {
            get => toggle.isOn;
            set
            {
                if (value != toggle.isOn)
                    toggle.SetIsOnWithoutNotify(value);

                if (SatelliteManager.Instance.satelliteGroups.TryGetValue(groupId, out var constellation))
                {
                    constellation.gameObject.SetActive(value);
                }
                else
                {
                    Debug.LogWarning($"Failed to find satellite group with id {groupId}");
                }

            }
        }
    }
}