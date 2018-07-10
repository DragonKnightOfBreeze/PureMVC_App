/***
 * 标题：
 * UI窗体的父类
 * 
 * 功能：
 * 定义四个生命周期
 * Display：显示状态（可以加载文字）
 * Hide：隐藏状态
 * ReDisplay：再显示状态
 * Freeze：冻结状态
 *
 * 用法：
 * （在子类中调用）
 * RigisterButtonObjectEvent(buttonName,delVoidDelegate); //注册按钮点击事件
 * OpenUIForm(uiFormName);		//打开窗体
 * CloseUIForm();				//关闭窗体
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
using SUIFW;
using UnityEngine;
using UnityEngine.UI;

//using Kernel;
//using Global;

namespace SUIFW {
	///<summary>
	///脚本：各种UI窗体的父类
	///</summary>
	public class BaseUIForm : MonoBehaviour {

		/* 字段 */

		private UIType _CurrentUIType = new UIType();

		/* 属性*/

		/// <summary>
		/// 属性：当前UI窗体类型
		/// </summary>
		public UIType CurrentUIType {
			get { return _CurrentUIType; }
			set { _CurrentUIType = value; }
		}



		#region 【窗体的四种状态】

		/// <summary>
		/// 虚公共方法：显示状态
		/// </summary>
		public virtual void Display() {
			this.gameObject.SetActive(true);
			//设置模态窗体调用，必须是弹出窗体
			if (_CurrentUIType.UIForms_Type == UIFormType.PopUp) {
				UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject,_CurrentUIType.UIForms_LucencyType);
			}
		}

		/// <summary>
		/// 虚公共方法：隐藏状态
		/// </summary>
		public virtual void Hiding() {
			this.gameObject.SetActive(false);
			//取消模态窗体调用，必须是弹出窗体
			if (_CurrentUIType.UIForms_Type == UIFormType.PopUp) {
				UIMaskMgr.GetInstance().CancelMaskWindow();
			}
		}

		/// <summary>
		/// 虚公共方法：重新显示状态
		/// </summary>
		public virtual void Redisplay() {
			this.gameObject.SetActive(true);
			//取消模态窗体调用，必须是弹出窗体
			if (_CurrentUIType.UIForms_Type == UIFormType.PopUp) {
				UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _CurrentUIType.UIForms_LucencyType);
			}
		}

		/// <summary>
		/// 虚公共方法：冻结状态
		/// </summary>
		public virtual void Freeze() {
			this.gameObject.SetActive(false);
		}

		#endregion


		#region 【封装子类常用的方法】

		/// <summary>
		/// 注册按钮事件
		/// （将方法的委托作为参数传给方法）
		/// </summary>
		/// <param name="buttonName">按钮节点的名称</param>
		/// <param name="delVoidDelegate">委托：需要注册的方法</param>
		protected void RigisterButtonObjectEvent(string buttonName, EventTriggerListener.VoidDelegate delVoidDelegate) {
			GameObject goButton = UnityHelper.FindChildNode(gameObject, buttonName).gameObject;
			//给按钮结点注册方法
			if (goButton != null) {
				EventTriggerListener.GetListener(goButton).onClick += delVoidDelegate;
			}
		}

		/// <summary>
		/// 打开UI窗体
		/// </summary>
		/// <param name="uiFormName"></param>
		protected void OpenUIForm(string uiFormName){
			UIManager.GetInstance().OpenUIForm(uiFormName);
		}

		/// <summary>
		/// 关闭UI窗体
		/// </summary>
		protected void CloseUIForm(){
			string strUIFormName = string.Empty;	//处理后的UIForm的名称
			int intPosition = -1;
			strUIFormName =  GetType().ToString();	//命名空间+类的名称
			intPosition = strUIFormName.IndexOf(".");
			if (intPosition != -1) {
				//去掉字符串中“.”之前的部分。
				strUIFormName = strUIFormName.Substring(intPosition + 1);
			}

			UIManager.GetInstance().CloseUIForm(strUIFormName);
		}

		/// <summary>
		/// 重构后的发送消息方法
		/// （相当于对于子类的公共接口）
		/// </summary>
		/// <param name="messageType">消息的类型</param>
		/// <param name="messageName">消息名称</param>
		/// <param name="content">消息类型</param>
		protected void SendMessage(string messageType, string messageName, object content){
			KeyValueUpdate kv = new KeyValueUpdate(messageName,content);
			MessageCenter.SendMessage(messageType,kv);
		}

		/// <summary>
		/// 重构后的接受消息方法
		/// （相当于对于子类的公共接口）
		/// </summary>
		/// <param name="messageType">消息的类型</param>
		/// <param name="handler"></param>
		protected void ReceiveMessage(string messageType,MessageCenter.Del_MessageDelivery handler){
			MessageCenter.AddMessageListener("Items",handler);
		}

		/// <summary>
		/// 重构后的显示文本方法（语言国际化）
		/// </summary>
		/// <param name="goText">游戏对象（文本）</param>
		/// <param name="id">文本的ID</param>
		protected void ShowText(Text goText,string id){
			if (goText) {
				goText.text = LanguageMgr.GetInstance().GetText(id);
			}
		}
		protected void ShowText(Button goButton, string id) {
			if (goButton && goButton.GetComponentInChildren<Text>() != null) {
				goButton.GetComponentInChildren<Text>().text =LanguageMgr.GetInstance().GetText(id);
			}
		}

		#endregion



	}
}