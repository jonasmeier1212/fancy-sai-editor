using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Windows;

namespace NodeAI
{
    public static class SAIImporter
    {
        public static async void ImportSAI(int entry)
        {
            DataTable data = await Database.SelectMysqlData("smart_scripts", "entryorguid", entry.ToString());
            if (data.Rows.Count == 0)
                return;

            Node saiOwnerNode = new Nodes.GeneralNodes.Npc();
            if (saiOwnerNode == null)
            {
                MessageBox.Show("This Npc is not a valid SAI owner!");
                return;
            }
            NodeManager.Instance.AddNode(saiOwnerNode);
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
                    if (type.GetCustomAttribute<NodeAttribute>() != null)
                    {
                        switch (Node.GetSuperiorType(type.GetCustomAttribute<NodeAttribute>().Type))
                        {
                            case NodeType.EVENT:
                                if (Node.GetRealId(type.GetCustomAttribute<NodeAttribute>().Type, NodeType.EVENT) == event_type)
                                    eventNode = Activator.CreateInstance(type) as Node;
                                break;
                            case NodeType.ACTION:
                                if (Node.GetRealId(type.GetCustomAttribute<NodeAttribute>().Type, NodeType.ACTION) == action_type)
                                    actionNode = Activator.CreateInstance(type) as Node;
                                break;
                            case NodeType.TARGET:
                                if (Node.GetRealId(type.GetCustomAttribute<NodeAttribute>().Type, NodeType.TARGET) == target_type)
                                    targetNode = Activator.CreateInstance(type) as Node;
                                break;
                        }
                    }
                }
                
                if(eventNode == null)
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
            }
        }
    }
}
