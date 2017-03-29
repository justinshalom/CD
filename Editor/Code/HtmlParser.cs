using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Editor.Code
{
    public class HtmlParser
    {
        public static NodesList parse(string content)
        {
            var nodeslist = new NodesList();

            var switchcase = "";
            var commentstarted = false;
            var tagstarted = false;
            var tagnamestarted = false;
            var scriptstarted = false;
            var stylestarted = false;
            var attributestarted = false;
            var uniquekey = 0;
            var currentparent = 0;
            Nodes node;
            for (int i = 0; i <= content.Length; i++)
            {
                node = new Nodes();
                char c = content[i];
                ////starting actions
                switch (c)
                {
                    case '<':
                    {
                        tagstarted = true;
                        switchcase = "";
                        break;
                    }
                    case '>':
                    {
                        tagstarted = false;

                        break;
                    }
                    case '"':
                    {
                        attributestarted = !attributestarted;

                        break;
                    }

                    case ' ':
                    {
                        if (tagstarted == true)
                        {
                                node.element = switchcase;
                                node.uniqueskey = uniquekey;
                                currentparent = uniquekey;
                                uniquekey++;
                                node.attributes = new List<Attributes>();
                                node.attributes.Add(new Attributes() { key = "html", value = switchcase });
                                nodeslist.nodes.Add(node);
                                switchcase = "";

                            }
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }

                switchcase = switchcase + c;
                if (switchcase.EndsWith($"-->"))
                {
                    commentstarted = false;
                    node.element = "comment";
                    node.uniqueskey = -1;
                    node.attributes = new List<Attributes>();
                    node.attributes.Add(new Attributes() { key = "html", value = switchcase });
                    nodeslist.nodes.Add(node);
                    switchcase = "";
                }
                if (switchcase != "")
                {
                    switch (switchcase)
                    {
                        case "<!--":
                        {
                            commentstarted = true;
                            tagstarted = false;
                            attributestarted = false;
                            break;
                        }
                        case "-->":
                        {
                            commentstarted = true;
                            break;
                        }
                        case "<!DOCTYPE":
                        {
                            node.element = switchcase;
                            node.uniqueskey = uniquekey;
                            currentparent = uniquekey;
                            uniquekey++;
                            nodeslist.nodes.Add(node);
                            break;
                        }
                        default:
                        {
                            break;
                        }
                    }
                }
                ////ending actions
                switch (c)
                {
                    case '<':
                    {
                        break;
                    }
                    case '>':
                    {
                        switchcase = "";
                        break;
                    }
                    case ' ':
                    {
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
            }

            return nodeslist;
        }
    }

    public class NodesList
    {
        public List<Nodes> nodes;
    }

    public class Nodes
    {
        public string element;
        public List<Attributes> attributes;
        public List<Nodes> childnodes;
        public int uniqueskey;
    }

    public class Attributes
    {
        public string key;
        public string value;
    }
}