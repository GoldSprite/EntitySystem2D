# 版本迭代
- temp: 临时版本, 不可使用
- alpha: 开发完成
- beta: 一测完成


## 待办
- [ ] 将States都可视化


# [V0.1.x - 0.2.0]
1. 
   - [x] 实现玩家操作角色: 刀客:
     - [x] 输入: wasd移动, shift疾跑, j攻击, k/space跳跃, y受击, T死亡
2. 
   - [ ] 实现AI攻击者: 刀客:
      1. 在其巡视范围内 闲逛
      2. 如果其他生物进入巡视范围则 追击
      3. 如果进入攻击范围则 发起攻击
      4. 攻击目标死亡则 继续闲逛



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


## 2024.4.3 - [V0.1.6-temp]
- [x] 将AnimEnum属性改回AnimName因为Enum不能可视化
  - 修改BaseStateAnimEnum为AnimName, 
  - 修改所有引用处
    - IBehaviourMeta BId改为string
      - 修改所有引用
- [x] 准备加攻击受伤死亡状态.
  - [x] 攻击
    - [x] 测试指令
    - [x] 测试AttackState的CanTranSelf是否有效
  - [x] 受伤
  - [x] 死亡


## 2024.4.3 - [V0.1.6.1-temp]
- [x] 加跳跃, 
- [x] 坠落


## 2024.4.3 - [V0.1.6.2-alpha]
- 解决坠落后先idle在move导致的idle动画闪现问题
  - 确实是因为状态机逻辑问题: 
    - 原来是先判断并进入新状态, 没有新状态则判断并退出状态; 
      - 所以会先退出idle之后到下一个循环帧再进入move
    - 现在先判断并退出状态, 然后判断进入状态, 这样就不会有帧间隔了.


## 2024.4.3 - [V0.1.6.3-alpha]
- [x] 疾跑忘了, 加上


## 2024.4.3 - [V0.1.6.4-alpha]
- [x] Plan:
  - Add Properties: Health, MaxHealth, AttackPower.
    - Update Fsm: 
      - HurtState: 
        - Add Judgment Of Death Method. 
      - Command: 
        - HurtMethod: Add a param of attacker.
- Real: 
  - 算了还是不用洋文了, 自己都懒得看
  - 更新如下: 
    1. 大更新:
       2. 增加血量, 最大血量, 攻击力属性
       3. 受伤状态OnEnter时做死亡判定, 扣血到0就转deathkey
       4. 指令执行器
          1. Action参数从Action<T>变为Delegate: 这样可以传入多个参数不然只能传一个参数, 
          2. 并删除无用重复的comListener表直接使用commands
    2. 其他小更新: 
       1. 受击改为传入攻击者实例, 并在Hurt状态初始化时注册回调, 获取攻击者攻击力
       2. 修正跳跃输入逻辑, KeyDown改为Key因为KeyDown发现抬起时并不消除JumpKey导致缓存行为
       3. 状态机初始化顺序改为: 首先初始化指令再初始化状态, 因为之前状态初始化时拿不到指令实例
       4. 调整状态优先级表: idle-0, move-1, attack/jump/-2, fall-0, hurt-3, death-4
          1. fall改0: 之前为2和attack同级导致跳跃时攻击到一半被fall打断了
       5. 死亡状态判定改为增加血量低于0自动deathKey=true, 不然取消deathKey之后血量为0都可以站起来
       6. 接口增加ILiving生命属性, 并给IVictim继承: 受伤者应该有生命
       7. 攻击者接口增加攻击力属性
       8. 之前没有成功应用优先级, 添加AddStateFix方法修正优先级
  - 总结: 
    - 加入了生命, 攻击力属性, 
    - 修复了一些状态转换bug
    - 优化了行为转换手感


## 2024.4.3 - [V0.1.8.0-temp]
- 额上一个版本搞错了, 应该换版本之后版本号进位的, 上个应该是[1.7.0]->1.6.4
- 好接着做ai, 其实就是Inputs的变种
  - 制作AI输入器:
- 做了随机左右走动, 先存档, 尝试换Dictionary为SortedDictionary
- 完成界面状态表排序





```


















```