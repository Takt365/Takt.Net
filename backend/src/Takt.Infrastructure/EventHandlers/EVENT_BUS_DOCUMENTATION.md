# 事件总线系统文档

## 概述

本项目实现了完整的前后端事件总线系统,采用**发布-订阅模式**,支持:
- 后端:基于 MediatR 的事件总线
- 前端:基于 mitt 的事件总线
- 跨端:基于 SignalR 的实时通信

---

## 后端事件总线 (.NET)

### 架构设计

```
Domain Layer (Takt.Domain)
  └── Events/
      ├── ITaktEventBus.cs          # 事件总线接口
      ├── ITaktEvent.cs             # 事件接口
      ├── TaktDomainEvent.cs        # 领域事件基类
      ├── TaktIntegrationEvent.cs   # 集成事件基类
      └── TaktCommonEvents.cs       # 通用事件定义

Infrastructure Layer (Takt.Infrastructure)
  ├── EventBus/
  │   ├── TaktEventBus.cs           # 事件总线实现(基于 MediatR)
  │   └── Behaviors/
  │       └── EventLoggingBehavior.cs  # 日志行为管道
  └── EventHandlers/
      ├── TaktCrudEventHandler.cs      # CRUD 事件处理器
      └── TaktBusinessEventHandler.cs  # 业务事件处理器
```

### 事件类型

#### 1. CRUD 事件

```csharp
// 定义:Takt.Domain.Events.TaktCommonEvents
TaktCrudCreatedEvent   // 创建事件
TaktCrudUpdatedEvent   // 更新事件
TaktCrudDeletedEvent   // 删除事件

// 属性
- Module: string          // 模块名称(如:User, Role, Leave)
- EntityId: long          // 实体ID
- EntityType: string      // 实体类型(如:TaktUser)
- OperatorId: long?       // 操作人ID
- Data: object?           // 扩展数据
```

#### 2. 业务事件

```csharp
TaktBusinessEvent

// 属性
- Module: string          // 模块名称(如:SignalR, Notification, Auth)
- Action: string          // 动作名称(如:UserConnected, MessageSent)
- EntityId: long          // 实体ID
- EntityType: string      // 实体类型
- OperatorId: long?       // 操作人ID
- Data: object?           // 扩展数据
```

### 使用方式

#### 发布事件

```csharp
// 注入事件总线
public class MyService
{
    private readonly ITaktEventBus _eventBus;
    
    public MyService(ITaktEventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public async Task CreateEntityAsync()
    {
        // 创建实体...
        
        // 发布事件
        await _eventBus.PublishAsync(new TaktCrudCreatedEvent
        {
            Module = "User",
            EntityId = userId,
            EntityType = "TaktUser",
            OperatorId = currentUserId,
            Data = new { UserName = "张三" }
        });
    }
}
```

#### 订阅事件

```csharp
// 创建事件处理器(自动被 MediatR 扫描注册)
public class TaktCrudCreatedEventHandler : INotificationHandler<TaktCrudCreatedEvent>
{
    public Task Handle(TaktCrudCreatedEvent notification, CancellationToken ct)
    {
        // 处理逻辑
        TaktLogger.Information("实体创建: {Module} - {EntityId}", 
            notification.Module, notification.EntityId);
        
        return Task.CompletedTask;
    }
}
```

### 事件处理器位置

**所有事件处理器位于:**
```
backend/src/Takt.Infrastructure/EventHandlers/
```

**已实现的处理器:**
- `TaktCrudCreatedEventHandler` - 处理创建事件
- `TaktCrudUpdatedEventHandler` - 处理更新事件
- `TaktCrudDeletedEventHandler` - 处理删除事件
- `TaktBusinessEventHandler` - 处理业务事件(路由分发)

### 行为管道

**EventLoggingBehavior** - 自动记录所有事件的处理日志:
- 事件开始处理
- 事件处理完成(含耗时)
- 事件处理异常

