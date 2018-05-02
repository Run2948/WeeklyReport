using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.WebPages;


namespace Sheng.Enterprise.Web
{
    public class CustomerHelpers
    {
        /// <summary>
        /// 组织选择器
        /// </summary>
        /// <param name="domId"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="domainId"></param>
        /// <param name="domainName"></param>
        /// <param name="chooseButton"></param>
        /// <returns></returns>
        public static HelperResult OrganizationSelector(string domId, string id, string name, Guid domainId, string domainName, bool chooseButton = true)
        {
            return new HelperResult(x =>
            {
                HelperPage.WriteLiteralTo(x, "    <div");
                HelperPage.WriteLiteralTo(x, " style=\"border:1px solid #BBBBBB;width:356px; height:32px; padding-left:6px;\"");
                HelperPage.WriteLiteralTo(x, ">\n        <input");
                HelperPage.WriteLiteralTo(x, " type=\"hidden\"");
                HelperPage.WriteAttributeTo(x, "id", Tuple.Create<string, int>(" id=\"", 245), Tuple.Create<string, int>("\"", 256), new AttributeValue[]
                {
                Tuple.Create(Tuple.Create<string, int>("", 250), Tuple.Create<object, int>(domId, 250), false)
                });
                HelperPage.WriteAttributeTo(x, "value", Tuple.Create<string, int>(" value=\"", 257), Tuple.Create<string, int>("\"", 268), new AttributeValue[]
                {
                Tuple.Create(Tuple.Create<string, int>("", 265), Tuple.Create<object, int>(id, 265), false)
                });
                HelperPage.WriteLiteralTo(x, " />\n        <span");
                HelperPage.WriteLiteralTo(x, " style=\"line-height:32px;\"");
                HelperPage.WriteAttributeTo(x, "id", Tuple.Create<string, int>(" id=\"", 312), Tuple.Create<string, int>("\"", 328), new AttributeValue[]
                {
                Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 317), Tuple.Create<string, int>("span_", 317), true),
                Tuple.Create(Tuple.Create<string, int>("", 322), Tuple.Create<object, int>(domId, 322), false)
                });
                HelperPage.WriteLiteralTo(x, ">");
                HelperPage.WriteTo(x, name);
                HelperPage.WriteLiteralTo(x, "</span>\n");
                if (chooseButton)
                {
                    HelperPage.WriteLiteralTo(x, "            <div");
                    HelperPage.WriteLiteralTo(x, " style=\"float:right;height:30px;\"");
                    HelperPage.WriteLiteralTo(x, ">\n                <table");
                    HelperPage.WriteLiteralTo(x, " height=\"32\"");
                    HelperPage.WriteLiteralTo(x, " border=\"0\"");
                    HelperPage.WriteLiteralTo(x, " cellpadding=\"0\"");
                    HelperPage.WriteLiteralTo(x, " cellspacing=\"0\"");
                    HelperPage.WriteLiteralTo(x, ">\n                    <tr>\n                        <td");
                    HelperPage.WriteLiteralTo(x, " valign=\"middle\"");
                    HelperPage.WriteLiteralTo(x, ">\n                            <input");
                    HelperPage.WriteLiteralTo(x, " type=\"button\"");
                    HelperPage.WriteLiteralTo(x, " style=\"margin-right:5px\"");
                    HelperPage.WriteLiteralTo(x, " value=\"选择\"");
                    HelperPage.WriteAttributeTo(x, "onclick", Tuple.Create<string, int>(" onclick=\"", 664), Tuple.Create<string, int>("\"", 702), new AttributeValue[]
                    {
                    Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 674), Tuple.Create<string, int>("chooseOrganization('", 674), true),
                    Tuple.Create(Tuple.Create<string, int>("", 694), Tuple.Create<object, int>(domId, 694), false),
                    Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 700), Tuple.Create<string, int>("')", 700), true)
                    });
                    HelperPage.WriteLiteralTo(x, " />\n                        </td>\n                    </tr>\n                </table>\n            </div>\n");
                }
                HelperPage.WriteLiteralTo(x, "    </div>\n");
                HelperPage.WriteLiteralTo(x, "    <script>\n\n        var _organizationSelectorLayerIndex_");
                HelperPage.WriteTo(x, domId);
                HelperPage.WriteLiteralTo(x, ";\n\n        function getSelectedOrganization(domId) {\n            return {\n                Id: $(\"#\" + domId).val(),\n                Name: $(\"#span_\" + domId).html()\n            };\n        }\n\n        function setSelectedOrganization(domId, id, name) {\n            $(\"#\" + domId).val(id);\n            $(\"#span_\" + domId).html(name);\n\n            $(\"#\" + domId).change();\n        }\n\n        function chooseOrganization(domId) {\n            _organizationSelectorLayerIndex_");
                HelperPage.WriteTo(x, domId);
                HelperPage.WriteLiteralTo(x, " = layer.open({\n                type: 2,\n                area: ['500px', '430px'], //宽高\n                closeBtn: false,\n                title: \"选择\",\n                shift: _layerShift,\n                content: '/Unity/OrganizationSelector?domId=' + domId\n            });\n        }\n\n        function selectOrganization(domId, id, name) {\n            $(\"#span_\" + domId).html(name);\n            $(\"#\" + domId).val(id);\n\n            layer.close(_organizationSelectorLayerIndex_");
                HelperPage.WriteTo(x, domId);
                HelperPage.WriteLiteralTo(x, ");\n            //layer.closeAll();\n\n            $(\"#\" + domId).change();\n        }\n\n\n    </script>\n");
            });
        }
        /// <summary>
        /// 人员选择器
        /// </summary>
        /// <param name="domId"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="domainId"></param>
        /// <param name="domainName"></param>
        /// <param name="chooseButton"></param>
        /// <returns></returns>
        public static HelperResult PersonSelector(string domId, string id, string name, Guid domainId, string domainName, bool chooseButton = true)
        {
            return new HelperResult(x =>
            {
                HelperPage.WriteLiteralTo(x, "    <div");
                HelperPage.WriteLiteralTo(x, " style=\"border:1px solid #BBBBBB;width:200px; height:32px; padding-left:6px;\"");
                HelperPage.WriteLiteralTo(x, ">\n        <input");
                HelperPage.WriteLiteralTo(x, " type=\"hidden\"");
                HelperPage.WriteAttributeTo(x, "id", Tuple.Create<string, int>(" id=\"", 2191), Tuple.Create<string, int>("\"", 2202), new AttributeValue[]
                {
                Tuple.Create(Tuple.Create<string, int>("", 2196), Tuple.Create<object, int>(domId, 2196), false)
                });
                HelperPage.WriteAttributeTo(x, "value", Tuple.Create<string, int>(" value=\"", 2203), Tuple.Create<string, int>("\"", 2214), new AttributeValue[]
                {
                Tuple.Create(Tuple.Create<string, int>("", 2211), Tuple.Create<object, int>(id, 2211), false)
                });
                HelperPage.WriteLiteralTo(x, " />\n        <span");
                HelperPage.WriteLiteralTo(x, " style=\"line-height:32px;\"");
                HelperPage.WriteAttributeTo(x, "id", Tuple.Create<string, int>(" id=\"", 2258), Tuple.Create<string, int>("\"", 2274), new AttributeValue[]
                {
                Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 2263), Tuple.Create<string, int>("span_", 2263), true),
                Tuple.Create(Tuple.Create<string, int>("", 2268), Tuple.Create<object, int>(domId, 2268), false)
                });
                HelperPage.WriteLiteralTo(x, ">");
                HelperPage.WriteTo(x, name);
                HelperPage.WriteLiteralTo(x, "</span>\n");
                if (chooseButton)
                {
                    HelperPage.WriteLiteralTo(x, "            <div");
                    HelperPage.WriteLiteralTo(x, " style=\"float:right;height:30px;\"");
                    HelperPage.WriteLiteralTo(x, ">\n                <table");
                    HelperPage.WriteLiteralTo(x, " height=\"32\"");
                    HelperPage.WriteLiteralTo(x, " border=\"0\"");
                    HelperPage.WriteLiteralTo(x, " cellpadding=\"0\"");
                    HelperPage.WriteLiteralTo(x, " cellspacing=\"0\"");
                    HelperPage.WriteLiteralTo(x, ">\n                    <tr>\n                        <td");
                    HelperPage.WriteLiteralTo(x, " valign=\"middle\"");
                    HelperPage.WriteLiteralTo(x, ">\n                            <input");
                    HelperPage.WriteLiteralTo(x, " type=\"button\"");
                    HelperPage.WriteLiteralTo(x, " style=\"margin-right:5px\"");
                    HelperPage.WriteLiteralTo(x, " value=\"选择\"");
                    HelperPage.WriteAttributeTo(x, "onclick", Tuple.Create<string, int>(" onclick=\"", 2610), Tuple.Create<string, int>("\"", 2642), new AttributeValue[]
                    {
                    Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 2620), Tuple.Create<string, int>("choosePerson('", 2620), true),
                    Tuple.Create(Tuple.Create<string, int>("", 2634), Tuple.Create<object, int>(domId, 2634), false),
                    Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 2640), Tuple.Create<string, int>("')", 2640), true)
                    });
                    HelperPage.WriteAttributeTo(x, "id", Tuple.Create<string, int>(" id=\"", 2643), Tuple.Create<string, int>("\"", 2667), new AttributeValue[]
                    {
                    Tuple.Create<Tuple<string, int>, Tuple<string, int>, bool>(Tuple.Create<string, int>("", 2648), Tuple.Create<string, int>("selectButton_", 2648), true),
                    Tuple.Create(Tuple.Create<string, int>("", 2661), Tuple.Create<object, int>(domId, 2661), false)
                    });
                    HelperPage.WriteLiteralTo(x, " />\n                        </td>\n                    </tr>\n                </table>\n            </div>\n");
                }
                HelperPage.WriteLiteralTo(x, "    </div>\n");
                HelperPage.WriteLiteralTo(x, "    <script>\n\n        var _personSelectorLayerIndex_");
                HelperPage.WriteTo(x, domId);
                HelperPage.WriteLiteralTo(x, ";\n\n        function setPersonSelectorDisabled(domId)\n        {\n            $(\"#selectButton_\" + domId).hide();\n        }\n\n        function setPersonSelectorEnabled(domId)\n        {\n            $(\"#selectButton_\" + domId).show();\n        }\n\n        function getSelectedPerson(domId)\n        {\n            return {\n                Id: $(\"#\" + domId).val(),\n                Name: $(\"#span_\" + domId).html()\n            };\n        }\n\n        function setSelectedPerson(domId, id, name)\n        {\n            $(\"#\" + domId).val(id);\n            $(\"#span_\" + domId).html(name);\n\n            $(\"#\" + domId).change();\n        }\n\n        function choosePerson(domId)\n        {\n            _personSelectorLayerIndex_");
                HelperPage.WriteTo(x, domId);
                HelperPage.WriteLiteralTo(x, " = layer.open({\n                type: 2,\n                area: ['740px', '530px'], //宽高\n                closeBtn: false,\n                title: \"选择\",\n                shift: _layerShift,\n                content: '/Unity/PersonSelector?domId=' + domId\n            });\n        }\n\n\n        function selectPerson(domId, id, name)\n        {\n            $(\"#span_\" + domId).html(name);\n            $(\"#\" + domId).val(id);\n\n            layer.close(_personSelectorLayerIndex_");
                HelperPage.WriteTo(x, domId);
                HelperPage.WriteLiteralTo(x, ");\n            // layer.closeAll();\n\n            $(\"#\" + domId).change();\n        }\n\n    </script>\n");
            });
        }
        /// <summary>
        /// 日期标题
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="viewBag">ViewBag传递的值</param>
        /// <returns></returns>
        public static HelperResult TitleWithDate(string title, dynamic viewBag)
        {
            return new HelperResult(x =>
            {
                HelperPage.WriteLiteralTo(x, "    <div");
                HelperPage.WriteLiteralTo(x, " id=\"divContent\"");
                HelperPage.WriteLiteralTo(x, ">\n        <div");
                HelperPage.WriteLiteralTo(x, " id=\"divContainerTitle\"");
                HelperPage.WriteLiteralTo(x, ">\n            <div");
                HelperPage.WriteLiteralTo(x, " style=\" float:left;font-size:26px;\"");
                HelperPage.WriteLiteralTo(x, ">\n");
                HelperPage.WriteLiteralTo(x, "                ");
                HelperPage.WriteTo(x, title);
                HelperPage.WriteLiteralTo(x, "\n            </div>\n            <div");
                HelperPage.WriteLiteralTo(x, " style=\" float:right\"");
                HelperPage.WriteLiteralTo(x, ">\n                <div>\n                    <span");
                HelperPage.WriteLiteralTo(x, " class=\"font_blue_18\"");
                HelperPage.WriteLiteralTo(x, ">第 ");
                HelperPage.WriteTo(x, viewBag.CurrentWeekOfYear);
                HelperPage.WriteLiteralTo(x, " 周</span>\n                </div>\n                <div>\n                    <span");
                HelperPage.WriteLiteralTo(x, " class=\"font_blue_18\"");
                HelperPage.WriteLiteralTo(x, ">");
                HelperPage.WriteTo(x, DateTime.Now.ToString("yyyy年M月d日"));
                HelperPage.WriteLiteralTo(x, "</span>\n                    <span");
                HelperPage.WriteLiteralTo(x, " class=\"font_blue_18\"");
                HelperPage.WriteLiteralTo(x, ">");
                HelperPage.WriteTo(x, DateTime.Now.ToString("dddd", new CultureInfo("zh-cn")));
                HelperPage.WriteLiteralTo(x, "</span>\n                </div>\n            </div>\n            <div");
                HelperPage.WriteLiteralTo(x, " style=\"clear:both\"");
                HelperPage.WriteLiteralTo(x, "></div>\n        </div>\n    </div>\n");
            });
        }
    }
}