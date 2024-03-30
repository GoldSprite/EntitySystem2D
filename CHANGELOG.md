# EntitySystem2D
实体生物系统

#目标: V0.2.0
- [ ] 实现logger日志器
- [ ] 实现序列化面板化数据类型

# 2024.3.29-V0.1.0: 
- [x] 创建项目
- [x] 基本设计: 
  - 创建State: 
    - IState, 状态原始接口: 定义状态优先级, 进退条件及伴随行为, 以及运行时行为接口
      - State, 接口的空实现: 增加状态机引用与构造传参, 重写退出/当进入/当退出方法用于增加一个逻辑: 在当进入后才可退出防止进入即退出
        - BaseState, 真正状态父类: 增加基本属性器引用与构造传参
          - IdleState, 待机状态: 默认状态, 始终进入, 始终退出, 并不可转换到自身(逻辑是当有其他状态满足时则退出默认状态, 反之无任何其他状态满足则待机)
          - MoveState, 移动状态: 运动方向x属性不为0且在地面则进入, 不在地面或速度属性静止则退出
  - 创建Fsm: 
    - IFsm, 状态机原始接口: 定义默认/当前状态, 提供更新下一个状态, 获取某个状态, 更新/修正更新循环接口
      - Fsm, 增加状态字典, 属性器引用,  实现所有接口逻辑
        - BaseFsm, 将属性器换为new的基本属性器
  - 创建Props: 
    - IProps, 
      - BaseProps
- [x] 更新设计:
  - 暂不统计...
  - 增加BaseFsmCommandManager
- [ ] 测试: 
  - [x] Idle<->Move是否可以正确转换
  - [x] 进行实际运行测试
    - 过程中学习: 
      1. [Test] 普通测试
      2. [UnityTest] + IEnumerator返回型方法 + yield return null 可等待一帧(实测可能是更多帧或者更少(也即漏帧或不跳))
         1. 实际使用yield return new WaitForSeconds(0.1f);
      3. TestRunner方法与其代码所在脚本无关系并不会创建组件实例, 需要手动创建.
         1. 配合new GameObject().AddComponent<TargetComponent>();使用可针对脚本进行测试.


# 2024.3.30-V0.2.0:
- 自定义Logger:
  - 功能设计: 
    - 使用上: 
      - 代码使用: LogTool.NLogXX("tag"="default", "msg")
    - 执行逻辑: 
      - 根据调用方法NLogInfo..NLogMsg...来选择Log方式.
      - 根据传入的tag来标记前缀, 默认为GetType().Name即调用者类名
      - 根据清单动态展示或过滤log, 根据过滤级别: 默认为Warn即Warn及以下使用过滤清单, 其上Err,Force始终展示.
    - 预设显示过滤清单: config-logs: Dictionary<tag, true>
      - 在初始化时加载LogFilter.data数据, 解析为Dictionary<tag, bool>字典: 
      - 登记在字典上的tag项根据其bool值决定是否展示/或过滤.
    - 动态过滤清单: realtime-logs:  
      - 在运行时自动检查到预设清单上不存在的tag则加入realtime-logs清单且默认为false.
      - 将动态清单项设置为true则会自动转移到预设清单上.
  - 要点分析:
      - 过滤信息结构filterInfo:
          - tag: string类型 表示标签名
          - display: bool类型 表示是否展示
          - used: enum类型 表示此次运行期间的使用状态: { UnUsed未使用, Used已使用, Intercepted被拦截 }.
      - 存储数据结构: 
        - 拦截等级interceptLevel: int
        - 默认未预设log是否展示: false
        - 预设白/黑名单 filterList: Dictionary<string, filterInfo>
        - 动态log清单: Dictionary<string, filterInfo>
      - 在执行Log()时, 该tag不存在于动态表时, 将其添加到动态表. 
      - 在执行Log()时, 该tag不存在于预设表时, 默认不显示. 
  - 设计可行性验证: 
    - Q1: 最简使用: 
        - LogTool.NLogMsg("Hello world!");
    - A1: 
      1. 参数检查: log等级为Msg, 无tag, 
      2. 参数转换: tag自动转换为调用者类名, 
      3. 检查过滤表配置: 
         1. filterLevel为Warn >= Msg, 通过,
         2. 处理过滤表:
            1. 
