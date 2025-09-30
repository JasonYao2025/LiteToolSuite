## 一、架构设计和实现

整体架构采用MVC来实现。

窗体：注意是控件和对应事件的处理。比如以下对于textbox的处理

```
 tbUserName.Text = "请输入11位手机号码";
 tbUserName.ForeColor = Color.Gray;

 tbUserName.GotFocus += (s, e) => {
     if (tbUserName.Text == "请输入11位手机号码") {   
         tbUserName.Text = "";
         tbUserName.ForeColor = Color.Black;
     }
 };

 tbUserName.LostFocus += (s, e) => {
     if (string.IsNullOrEmpty(tbUserName.Text)){
         tbUserName.Text = "请输入11位手机号码";
         tbUserName.ForeColor = Color.Gray;
     }
 };
```

Models：实体类都放这个目录下，可以用于json文件的解析。比如下面的ProfileModel类。

```
 profile = JsonConvert.DeserializeObject<ProfileModel>(response);
```

BLL: 此目录下存放所有的业务处理，比如获取站点所有的车辆，作为Dictionary返回。注意：简单数据类型可以作为全局变量被引用；而class和Dictionary好像不行，一开始过子窗口构造函数传值；后来仔细考虑过，还是在每个页面里面去请求数据。

Common：定义了很多的helper类，比如HttpClientHelper，LanguageHelper，LogHelper, RegexHelper, SecurityHelper, SQLiteHelper, StringHelper, TimeHelper, TencentCOSHelper。 方便窗体和BLL中的方法调用。



## 二、多语言框架实现

也即是国际化与本地化开发，首先要实现中英切换。切换又分为动态切换和重启程序加载。

### 2.1  WinForm国际化的核心组件

#### 资源文件

- 存储文本、图像等本地化资源，按语言生成不同版本（如Resources.resx、Resources.zh-CN.resx）。
- 键值对结构，支持动态加载，MessageBox的内容应该需要 取键值对内容

#### CultureInfo类

- CurrentCulture：控制数字、日期等数据格式。
- CurrentUICulture：控制界面语言资源加载。

#### 设计器支持

- 设置窗体Localizable属性为True，通过切换Language属性 【可以自动生成】  多语言资源文件，如1.1.2的内容。



### 2.2 创建与管理资源文件

#### 添加资源文件

两种方式：

- 右键项目 → 添加 → 新建项 → 资源文件，命名格式为Resources.<语言代码>.resx（如Resources.zh-CN.resx）。
- 设计器自动化支持：**这种方式慎用，**可能会导致前面已经设置好的其他语言的resx被清空。
  • 启用本地化属性：设置窗体Localizable为True，选择Language为特定语言（如“中文(简体)”），直接修改控件文本，设计器自动生成.resx文件。
  生成的多语言文件： 默认资源文件：Form1.resx； 中文资源文件：Form1.zh-CN.resx。

#### 如何编辑资源文件

资源文件命名比如  zh-CN，中间是横杠，不是下划线。要方便使用resx文件，安装**ResX Manager**，在VS2022工具可固定到选项卡。

- 修改控件对应的语言内容
- 添加键值对，供MessageBox等用

备注：解决resx或者Designer.cs文件不在cs文件下面的问题：

写C#项目时，会复用到以前项目中的.cs文件；在解决方案管理器中手动添加窗口文件后，*.Designer.cs文件和*.resx文件不会在.cs文件下。

原项目中Form1的设计器文件和资源文件在Form1.cs文件下；当复制到新项目后，三个文件在同一级，在新项目中使用窗口设计器打开Form1，也显示不出原来的界面。

为了让设计器能够正常使用，只有将*.Designer.cs文件和*.resx文件放到.cs文件下；具体做法如下：

- 找到NewWinformTest项目的项目文件NewWinformTest.csproj，使用文本编辑器打开

- 找到Form1.Designer.cs文件和Form1*.resx文件的节点

- 增加节点的DependentUpon属性，属性值上层文件的文件名

  ```
  <DependentUpon>Form1.cs</DependentUpon>
  ```

- 保存后重新打开项目*.Designer.cs文件和*.resx文件就在.cs文件下了！！

