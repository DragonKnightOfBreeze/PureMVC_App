/***
 * 标题：
 * 消息传递中心
 * 
 * 功能：
 * 负责UI框架中，所有UI窗体中间的数值的传值
 * （也可以用来传值角色属性，描述信息等）
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
	/// <summary>
	/// 类：消息传递中心
	/// </summary>
	public class MessageCenter {

		/// <summary>
		/// 委托：消息传递
		/// </summary>
		public delegate void Del_MessageDelivery(KeyValueUpdate kv);

		/// <summary>
		/// 消息中心缓存集合（字典）
		/// </summary>
		public static Dictionary<string ,Del_MessageDelivery> DicMessages = new Dictionary<string, Del_MessageDelivery>();



		#region 公共方法

		/// <summary>
		/// 公共方法：添加消息的监听
		/// </summary>
		/// <param name="messageType">消息分类</param>
		/// <param name="handler">消息的委托</param>
		public static void AddMessageListener(string messageType, Del_MessageDelivery handler) {
			if (!DicMessages.ContainsKey(messageType)) {
				DicMessages.Add(messageType,null);
			}
			DicMessages[messageType] += handler;
		}


		/// <summary>
		/// 公共方法：取消消息的监听
		/// </summary>
		/// <param name="messageType"></param>
		/// <param name="handler"></param>
		public static void SubMessageListener(string messageType, Del_MessageDelivery handler){
			if (DicMessages.ContainsKey(messageType)) {
				DicMessages[messageType] -= handler;
			}
		}

		/// <summary>
		/// 公共方法：取消所有消息的监听
		/// </summary>
		public static void CleanAllMessageListener(){
			if (DicMessages != null) {
				DicMessages.Clear();
			}
		}


		/// <summary>
		/// 公共方法：发送消息
		/// </summary>
		/// <param name="messageType">消息分类</param>
		/// <param name="kv">键值对对象 </param>
		public static void SendMessage(string messageType, KeyValueUpdate kv){
			Del_MessageDelivery del;
			if (DicMessages.TryGetValue(messageType, out del)) {
				if (del != null) {
					//调用委托
					del(kv);
				}
			}
		}

		#endregion

	}







	/// <summary>
	/// 键值对更新类
	/// 功能：配合委托，实现委托数据传递
	/// </summary>
	public class KeyValueUpdate {

		/* 只读属性 */
		public string Key { get; private set; }
		public object Value { get; private set; }

		public KeyValueUpdate(string key, object valueobj){
			Key = key;
			Value = valueobj;
		}
	}
}