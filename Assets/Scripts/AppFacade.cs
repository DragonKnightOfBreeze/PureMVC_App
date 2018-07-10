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
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using UnityEngine;

//using Kernel;
//using Global;

namespace PureMVCApp {
	/// <summary>
	/// 
	/// </summary>
	public class AppFacade : Facade {

		public AppFacade(){

		}

		/// <summary>
		/// PureMVC得到实例引用
		///
		/// 得到“单例”且“线程安全”的引用
		/// 在父类中已经实现了“单例”引用
		///
		/// 这种写法真的可以吗？
		/// </summary>
		//TODO：直接使用会报StackOverFlow错误
		public static IFacade Instance {
			get {
				if (Instance == null) {
					//静态对象可以作为一个锁
					lock (staticSyncRoot) {
						if (instance == null) {
							instance = new AppFacade();
						}
					}
				}
				return instance;
			}
		}

		

		/// <summary>
		/// 注册控制层实例
		/// </summary>
		protected override void InitializeController(){
			base.InitializeController();
			RegisterCommand(ProConsts.MSG_Cmd_InitMediator,()=>new StartUpApp());
		}

		/// <summary>
		/// 注册视图层实例
		/// </summary>
		protected override void InitializeView(){
			base.InitializeView();
			RegisterMediator(new UserListMediator());
		}

		/// <summary>
		/// 注册模型层实例
		/// </summary>
		protected override void InitializeModel(){
			base.InitializeModel();
			RegisterProxy(new UserProxy());
		}
	}
}