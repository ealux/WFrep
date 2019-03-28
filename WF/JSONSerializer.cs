using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WF
{
    public static class JSONSerializer
    {
        public static void Serialize(TreeView tree)
        {
            if (tree.Nodes[0].Nodes.Count == 0) return;

            List<Tuple<string, string>> t = new List<Tuple<string, string>>();

            foreach (TreeNode node in tree.Nodes[0].Nodes)
            {
                foreach (TreeNode subnode in tree.Nodes[0].Nodes[node.Index].Nodes)
                {
                    t.Add(Tuple.Create(subnode.Parent.Name, subnode.Text));
                }
            }

            string s = JsonConvert.SerializeObject(t);
            File.WriteAllText(@"savedhierarchy.json", s);
        }

        public static void DeSerialize(ref TreeView tree)
        {
            FileInfo file = new FileInfo(@"savedhierarchy.json");
            if (!file.Exists) return;

            string path = "savedhierarchy.json";
            string s = File.ReadAllText(path);

            if (String.IsNullOrEmpty(s)) return;

            List<Tuple<string, string>> t = new List<Tuple<string, string>>();

            try
            {
                t = JsonConvert.DeserializeObject<List<Tuple<string, string>>>(s);
                foreach (var tup in t)
                {
                    if (!tree.Nodes[0].Nodes.ContainsKey(tup.Item1))
                    {
                        tree.Nodes[0].Nodes.Add(tup.Item1, tup.Item1);
                    }

                    tree.Nodes[0].Nodes[tup.Item1].Nodes.Add(tup.Item2);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
