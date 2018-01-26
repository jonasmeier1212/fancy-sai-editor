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
    class SqlExporter
    {
        public SqlExporter(List<Node> _nodeStore)
        {
            nodeStore = _nodeStore;
        }

        private List<Node> nodeStore;

        public void Export(string fileName)
        {
            string update = "";

            //First find the NPCs which should be affected
            List<Npc> npcNodes = new List<Npc>();
            foreach(Node node in nodeStore)
            {
                if (node.Type == NodeType.GENERAL_NPC && node is Npc npcNode && npcNode.GetDirectlyConnectedNodes(NodeType.EVENT).Count != 0)
                    npcNodes.Add(npcNode);
            }

            try
            {
                //Iterate over every Node with Type GENERAL_NPC and generate the SQL for each of these
                foreach (Npc npcNode in npcNodes)
                {
                    update += "SET @NPC := " + npcNode.GetParamValue() + ";" + Environment.NewLine;
                    //First generate update for creature_template
                    update += "UPDATE creature_template SET AIName='SmartAI' WHERE entry=@NPC;" + Environment.NewLine;
                    //DELETE and INSERT query for this NPC
                    update += "DELETE FROM smart_scripts WHERE entryorguid=@NPC AND source_type=0;" + Environment.NewLine;
                    update += "INSERT INTO `smart_scripts` (`entryorguid`,`source_type`,`id`,`link`,`event_type`,`event_phase_mask`,`event_chance`,`event_flags`,`event_param1`,`event_param2`,`event_param3`,`event_param4`,`action_type`,`action_param1`,`action_param2`,`action_param3`,`action_param4`,`action_param5`,`action_param6`,`target_type`,`target_param1`,`target_param2`,`target_param3`,`target_x`,`target_y`,`target_z`,`target_o`,`comment`) VALUES" + Environment.NewLine;

                    int id = 0;

                    List<Node> connectedEventNodes = npcNode.GetDirectlyConnectedNodes(NodeType.EVENT);
                    foreach (Node connectedEventNode in connectedEventNodes)
                    {
                        List<Node> connectedActionNodes = connectedEventNode.GetDirectlyConnectedNodes(NodeType.ACTION);
                        foreach (Node connectedActionNode in connectedActionNodes)
                        {
                            Node connectedTargetNode = connectedActionNode.GetDirectlyConnectedNode(NodeType.TARGET);
                            if (connectedTargetNode == null)
                                continue;

                            EventData eventData = new EventData();
                            ActionData actionData = new ActionData();
                            TargetData targetData = new TargetData();

                            if (connectedEventNode is Nodes.EventNodes.EventNode eventNode)
                                eventData = eventNode.ExportData();
                            if (connectedActionNode is Nodes.ActionNodes.ActionNode actionNode)
                                actionData = actionNode.ExportData();
                            if (connectedTargetNode is Nodes.TargetNodes.TargetNode targetNode)
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

                    update = update.Remove(update.Length - 3);
                    update += ";" + Environment.NewLine;

                    //Check if any indirectly connected general node has additional querys
                    foreach (GeneralNode generalNode in npcNode.GetConnectedNodes(NodeType.GENERAL))
                    {
                        if (generalNode.AdditionalQuery.Length > 0)
                        {
                            update += Environment.NewLine;
                            update += generalNode.AdditionalQuery;
                        }
                    }
                }

                try
                {
                    File.WriteAllText(fileName, update);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("File name or path is empty or invalid!");
                }
                catch(PathTooLongException)
                {
                    MessageBox.Show("File name or path is too long!");
                }
                catch (DirectoryNotFoundException)
                {
                    MessageBox.Show("The specified path is invalid!");
                }
                catch(UnauthorizedAccessException e)
                {
                    MessageBox.Show(e.Message);
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