已在 `TaktMediatRCollectionExtensions.cs` 中注册。

---

## 前端事件总线 (Vue 3)

### 架构设计

```
frontend/takt.antd/src/utils/
  ├── eventBus.ts              # 事件总线核心实现
  └── eventBusMonitor.ts       # 性能监控工具
```

### 事件类型定义

```typescript
// 类型安全的事件定义 (eventBus.ts)
export type Events = {
  // 认证模块
  'auth:redirectToLogin': void
  'auth:didLogout': void
  'auth:loginSuccess': { userId: string; username: string }
  'auth:tokenRefreshed': { token: string; expiresIn: number }
  
  // 通知模块
  'notification:received': NotificationPayload
  'notification:broadcast': NotificationPayload
  'notification:count:update': number
  'notification:clear': void
  
  // CRUD 模块
  'crud:created': { module: string; id: string | number; data?: unknown }
  'crud:updated': { module: string; id: string | number; data?: unknown }
  'crud:deleted': { module: string; id: string | number }
  
  // SignalR 模块
  'signalr:connected': { connectionId: string; hubs: string[] }
  'signalr:disconnected': { reason?: string }
  
  // ... 更多事件类型
}
```

### 使用方式

#### 1. 组合式函数(推荐)

```vue
<script setup lang="ts">
import { useEventBus, AuthEvents, CrudEvents } from '@/utils/eventBus'

const { on, emit } = useEventBus()

// 订阅事件(组件卸载时自动清理)
on(AuthEvents.LoginSuccess, ({ userId, username }) => {
  console.log('登录成功:', userId, username)
})

// 发布事件
emit(CrudEvents.Created, { module: 'user', id: 123 })
</script>
```

#### 2. 全局实例

```typescript
import { eventBus, NotificationEvents } from '@/utils/eventBus'

// 订阅
eventBus.$on(NotificationEvents.Received, (payload) => {
  console.log('收到通知:', payload)
})

// 发布
eventBus.$emit(NotificationEvents.Received, {
  title: '新消息',
  content: '您有一条新消息',
  type: 'info'
})
```

### 事件常量

```typescript
// 按命名空间组织的事件常量
AuthEvents          // 认证事件
NotificationEvents  // 通知事件
MenuEvents          // 菜单事件
CrudEvents          // CRUD 事件
DataEvents          // 数据事件
SystemEvents        // 系统事件
SignalREvents       // SignalR 事件
UserEvents          // 用户事件
TaskEvents          // 任务事件
```

### 性能监控

```typescript
import { useEventBusMonitor } from '@/utils/eventBusMonitor'

const monitor = useEventBusMonitor()

// 启用监控
monitor.enable()

// 获取性能数据
const summary = monitor.getSummary()
console.log('事件统计:', summary)

// 打印报告
monitor.printReport()

// 清空数据
monitor.clear()
```

---

## 前后端事件映射

| 前端事件 | 后端事件 | 说明 |
|---------|---------|------|
| `auth:loginSuccess` | `TaktBusinessEvent(Auth:LoginSuccess)` | 登录成功 |
| `auth:tokenRefreshed` | `TaktBusinessEvent(Auth:TokenRefreshed)` | Token 刷新 |
| `auth:didLogout` | `TaktBusinessEvent(Auth:Logout)` | 登出 |
| `crud:created` | `TaktCrudCreatedEvent` | 创建实体 |
| `crud:updated` | `TaktCrudUpdatedEvent` | 更新实体 |
| `crud:deleted` | `TaktCrudDeletedEvent` | 删除实体 |
| `signalr:connected` | `TaktBusinessEvent(SignalR:UserConnected)` | SignalR 连接 |
| `signalr:disconnected` | `TaktBusinessEvent(SignalR:UserDisconnected)` | SignalR 断开 |
| `notification:received` | `TaktBusinessEvent(Notification:MessageSent)` | 收到通知 |

