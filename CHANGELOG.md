# 版本迭代
- temp: 临时版本, 不可使用
- alpha: 开发完成
- beta: 一测完成

## 2024.4.2 - [V0.1.0-alpha]
- 创建项目
- 导入底层工具 Tools: 
  - [x] DateTool
    - [x] 已测试
  - [x] MathTool
    - [x] 已测试
  - [x] ReflectionHelper
    - [x] 已测试
  - [x] StackTraceHelper
    - [x] 已测试
- 导入Unity基本工具 UnityTools: 
    - [x] LogTool
    - [x] InspectorHelper
    - [x] CustomKVpair
- 导入基本功能组件 UnityPlugins: 
    - [x] 输入管理 MyInputSystem
    - [x] 动画管理 MyAnimator
    - [x] 物理管理 PhysicsManager
    - [ ] 实体属性 EntityProps
    - [x] 状态机控制 UFsm


## 2024.4.2 - [V0.1.1-temp]
- 创建实际工作的状态机: AnimalUFsm
  - BaseFsm状态机结构重设计: 
    - 普通类变为Component类
    - BaseProps具象依赖变为IBaseProps接口依赖
  - 增加实体属性器组件
  - 增加实体输入器组件
- 创建Editor绘制工具: InspectorTools
  - 创建手动请求属性: ManualRequireAttribute, 用于请求组件并发出警告
    - 以及其属性绘制器
- 增加完善ReflectionHelper方法并通过测试
