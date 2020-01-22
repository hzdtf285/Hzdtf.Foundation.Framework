# Hzdtf.FoundationFramework
基础框架系统，支持.NET和.NET Core平台，语言：C#，DB支持MySql和SqlServer，主要功能有抽象持久化、服务层，将业务基本的增删改查抽离复用；提供代码生成器从DB生成实体、持久化、服务以及MVC控制器，每层依赖接口，并需要在客户端将对应实现层用Autofac程序集依赖注入，用AOP提供日志跟踪、事务、模型验证等。对Autofac、Redis、RabbitMQ封装扩展；DB访问提供自动主从访问，Redis客户端分区。特别适合管理系统。

本框架必须运行在.NET Standard 2.0、.NET Framework 4.6.1和.NET Core 2.2以上。下载源码用Visual Studio 2017打开。
工程以Standard或Std结尾是标准库，以Framework或Frm结尾为Framework库，以Core结尾为Core库。
初始编译时会耗些时间，因为要从nuget下载包。
框架使用依赖接口注入，使用对象要优先依赖接口，用Autofac属性注入方式。
工程包含contract表示契约工程，专门定义接口以及抽象部分类；包含impl为实现接口的具体类。
实体类以Info结尾。

一、Comnon包
所有公共工具类库

二、Platform包
基于不同平台实现不同的配置类
使用配置是，直接用Contract项目里的IAppConfiguration

三、Logger包
框架自定义的日志组件
日志等级有：debug,info,wran,error,fatal，默认等级为debug，如设置info，则大于或info等级都会记录，而小于该等级（如debug）不会记录。等级在配置文件里可设置，配置名为：HzdtfLog:LogLevel:Default。

四、IOC包
DI与AOP的扩展功能，目前只实现Autofac扩展。提供了读入配置文件进行加载程序集进行注入，需要注入的类必须加上Inject特性描述；实现了部分的拦截器（AOP），如日志记录拦截、禁用拦截、授权拦截和参数验证拦截。注：使用拦截器的方法必须是virtual或override，因为框架使用的是类拦截。

五、Tool\CodeGenerator包
代码生成器工具，提供从数据库读取所有表，然后生成所有码（包含模型、持久化接口、持久化实现、服务接口、服务实现、Framework控制器和Core控制器），生成的代码只需要放在对应各在的项目工程里即可，具体生成的代码参考BasicFunction。
1、命名空间前辍：您的项目命名空间前辍，生成的所有代码都会以这个作为前辍。
2、此工具目前只支持MySql（5.0以上）和SQL Server（2008以上）。

六、Tool\EncryptionAndDecryption包
提供对字符串加解密工具，主要用在对数据库字符串加密。

七、Authorization包
提供针对不同平台进行用户授权验证，将用户授权信息存储到cookie里等公共操作。

八、Cache包
第三方的缓存组件封装，目前只实现了对Redis扩展，依赖于StackExchange.Redis组件。
1、封装了ConnectionMultiplexer对象，使用IConnectionMultiplexerManager接口，自动通过传入的访问模式的参数来判断操作主或从，在GetDatabase(AccessMode accessMode = AccessMode.MASTER, int db = -1, string key = null)里，这里有个key，如果传入了，则会在客户端分区，经过key的HashCode % Redis实例个数，得到哪个实例进行操作。
（1）、主库数据库字符串配置Key名：Redis:Production:DefaultConnection，如果有多个字符串，则以|分隔。
（2）、键名Redis:Encrypt，表示对字符串是否加密
使用时必须要IConnectionMultiplexerManager得到Database，否则框架扩展的功能都使用不上。

2、扩展了针对对象存储到Redis的Hash类型，在ObjectSet(this IDatabase db, RedisKey key, object value, TimeSpan? expiry = null)里。
3、扩展了针对分布式锁的功能，在LockTake(this IDatabase db, RedisKey key, RedisValue value, TimeSpan expiry, Action action, int retryIntervalMillisecond = 200)

九、MessageQueue包
对消息队列进行扩展封装，目前只实现了RabbitMQ，核心都在rabbitMessageQueue.xml配置文件里，使用生产者或消费者，输入对应参数，会找这个配置文件找到对应的交换机和队列名。具体参考DEMO。