---

## SignalR 跨端通信

### 后端发布 → 前端订阅

```csharp
// 后端 SignalR Hub 发布事件
public override async Task OnConnectedAsync()
{
    // 发布业务事件
    await _eventBus.PublishAsync(new TaktBusinessEvent
    {
        Module = "SignalR",
        Action = "UserConnected",
        EntityId = userId,
        Data = new { UserName, ConnectionId }
    });
    
    // 同时通过 SignalR 推送给前端
    await Clients.Group($"User_{userName}").SendAsync("UserConnected", new { ... });
}
```

```typescript
// 前端 SignalR 接收并转发到 EventBus
connectHub.on('UserConnected', (event) => {
  // 发布到前端 EventBus
  eventBus.$emit(SignalREvents.Connected, {
    connectionId: event.connectionId,
    hubs: ['connect']
  })
})
```

---

## 最佳实践

### 1. 后端

✅ **DO:**
- 在 Infrastructure 层创建事件处理器
- 使用结构化日志记录事件
- 在事件处理器中处理缓存刷新、通知等副作用
- 使用行为管道统一处理日志和异常

❌ **DON'T:**
- 不要在 Application 层创建事件处理器
- 不要在事件处理器中执行耗时操作(使用后台任务)
- 不要忘记处理异常

### 2. 前端

✅ **DO:**
- 使用 `useEventBus()` 组合式函数(自动清理)
- 使用类型安全的事件常量
- 在开发环境启用性能监控
- 合理组织事件命名空间

❌ **DON'T:**
- 不要手动管理订阅清理(使用组合式函数)
- 不要在事件处理器中执行阻塞操作
- 不要滥用全局事件(优先使用组件 props/emit)

---

## 调试技巧

### 后端

```csharp
// 查看 MediatR 注册的处理器
// 启动日志会显示所有扫描到的 INotificationHandler

// 查看事件处理日志
// 日志格式: [Event Start/Success/Error] ...
```

### 前端

```typescript
// 启用事件监控
import { eventBusMonitor } from '@/utils/eventBusMonitor'
eventBusMonitor.enable()

// 查看订阅统计
console.log(eventBus.getStats())

// 打印性能报告
eventBusMonitor.printReport()
```

---

## 扩展指南

### 添加新事件类型

#### 后端

1. 在 `TaktCommonEvents.cs` 定义新事件类
2. 在 `EventHandlers/` 创建对应的处理器
3. MediatR 会自动扫描注册

#### 前端

1. 在 `eventBus.ts` 的 `Events` 类型中添加
2. 在对应的事件常量对象中添加
3. TypeScript 会自动提供类型检查

---

## 性能优化

1. **异步处理**: 事件处理器应该快速返回,耗时操作使用后台任务
2. **批量发布**: 使用 `PublishBatchAsync` 批量发布事件
3. **条件订阅**: 根据模块/动作过滤事件,避免不必要的处理
4. **监控告警**: 使用 EventBusMonitor 监控性能,设置告警阈值

---

## 常见问题

### Q: 事件处理器没有被调用?

A: 检查:
1. 处理器是否在 `Infrastructure/EventHandlers/` 目录下
2. 是否实现了 `INotificationHandler<TEvent>`
3. MediatR 是否扫描了该程序集

### Q: 前端事件没有触发?

A: 检查:
1. 是否使用了正确的事件常量
2. 订阅是否在组件卸载前被清理
3. 使用 `eventBus.getStats()` 查看订阅统计

### Q: 如何追踪事件流转?

A: 
- 后端: 查看 `[Event Start/Success/Error]` 日志
- 前端: 启用 `eventBusMonitor` 查看性能报告
- SignalR: 查看 `[SignalR]` 日志

---

**文档版本**: 1.0  
**更新时间**: 2026-05-10  
**维护者**: Takt365