不光是窗口文件，其他所有的文件都能使用这种方式添加文件结构，让文件之间内容的关系通过结构体现出来，可以让代码结构显示更加清晰

注意：.csproj文件是项目文件，修改前最好备份，如果修改后xml文件格式不正确项目都打不开



**通过导入导出Excel文件来改语言的内容，这个挺好用。**



### 2.3 多语言代码实现

通过重启程序加载或者动态加载实现。如果不经常切换语言或者安装的时候确定语言，优先采用重启程序加载的方式。

#### 重启程序加载

这样的加载方式不用调控件的位置了，当然也可以微调。

1. 添加Settings.settings文件到Properties下面，然后打开并添加项目：名称为Language， 类型为string, 范围是用户，值写成：en-US

​     在LanguageHelper.cs里面添加如下几个方法。

```
 #region 以下的代码用于重启程序设置语言
 /// <summary>
 /// 支持的语言（区域）
 /// </summary>
 public static string[] SupportLanguages = new string[] { "zh-CN", "en-US" };

 /// <summary>
 /// 应用特定区域语言
 /// </summary>
 /// <param name="culture">区域标识</param>
 public static void ApplyLang(string culture)
 {
     CultureInfo ci = new CultureInfo(culture, false);
     CultureInfo.CurrentCulture = ci;
     CultureInfo.CurrentUICulture = ci;
 }

 /// <summary>
 /// 应用默认语言
 /// </summary>
 public static void ApplyDefaultLang()
 {
     if (!string.IsNullOrWhiteSpace(QAToolKit.Properties.Settings.Default.Language))
     {
         ApplyLang(QAToolKit.Properties.Settings.Default.Language);
     }
 }

 /// <summary>
 /// 保存默认语言的配置
 /// </summary>
 /// <param name="culture">区域（语言）</param>
 public static void SetDefaultLang(string culture)
 {
     QAToolKit.Properties.Settings.Default.Language = culture;
     QAToolKit.Properties.Settings.Default.Save();
 }
 #endregion
```

2. 设置全局语言

   在Program.cs里面调用 以下语句，这样所有的Form都会去取对应resx的语言

   ```
   LanguageHelper.ApplyDefaultLang();// 初始化区域信息（即配置语言）
   ```

   如果要跟随系统语言，上面那一句要改成如下几句 

   ```
   #region 语言设置
    CultureInfo systemCulture = CultureInfo.InstalledUICulture;       //获取系统语言
    LanguageHelper.SetDefaultLang(systemCulture.ToString());   //将系统语言写入Properties.Settings里面
   
    LanguageHelper.ApplyDefaultLang();  //调用Properties.Settings中的语言，并初始化区域信息（即配置语言），所有Form都会变
    #endregion
   ```

   

3. 消息框内容和标题等

   需要在对应的resx添加key:value， 比如添加key：MessageBoxConnectMqttContent，然后给这个key设定各种语言内容。

   然后使用下面的代码来获取value并赋值给对应的消息框

       ResourceManager rm = new ResourceManager("QAToolKit.FrmMqttUploadLog", Assembly.GetExecutingAssembly());
       string text = rm.GetString("MessageBoxConnectMqttContent");  // 获取字符串
       string caption = rm.GetString("MessageBoxConnectMqttCaption");
       
       
        //下面是直接从全局的Properties里面取多语言Resources文件里面的内容
         ToolName[0, 0] = QAToolKit.Properties.Resources.Feature1;
         ToolName[0, 1] = QAToolKit.Properties.Resources.Feature2;
   
   

#### 动态加载资源

// 设置当前语言  

```
Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");  
```

// 加载资源  

