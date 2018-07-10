/***
 * 标题：
 * UI遮罩面板管理器
 * 
 * 功能：
 * 
 * 
 * 思路：
 * 平时这个面板是不可见状态。
 * 当需要进行模态显示的时候，则定义脚本，控制起在PopUp节点下倒数第二的位置，起到遮挡作用。
 * 
 * 改进：
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using Kernel;
//using Global;

namespace SUIFW {
	///<summary>
	///脚本：UI遮罩面板管理器
	///</summary>
	public class UIMaskMgr : MonoBehaviour {

		/* 字段 */
		//本脚本私有单例
		private static UIMaskMgr _Instance;
		
		//UI根节点对象
		private GameObject _GoCanvasRoot;
		//UI脚本节点对象
		private Transform _TraUIScriptsNode;
		//顶层面板
		private GameObject _GoTopPanel;
		//遮罩面板
		private GameObject _GoUIMaskPanel;
		//UI摄像机
		private Camera _UICamera;
		//UI摄像机原始层深
		private float _OriginalUICameraDepth;


		private void Awake(){
			//得到UI根节点和UI脚本节点对象
			_GoCanvasRoot = GameObject.FindGameObjectWithTag(SysDefine.TAG_Canvas);
			_TraUIScriptsNode = UnityHelper.FindChildNode(_GoCanvasRoot, SysDefine.NODE_UIScripts);
			//把本脚本实例，作为“脚本节点对象”的子节点。
			UnityHelper.AddParentToChildNode(_TraUIScriptsNode,gameObject.transform);
			//得到顶层面板和遮罩面板
			_GoTopPanel = _GoCanvasRoot;
			_GoUIMaskPanel = UnityHelper.FindChildNode(_GoCanvasRoot, SysDefine.GO_UIMaskPanel).gameObject;
			//得到UI摄像机以及原始层深
			_UICamera = GameObject.FindGameObjectWithTag(SysDefine.TAG_UICamera).GetComponent<Camera>();
			if (_UICamera != null) {
				_OriginalUICameraDepth = _UICamera.depth;
			} else {
				Debug.LogWarning("UI摄像机为空！");
			}
		}


		/// <summary>
		/// 公共方法：得到实例
		/// </summary>
		/// <returns></returns>
		public static UIMaskMgr GetInstance(){
			if (_Instance == null) {
				_Instance = new GameObject("UIMaskMgr").AddComponent<UIMaskMgr>();
			}
			return _Instance;
		}

		/// <summary>
		/// 公共方法：设置遮罩状态
		/// </summary>
		/// <param name="goDisplayUIForm">需要显示的UI窗体</param>
		/// <param name="lucenyType">显示透明度属性</param>
		public void SetMaskWindow(GameObject goDisplayUIForm, UIFormLucenyType lucenyType= UIFormLucenyType.Pentrate){
			//顶层窗体下移
			_GoTopPanel.transform.SetAsLastSibling();
			//按照透明度类型，启用遮罩窗体，并设置透明度
			//TODO
			switch (lucenyType) {
				//TODO为什么不弄个颜色库？
				case UIFormLucenyType.Lucency:
					_GoUIMaskPanel.SetActive(true);
					_GoUIMaskPanel.GetComponent<Image>().color = new Color(
						SysDefine.UIMASK_LucencyColor_RGB, SysDefine.UIMASK_LucencyColor_RGB, SysDefine.UIMASK_LucencyColor_RGB, SysDefine.UIMASK_LucencyColor_A
					);
					break;
				case UIFormLucenyType.Translucency:
					_GoUIMaskPanel.SetActive(true);
					_GoUIMaskPanel.GetComponent<Image>().color = new Color(
						SysDefine.UIMASK_TranslucencyColor_RGB, SysDefine.UIMASK_TranslucencyColor_RGB, SysDefine.UIMASK_TranslucencyColor_RGB, SysDefine.UIMASK_TranslucencyColor_A
					);
					break;
				case UIFormLucenyType.Impenetrable:
					_GoUIMaskPanel.SetActive(true);
					_GoUIMaskPanel.GetComponent<Image>().color = new Color(
						SysDefine.UIMASK_ImpenetrableColor_RGB, SysDefine.UIMASK_ImpenetrableColor_RGB, SysDefine.UIMASK_ImpenetrableColor_RGB, SysDefine.UIMASK_ImpenetrableColor_A
					);
					break;
				case UIFormLucenyType.Pentrate:
					if (_GoUIMaskPanel.activeInHierarchy) {
						_GoUIMaskPanel.SetActive(false);
					}
					break;
			}

			//遮罩窗体下移
			_GoUIMaskPanel.transform.SetAsLastSibling();
			//显示窗体下移
			goDisplayUIForm.transform.SetAsLastSibling();
			//增加当前UI摄像机的层深，保证当前摄像机为最前显示
			if (_UICamera != null) {
				_UICamera.depth += SysDefine.ADD_UICameraDepth;
			}
		}

		/// <summary>
		/// 公共方法：取消遮罩状态
		/// </summary>
		public void CancelMaskWindow(){
			//顶层窗体上移
			_GoTopPanel.transform.SetAsFirstSibling();
			//禁用遮罩窗体（如果不是隐藏的）
			if (_GoUIMaskPanel.activeInHierarchy) {
				_GoUIMaskPanel.SetActive(false);
			}

			//回复当前UI摄像机的层深
			if (_UICamera != null) {
				_UICamera.depth = _OriginalUICameraDepth;
			}
		}
	}
}