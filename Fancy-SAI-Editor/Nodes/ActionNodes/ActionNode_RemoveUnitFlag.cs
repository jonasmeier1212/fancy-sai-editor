using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Remove UnitFlags", Type = NodeType.ACTION_REMOVE_UNIT_FLAG, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class RemoveUnitFlag : ActionNode
    {
        public RemoveUnitFlag()
        {
            Type = NodeType.ACTION_REMOVE_UNIT_FLAG;

            ActionId = "19";

            //Update text
            NodeName.Content = "Remove UnitFlags";

            AddParam<UnitFlags>(ParamId.PARAM_1, "Flags");
            AddParam<UnitFlagTypes>(ParamId.PARAM_2, "Flag type");
        }

        [Flags]
        enum UnitFlags : UInt32
        {
            UNIT_FLAG_SERVER_CONTROLLED = 0x00000001,           // set only when unit movement is controlled by server - by SPLINE/MONSTER_MOVE packets, together with UNIT_FLAG_STUNNED; only set to units controlled by client; client function CGUnit_C::IsClientControlled returns false when set for owner
            UNIT_FLAG_NON_ATTACKABLE = 0x00000002,           // not attackable
            UNIT_FLAG_DISABLE_MOVE = 0x00000004,
            UNIT_FLAG_PVP_ATTACKABLE = 0x00000008,           // allow apply pvp rules to attackable state in addition to faction dependent state
            UNIT_FLAG_RENAME = 0x00000010,
            UNIT_FLAG_PREPARATION = 0x00000020,           // don't take reagents for spells with SPELL_ATTR5_NO_REAGENT_WHILE_PREP
            UNIT_FLAG_UNK_6 = 0x00000040,
            UNIT_FLAG_NOT_ATTACKABLE_1 = 0x00000080,           // ?? (UNIT_FLAG_PVP_ATTACKABLE | UNIT_FLAG_NOT_ATTACKABLE_1) is NON_PVP_ATTACKABLE
            UNIT_FLAG_IMMUNE_TO_PC = 0x00000100,           // disables combat/assistance with PlayerCharacters (PC) - see Unit::_IsValidAttackTarget, Unit::_IsValidAssistTarget
            UNIT_FLAG_IMMUNE_TO_NPC = 0x00000200,           // disables combat/assistance with NonPlayerCharacters (NPC) - see Unit::_IsValidAttackTarget, Unit::_IsValidAssistTarget
            UNIT_FLAG_LOOTING = 0x00000400,           // loot animation
            UNIT_FLAG_PET_IN_COMBAT = 0x00000800,           // in combat?, 2.0.8
            UNIT_FLAG_PVP = 0x00001000,           // changed in 3.0.3
            UNIT_FLAG_SILENCED = 0x00002000,           // silenced, 2.1.1
            UNIT_FLAG_UNK_14 = 0x00004000,           // 2.0.8
            UNIT_FLAG_UNK_15 = 0x00008000,
            UNIT_FLAG_UNK_16 = 0x00010000,
            UNIT_FLAG_PACIFIED = 0x00020000,           // 3.0.3 ok
            UNIT_FLAG_STUNNED = 0x00040000,           // 3.0.3 ok
            UNIT_FLAG_IN_COMBAT = 0x00080000,
            UNIT_FLAG_TAXI_FLIGHT = 0x00100000,           // disable casting at client side spell not allowed by taxi flight (mounted?), probably used with 0x4 flag
            UNIT_FLAG_DISARMED = 0x00200000,           // 3.0.3, disable melee spells casting..., "Required melee weapon" added to melee spells tooltip.
            UNIT_FLAG_CONFUSED = 0x00400000,
            UNIT_FLAG_FLEEING = 0x00800000,
            UNIT_FLAG_PLAYER_CONTROLLED = 0x01000000,           // used in spell Eyes of the Beast for pet... let attack by controlled creature
            UNIT_FLAG_NOT_SELECTABLE = 0x02000000,
            UNIT_FLAG_SKINNABLE = 0x04000000,
            UNIT_FLAG_MOUNT = 0x08000000,
            UNIT_FLAG_UNK_28 = 0x10000000,
            UNIT_FLAG_UNK_29 = 0x20000000,           // used in Feing Death spell
            UNIT_FLAG_SHEATHE = 0x40000000,
            UNIT_FLAG_UNK_31 = 0x80000000,
        }

        enum UnitFlagTypes
        {
            UNIT_FLAG,
            //UNIT_FLAG_2, //TODO
        }

        public override Node Clone()
        {
            return new RemoveUnitFlag();
        }
    }
}
