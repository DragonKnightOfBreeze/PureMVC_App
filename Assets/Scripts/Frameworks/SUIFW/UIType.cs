/***
 * 标题：
 * 窗体类型
 * 
 * 功能：
 * 
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
using UnityEngine;

//using Kernel;
//using Global;

namespace SUIFW {
	///<summary>
	///类：各种窗体类型
	///</summary>
	public class UIType {

		/* 设置各种窗体类型的默认类型 */

		//是否清空栈集合
		public bool IsCleanStack = false;

		//UI窗体（位置类型）
		public UIFormType UIForms_Type = UIFormType.Normal;
		//UI窗体显示类型
		public UIFormShowType UIForms_ShowType = UIFormShowType.Normal;
		//UI窗体透明度类型
		public UIFormLucenyType UIForms_LucencyType = UIFormLucenyType.Lucency;

	}
}