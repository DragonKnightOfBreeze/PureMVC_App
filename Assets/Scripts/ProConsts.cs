/***
 * 标题：
 * 
 * 
 * 功能：
 * 
 * 
 * 用法：
 * 
 * 
 * 思路：
 * 
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Kernel;
//using Global;

namespace PureMVCApp {
	/// <summary>
	/// 项目中所有的常量
	/// </summary>
	public  static class ProConsts {
		/* 命令消息 */
		public const string MSG_Cmd_InitMediator = "Cmd_InitMediator";

		/* 通知消息 */

		/// <summary>
		/// 通知消息：初始化成员列表信息Mediator
		/// </summary>
		public const string MSG_Not_InitUserListMediator = "Not_InitUserListMediator";
		/// <summary>
		/// 通知消息：初始化成员表单Mediator
		/// </summary>
		public const string MSG_Not_InitUserFormMediator = "Not_InitUserFormMediator";
		/// <summary>
		/// 通知消息：处理用户选择的数据信息
		/// </summary>
		public const string MSG_Not_SendCurUserInfoToMediator = "Not_SendCurUserInfoToMediator";

		public const string MSG_Not_SendCurUserInfoToUserForm = "Not_SendCurUserInfoTOUserForm";

		/// <summary>
		/// 通知消息：增加用户信息
		/// </summary>
		public const string MSG_Not_AddUserInfo = "Not_AddUserInfo";
		/// <summary>
		/// 通知消息：清除用户信息
		/// </summary>
		public const string MSG_Not_ClearUserInfo = "Not_ClearUserInfo";

		public const string MSG_Not_AddUserInfoToList = "Not_AddUserInfoToList";
		public const string MSG_Not_UpdateUserInfoToList = "Not_UpdateUserInfoToList";




	}
}