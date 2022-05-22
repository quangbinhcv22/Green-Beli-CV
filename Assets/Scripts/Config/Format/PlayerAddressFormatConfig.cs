using UnityEngine;

namespace Config.Format
{
    [CreateAssetMenu(fileName = "PlayerAddressFormatConfig", menuName = "ScriptableObjects/FormatConfig/PlayerAddress")]
    public class PlayerAddressFormatConfig : UnityEngine.ScriptableObject
    {
        [SerializeField] private int trimStartLength;
        [SerializeField] private int trimEndLength;
        [SerializeField] private string middleSeparator;
        [SerializeField] private string errorResult;

        public string FormattedAddress(string address)
        {
            if (address.Length < Mathf.Max(trimStartLength, trimEndLength)) return errorResult;

            var trimStartAddress = address.Substring(startIndex: 0, length: trimStartLength);
            var trimEndAddress = address.Substring(startIndex: address.Length - trimEndLength, length: trimEndLength);

            var formattedString = $"{trimStartAddress}{middleSeparator}{trimEndAddress}";

            return formattedString;
        }
    }
}