```
    CultureCode = combLang.SelectedValue.ToString();
    ////SwitchLanguage("zh-CN");
    //SwitchLanguage(CultureCode);            

    if (combLang.SelectedValue.ToString() == "zh-CN")
    {
        LanguageHelper.SetLang("zh-CN", this, typeof(FrmLogin));
    }
    else if (combLang.SelectedValue.ToString() == "en-US")
    {
        LanguageHelper.SetLang("en-US", this, typeof(FrmLogin));
    }    
    else { }
    
    // 读取嵌入资源
    //foreach (string name in Assembly.GetExecutingAssembly().GetManifestResourceNames())
    //{
    //    MessageBox.Show(name, "嵌入资源");
    //}

    //上面打印出的name，不带 .resources，然后填入下面语句； 所有的资源文件会被编译成 Namespace.resources.dll，在不同语言的文件夹下
    ResourceManager rm = new ResourceManager("QAToolKit.FrmLogin", Assembly.GetExecutingAssembly());
    string value = rm.GetString("MessageForLang");  // 获取字符串
    //byte[] fileData = (byte[])rm.GetObject("BinaryKey");  // 获取二进制数据

    //MessageBox.Show(Properties.Resources.PopMessage);  //在Properties下面的各种语言的resx中，存系统的提示信息挺好
    MessageBox.Show(value,"提示");
```

#### 问题1：动态加载以后，Label不对齐了。

解决办法：1. AutoSize设为 false，大小才能调整，要对齐的Label都调整成一样大小；2. 要对齐的所有Label设置成右对齐；3. TextAlign也设置成右对齐.

如何还有问题，可以通过以下代码来固定相对位置。Location是相对父控件而言的

```
  label2.Location = new Point(label1.Left - 10, label1.Top + 60);
  label3.Location = new Point(label1.Left - 10, label1.Top + 120);
```



