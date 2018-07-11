/***
 * 标题：
 * 控制层，启动且初始化用户列表Mediator
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
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

//using Kernel;
//using Global;

namespace PureMVCApp {
	/// <summary>
	/// 
	/// </summary>
	public class StartUpApp : SimpleCommand {

		private UserEmpInfo _UserEmpInfo = null;

		public override void Execute(INotification notification){
			_UserEmpInfo = notification.Body as UserEmpInfo;
			if (_UserEmpInfo == null)
				return;

			//初始化用户列表操作类
			SendNotification(ProConsts.MSG_Not_InitUserListMediator, _UserEmpInfo.UserListObj);
			//初始化用户窗体操作类
			SendNotification(ProConsts.MSG_Not_InitUserFormMediator, _UserEmpInfo.UserFormObj);
		}
	}
}