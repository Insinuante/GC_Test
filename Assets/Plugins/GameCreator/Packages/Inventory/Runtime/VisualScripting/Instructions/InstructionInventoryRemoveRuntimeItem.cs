using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Inventory
{
    [Version(0, 0, 1)]
    
    [Title("Remove Runtime Item")]
    [Description("Removes an Item instance from its associated Bag")]

    [Category("Inventory/Bags/Remove Runtime Item")]
    
    [Parameter("Runtime Item", "The item instance to be removed")]

    [Keywords("Bag", "Inventory", "Container", "Stash")]
    [Keywords("Give", "Take", "Borrow", "Lend", "Buy", "Purchase", "Sell", "Steal", "Rob")]
    
    [Image(typeof(IconItem), ColorTheme.Type.Blue, typeof(OverlayMinus))]
    
    [Serializable]
    public class InstructionInventoryRemoveRuntimeItem : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private PropertyGetRuntimeItem m_RuntimeItem = new PropertyGetRuntimeItem();

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Remove {this.m_RuntimeItem}";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            RuntimeItem runtimeItem = this.m_RuntimeItem.Get(args);
            if (runtimeItem == null || runtimeItem.Item == null) return DefaultResult;

            Bag bag = runtimeItem.Bag;
            if (bag == null) return DefaultResult;

            bag.Content.Remove(runtimeItem);
            return DefaultResult;
        }
    }
}