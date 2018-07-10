/***
 * 标题：
 * 框架核心参数
 *
 * 功能：
 * 1.系统常量
 * 2.全局性方法
 * 3.系统枚举类型
 *
 * 思路：
 *
 *
 * 改进：
 *
 *
 */

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

//using Kernel;
//using Global;

namespace SUIFW {

	#region 【系统枚举类型】

	/// <summary>
	/// 枚举：UI窗体类型
	/// </summary>
	public enum UIFormType {
		/// <summary>
		/// 普通窗体
		/// </summary>
		Normal,
		/// <summary>
		/// 固定窗体
		/// </summary>
		Fixed,
		/// <summary>
		/// 弹出窗体
		/// </summary>
		PopUp
	}

	/// <summary>
	/// 枚举：UI窗体的显示类型
	/// </summary>
	public enum UIFormShowType {
		/// <summary>
		/// 普通
		/// </summary>
		Normal,
		/// <summary>
		/// 反向切换（模态，堆栈）（可以通过取消按钮退出）
		/// </summary>
		ReverseChange,
		/// <summary>
		/// 隐藏其他
		/// </summary>
		HideOther
	}

	/// <summary>
	/// 枚举：UI窗体的透明度类型
	/// </summary>xxc
	public enum UIFormLucenyType {
		/// <summary>
		/// 完全透明，不能穿透（模态）
		/// </summary>
		Lucency,
		/// <summary>
		/// 半透明，不能穿透（模态）
		/// </summary>
		Translucency,
		/// <summary>
		/// 低透明度，不能穿透（模态）
		/// </summary>
		Impenetrable,
		/// <summary>
		/// 可以穿透
		/// </summary>
		Pentrate
	}



	#endregion 【系统常量】

	public static class SysDefine {
		/* 路径常量 */
		/// <summary>
		/// 路径：画布预制体
		/// </summary>
		public const string PATH_Canvas = "Prefabs/Canvas";
		/// <summary>
		/// 路径：Json配置文件
		/// </summary>
		public const string PATH_UIFormsConfigInfo = "UIFormsConfig";
		/// <summary>
		/// 路径：Log系统配置文件
		/// </summary>
		public const string PATH_LogConfig = "LogConfig";
		/// <summary>
		/// 路径：Log系统配置文件（中文）
		/// </summary>
		public const string PATH_LanguageConfig_CN = "LanguageConfig_CN";
		/// <summary>
		/// 路径：Log系统配置文件（英文）
		/// </summary>
		public const string PATH_LanguageConfig_EN = "LanguageConfig_EN";

		/* 标签常量 */
		/// <summary>
		/// 标签：画布
		/// </summary>
		public const string TAG_Canvas = "_Canvas";
		/// <summary>
		/// 标签：UI摄像机
		/// </summary>
		public const string TAG_UICamera = "_UICamera";

		/* 游戏对象名称常量 */
		/// <summary>
		/// 名称：UI遮罩面板
		/// </summary>
		public const string GO_UIMaskPanel = "_UIMaskPanel";

		/* 节点常量 */
		public const string NODE_Normal = "Normal";
		public const string NODE_Fixed = "Fixed";
		public const string NODE_PopUp = "PopUp";
		public const string NODE_UIScripts = "_ScriptsMgr";

		/* 遮罩管理器中的透明度常量 */
		public const float UIMASK_LucencyColor_RGB = 255 / 255f;
		public const float UIMASK_LucencyColor_A = 0 / 255f;

		public const float UIMASK_TranslucencyColor_RGB = 220 / 255f;
		public const float UIMASK_TranslucencyColor_A = 50 / 255f;

		public const float UIMASK_ImpenetrableColor_RGB = 50 / 255f;
		public const float UIMASK_ImpenetrableColor_A = 200 / 255f;

		/* 其他常量 */
		/// <summary>
		/// UI摄像机增加的层深
		/// </summary>
		public const float ADD_UICameraDepth = 100;

		/* 全局性的方法*/
		//TODO

		/* 委托的定义 */
		//TODO

	}
}