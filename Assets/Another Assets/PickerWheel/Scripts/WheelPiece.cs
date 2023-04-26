using UnityEngine ;
using UnityEngine.UI;

namespace EasyUI.PickerWheelUI 
{
    [System.Serializable]
    public class WheelPiece
    {
        [SerializeField] private Prize _prize;
        [SerializeField] private GameObject _selectFrame;
        public Prize Prize => _prize;
        public Sprite Icon => Prize.UIIcon;
        public string Label => (Prize.PrizeAsInterface as ScriptableObject).name;

        [Tooltip("Reward amount")] public int Amount => Prize.AmountPrize;

        [Tooltip("Probability in %")]
        [Range(0f, 100f)]
        public float Chance = 100f;

        [HideInInspector] public int Index;
        [HideInInspector] public double _weight = 0f;


        public void Select()
        {
            _selectFrame.SetActive(true);
        }
    }
}
