
/***
 * 标题：
 * 视图层，用户列表信息操作类
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
using PureMVC.Patterns.Mediator;
using UnityEngine;

//using Kernel;
//using Global;

namespace PureMVCApp {
	/// <summary>
	/// 视图层，用户列表信息操作类
	/// </summary>
	public class UserListMediator : Mediator {

		//字段
		private new const string NAME = "UserListMediator";
		private UserProxy _UserProxy;

		//只读属性
		private UserList _UserListProp {
			//相当于模型层的data
			get { return base.ViewComponent as UserList; }	
		}

		/// <summary>
		/// 空的构造方法
		/// （让mediator一开始就可以注册，通过在其他类发送来初始化）
		/// </summary>
		public UserListMediator() : base(NAME){}



		/// <summary>
		/// 初始化用户列表数据
		/// </summary>
		/// <param name="userList"></param>
		internal void InitUserListMediator(UserList userListObj){
			if (userListObj == null)
				return;

			base.MediatorName = NAME;
			base.ViewComponent = userListObj;

			//委托注册
			userListObj.NewUser += HandleNewUser;
			userListObj.DeleteUser += HandleDeleteUser;
			//加载显示初始视图内容
			userListObj.LoadAndShowUserListInfo(_UserProxy.Users);
		}

		/// <summary>
		/// 本类实例在注册时触发
		/// </summary>
		public override void OnRegister(){
			//得到“用户代理”引用
			_UserProxy = Facade.RetrieveProxy(UserProxy.NAME) as UserProxy;
		}

		/// <summary>
		/// 列举出所有关注的消息
		/// </summary>
		/// <returns></returns>
		public override string[] ListNotificationInterests(){
			string[] strList = new string[] {
				ProConsts.MSG_Not_InitUserLsitMediator
			};
			return strList;
		}

		/// <summary>
		/// 处理所有关注的消息
		/// </summary>
		/// <param name="notification"></param>
		public override void HandleNotification(INotification notification){
			switch (notification.Name) {
				case ProConsts.MSG_Not_InitUserLsitMediator:
					//调用初始化方法
					UserList userListObj = notification.Body as UserList;
					InitUserListMediator(userListObj);
					break;
				default:
					break;
			}
		}


		#region 【私有方法】

		/// <summary>
		/// 处理新用户操作
		/// </summary>
		private void HandleNewUser(){
			//发送消息给“用户（窗体）表单”视图
			//TODO
		}

		/// <summary>
		/// 处理“删除用户”操作
		/// </summary>
		private void HandleDeleteUser(){
			//发送消息
			//TODO
		}

		#endregion




	}
}