这篇文章还是有点用的：[C# Winform本地化](https://www.jytek.com/news?article_id=424&pagenum=all)



## 三、数据库操作

程序在操作过程中，需要记录设置和操作的记录。考虑过：access依赖项太多；json和xml没有数据库操作方便，功能也太弱。最后还是选用sqlite，既够轻量，又够方便。

NuGet中安装System.Data.SQLite.Core，**特别注意：制作安装包的时候，需要将SQLite.Interop.dll也要包进去**。

所有和数据的操作都放在SQLiteHelper中，调用很方便，如下：

```
 TestDb = new SQLiteHelper(System.Environment.CurrentDirectory + @"\QAToolKit.db");     //初始化数据库    

 try {
     string sqliteSQL = string.Format("SELECT site_name,site_url FROM server_site");
     var dataTable =TestDb.ExecuteDataset(sqliteSQL,null).Tables[0];

    SiteDict = dataTable.AsEnumerable().ToDictionary(row => row["site_name"].ToString(), row =>  row["site_url"].ToString());    //DataTable直接转换成Dictionary
  }
 catch (Exception ex) { }

 if (SiteDict.Count > 0) {
     //按照首字母排序
     var sort = SiteDict.OrderBy(kv => StringHelper.GetFirstPinyin(kv.Key));
     SiteDict = sort.ToDictionary(kv => kv.Key, kv => kv.Value);

     combSite.DataSource = new BindingSource(SiteDict, null);  //Dictionary绑定到ComboBox
 }         
```



## 四、Tencent-COS操作

Tencent提供SDK，NuGet中安装Tencent.QCloud.Cos.Sdk，操作放在TencentCOSHelper中。

            //调用方法获取到Minio等storage server的用户名和密码
          if (ProfileOperation.GetStorageCredential(FrmFunctionPortal.URL, deviceId, FrmFunctionPortal.Token, out accessKeyId, out secretAccessKey))
                {
                    TencentCOS = new TencentCOSHelper(accessKeyId, secretAccessKey, Profile.Storage.Region);
                    var bucketFiles = TencentCOS.ListFiles(Profile.Storage.TripBucket,filterFolder,".tgz","","100");
    
                    if (bucketFiles == null) 
                    {
                        MessageBox.Show("获取文件失败，请稍微再试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (bucketFiles.Count > 0)
                    {
                        //链式写法，先过滤出 deviceId 的文件，再排序，最后输出成List
                        //在TencentCOSHelper里面已经按照文件夹(Debug/Log/yyyyMMdd/) 过滤  
                        var downloadLogFiles = bucketFiles.Where(x => x.CosKey.Contains(deviceId))
                                                                       .OrderByDescending(x => x.LastModified)
                                                                       .ToList();
    
                        //LoadFileList(fileList);  //弃用                  
    
                        if (downloadLogFiles.Count > 0)
                        {
                            LoadDownloadFileList(filterFolder, downloadLogFiles); //调用方法获取所有Log并填充
                        }
                        else
                        {
                            MessageBox.Show($"未找到{dtpDate.Value.ToString("yyyyMMdd")} 【{deviceId}】AI主机的Log", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }          

注意：上传图片实在太多，还需要用到Prefix过滤和NextMarker



## 五、Mqtt消息

### 5.1 连接Mqtt服务器

### 5.2 订阅消息

Topic：

``` 
via/liveData/[deviceId]     //liveData
via/cmd/request/[deviceId]
via/cmd/response/[deviceId]
```



### 5.3  发布消息

#### 上传Log

DebugType不一样：EnableLog、UploadLog、DisableLog

ModuleName的sys对应与system log，可以修改成iot或者ota上传相应的iot或者ota log。sys时，在SD卡根目录建_DebugLog目录；iot时建debug.db，两者都需要重启。

```
topic：via/cmd/request/[deviceId]   //如deviceId：2CC6822D8490
payload：
{"Action":"SystemDebug","Data":{"Id":"qvl-1701142032","DebugType":"EnableLog","ModuleName":"sys"}}"
```

如果要取消Retain影响，下发一条空指令即可

```
topic：via/cmd/request/2CC6822D8490

payload为空即可
```



**关于Retain的设置解释**

Retain表示服务端是否保留消息。true为保留，如果有新的订阅者连接，就会立马收到该消息。

Enable和Disable加了Retain为true，即使在关机状态下也可以。 开机情况下下指令：重启一次；关机情况下下指令，要重启两次才有log。

Upload还是要在开机状态下才行，不敢把Retain设为true，怕如果代码有bug，Mqtt重连，如设置成true，每次重连就要下发一次；所以保险起见设置成false



#### 周期拍照

```
topic：via/cmd/request/[deviceId]
payload：
 '{"Action":"SystemDebug","Data":{"Id":"debug-001","Start":1,"Cameras":[
 {"Id":"0"},
 {"Id":"1"},  {"Id":"2"}],"DebugType":"CapturePeriodically","ModuleName":"Capture","Prefix":"Pod","Interval":10000}}'
```

**关闭拍照**：目前是关闭所有camera 的拍照 （不检查cmd id 和 camera 数组）。

```
topic：via/cmd/request/EAE171EB5E29D5F55F9CC5C0DF916B09
payload：'{"Action":"SystemDebug","Data":{"Id":"debug-001","Start":0,"Cameras":[{"Id":"0"},{"Id":"2"}],"DebugType":"CapturePeriodically","ModuleName":"Capture"}}'
```

Bucket: media-perf06-1316710660

Path:  BaoGang03/[DeviceId]/yyyyMMdd/

一天也是有n多个文档，可以点击循环下载

下载文件同时就批量改名



## 六、车辆信息查询

查看车辆/device信息：get  包括CloudOff、VideoUploadOff和VideoStreamingOff等值

代码处理都在 TreeViewHelper中

做了一个treeview来查询json数据，其实已经做到json格式化和解析了，并且可以Expand也可以collapse。

还提供右键菜单复制node或者子树信息。

```
// 右键菜单
ctxMenu = new ContextMenuStrip();
ctxMenu.Items.Add("复制节点", null, (s, e) => CopySelectedNode(false));
ctxMenu.Items.Add("复制子树", null, (s, e) => CopySelectedNode(true));
treeView1.ContextMenuStrip = ctxMenu;

 private void CopySelectedNode(bool withChildren)
        {
            if (treeView1.SelectedNode != null)
            {
                string content = withChildren
                    ? TreeViewHelper.CopyNodeWithChildren(treeView1.SelectedNode)
                    : treeView1.SelectedNode.Text;

                TreeViewHelper.CopyToClipboard(content);
            }
        }
```



生成设备二维码，直接扫设备二维码来绑定车辆：

DeviceId：M350和R390不一样 

```
{"ID":"VehicleId"}   //M350
R390:C0F535B2A4AA    //R390
C200:2CC6822D8490    //C200
```



## 七、查看Quota

目前仅仅支持查询，不能修改，存在风险



## 八、时间计算

2.1 时间戳和人类时间（Datetime）互相转换

2.2 提醒时间的精确计算



