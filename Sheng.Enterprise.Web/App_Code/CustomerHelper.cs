using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.WebPages;
using ASP;
using Microsoft.CSharp.RuntimeBinder;

namespace Sheng.Enterprise.Web
{
    public class CustomerHelper
    {
        public static HelperResult OrganizationSelector(string domId, string id, string name, Guid domainId, string domainName, bool chooseButton = true)
        {
            return new HelperResult(delegate (TextWriter __razor_helper_writer)
            {
                HelperPage.WriteLiteralTo(__razor_helper_writer, "    <div");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " style=\"border:1px solid #BBBBBB;width:356px; height:32px; padding-left:6px;\"");
                HelperPage.WriteLiteralTo(__razor_helper_writer, ">\n        <input");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");
                HelperPage.WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create<string, int>(" id=\"", 245), Tuple.Create<string, int>("\"", 256), new AttributeValue[]
                {
                    Tuple.Create<Tuple<string, int>, Tuple<object, int>, bool>(Tuple.Create<string, int>("", 250), Tuple.Create<object, int>(domId, 250), false)
                });
                HelperPage.WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create<string, int>(" value=\"", 257), Tuple.Create<string, int>("\"", 268), new AttributeValue[]
                {
                    Tuple.Create<Tuple<string, int>, Tuple<object, int>, bool>(Tuple.Create<string, int>("", 265), Tuple.Create<object, int>(id, 265), false)
                });
                HelperPage.WriteLiteralTo(__razor_helper_writer, " />\n        <span");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " style=\"line-height:32px;\"");
                HelperPage.WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create<string, int>(" id=\"", 312), Tuple.Create<string, int>("\"", 328), new AttributeValue[]
                {
                    Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 317), Tuple.Create<string, int>("span_", 317), true),
                    Tuple.Create<Tuple<string, int>, Tuple<object, int>, bool>(Tuple.Create<string, int>("", 322), Tuple.Create<object, int>(domId, 322), false)
                });
                HelperPage.WriteLiteralTo(__razor_helper_writer, ">");
                HelperPage.WriteTo(__razor_helper_writer, name);
                HelperPage.WriteLiteralTo(__razor_helper_writer, "</span>\n");
                if (chooseButton)
                {
                    HelperPage.WriteLiteralTo(__razor_helper_writer, "            <div");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " style=\"float:right;height:30px;\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, ">\n                <table");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " height=\"32\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " border=\"0\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " cellpadding=\"0\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " cellspacing=\"0\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, ">\n                    <tr>\n                        <td");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " valign=\"middle\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, ">\n                            <input");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " type=\"button\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " style=\"margin-right:5px\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " value=\"选择\"");
                    HelperPage.WriteAttributeTo(__razor_helper_writer, "onclick", Tuple.Create<string, int>(" onclick=\"", 664), Tuple.Create<string, int>("\"", 702), new AttributeValue[]
                    {
                        Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 674), Tuple.Create<string, int>("chooseOrganization('", 674), true),
                        Tuple.Create<Tuple<string, int>, Tuple<object, int>, bool>(Tuple.Create<string, int>("", 694), Tuple.Create<object, int>(domId, 694), false),
                        Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 700), Tuple.Create<string, int>("')", 700), true)
                    });
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " />\n                        </td>\n                    </tr>\n                </table>\n            </div>\n");
                }
                HelperPage.WriteLiteralTo(__razor_helper_writer, "    </div>\n");
                HelperPage.WriteLiteralTo(__razor_helper_writer, "    <script>\n\n        var _organizationSelectorLayerIndex_");
                HelperPage.WriteTo(__razor_helper_writer, domId);
                HelperPage.WriteLiteralTo(__razor_helper_writer, ";\n\n        function getSelectedOrganization(domId) {\n            return {\n                Id: $(\"#\" + domId).val(),\n                Name: $(\"#span_\" + domId).html()\n            };\n        }\n\n        function setSelectedOrganization(domId, id, name) {\n            $(\"#\" + domId).val(id);\n            $(\"#span_\" + domId).html(name);\n\n            $(\"#\" + domId).change();\n        }\n\n        function chooseOrganization(domId) {\n            _organizationSelectorLayerIndex_");
                HelperPage.WriteTo(__razor_helper_writer, domId);
                HelperPage.WriteLiteralTo(__razor_helper_writer, " = layer.open({\n                type: 2,\n                area: ['500px', '430px'], //宽高\n                closeBtn: false,\n                title: \"选择\",\n                shift: _layerShift,\n                content: '/Unity/OrganizationSelector?domId=' + domId\n            });\n        }\n\n        function selectOrganization(domId, id, name) {\n            $(\"#span_\" + domId).html(name);\n            $(\"#\" + domId).val(id);\n\n            layer.close(_organizationSelectorLayerIndex_");
                HelperPage.WriteTo(__razor_helper_writer, domId);
                HelperPage.WriteLiteralTo(__razor_helper_writer, ");\n            //layer.closeAll();\n\n            $(\"#\" + domId).change();\n        }\n\n\n    </script>\n");
            });
        }


        public static HelperResult PersonSelector(string domId, string id, string name, Guid domainId, string domainName, bool chooseButton = true)
        {
            return new HelperResult(delegate (TextWriter __razor_helper_writer)
            {
                HelperPage.WriteLiteralTo(__razor_helper_writer, "    <div");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " style=\"border:1px solid #BBBBBB;width:200px; height:32px; padding-left:6px;\"");
                HelperPage.WriteLiteralTo(__razor_helper_writer, ">\n        <input");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");
                HelperPage.WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create<string, int>(" id=\"", 2191), Tuple.Create<string, int>("\"", 2202), new AttributeValue[]
                {
                    Tuple.Create<Tuple<string, int>, Tuple<object, int>, bool>(Tuple.Create<string, int>("", 2196), Tuple.Create<object, int>(domId, 2196), false)
                });
                HelperPage.WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create<string, int>(" value=\"", 2203), Tuple.Create<string, int>("\"", 2214), new AttributeValue[]
                {
                    Tuple.Create<Tuple<string, int>, Tuple<object, int>, bool>(Tuple.Create<string, int>("", 2211), Tuple.Create<object, int>(id, 2211), false)
                });
                HelperPage.WriteLiteralTo(__razor_helper_writer, " />\n        <span");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " style=\"line-height:32px;\"");
                HelperPage.WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create<string, int>(" id=\"", 2258), Tuple.Create<string, int>("\"", 2274), new AttributeValue[]
                {
                    Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 2263), Tuple.Create<string, int>("span_", 2263), true),
                    Tuple.Create<Tuple<string, int>, Tuple<object, int>, bool>(Tuple.Create<string, int>("", 2268), Tuple.Create<object, int>(domId, 2268), false)
                });
                HelperPage.WriteLiteralTo(__razor_helper_writer, ">");
                HelperPage.WriteTo(__razor_helper_writer, name);
                HelperPage.WriteLiteralTo(__razor_helper_writer, "</span>\n");
                if (chooseButton)
                {
                    HelperPage.WriteLiteralTo(__razor_helper_writer, "            <div");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " style=\"float:right;height:30px;\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, ">\n                <table");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " height=\"32\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " border=\"0\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " cellpadding=\"0\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " cellspacing=\"0\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, ">\n                    <tr>\n                        <td");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " valign=\"middle\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, ">\n                            <input");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " type=\"button\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " style=\"margin-right:5px\"");
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " value=\"选择\"");
                    HelperPage.WriteAttributeTo(__razor_helper_writer, "onclick", Tuple.Create<string, int>(" onclick=\"", 2610), Tuple.Create<string, int>("\"", 2642), new AttributeValue[]
                    {
                        Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 2620), Tuple.Create<string, int>("choosePerson('", 2620), true),
                        Tuple.Create<Tuple<string, int>, Tuple<object, int>, bool>(Tuple.Create<string, int>("", 2634), Tuple.Create<object, int>(domId, 2634), false),
                        Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 2640), Tuple.Create<string, int>("')", 2640), true)
                    });
                    HelperPage.WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create<string, int>(" id=\"", 2643), Tuple.Create<string, int>("\"", 2667), new AttributeValue[]
                    {
                        Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 2648), Tuple.Create<string, int>("selectButton_", 2648), true),
                        Tuple.Create<Tuple<string, int>, Tuple<object, int>, bool>(Tuple.Create<string, int>("", 2661), Tuple.Create<object, int>(domId, 2661), false)
                    });
                    HelperPage.WriteLiteralTo(__razor_helper_writer, " />\n                        </td>\n                    </tr>\n                </table>\n            </div>\n");
                }
                HelperPage.WriteLiteralTo(__razor_helper_writer, "    </div>\n");
                HelperPage.WriteLiteralTo(__razor_helper_writer, "    <script>\n\n        var _personSelectorLayerIndex_");
                HelperPage.WriteTo(__razor_helper_writer, domId);
                HelperPage.WriteLiteralTo(__razor_helper_writer, ";\n\n        function setPersonSelectorDisabled(domId)\n        {\n            $(\"#selectButton_\" + domId).hide();\n        }\n\n        function setPersonSelectorEnabled(domId)\n        {\n            $(\"#selectButton_\" + domId).show();\n        }\n\n        function getSelectedPerson(domId)\n        {\n            return {\n                Id: $(\"#\" + domId).val(),\n                Name: $(\"#span_\" + domId).html()\n            };\n        }\n\n        function setSelectedPerson(domId, id, name)\n        {\n            $(\"#\" + domId).val(id);\n            $(\"#span_\" + domId).html(name);\n\n            $(\"#\" + domId).change();\n        }\n\n        function choosePerson(domId)\n        {\n            _personSelectorLayerIndex_");
                HelperPage.WriteTo(__razor_helper_writer, domId);
                HelperPage.WriteLiteralTo(__razor_helper_writer, " = layer.open({\n                type: 2,\n                area: ['740px', '530px'], //宽高\n                closeBtn: false,\n                title: \"选择\",\n                shift: _layerShift,\n                content: '/Unity/PersonSelector?domId=' + domId\n            });\n        }\n\n\n        function selectPerson(domId, id, name)\n        {\n            $(\"#span_\" + domId).html(name);\n            $(\"#\" + domId).val(id);\n\n            layer.close(_personSelectorLayerIndex_");
                HelperPage.WriteTo(__razor_helper_writer, domId);
                HelperPage.WriteLiteralTo(__razor_helper_writer, ");\n            // layer.closeAll();\n\n            $(\"#\" + domId).change();\n        }\n\n    </script>\n");
            });
        }

        public static HelperResult TitleWithDate(string title, dynamic ViewBag)
        {
            return new HelperResult(delegate (TextWriter __razor_helper_writer)
            {
                HelperPage.WriteLiteralTo(__razor_helper_writer, "    <div");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " id=\"divContent\"");
                HelperPage.WriteLiteralTo(__razor_helper_writer, ">\n        <div");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " id=\"divContainerTitle\"");
                HelperPage.WriteLiteralTo(__razor_helper_writer, ">\n            <div");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " style=\" float:left;font-size:26px;\"");
                HelperPage.WriteLiteralTo(__razor_helper_writer, ">\n");
                HelperPage.WriteLiteralTo(__razor_helper_writer, "                ");
                HelperPage.WriteTo(__razor_helper_writer, title);
                HelperPage.WriteLiteralTo(__razor_helper_writer, "\n            </div>\n            <div");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " style=\" float:right\"");
                HelperPage.WriteLiteralTo(__razor_helper_writer, ">\n                <div>\n                    <span");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " class=\"font_blue_18\"");
                HelperPage.WriteLiteralTo(__razor_helper_writer, ">第 ");
                HelperPage.WriteLiteralTo(__razor_helper_writer,ViewBag.CurrentWeekOfYear);
                HelperPage.WriteLiteralTo(__razor_helper_writer, " 周</span>\n                </div>\n                <div>\n                    <span");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " class=\"font_blue_18\"");
                HelperPage.WriteLiteralTo(__razor_helper_writer, ">");
                HelperPage.WriteTo(__razor_helper_writer, DateTime.Now.ToString("yyyy年M月d日"));
                HelperPage.WriteLiteralTo(__razor_helper_writer, "</span>\n                    <span");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " class=\"font_blue_18\"");
                HelperPage.WriteLiteralTo(__razor_helper_writer, ">");
                HelperPage.WriteTo(__razor_helper_writer, DateTime.Now.ToString("dddd", new CultureInfo("zh-cn")));
                HelperPage.WriteLiteralTo(__razor_helper_writer, "</span>\n                </div>\n            </div>\n            <div");
                HelperPage.WriteLiteralTo(__razor_helper_writer, " style=\"clear:both\"");
                HelperPage.WriteLiteralTo(__razor_helper_writer, "></div>\n        </div>\n    </div>\n");
            });
        }
    }
}