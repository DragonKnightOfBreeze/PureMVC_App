//TODO：更新

/***
 * 标题：
 * UI管理器
 * 
 * 功能：
 * 是整个UI框架的核心，用户通过本脚本，来实现框架绝大多数的功能实现。
 * 不需要手动挂载
 *
 * 思路：
 * 
 * 
 * 改进：
 * 
 * 软件开发原则：
 * 1.“高内聚，低耦合”；
 * 2.方法的“单一职责原则”；
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

//using Kernel;
//using Global;

namespace SUIFW {
	///<summary>
	///脚本：UI管理器
	///</summary>
	public class UIManager : MonoBehaviour {

		/* 字段 */

		private static UIManager _Instance = null;
		// UI窗体预设路径（参数：窗体预设名称，窗体预设路径）
		private Dictionary<string, string> _DicFormsPaths;
		// 缓存所有UI窗体
		private Dictionary<string, BaseUIForm> _DicAllUIForms;
		// 当前显示的UI窗体
		private Dictionary<string, BaseUIForm> _DicCurrentUIForms;

		//定义栈集合，显示当前所有“反向切换”的窗体类型
		//管理“反向切换”类型的窗体
		private Stack<BaseUIForm> _StaCurrentUIForms;

		/* 定义节点*/

		//UI根节点
		private Transform _TraCanvasTransform;
		//全屏幕显示的节点
		private Transform _TraNormal;
		//固定显示的节点
		private Transform _TraFixed;
		//弹出节点
		private Transform _TraPopUp;
		//UI管理脚本的节点
		private Transform _TraUIScripts;





		/// <summary>
		/// 公共方法：得到单例实例，自动创建GO
		/// </summary>
		/// <returns></returns>
		public static UIManager GetInstance(){
			if (_Instance == null) {
				_Instance = new GameObject("_UIManager").AddComponent<UIManager>();
			}
			return _Instance;
		}


		/// <summary>
		/// 初始化核心数据
		/// 加载“UI窗体”路径到集合中
		/// 执行优先级比较高，可以提前加载
		/// </summary>
		public void Awake(){
			//字段初始化
			_DicAllUIForms = new Dictionary<string, BaseUIForm>();
			_DicCurrentUIForms = new Dictionary<string, BaseUIForm>();
			_DicFormsPaths = new Dictionary<string, string>();

			_StaCurrentUIForms = new Stack<BaseUIForm>();

			//初始化加载（根UI窗体）Canvas预设
			InitRootCanvasLoading();

			//得到UI的根节点、全屏结点、固定结点、弹出结点

			//UI根节点
			_TraCanvasTransform = GameObject.FindGameObjectWithTag(SysDefine.TAG_Canvas).transform;

			_TraNormal = UnityHelper.FindChildNode(_TraCanvasTransform.gameObject, SysDefine.NODE_Normal);
			_TraFixed = UnityHelper.FindChildNode(_TraCanvasTransform.gameObject, SysDefine.NODE_Fixed);
			_TraPopUp = UnityHelper.FindChildNode(_TraCanvasTransform.gameObject, SysDefine.NODE_PopUp);
			_TraUIScripts = UnityHelper.FindChildNode(_TraCanvasTransform.gameObject, SysDefine.NODE_UIScripts);

			//把本脚本作为脚本管理器游戏对象的子结点（动态创建GO？）（非世界坐标系）
			this.gameObject.transform.SetParent(_TraUIScripts, false);

			//“根UI窗体”在场景转换的时候，不允许销毁（所有的子结点都不会被销毁）
			DontDestroyOnLoad(_TraCanvasTransform);

			//初始化“UI窗体的预设”路径数据
			//先写简单的，后面使用Json做配置文件来完善
			InitUIFormsPathData();

			//if (_DicFormsPaths != null) {
			//	_DicFormsPaths.Add("LoginUIForm",@"Prefabs/UI/LoginUIForm");
			//	_DicFormsPaths.Add("SelectHeroUIForm",@"Prefabs/UI/SelectHeroUIForm");
			//	_DicFormsPaths.Add("MajorCityUIForm", @"Prefabs/UI/MajorCityUIForm");
			//	_DicFormsPaths.Add("MarketUIForm", @"Prefabs/UI/MarketUIForm");
			//	_DicFormsPaths.Add("HeroInfoUIForm",@"Prefabs/UI/HeroInfoUIForm");
			//}
		}



		#region 【公共方法】

		/// <summary>
		/// 公共方法：打开指定名称的窗体
		/// 功能：
		/// 1.加载与判断指定的UI窗体的名称，加载到“所有UI窗体缓冲集合”中。
		/// 2.根据不同的UI窗体的显示模式，分别作不同的加载处理。
		/// </summary>
		/// <param name="uiFormName">窗体的名称</param>
		public void OpenUIForm(string uiFormName){
			//参数检查
			if (string.IsNullOrEmpty(uiFormName))
				return;

			BaseUIForm baseUIForm = null;     //UI窗体基类

			//根据UI窗体的名称加载到“UI窗体缓冲集合”中
			baseUIForm = LoadUIFormToAll(uiFormName);
			if (baseUIForm == null)
				return;

			//是否清空栈集合中的数据的判断
			if (baseUIForm.CurrentUIType.IsCleanStack) {
				CleanStackArray();
			}
			
			//作不同的加载处理
			//TODO
			switch (baseUIForm.CurrentUIType.UIForms_ShowType) {
				//普通类型
				case UIFormShowType.Normal:
					EnterUIForm(uiFormName);
					break;
				//反向切换类型
				case UIFormShowType.ReverseChange:
					PushUIForm(uiFormName);
					break;
				//隐藏其他类型
				case UIFormShowType.HideOther:
					EnterUIForm_HideOther(uiFormName);
					break;
			}
		}


		/// <summary>
		/// 公共方法：关闭指定名称的窗体
		/// </summary>
		/// <param name="uiFormName">窗体的名称</param>
		public void CloseUIForm(string uiFormName){
			//参数检查
			if (string.IsNullOrEmpty(uiFormName))
				return;

			BaseUIForm baseUIFormFromAll;		//窗体基类

			//“所有UI窗体”集合中，如果没有记录，则直接返回
			//不应该是当前显示的吗？
			_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormFromAll);
			if (baseUIFormFromAll == null) 
				return;

			//根据不同的显示类型，分别做不同的关闭处理
			switch (baseUIFormFromAll.CurrentUIType.UIForms_ShowType) {
				case UIFormShowType.Normal:
					//普通类型
					ExitUIForm(uiFormName);
					break;
				case UIFormShowType.ReverseChange:
					//反向切换类型
					PopUIForm();
					break;
				case UIFormShowType.HideOther:
					//隐藏其他类型
					ExitUIForm_HideOther(uiFormName);
					break;
			}
		}

		#endregion



		#region 【私有方法】

		/// <summary>
		/// 初始化加载（根UI窗体）Canvas预设
		/// </summary>
		private void InitRootCanvasLoading() {
			//使用对象缓冲管理器脚本
			ResourcesMgr.GetInstance().LoadAsset(SysDefine.PATH_Canvas, false);
		}


		/// <summary>
		/// 根据UI窗体的名称，加载到所有UI窗体的缓存集合中
		/// 功能：检查“所有UI窗体”集合中，是否已经加载过，否则才加载
		/// </summary>
		/// <param name="uiFormName">窗体的名称</param>
		/// <returns></returns>
		private BaseUIForm LoadUIFormToAll(string uiFormName){
			BaseUIForm baseUIForm = null;	//加载的返回UI窗体的基类
			_DicAllUIForms.TryGetValue(uiFormName, out baseUIForm);
			//如果已有，就直接返回null，否则做一次加载
			if (baseUIForm == null) {
				//加载指定名称的“UI窗体，并放到所有UI窗体的缓存集合中
				baseUIForm = LoadUIForm(uiFormName);
			}
			return baseUIForm;
		}


		/// <summary>
		/// 加载指定名称的UI窗体
		/// 功能：
		/// 1.根据“UI窗体名称”，加载预设克隆体。
		/// 2.根据不同预设克隆体中带的脚本中不同的“位置信息”，加载到根窗体下不同的节点。
		/// 3.隐藏刚创建的UI克隆体（不知道刚创建时是否显示）。
		/// 4.把克隆体，加载到“所有UI窗体”缓存集合中
		/// </summary>
		/// <param name="uiFormName">窗体的名称</param>
		private BaseUIForm LoadUIForm(string uiFormName){
			string strUIFormPath = null;	//UI窗体查询路径
			GameObject goCloneUIPrefab = null;	//得到创建的UI克隆体预设
			BaseUIForm baseUIForm;  //窗体基类

			//根据“UI窗体名称”，得到对应的加载路径
			_DicFormsPaths.TryGetValue(uiFormName, out strUIFormPath);
			//根据“UI窗体名称”，加载预设克隆体
			if (!string.IsNullOrEmpty(strUIFormPath)) {
				goCloneUIPrefab = ResourcesMgr.GetInstance().LoadAsset(strUIFormPath, false);
			}

			//设置“UI克隆体”的父节点（根据克隆体中带的脚本中的不同位置信息）
			if (_TraCanvasTransform != null && goCloneUIPrefab != null) {
				//利用了C#的动态多态性，加载的实际上是具体的子类
				baseUIForm = goCloneUIPrefab.GetComponent<BaseUIForm>();

				if (baseUIForm != null) {
					//switch当前位置信息
					switch (baseUIForm.CurrentUIType.UIForms_Type) {
						case UIFormType.Normal: //普通窗体结点
							goCloneUIPrefab.transform.SetParent(_TraNormal, false);
							break;
						case UIFormType.Fixed: //固定窗体结点
							goCloneUIPrefab.transform.SetParent(_TraFixed, false);
							break;
						case UIFormType.PopUp: //弹出窗体结点
							goCloneUIPrefab.transform.SetParent(_TraPopUp, false);
							break;
					}

					//设置克隆体的隐藏
					goCloneUIPrefab.SetActive(false);
					//把克隆体，加载到“所有UI窗体”缓存集合中
					_DicAllUIForms.Add(uiFormName, baseUIForm);
					return baseUIForm;
				}
				Debug.LogWarning("baseUIForm == null!，请写确认窗体预设对象中是否加载了baseUIForm的子类脚本！");
			}
			Debug.LogWarning("_TraCanvasTransform == null 或者 goCloneUIPrefabs == null！" + "\tuiFormName：" +uiFormName);
			return null;
		}


		/// <summary>
		/// 普通类型的窗体的进入
		/// （把当前窗体加载到“当前窗体”集合中，显示指定名称的窗体）
		/// </summary>
		/// <param name="uiFormName">窗体的名称</param>
		private void EnterUIForm(string uiFormName){
			BaseUIForm baseUIForm;					//UI窗体基类
			BaseUIForm baseUIFormFromAll;		//从“所有UI窗体”集合中得到的窗体

			//如果“正在显示”的集合中，存在整个UI窗体，则直接返回
			_DicCurrentUIForms.TryGetValue(uiFormName, out baseUIForm);
			if (baseUIForm != null) 
				return;

			//把当前窗体，加载到“正在显示”的集合中，然后显示
			_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormFromAll);
			if (baseUIFormFromAll == null)
				return;
			_DicCurrentUIForms.Add(uiFormName,baseUIFormFromAll);
			baseUIFormFromAll.Display();
		}


		/// <summary>
		/// 反向切换类型的窗体的进入
		/// （UI窗体的入栈，显示指定名称的窗体）
		/// </summary>
		/// <param name="uiFormName">窗体的名称</param>
		private void PushUIForm(string uiFormName){
			BaseUIForm baseUIForm;		//UI窗体

			//判断栈集合中，是否有其他的窗体，有则进行冻结处理
			if (_StaCurrentUIForms.Count > 0) {
				BaseUIForm topUIForm = _StaCurrentUIForms.Peek();
				topUIForm.Freeze();
			}
			//判断“所有UI窗体”集合是否有指定的UI窗体，有则处理
			_DicAllUIForms.TryGetValue(uiFormName, out baseUIForm);
			if (baseUIForm == null)
				return;
				
			//把指定的UI窗体入栈
			_StaCurrentUIForms.Push(baseUIForm);
			//显示窗体
			baseUIForm.Display();
		}


		/// <summary>
		/// 隐藏其他类型的窗体的进入
		/// （打开指定名称的窗体，且隐藏其他窗体）
		/// </summary>
		/// <param name="uiFormName">打开的指定窗体的名称</param>
		private void EnterUIForm_HideOther(string uiFormName) {
			//+++把正在显示集合与栈集合中所有的窗体隐藏
			foreach (BaseUIForm baseUI in _DicCurrentUIForms.Values) {
				if (baseUI.name != uiFormName) {
					baseUI.Hiding();
				}
			}
			foreach (BaseUIForm stackUI in _StaCurrentUIForms) {
				if (stackUI.name != uiFormName) {
					stackUI.Hiding();
				}
			}

			_DicCurrentUIForms?.Clear();

			CleanStackArray();

			EnterUIForm(uiFormName);

			//BaseUIForm baseUIForm;      //UI窗体基类
			//BaseUIForm baseUIFormFromAll;   //从集合中得到的UI窗体基类

			////如果“正在显示”的集合中，存在整个UI窗体，则直接返回
			//_DicCurrentUIForms.TryGetValue(uiFormName, out baseUIForm);
			//if (baseUIForm == null)
			//	return;

			////+++把正在显示集合与栈集合中所有的窗体隐藏
			//foreach (BaseUIForm baseUI in _DicCurrentUIForms.Values) {
			//	baseUI.Hiding();
			//}
			//foreach (BaseUIForm stackUI in _StaCurrentUIForms) {
			//	stackUI.Hiding();
			//}

			////把当前窗体，加载到“正在显示”的集合中，然后显示
			//_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormFromAll);
			//if (baseUIFormFromAll == null)
			//	return;
			//_DicCurrentUIForms.Add(uiFormName, baseUIFormFromAll);
			//baseUIFormFromAll.Display();
		}


		/// <summary>
		/// 普通类型的窗体的退出
		/// </summary>
		/// <param name="uiFormName"></param>
		private void ExitUIForm(string uiFormName){
			BaseUIForm baseUIForm;		//窗体基类

			//“正在显示集合”中如果没有记录，则直接返回。
			_DicCurrentUIForms.TryGetValue(uiFormName, out baseUIForm);
			if (baseUIForm == null) 
				return;
		
			//指定窗体标记为“隐藏状态”，且从“正在显示集合”中移除
			_DicCurrentUIForms.Remove(uiFormName);
			baseUIForm.Hiding();
		}


		/// <summary>
		/// 反向切换类型的窗体的退出
		/// （反向切换类型的窗体的出栈，退出这个窗体）
		/// </summary>
		private void PopUIForm(){
			if (_StaCurrentUIForms.Count > 1) {
				//出栈处理
				BaseUIForm topUIForm = _StaCurrentUIForms.Pop();
				//做隐藏处理
				topUIForm.Hiding();
				//下一个窗体重新显示
				BaseUIForm nextUIForm = _StaCurrentUIForms.Peek();
				nextUIForm.Redisplay();
			}
			else if (_StaCurrentUIForms.Count == 1) {
				//出栈处理
				BaseUIForm topUIForm = _StaCurrentUIForms.Pop();
				//做隐藏处理
				topUIForm.Hiding();
			}
		}


		/// <summary>
		/// 隐藏其他类型的窗体的退出
		/// </summary>
		/// <param name="uiFormName">窗体的名称</param>
		private void ExitUIForm_HideOther(string uiFormName) {
			

			ExitUIForm(uiFormName);

			////+++把正在显示集合与栈集合中所有的窗体重新显示
			//foreach (BaseUIForm baseUI in _DicCurrentUIForms.Values) {
			//	if(baseUI.name != uiFormName)
			//		baseUI.Redisplay();
			//}
			//foreach (BaseUIForm stackUI in _StaCurrentUIForms) {
			//	if(stackUI.name != uiFormName)
			//		stackUI.Redisplay();
			//}

		}



		/// <summary>
		/// 清空栈集合中的数据
		/// </summary>
		/// <returns>true：操作成功</returns>
		private bool CleanStackArray() {
			if (_StaCurrentUIForms != null && _StaCurrentUIForms.Count > 0) {
				//清空栈集合
				_StaCurrentUIForms.Clear();
				return true;
			}
			return false;
		}


		/// <summary>
		/// 初始化配置文件路径
		/// </summary>
		private void InitUIFormsPathData(){
			IconfigMgr configMgr = new ConfigMgr(SysDefine.PATH_UIFormsConfigInfo);
			_DicFormsPaths = configMgr.AppSetting;
		}

		
		#endregion


		#region 【测试：显示UI管理器内部核心数据】

		/// <summary>
		/// 显示所有UI窗体数量
		/// </summary>
		/// <returns></returns>
		public int ShowAllUIFormCount(){
			if (_DicAllUIForms != null) {
				return _DicAllUIForms.Count;
			}
			return 0;
		}

		/// <summary>
		/// 显示当前窗体集合中数量
		/// </summary>
		/// <returns></returns>
		public int ShowCurrentUIFormCount(){
			if (_DicCurrentUIForms != null) {
				return _DicCurrentUIForms.Count;
			}
			return 0;
		}

		/// <summary>
		/// 显示当前栈集合中数量
		/// </summary>
		/// <returns></returns>
		public int ShowCurrentStackUIFormCount(){
			if (_StaCurrentUIForms != null) {
				return _StaCurrentUIForms.Count;
			}
			return 0;
		}


#endregion

	}
}