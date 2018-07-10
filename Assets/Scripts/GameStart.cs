/***
 * 标题：
 * 启动PureMVC框架，启动项目
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
	/// 游戏开始脚本
	/// </summary>
	public class GameStart : MonoBehaviour {

		public UserList UserListObj;

		void Start(){
			
			//启动框架
			AppFacade appFacade =  new AppFacade();
			//AppFacade appFacade = AppFacade.Instance as AppFacade;

			//启动项目（发送一个消息）
			if (appFacade != null && UserListObj != null) {
				appFacade.SendNotification(ProConsts.MSG_Cmd_InitMediator,UserListObj);
			}

		}
		


	}
}