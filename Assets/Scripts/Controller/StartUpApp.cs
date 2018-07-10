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

		private UserList userListObj = null;

		public override void Execute(INotification notification){
			userListObj = notification.Body as UserList;
			if (userListObj != null) {
				SendNotification(ProConsts.MSG_Not_InitUserLsitMediator,userListObj);
			}
		}
	}
}