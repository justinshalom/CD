using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Editor.Code
{
    public class MyHtmlParser
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
            var allowtostartattribute = false;
            var uniquekey = 0;
            var attributekey = "";
            var attributevalue = "";
            var currentparent = 0;
            var razorcodestarted = false;
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
                        if (razorcodestarted = true)
                        {
                            
                        }
                        razorcodestarted = false;
                        switchcase = "";
                        break;
                    }
                    case '@':
                    {
                        razorcodestarted = true;
                      //  switchcase = "";
                        break;
                    }
                    case '>':
                    {
                        allowtostartattribute = false;
                        tagstarted = false;

                        break;
                    }
                    case '"':
                    {
                        if (allowtostartattribute)
                        {
                            attributestarted = !attributestarted;
                            if (!attributestarted)
                            {
                            }
                            else
                            {
                                attributekey = switchcase;
                            }
                        }

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
                            nodeslist.nodes = new List<Nodes>();
                            nodeslist.nodes.Add(node);
                            //switchcase = "";    
                            allowtostartattribute = true;
                        }
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }

                switchcase = switchcase + c;
                if (switchcase.EndsWith("-->"))
                {
                    commentstarted = false;
                    node.element = "comment";
                    node.uniqueskey = -1;
                    node.attributes = new List<Attributes>();
                    node.attributes.Add(new Attributes() { key = "html", value = switchcase });
                    nodeslist.nodes = new List<Nodes>();
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
                    //case '\r':
                    //{
                    //    switchcase = "";
                    //    break;
                    //}
                    //case '\n':
                    //{
                    //    switchcase = "";
                    //    break;
                    //}
                    case '<':
                    {
                        break;
                    }
                    case '>':
                    {
                        try
                        {
                            var attrs = XElement.Parse(switchcase);
                            var elm = attrs.Elements();
                        }
                        catch(Exception e)
                        {
                            break;
                        }
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