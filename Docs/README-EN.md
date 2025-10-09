# LiteToolSuite 中文 | [English](https://github.com/walker0012025/API-TestPilot/blob/main/EN-README.md) 

### 🌟 LiteToolSuite 工具集简介

LiteToolSuite，中文名**小工具集**，由测试开发Jason开发的测试小工具的集合。旨在提供和硬件对接的工具，提升软件测试工程师硬件测试工作效率和帮助FAE现在快速处理问题和采集Log。

### 🍓 LiteToolSuite 使用必读
- 🔥 使用本项目，请注明出处。
- 🔥 我的愿景：提升整个测试行业的技术水平。倘若您喜欢本项目，欢迎给予star支持。
- 🔥 作者、贡献者及关联方不承担任何因使用本开源项目导致的直接或间接责任。
- 🔥 禁止将本开源项目用于：违反法律法规或公序良俗的行为；侵害他人隐私、知识产权或其他合法权益；可能对人身、财产或环境造成危害的场景。
  
### 🎉 LiteToolSuite 发布历史
- 🎁 2025.10.10 API-TestPilot 1.0 版本正式开源，正式上线。

### 🚀 LiteToolSuite 达成目的

主要是要达到以下几个方面：

1. 多语言框架，整体架构采用MVC来实现
2. MQTT消息订阅和发布
3. WebAPI调用示例
4. 时间和时间戳计算
5. 提供一系列的Helper方便进一步开发，包括SQLiteHelper

窗体：注意是控件和对应事件的处理。

Models：实体类都放这个目录下，可以用于json文件的解析。比如下面的ProfileModel类。

```
 profile = JsonConvert.DeserializeObject<ProfileModel>(response);
```

BLL: 此目录下存放所有的业务处理，比如获取站点所有的车辆，作为Dictionary返回。注意：简单数据类型可以作为全局变量被引用；而class和Dictionary好像不行，一开始过子窗口构造函数传值；后来仔细考虑过，还是在每个页面里面去请求数据。

Common：定义了很多的helper类，比如HttpClientHelper，LanguageHelper，LogHelper, RegexHelper, SecurityHelper, SQLiteHelper, StringHelper, TimeHelper, TencentCOSHelper。 方便窗体和BLL中的方法调用。

### 💡 LiteToolSuite 重点提醒

LiteToolSuite只是一个客户端工具，需要服务器端提供服务才能调用成功。不同用户的服务器WebAPI不同，所以需要自行修改接口和返回的json数据格式。**解决办法**有：

1、利用数据库修改API路径；

2、建立自己的实体类

3、有需要可以联系我

### 👥 LiteToolSuite 贡献同学

1、目前还只有我一个人，希望未来有更多的人参与。



### 📌 LiteToolSuite 实操教程

使用VS2022可以打开编译。

2、项目下载：请git clone https://github.com/walker0012025/API-TestPilot.git 。

3、进入项目：cd API-TestPilot。

### 📌 LiteToolSuite 使用教程

1、项目下载：请git clone https://github.com/walker0012025/API-TestPilot.git 。

2、进入项目：cd API-TestPilot/api。

3、安装依赖：pip install -r requirements.txt。

4、生成用例：执行client.py。

### 🙏 引用
```bibtex

```




