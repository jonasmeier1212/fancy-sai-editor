using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NodeAI
{
#pragma warning disable 1591
    /// <summary>
    /// Enumeration for all node types.
    /// _MAX is used to identify the base node type.
    /// </summary>
    public enum NodeType
    {
        NONE,

        /// <summary>
        /// Includes all event node types
        /// </summary>
        EVENT,
        EVENT_UPDATE,
        EVENT_HEALTH_PTC,
        EVENT_MANA_PTC,
        EVENT_AGGRO,
        EVENT_KILL,
        EVENT_DEATH,
        EVENT_EVADE,
        EVENT_SPELLHIT,
        EVENT_RANGE,
        EVENT_LOS,
        EVENT_RESPAWN,
        EVENT_TARGET_HEALTH_PCT,
        EVENT_VICTIM_CASTING,
        EVENT_FRIENDLY_HEALTH,
        EVENT_FRIENDLY_IS_CC,
        EVENT_FRIENDLY_MISSING_BUFF,
        EVENT_SUMMONED_UNIT,
        EVENT_TARGET_MANA_PCT,
        EVENT_QUEST_ACCEPT,
        EVENT_MAX,

        /// <summary>
        /// Includes all action node types
        /// </summary>
        ACTION,
        ACTION_TALK,
        ACTION_SET_FACTION,
        ACTION_MORPH_TO_ENTRY_OR_MODEL,
        ACTION_PLAY_SOUND,
        ACTION_PLAY_EMOTE,
        ACTION_FAIL_QUEST,
        ACTION_OFFER_QUEST,
        ACTION_SET_REACT_STATE,
        ACTION_ACTIVATE_GOBJECT,
        ACTION_PLAY_RANDOM_EMOTE,
        ACTION_CAST,
        ACTION_SUMMON_CREATURE,
        ACTION_THREAT_SINGLE_PCT,
        ACTION_CALL_AREAEXPLORED,
        ACTION_SET_EMOTE_STATE,
        ACTION_SET_UNIT_FLAG,
        ACTION_REMOVE_UNIT_FLAG,
        ACTION_AUTO_ATTACK,
        ACTION_ALLOW_COMBAT_MOVEMENT,
        ACTION_SET_EVENT_PHASE,
        ACTION_INC_EVENT_PHASE,
        ACTION_EVADE,
        ACTION_FLEE_FOR_ASSIST,
        ACTION_COMBAT_STOP,
        ACTION_REMOVEAURASFROMSPELL,
        ACTION_FOLLOW,
        ACTION_RESET_GOBJECT,
        ACTION_SUMMON_GAMEOBJECT,
        ACTION_KILL_UNIT,
        ACTION_ATTACK_START,
        ACTION_CLOSE_GOSSIP,
        ACTION_ADD_AURA,
        ACTION_SET_NPC_FLAG,
        ACTION_ADD_NPC_FLAG,
        ACTION_REMOVE_NPC_FLAG,
        ACTION_MAX,

        /// <summary>
        /// Includes all general node types
        /// </summary>
        GENERAL,
        GENERAL_TEXT,
        GENERAL_QUEST,
        GENERAL_NPC,
        GENERAL_NPC_MODEL,
        GENERAL_SPELL,
        GENERAL_FACTION,
        GENERAL_EMOTE,
        GENERAL_SOUND,
        GENERAL_GAMEOBJECT,
        GENERAL_MAX,

        /// <summary>
        /// Includes all target node types
        /// </summary>
        TARGET,
        TARGET_SELF,
        TARGET_VICTIM,
        TARGET_HOSTILE_SECOND_AGGRO,
        TARGET_HOSTILE_LAST_AGGRO,
        TARGET_HOSTILE_RANDOM,
        TARGET_HOSTILE_RANDOM_NOT_TOP,
        TARGET_ACTION_INVOKER,
        TARGET_POSITION,
        TARGET_CREATURE_RANGE,
        TARGET_CREATURE_GUID,
        TARGET_CREATURE_DISTANCE,
        TARGET_STORED,
        TARGET_GAMEOBJECT_RANGE,
        TARGET_GAMEOBJECT_GUID,
        TARGET_GAMEOBJECT_DISTANCE,
        TARGET_INVOKER_PARTY,
        TARGET_PLAYER_RANGE,
        TARGET_PLAYER_DISTANCE,
        TARGET_CLOSEST_CREATURE,
        TARGET_CLOSEST_GAMEOBJECT,
        TARGET_CLOSEST_PLAYER,
        TARGET_ACTION_INVOKER_VEHICLE,
        TARGET_OWNER_OR_SUMMONER,
        TARGET_THREAT_LIST,
        TARGET_CLOSEST_ENEMY,
        TARGET_CLOSEST_FRIENDLY,
        TARGET_LOOT_RECIPIENTS,
        TARGET_FARTHEST,
        TARGET_VEHICLE_ACCESSORY,
        TARGET_MAX,
    }

    /// <summary>
    /// Every node can have these connectors.
    /// </summary>
    public enum NodeConnectorType
    {
        INPUT,
        OUTPUT,
    }

    public enum ParamId
    {
        PARAM_1,
        PARAM_2,
        PARAM_3,
        PARAM_4,
        PARAM_5,
        PARAM_6,
        PARAM_7,

        PARAM_SPECIFIC_TYPE,
    }

    public enum TextType
    {
        SAY         = 12,
        YELL        = 14,
        EMOTE       = 16,
        BOSS_EMOTE  = 41,
    }

    [Flags]
    public enum NpcFlags
    {
        UNIT_NPC_FLAG_NONE = 0x00000000,
        UNIT_NPC_FLAG_GOSSIP = 0x00000001,       // 100%
        UNIT_NPC_FLAG_QUESTGIVER = 0x00000002,       // 100%
        UNIT_NPC_FLAG_UNK1 = 0x00000004,
        UNIT_NPC_FLAG_UNK2 = 0x00000008,
        UNIT_NPC_FLAG_TRAINER = 0x00000010,       // 100%
        UNIT_NPC_FLAG_TRAINER_CLASS = 0x00000020,       // 100%
        UNIT_NPC_FLAG_TRAINER_PROFESSION = 0x00000040,       // 100%
        UNIT_NPC_FLAG_VENDOR = 0x00000080,       // 100%
        UNIT_NPC_FLAG_VENDOR_AMMO = 0x00000100,       // 100%, general goods vendor
        UNIT_NPC_FLAG_VENDOR_FOOD = 0x00000200,       // 100%
        UNIT_NPC_FLAG_VENDOR_POISON = 0x00000400,       // guessed
        UNIT_NPC_FLAG_VENDOR_REAGENT = 0x00000800,       // 100%
        UNIT_NPC_FLAG_REPAIR = 0x00001000,       // 100%
        UNIT_NPC_FLAG_FLIGHTMASTER = 0x00002000,       // 100%
        UNIT_NPC_FLAG_SPIRITHEALER = 0x00004000,       // guessed
        UNIT_NPC_FLAG_SPIRITGUIDE = 0x00008000,       // guessed
        UNIT_NPC_FLAG_INNKEEPER = 0x00010000,       // 100%
        UNIT_NPC_FLAG_BANKER = 0x00020000,       // 100%
        UNIT_NPC_FLAG_PETITIONER = 0x00040000,       // 100% 0xC0000 = guild petitions, 0x40000 = arena team petitions
        UNIT_NPC_FLAG_TABARDDESIGNER = 0x00080000,       // 100%
        UNIT_NPC_FLAG_BATTLEMASTER = 0x00100000,       // 100%
        UNIT_NPC_FLAG_AUCTIONEER = 0x00200000,       // 100%
        UNIT_NPC_FLAG_STABLEMASTER = 0x00400000,       // 100%
        UNIT_NPC_FLAG_GUILD_BANKER = 0x00800000,       // cause client to send 997 opcode
        UNIT_NPC_FLAG_SPELLCLICK = 0x01000000,       // cause client to send 1015 opcode (spell click)
        UNIT_NPC_FLAG_PLAYER_VEHICLE = 0x02000000,       // players with mounts that have vehicle data should have it set
        UNIT_NPC_FLAG_MAILBOX = 0x04000000,       // mailbox
        UNIT_NPC_FLAG_REFORGER = 0x08000000,       // reforging
        UNIT_NPC_FLAG_TRANSMOGRIFIER = 0x10000000,       // transmogrification
        UNIT_NPC_FLAG_VAULTKEEPER = 0x20000000        // void storage
    }

    public enum YesNo
    {
        NO,
        YES,
    }

#pragma warning restore 1591

    /// <summary>
    /// Utility class.
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// Tries to find a logical parent with the given type.
        /// </summary>
        /// <typeparam name="T">Type of the desired logical parent.</typeparam>
        /// <param name="source">Object which parent is going to be searched.</param>
        /// <returns>Returns the parent with type T or null if it can't find a parent with the given type.</returns>
        public static T GetParentWithType<T>(object source)
            where T : class
        {
            DependencyObject parent = (source as FrameworkElement).Parent;
            while (!(parent is T) && parent != null)
            {
                parent = LogicalTreeHelper.GetParent(parent);
            }

            return parent as T;
        }

        /// <summary>
        /// Tries to find a visual child with the given type.
        /// </summary>
        /// <typeparam name="T">Type of the desired child.</typeparam>
        /// <param name="depObj">Object which child is going to be searched.</param>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
