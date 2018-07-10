/***
 * 标题：
 * 事件触发监听器
 *
 * 功能：
 * 实现对于任何游戏对象的事件监听处理。
 *
 * 用法：
 * //给任意一个UI对象的点击事件注册指定的方法。
 * if (goButton != null) {
 *		EventTriggerListener.GetListener(goButton).onClick += Method1;
 * }
 *
 */
using UnityEngine;
using UnityEngine.EventSystems;

namespace SUIFW {
	/// <summary>
	/// 类：事件触发监听器
	/// </summary>
	public class EventTriggerListener :EventTrigger {
		//定义空委托
		public delegate void VoidDelegate(GameObject go);

		//定义各种对象互动触发事件
		public VoidDelegate onClick;
		public VoidDelegate onDown;
		public VoidDelegate onEnter;
		public VoidDelegate onExit;
		public VoidDelegate onUp;
		public VoidDelegate onSelect;
		public VoidDelegate onUpdateSelect;


		/// <summary>
		/// 得到指定游戏对象的“监听器”组件
		/// </summary>
		/// <param name="go">监听的游戏对象</param>
		/// <returns>监听器</returns>
		public static EventTriggerListener GetListener(GameObject go) {
			EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
			if (listener==null) {
				listener = go.AddComponent<EventTriggerListener>();	
			}
			return listener;
		}


		/// <summary>
		/// 重载点击方法
		/// </summary>
		/// <param name="eventData"></param>
		public override void OnPointerClick(PointerEventData eventData) {
			if(onClick!=null) {
				onClick(gameObject);
			}
		}


		/// <summary>
		/// 重载按下方法
		/// </summary>
		/// <param name="eventData"></param>
		public override void OnPointerDown(PointerEventData eventData) {
			if (onDown != null) {
				onDown(gameObject);
			}
		}


		/// <summary>
		/// 重载鼠标进入方法
		/// </summary>
		/// <param name="eventData"></param>
		public override void OnPointerEnter(PointerEventData eventData) {
			if (onEnter != null) {
				onEnter(gameObject);
			}
		}


		/// <summary>
		/// 重载鼠标退出方法
		/// </summary>
		/// <param name="eventData"></param>
		public override void OnPointerExit(PointerEventData eventData) {
			if (onExit != null)	{
				onExit(gameObject);
			}
		}


		/// <summary>
		/// 重载鼠标按键抬起方法
		/// </summary>
		/// <param name="eventData"></param>
		public override void OnPointerUp(PointerEventData eventData) {
			if (onUp != null) {
				onUp(gameObject);
			}
		}
	

		/// <summary>
		/// 重载选择方法
		/// </summary>
		/// <param name="eventBaseData"></param>
		public override void OnSelect(BaseEventData eventBaseData) {
			if (onSelect != null) {
				onSelect(gameObject);
			}
		}

		/// <summary>
		/// 重载更新选择方法
		/// </summary>
		/// <param name="eventBaseData"></param>
		public override void OnUpdateSelected(BaseEventData eventBaseData) {
			if (onUpdateSelect != null) {
				onUpdateSelect(gameObject);
			}
		}

	}
}
