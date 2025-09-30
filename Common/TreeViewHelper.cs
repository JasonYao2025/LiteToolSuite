using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiteToolSuite.Common
{
    public class TreeViewHelper
    {
        // 复制当前选中节点及其所有子节点    
        public static string CopyNodeWithChildren(TreeNode node)
        {
            if (node == null) return string.Empty;
            StringBuilder sb = new StringBuilder();
            BuildNodeText(node, sb, 0);
            return sb.ToString();
        }
        // 递归构建节点文本    
        private static void BuildNodeText(TreeNode node, StringBuilder sb, int level)
        {
            sb.Append(' ', level * 2)
            .Append(node.Text)
            .Append(Environment.NewLine);
            foreach (TreeNode child in node.Nodes)
            {
                BuildNodeText(child, sb, level + 1);
            }
        }
        // 复制到剪贴板（带格式提示）    
        public static void CopyToClipboard(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                Clipboard.SetText(content);
                MessageBox.Show($"已复制 {content.Length} 个字符到剪贴板",
                "复制成功",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }

       # region 构建TreeView
        /// <summary>
        /// 构建TreeView，传入JToken格式数据
        /// </summary>
        /// <param name="token"></param>
        /// <param name="nodes"></param>
        public static void BuildTreeNodes(JToken token, TreeNodeCollection nodes)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    var objNode = new TreeNode("{}");
                    nodes.Add(objNode);
                    foreach (var prop in token.Children<JProperty>())
                    {
                        var propNode = new TreeNode(prop.Name);
                        objNode.Nodes.Add(propNode);
                        BuildTreeNodes(prop.Value, propNode.Nodes);
                        UpdateNodeText(propNode);
                    }
                    UpdateNodeText(objNode);
                    break;
                case JTokenType.Array:
                    var arrNode = new TreeNode("[]");
                    nodes.Add(arrNode);
                    int index = 0;
                    foreach (var item in token.Children())
                    {
                        var itemNode = new TreeNode($"[{index}]");
                        arrNode.Nodes.Add(itemNode);
                        BuildTreeNodes(item, itemNode.Nodes);
                        UpdateNodeText(itemNode);
                        index++;
                    }
                    UpdateNodeText(arrNode);
                    break;
                default:
                    nodes.Add(new TreeNode(token.ToString()));
                    break;
            }
        }
        public static void UpdateNodeText(TreeNode node)
        {
            if (node.Nodes.Count > 0)
            {
                node.Text = $"{node.Text.Split('(')[0]} ({node.Nodes.Count})";
                node.ToolTipText = $"包含 {node.Nodes.Count} 个子项";
            }
        }
        #endregion

        /// <summary>
        /// 递归搜索Treeview，并高亮搜索到的内容
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="searchText"></param>
        public static void SearchNodes(TreeNodeCollection nodes, string searchText)
        {
            //先复原颜色
            ResetTreeViewColor(nodes);

            if (string.IsNullOrEmpty(searchText)) return;

            //List<TreeNode> matchedNodes= new List<TreeNode>();

            //foreach (TreeNode node in nodes) 
            //{
            //    FindMatchedNodes(node, searchText, matchedNodes);
            //}         

            foreach(TreeNode node in nodes)
            {
                if (node.Text.ToLower().Contains(searchText.ToLower()))   //统一小写以后再判断
                {
                    node.ForeColor = Color.Blue;
                    TreeNode parent = node.Parent;
                    while (parent != null)
                    {
                        parent.Expand();          //展开父节点
                        parent = parent.Parent;
                    }
                }
                SearchNodes(node.Nodes, searchText);
            }
        }

        public static void ResetTreeViewColor(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.ForeColor = Color.Black;
                ResetTreeViewColor(node.Nodes);
            }
        }


        public static void FindMatchedNodes(TreeNode currentNode, string searchText, List<TreeNode> matchedNodes)
        {
            if (currentNode.Text.Contains(searchText))
            {
                matchedNodes.Add(currentNode);
            }

            foreach (TreeNode childNode in currentNode.Nodes)
            {
                FindMatchedNodes(childNode, searchText, matchedNodes);
            }
        }

        /// <summary>
        /// Collapse Node
        /// </summary>
        /// <param name="node"></param>
       
        public static void SafeCollapse(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag?.ToString() != "locked")
                {
                    node.Collapse();
                    SafeCollapse(node.Nodes);
                }
            }
        }
    }
}
