# IP定位数据库文件说明

## 文件位置

- **IPv4数据库**: `ip2region_v4.xdb`
- **IPv6数据库**: `ip2region_v6.xdb`（可选）

## 数据格式

XDB格式是IP2Region的专用数据格式，具有以下特点：
- 支持亿级别的IP数据段管理
- 查询性能达到微秒级别（10微秒左右）
- 自动去重压缩，数据文件体积小
- 支持离线查询，无需网络连接

## 数据来源

### 官方数据源
- **官方网站**: https://ip2region.net/
- **GitHub仓库**: https://github.com/lionsoul2014/ip2region
- **Gitee镜像**: https://gitee.com/lionsoul/ip2region
- **C#绑定实现**: https://gitee.com/lionsoul/ip2region/tree/master/binding/csharp

### 下载方式

#### 方式1：从GitHub下载
```bash
# 克隆仓库
git clone https://github.com/lionsoul2014/ip2region.git

# 进入数据目录
cd ip2region/data

# 复制XDB文件到项目目录
# ip2region.xdb -> wwwroot/Region/ip2region_v4.xdb
```

#### 方式2：直接下载XDB文件
- 访问 https://github.com/lionsoul2014/ip2region/tree/master/data
- 下载 `ip2region.xdb` 文件
- 重命名为 `ip2region_v4.xdb` 并放置到 `wwwroot/Region/` 目录

#### 方式3：使用官方工具生成
如果需要自定义数据，可以使用官方提供的工具生成XDB文件：
- **Golang工具**: https://github.com/lionsoul2014/ip2region/tree/master/maker/golang
- **Java工具**: https://github.com/lionsoul2014/ip2region/tree/master/maker/java
- **C#工具**: https://github.com/lionsoul2014/ip2region/tree/master/maker/csharp
- **C#绑定**: https://gitee.com/lionsoul/ip2region/tree/master/binding/csharp

## 数据更新

### 更新步骤
1. 从官方仓库下载最新的XDB文件
2. 备份当前数据库文件
3. 替换 `wwwroot/Region/` 目录下的文件
4. 重启应用程序（会自动重新加载）

### 更新频率建议
- **生产环境**: 建议每月更新一次
- **开发环境**: 可根据需要随时更新

## 文件大小

- **IPv4数据库**: 约 10-15 MB
- **IPv6数据库**: 约 50-100 MB（如果使用）

## 数据内容

XDB文件包含以下信息：
- 国家（Country）
- 区域（Region）
- 省份（Province）
- 城市（City）
- ISP（互联网服务提供商）

## 使用说明

### 初始化
数据库在应用启动时自动初始化，无需手动操作。

### 查询示例
```csharp
// 查询IP地址位置
string ip = "114.114.114.114";
IpLocationResult? result = TaktLocationHelper.Search(ip);

if (result != null)
{
    Console.WriteLine($"位置: {result.FormattedAddress}");
}
```

详细使用说明请查看 `使用示例.md` 文件。

## 注意事项

1. **文件权限**: 确保应用程序有读取XDB文件的权限
2. **文件路径**: 文件必须放置在 `wwwroot/Region/` 目录下
3. **文件格式**: 必须使用XDB格式，不支持其他格式
4. **IPv6支持**: IPv6数据库是可选的，如果不存在，IPv6地址查询会返回null
5. **性能**: 使用Content缓存策略，首次加载会将整个数据库加载到内存，查询速度最快

## 技术支持

- **官方文档**: https://ip2region.net/
- **GitHub Issues**: https://github.com/lionsoul2014/ip2region/issues
- **Gitee Issues**: https://gitee.com/lionsoul/ip2region/issues
- **C#绑定文档**: https://gitee.com/lionsoul/ip2region/tree/master/binding/csharp
- **社区支持**: 访问官方社区获取帮助

## 许可证

IP2Region使用Apache 2.0许可证，可以自由使用和修改。
