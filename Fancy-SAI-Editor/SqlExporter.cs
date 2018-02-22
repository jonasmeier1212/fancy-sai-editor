using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeAI.Nodes.GeneralNodes;
using System.Windows;
using System.IO;

namespace NodeAI
{
    static class SqlExporter
    {
        public static void ExportNodeTree(NodeTree _tree)
        {
            string update = "";

            //First find the NPCs which should be affected
            List<Npc> npcNodes = _tree.GetSAIOwnerNodes();
            
            try
            {
                foreach(Npc npcNode in npcNodes)
                {
                    update += "-- " + npcNode.GetNpcName() + " SAI" + Environment.NewLine;
                    update += "SET @NPC := " + npcNode.GetParamValue() + ";" + Environment.NewLine;
                    update += "UPDATE `creature_template` SET `AIName`='SmartAI' WHERE `entry`=@NPC;" + Environment.NewLine;
                    update += "DELETE FROM `smart_scripts` WHERE `entryorguid`=@NPC AND `source_type`=0;" + Environment.NewLine;
                    update += "INSERT INTO `smart_scripts` (`entryorguid`,`source_type`,`id`,`link`,`event_type`,`event_phase_mask`,`event_chance`,`event_flags`,`event_param1`,`event_param2`,`event_param3`,`event_param4`,`action_type`,`action_param1`,`action_param2`,`action_param3`,`action_param4`,`action_param5`,`action_param6`,`target_type`,`target_param1`,`target_param2`,`target_param3`,`target_x`,`target_y`,`target_z`,`target_o`,`comment`) VALUES" + Environment.NewLine;
                    int id = 0;
                    foreach (Nodes.EventNodes.EventNode eventNode in npcNode.GetDirectlyConnectedNodes(NodeType.EVENT, NodeConnectorType.OUTPUT))
                    {
                        foreach(Nodes.ActionNodes.ActionNode actionNode in eventNode.GetDirectlyConnectedNodes(NodeType.ACTION, NodeConnectorType.OUTPUT))
                        {
                            foreach(Nodes.TargetNodes.TargetNode targetNode in actionNode.GetDirectlyConnectedNodes(NodeType.TARGET, NodeConnectorType.OUTPUT))
                            {
                                EventData eventData = new EventData();
                                ActionData actionData = new ActionData();
                                TargetData targetData = new TargetData();

                                eventData = eventNode.ExportData();
                                actionData = actionNode.ExportData();
                                targetData = targetNode.ExportData();

                                #region Verify data
                                if (eventData == null)
                                {
                                    MessageBox.Show("Event data is invalid! Is there any valid event node?", "Export error");
                                    return;
                                }
                                if (actionData == null)
                                {
                                    MessageBox.Show("Action data is invalid! Is there any valid action node?", "Export error");
                                    return;
                                }
                                if (!eventData.VerifyData())
                                {
                                    MessageBox.Show("Event data is not valid!", "Export error");
                                }
                                if (!actionData.VerifyData())
                                {
                                    MessageBox.Show("Action data is not valid!", "Export error");
                                }
                                if (!targetData.VerifyData())
                                {
                                    MessageBox.Show("Target data is not valid!", "Export error");
                                }
                                #endregion

                                update += "(@NPC, 0, " + id.ToString() + ", 0, "
                                + eventData.type + ", " + eventData.phaseMask + ", " + eventData.chance + ", " + eventData.flags + ", " + eventData.param1 + ", " + eventData.param2 + ", " + eventData.param3 + ", " + eventData.param4
                                + ", " + actionData.type + ", " + actionData.param1 + ", " + actionData.param2 + ", " + actionData.param3 + ", " + actionData.param4 + ", " + actionData.param5 + ", " + actionData.param6
                                + ", " + targetData.targetType + ", " + targetData.targetParam1 + ", " + targetData.targetParam2 + ", " + targetData.targetParam3 + ", " + targetData.target_x + ", " + targetData.target_y + ", " + targetData.target_z + ", " + targetData.target_o + ", ''"
                                + ")," + Environment.NewLine;

                                ++id;
                            }
                        }
                    }

                    update = update.TrimEnd(Environment.NewLine.ToCharArray());
                    update = update.TrimEnd(',');
                    update += ";";
                    update += Environment.NewLine;
                }

                try
                {
                    new ShowSqlExport(update).ShowDialog();
                }
                catch(Exception e)
                {
                    MessageBox.Show("Unknow error while writing the file!\nError: " + e.Message);
                }
            }
            catch (ExportException e)
            {
                MessageBox.Show("Export error!\n" + e.Message);
                return;
            }
            catch (Exception e)
            {
                MessageBox.Show("Unknow error while exporting!\nError: " + e.Message);
            }
        }
    }

    public class ExportException : Exception
    {
        public ExportException() : base("Unknow error at exporting") { }

        public ExportException(string errorMessage) : base("Export Error:" + errorMessage) { }
    }
}