十、Persistence包
1、对持久化操作进行封装，包含一个表的基本增删改查（包括分页查）功能，并支持异步操作。此持久化依赖于Dapper组件。
2、提供了针对数据库事务方便操作，只需要在具体方法里加上Transaction特性描述即可。
3、关于ConnectionId：名称为连接ID（Guid)，每个持久化方法里必须含有的参数，作用是为了在多个方法之间复用同一个连接对象，提高性能。原则是：哪里创建的连接ID哪里负责销毁，不是您创建的就不要自行销毁它。
4、DbConnectionManager数据库连接管理器：提供了相对智能的维护DB连接资源池，主要存在目的是为了连接ID而存在，并支持通过传入访问模式参数，自动进行主从操作。主数据库字符串配置名：ConnectionStrings:DefaultConnection；从数据库字符串配置名：ConnectionStrings:SlaveConnection，当没有配置从时，会自动找主。ConnectionStrings:Encrypt：表示字符串是否加密。

十一、Service包
俗称服务层，所有业务逻辑都放在此包里。
1、在方法里加了Auth特性，表示调用此方法前会验证当前用户是否有权限。
2、在方法参数里加了验证特性，调用此方法前首先会验证参数有效性，如：
ModifyPasswordByLoginId([Model] CurrUserModifyPasswordInfo currUserModifyPassword, string connectionId = null)，加了Model特性，会验证currUserModifyPassword里的属性有效值。验证方法与微软提供的模型验证一致。
3、所有服务层返回的方法都必须是ReturnInfo<T>对象，该对象里的Code为0时，表示业务处理成功。非0为失败。
4、由工具自动生成代码统一由Generators文件夹包含，此处的文件最好不要改，如要扩展功能，则用部分类特性，新建Expand文件夹。

十二、Controller包
控制器包，提供基本的MVC控制器增删改查的抽象功能方法，符合RESTFull风格。

十三、BasicFunction包
基本功能，提供了用户、角色、角色权限、数据字典四大功能模块，还提供登录退出功能，可以理解为基本的范例。
注：
1、MenuCode:菜单编码，表示菜单的唯一编码，如用户菜单，角色菜单等。
2、FunctionCode:功能编码，表示一个具体功能，如添加、编辑、查询和删除等。
3、菜单与功能关联：如一个菜单具体哪些功能，可通过此处关联。
4、角色与菜单功能关联：如一个角色具体哪些权限，可通过此处关联。

在范例的控制器里有[MenuCode("User")]，Action方法里有[FunctionCode("Add")]，表示执行这个控制器的这个Action时，拦截器会判断该用户是否有用户的添加这个权限。

十四、Workflow包
工作流，提供流程审核功能，可配置送件、退件路线，由表单与流程组成一个审核流程。

十五、WebDemo包
提供Web的Demo网站程序，采用WebAPI方式，前后端分离，使用Bootstreap UI框架。运行时，先创建数据库，导入Doc\创建表与初始化基本数据_mysql.sql，目前只有MySql才做了基础数据。用户名：system，密码：123。
依赖注入程序集都在：assemblyConfig.json配置文件里，包含了要注入哪些程序集，以及哪些程序集要使用哪些拦截器。
所有要使用具体程序集（包含具体使用哪些对象）都要在客户端里装载，因为所有项目都依赖于接口，而不是依赖于具体实现，具体类里也不创建具体依赖类。

结语：
1、获取当前用户：UserTool.CurrUser，在客户端（WEB站点）里要实现获取当前用户的方法，如User.GetCurrUserFunc => 从Cookie或Session取出当前用户。
2、获取当前环境：UtilTool.CurrEnvironmentType （目前只有生产与测试环境，默认是生产）
3、获取具体日志对象：LogTool.DefaultLog，默认是ConsoleLog，如系统未使用依赖注入，又要改变记录日志方式，则需要在系统启动时手工设置此对象。
4、获取IOC的对象：框架使用的是属性注入，所以一般使用属性的公有权限，如下：
public IUserService UserService
{
  get;
  set;
}
直接拿来使用，如要手工获取，则使用AutofacTool.Resolve<对象类型>()。
5、程序启动时，必须先加载依赖注入的对象，具体参考DEMO。

概括介绍基本功能远不足于完全描述框架的功能，一切看源码，运行DEMO，问题自然迎刃而解。框架曾分别部署到Windows与Linux（centos7)运行，暂无发现问题。如有更好的改进也可私信我：hzdtf285@126.com，有利于改善框架都无限感激！
