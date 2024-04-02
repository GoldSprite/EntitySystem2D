# 版本迭代
- temp: 临时版本, 不可使用
- alpha: 开发完成
- beta: 一测完成



# [V0.1.x - 0.2.0]
- [ ] 实现玩家操作角色: 刀客: 
  - [ ] 输入: wasd移动, shift疾跑, j攻击 
- [ ] 实现AI攻击者: 刀客:
  - [ ] 行为
    1. 在其巡视范围内 闲逛
    2. 如果其他生物进入巡视范围则 追击
    3. 如果进入攻击范围则 发起攻击
    4. 攻击目标死亡则 继续闲逛
- [ ] 创建EntitySystem管理者(中介者): 统一管理属性器, 输入器, 物理器, 动画器, 状态器
  - 关联
    - 输入器->状态器
    - 物理器->状态器
    - 状态器->属性器
    - 状态器->动画器



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


## 2024.4.2 - [V0.1.2-alpha]
- [x] 日志工具加入 LogTool
  - [x] 通过测试
- [x] 更新反射工具: 基本查找, 异常查找, 继承成员查找
  - [x] 通过测试


## 2024.4.2 - [V0.1.3-temp]
- 有点乱, 一步步来


## 2024.4.2 - [V0.1.4-temp]
- 首先组合组件构成一个基本单元: 有待机和移动行为的实体
- 元件: 作为系统运行的基本单元
  - 状态元件: 
    - 待机: 
      - 依赖: 无, 
      - 条件: 始终进入, 始终退出, 不可转换自身
      - 执行: 无
    - 移动: 
      - 依赖: 属性器, 动画器
      - 条件: 在地面且Dir不为0时进入, 在空中或速度基本静止时退出, 不可转换自身
      - 执行: 朝dir方向移动自身, 播放移动动画
  - 属性元件: 
    - 运动属性: 
      - Direction方向
      - Velocity实时速度
      - IsGround是否在地面
      - Speed移动速率
- 组件: 根据数据组合元件, 运行系统
  - BaseFsm状态器: 
    - 提供状态运转方法, 
    - 注册/添加状态方法
  - MyAnimator动画器: 
    - 提供注册`动画键-资源表`方法, 
    - 动画退出/转换事件, 
    - 播放动画方法
  - BaseProps: 
    - 提供添加属性组, 
    - 移除属性组, 
    - 获取属性组方法
  - PhysicsManager: 
    - 提供地面碰撞信息, 
    - 实体碰撞监听方法
  - MyInputManager: 
    - 提供注册按键监听方法
    - 提供获取按键值方法
---
- [ ] 开发数据模块结构
  - 本质是一个ScriptableObject, 
    - 在配置时, 填入各项元件数据
    - 在运行时, 被实体系统引用, 检测并实例化相应行为
  - Behaviours: 一整个行为配置表 
    - Behaviour: 里面包含各项数据单元
- 依旧是临时存档, 准备依旧使用odin来做序列化, 自己做太麻烦了.


## 2024.4.3 - [V0.1.5-temp]
- 各组件搭配, 实现左右移动, 不过项目结构略凌乱, 待梳理