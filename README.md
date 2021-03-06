# GPS车辆监控平台

## 瞎逼逼：

&emsp;&emsp;搬砖搬了快四年呐，果然很快啊~~前几年都在与Web服务打交道，突然间公司要搞软硬结合，需要有个平台，这下好吧。。。公司原先有个老平台，窃喜之下打开老平台一看，好复杂，一脸懵逼的说。。。但是慢慢的研究，原来这一坨屎的代码是写的那么优美，那么生动。但是，老平台存在的问题有耦合度高、扩展性差、维护难度大，这才让我有业余时间重新自己瞎整的念头。

### 新平台的优势：

1. 跨平台
2. 借助.NET Core模块化的思想
3. 分久必合，合久必分的服务化思想
4. 站在巨人的肩膀上搬砖

### GPS网关

| 功能 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| GPS.Gateway.JT808SuperSocketServer | 使用SuperSocket实现 | √ |
| GPS.JT808NettyServer| 使用DotNetty实现 | √ |

#### GPS.JT808NettyServer网关已实现的功能：

| 功能 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| IdleStateHandler（系统自带） | 心跳机制 | √ |
| JT808ConnectionHandler | JT808(超时策略、ip白名单) | √ |
| DelimiterBasedFrameDecoder（系统自带） | JT808消息边界（防止粘包、半包） | √ |
| JT808DecodeHandler | JT808解码器 | √ |
| JT808ServiceHandler | JT808业务处理 | √ |

### 发布/订阅(GPS.PubSub.Abstractions)：

| 功能 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| GPS.JT808PubSubToKafka | 实现kafka发布/订阅 | 小部分完成 |
| GPS.JT808PubSubToRabbitMQ | 实现RabbitMQ发布/订阅 | 小部分完成 |

> 注意：MQ的选择适平台设备数量而定，选择kafka的原因是平台需要承载10万台设备。

#### GPS.JT808PubSubToKafka已实现的功能：

| 功能 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| JT808_UnificationSend | 统一下发设备消息 | √ |
| JT808_0x0200 | 位置信息汇报 | √ |
| JT808_DeviceMonitoringDispatcher| 设备监控分发器 | √ |
| JT808_UnificationPushToWebSocket| 统一推送WebSocket | √ |

#### GPS.JT808PubSubToRabbitMQ已实现的功能：

| 功能 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| JT808_UnificationSend | 统一下发设备消息 | √ |
| JT808_0x0200 | 位置信息汇报 | √ |

##### 分发器(GPS.Dispatcher.Abstractions)：

| 功能 | 说明 | 使用场景 |
|:-------:|:-------:|:-------:|
| IDeviceMonitoringDispatcher | 设备监控(点名）分发器 | 后台需要监控某一辆车的所有消息 |
| ISourcePackageDispatcher | 原包分发器 | 需要将源数据转给其他平台 |

##### 分发器(GPS.Dispatcher.Abstractions)功能实现：

| 功能 | 实现方式 | 备注 |
|:-------:|:-------:|:-------:|
| GPS.JT808DeviceMonitoringDispatcher | 通过kafka实现 | 可选择合适的方式实现 |
| GPS.JT808SourcePackageDispatcher | 通过kafka实现 | 可选择合适的方式实现 |
| GPS.JT808SampleDeviceMonitoring | 简单设备点名监控打印原数据 | GPS.JT808DeviceMonitoringDispatcher 消费者 |

### 基于SignalR的JT808.WebSocketServer服务：

> 推送平台：Web平台、App、IOS、及微信小程序。【注意：正式请使用HTTPS】

> 推送客户端demo：

| 功能 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| alarm.html demo| js | √ |
| index_vue.html demo| vue | √ |

> 推送场景：

| 功能 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| 最新定位数据 | - | × |
| 围栏报警 | - | × |
| 胎压报警 | - | × |
| 疲劳驾驶报警 | - | × |
| 超速报警 | - | × |
| 急加速报警 | - | × |
| 急减速报警 | - | × |
| 急转弯报警 | - | × |

### 基于GRPC实现内部系统调用：

| 功能 | 说明 |
|:-------:|:-------:|
| GPS.GrpcProtocol | 消息协议 |
| GPS.GrpcServiceBase | 消息接口服务 |

#### 基于GRPC实现GPS.IdentityServer4GrpcServer统一用户鉴权（使用JWT方式）

> 不同平台（Web平台、App、IOS、及微信小程序）请求认证服务器。

| 项目 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| GenerateToken | 颁发令牌 | √ |
| VerifyToken  | 认证令牌 | √ |
| RefreshToken | 刷新令牌 | √ |