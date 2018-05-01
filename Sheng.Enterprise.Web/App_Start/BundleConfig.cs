using System;
using System.Web.Optimization;

namespace Sheng.Enterprise.Web
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js", new IItemTransform[0]));
			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(new string[]
			{
				"~/Scripts/validate/jquery.validate.js",
				"~/Scripts/validate/localization/messages_zh.js"
			}));
			bundles.Add(new ScriptBundle("~/bundles/layer").Include("~/Scripts/layer/layer.js", new IItemTransform[0]));
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*", new IItemTransform[0]));
			bundles.Add(new ScriptBundle("~/bundles/common").Include("~/Scripts/common.js", new IItemTransform[0]));
			bundles.Add(new ScriptBundle("~/bundles/ztree").Include("~/Scripts/ztree/jquery.ztree.core-3.5.min.js", new IItemTransform[0]));
			bundles.Add(new StyleBundle("~/Content/ztree").Include("~/Content/zTreeStyle/zTreeStyle.css", new IItemTransform[0]));
			bundles.Add(new StyleBundle("~/Content/css").Include(new string[]
			{
				"~/Content/Style.css",
				"~/Content/WeeklyReport.css"
			}));
		}
	}
}
