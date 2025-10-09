# LiteToolSuite 中文 | [English](https://github.com/JasonYao2025/LiteToolSuite/blob/main/Docs/README-EN.md) 

### 1. LiteToolSuite 工具集简介

LiteToolSuite，中文名**小工具集**，由Jason研发并提供技术支持的测试小工具集合。旨在提供和设备对接的工具，提升软件测试工程师硬件测试工作效率和帮助FAE现在快速处理问题。

### 2. LiteToolSuite 使用必读
- 使用本项目，请注明出处。
- 愿景：让测试工作者更加轻松地完成工作。倘若您喜欢本项目，欢迎给予star支持。
- 作者、贡献者及关联方不承担任何因使用本开源项目导致的直接或间接责任。
- 禁止将本开源项目用于：违反法律法规或公序良俗的行为；侵害他人隐私、知识产权或其他合法权益；可能对人身、财产或环境造成危害的场景。
  
### 3. LiteToolSuite 发布历史
- 2025.10.10  LiteToolSuite 1.0 版本正式开源，正式上线。

### 4. LiteToolSuite 开发目的

最初的想法是让MQTT指令直接在Winform窗口点击操作即可，方便使用。后来加入其他功能，也是方便测试工作，主要是要达到以下几个方面：

- [x] 多语言框架，整体架构采用MVC来实现
- [ ] MQTT消息订阅和发布
- [x] WebAPI调用示例
- [x] 时间和时间戳计算
- [x] 提供一系列的Helper方便进一步开发，包括SQLiteHelper等等

### 5. LiteToolSuite 架构

LiteToolSuite的代码follow MVC的架构，说明如下：

- 窗体：包括UI、控件和对应事件的处理。

- Models：实体类都放这个目录下，方便用于json文件的解析。
- BLL: 此目录下存放所有的业务处理，比如获取站点所有的车辆，作为Dictionary返回。
- Common：定义了很多的helper类，比如HttpClientHelper，LanguageHelper，RegexHelper, SecurityHelper, SQLiteHelper, StringHelper, TimeHelper， 方便窗体和BLL中的方法调用。

### 6. LiteToolSuite 贡献者

- 目前还只有我一个人，希望未来有更多的人参与。

### 7. LiteToolSuite 使用简单说明

- 项目下载：请git clone https://github.com/JasonYao2025/LiteToolSuite.git 
- 打开项目：使用VS2022可以打开工程文件
- 复制文件：复制 Docs/LiteToolSuite和Imgs下面的图片至 编译目录下面
- 编译运行：使用VS2022编译运行即可

LiteToolSuite只是一个客户端工具，需要服务器端提供服务才能调用成功。不同用户的服务器WebAPI不同，所以需要自行修改接口和返回的json数据格式。

**解决办法**有：

- 修改代码中的API路径
- 建立自己的实体类
- 有需要可以联系作者

 



