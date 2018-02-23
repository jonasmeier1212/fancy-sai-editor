using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Windows;

namespace FancySaiEditor
{
    public static class SAIImporter
    {
        public static async void ImportSAI(int entry)
        {
            DataTable data = await Database.SelectMysqlData("smart_scripts", "entryorguid", entry.ToString());
            if (data.Rows.Count == 0)
            {
                MessageBox.Show($"No SAI found for entry: {entry}");
                return;
            }

            Node saiOwnerNode = new Nodes.GeneralNodes.Npc();
            if (saiOwnerNode == null)
            {
                MessageBox.Show("This Npc is not a valid SAI owner!");
                return;
            }

            HashSet<Node> createdEventNodes = new HashSet<Node>();
            HashSet<Node> createdActionNodes = new HashSet<Node>();
            HashSet<Node> createdTargetNodes = new HashSet<Node>();

            try
            {
                NodeManager.Instance.AddNode(saiOwnerNode);
                (saiOwnerNode as Nodes.GeneralNodes.GeneralNode).SetParamValue(entry.ToString());
                foreach (DataRow row in data.Rows)
                {
                    Node eventNode = null;
                    Node actionNode = null;
                    Node targetNode = null;

                    int event_type = Convert.ToInt32(row["event_type"]);
                    int action_type = Convert.ToInt32(row["action_type"]);
                    int target_type = Convert.ToInt32(row["target_type"]);

                    //Search the nodes with these types
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.GetCustomAttribute<NodeAttribute>() is NodeAttribute attribute)
                        {
                            switch (Node.GetSuperiorType(attribute.Type))
                            {
                                case NodeType.EVENT:
                                    if (Node.GetRealId(attribute.Type, NodeType.EVENT) == event_type)
                                        eventNode = Activator.CreateInstance(type) as Node;
                                    break;
                                case NodeType.ACTION:
                                    if (Node.GetRealId(attribute.Type, NodeType.ACTION) == action_type)
                                        actionNode = Activator.CreateInstance(type) as Node;
                                    break;
                                case NodeType.TARGET:
                                    if (Node.GetRealId(attribute.Type, NodeType.TARGET) == target_type)
                                        targetNode = Activator.CreateInstance(type) as Node;
                                    break;
                            }
                        }
                    }

                    if (eventNode == null)
                    {
                        MessageBox.Show($"Event node for event type {event_type} can't be created!");
                        continue;
                    }
                    else if (actionNode == null)
                    {
                        MessageBox.Show($"Action node for action type {action_type} can't be created!");
                        continue;
                    }
                    else if (targetNode == null)
                    {
                        MessageBox.Show($"Target node for target type {target_type} can't be created!");
                        continue;
                    }

                    NodeManager.Instance.AddNode(eventNode, saiOwnerNode);
                    NodeManager.Instance.AddNode(actionNode, eventNode);
                    NodeManager.Instance.AddNode(targetNode, actionNode);

                    for (int i = 0; i < 4; ++i)
                        eventNode.SetParam((ParamId)i, Convert.ToInt32(row["event_param" + (i + 1).ToString()]).ToString());

                    for (int i = 0; i < 6; ++i)
                        actionNode.SetParam((ParamId)i, Convert.ToInt32(row["action_param" + (i + 1).ToString()]).ToString());

                    for (int i = 0; i < 7; ++i)
                    {
                        if (i < 3)
                            targetNode.SetParam((ParamId)i, Convert.ToInt32(row["target_param" + (i + 1).ToString()]).ToString());
                        else
                        {
                            switch (i)
                            {
                                case 3:
                                    targetNode.SetParam((ParamId)i, Convert.ToInt32(row["target_x"]).ToString());
                                    break;
                                case 4:
                                    targetNode.SetParam((ParamId)i, Convert.ToInt32(row["target_y"]).ToString());
                                    break;
                                case 5:
                                    targetNode.SetParam((ParamId)i, Convert.ToInt32(row["target_z"]).ToString());
                                    break;
                                case 6:
                                    targetNode.SetParam((ParamId)i, Convert.ToInt32(row["target_o"]).ToString());
                                    break;
                            }
                        }
                    }

                    
                    //Merge duplicated nodes
                    if (FindEqualNodeIn(createdEventNodes, eventNode) is Node existingEventNode)
                    {
                        NodeManager.Instance.RemoveNode(eventNode);
                        NodeManager.Instance.ConnectNodes(saiOwnerNode, existingEventNode);
                        NodeManager.Instance.ConnectNodes(existingEventNode, actionNode);
                        eventNode = existingEventNode;
                    }
                    else
                        createdEventNodes.Add(eventNode);

                    if (FindEqualNodeIn(createdActionNodes, actionNode) is Node existingActionNode)
                    {
                        NodeManager.Instance.RemoveNode(actionNode);
                        NodeManager.Instance.ConnectNodes(eventNode, existingActionNode);
                        NodeManager.Instance.ConnectNodes(existingActionNode, targetNode);
                        actionNode = existingActionNode;
                    }
                    else
                        createdActionNodes.Add(actionNode);

                    if (FindEqualNodeIn(createdTargetNodes, targetNode) is Node existingTargetNode)
                    {
                        NodeManager.Instance.RemoveNode(targetNode);
                        NodeManager.Instance.ConnectNodes(actionNode, existingTargetNode);
                    }
                    else
                        createdTargetNodes.Add(targetNode);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Invalid SAI!\nError: {e.Message} \nStack:\n {e.StackTrace}");
                return;
            }
        }

        private static Node FindEqualNodeIn(HashSet<Node> nodes, Node searchNode)
        {
            foreach (Node node in nodes)
            {
                if (searchNode.IsEqualTo(node))
                    return node;
            }
            return null;
        }
    }
}
