using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

#pragma warning disable 1591

namespace NodeAI
{
    public class CreatureText
    {
        public int groudid;
        public int id;
        public string text;
        public TextType type;
        public int language;
        public int probability;
        public int emote;
        public int duration;
        public int sound;
        public int broadcastTextId;
        public int textRange;
        public string comment;
    }

    public class NodeData : DataTable
    {
        protected string selectTableName;
        protected bool sqlite = false;

        public string SelectTableName { get => selectTableName; }
        public bool Sqlite { get => sqlite; }
    }

    /*
     * 
     * 
     * 
     * IMPORTANT: The Names of the column names must match the names of the appropriate database column name
     * 
     * 
     * 
     */

    public class NpcData : NodeData
    {
        public NpcData()
        {
            Columns.Add("Entry").ReadOnly = true;
            Columns.Add("Name").ReadOnly = true;

            selectTableName = "creature_template";
        }
    }

    public class QuestData : NodeData
    {
        public QuestData()
        {
            Columns.Add("ID").ReadOnly = true;
            Columns.Add("Title").ReadOnly = true;

            selectTableName = "quest_template";
        }
    }

    public class TextData : NodeData
    {
        public TextData()
        {
            Columns.Add("GroupID").ReadOnly = true;
            Columns.Add("Text").ReadOnly = true;
            Columns.Add("Type").ReadOnly = true;

            selectTableName = "creature_text";
        }
    }

    public class BroadcastTextData : NodeData
    {
        public BroadcastTextData()
        {
            Columns.Add("ID").ReadOnly = true;
            Columns.Add("Language").ReadOnly = true;
            Columns.Add("MaleText").ReadOnly = true;
            Columns.Add("FemaleText").ReadOnly = true;

            selectTableName = "broadcast_text";
        }
    }

    public class SpellData : NodeData
    {
        public SpellData()
        {
            Columns.Add("ID").ReadOnly = true;
            Columns.Add("Name").ReadOnly = true;

            sqlite = true;

            selectTableName = "spells_wotlk";
        }
    }

    public class FactionData : NodeData
    {
        public FactionData()
        {
            Columns.Add("ID").ReadOnly = true;
            Columns.Add("Name").ReadOnly = true;

            sqlite = true;

            selectTableName = "factions_wotlk";
        }
    }

    public class EmoteData : NodeData
    {
        public EmoteData()
        {
            Columns.Add("ID").ReadOnly = true;
            Columns.Add("Name").ReadOnly = true;

            sqlite = true;

            selectTableName = "emotes_wotlk";
        }
    }

    public class SoundData : NodeData
    {
        public SoundData()
        {
            Columns.Add("ID").ReadOnly = true;
            Columns.Add("Name").ReadOnly = true;

            sqlite = true;

            selectTableName = "sound_entries_wotlk";
        }
    }

    public class ActionData
    {
        public string type = "0";
        public string param1 = "0";
        public string param2 = "0";
        public string param3 = "0";
        public string param4 = "0";
        public string param5 = "0";
        public string param6 = "0";

        /// <summary>
        /// Checks if the data is valid.
        /// </summary>
        /// <returns>Return true if the data is valid. Returns false if it's not.</returns>
        public bool VerifyData()
        {
            if (type == "0")
                return false;

            if (type == "" || param1 == "" || param2 == "" || param3 == "" || param4 == "" || param5 == "" || param6 == "")
                return false;

            return true;
        }
    }

    public class EventData
    {
        public string type = "0";
        public string param1 = "0";
        public string param2 = "0";
        public string param3 = "0";
        public string param4 = "0";

        public string chance = "100";
        public string flags = "0";
        public string phaseMask = "0";

        /// <summary>
        /// Checks if the data is valid.
        /// </summary>
        /// <returns>Return true if the data is valid. Returns false if it's not.</returns>
        public bool VerifyData()
        {
            if (type == "0")
                return false;

            if (param1 == "" || param2 == "" || param3 == "" || param4 == "" || type == "")
                return false;

            if (chance == "0")
                return false;

            return true;
        }
    }

    public class TargetData
    {
        public string targetType = "0";
        public string targetParam1 = "0";
        public string targetParam2 = "0";
        public string targetParam3 = "0";
        public string target_x = "0";
        public string target_y = "0";
        public string target_z = "0";
        public string target_o = "0";

        /// <summary>
        /// Checks if the data is valid.
        /// </summary>
        /// <returns>Return true if the data is valid. Returns false if it's not.</returns>
        public bool VerifyData()
        {
            if (targetType == "0")
                return false;

            if (targetType == "" || targetParam1 == "" || targetParam2 == "" || targetParam3 == "" || target_x == "" || target_y == "" || target_z == "" || target_o == "")
                return false;

            return true;
        }
    }

    public enum CreatureTextType
    {
        SAY             = 12,
        YELL            = 14,
        EMOTE           = 16,
        BOSS_EMOTE      = 41,
        WHISPER         = 15,
        BOSS_WHISPER    = 42,
    }
}

#pragma warning restore 1591
