// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.DependencyInjection
// 文件名称：TaktAutofacModule.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt Autofac模块，配置依赖注入容器和SqlSugar多租户支持
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Application.Services.Statistics.Logging;
using Takt.Infrastructure.Data;
using Takt.Infrastructure.Data.Seeds;
using Takt.Infrastructure.Extensions;
using Takt.Infrastructure.Helpers;
using Takt.Infrastructure.User;

namespace Takt.Infrastructure.DependencyInjection;

/// <summary>
/// Takt Autofac模块
/// </summary>
public class TaktAutofacModule : Module
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration">配置</param>
    public TaktAutofacModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// 加载模块
    /// </summary>
    /// <param name="builder">容器构建器</param>
    protected override void Load(ContainerBuilder builder)
    {
        // 注册SqlSugar（使用内置多租户支持，按照官方Demo方式）
        builder.Register(c =>
        {
            var configs = new List<ConnectionConfig>();

            // 从 dbConfigs 配置读取数据库配置
            var dbConfigsSection = _configuration.GetSection("dbConfigs");
            if (dbConfigsSection.Exists())
            {
                foreach (var dbConfig in dbConfigsSection.GetChildren())
                {
                    var conn = dbConfig["Conn"];
                    var dbType = dbConfig.GetValue<int>("DbType", 1);
                    var configId = dbConfig["ConfigId"];
                    var isAutoCloseConnection = dbConfig.GetValue<bool>("IsAutoCloseConnection", true);

                    if (string.IsNullOrEmpty(conn) || string.IsNullOrEmpty(configId))
                        continue;

                    // 转换数据库类型
                    var sqlSugarDbType = dbType switch
                    {
                        0 => DbType.MySql,
                        1 => DbType.SqlServer,
                        3 => DbType.Oracle,
                        4 => DbType.PostgreSQL,
                        _ => DbType.SqlServer
                    };

                    // 读取雪花ID配置
                    var snowflakeSection = _configuration.GetSection("Snowflake");
                    var snowflakeId = snowflakeSection.GetValue<bool>("Enabled", true);

                    var connectionConfig = new ConnectionConfig
                    {
                        ConfigId = configId,
                        ConnectionString = conn,
                        DbType = sqlSugarDbType,
                        IsAutoCloseConnection = isAutoCloseConnection
                    };

                    // 根据配置决定使用雪花ID还是自增ID
                    // InitKeyType.Attribute 表示使用特性（雪花ID）
                    // 如果不设置 InitKeyType，则使用默认值（自增ID）
                    if (snowflakeId)
                    {
                        connectionConfig.InitKeyType = InitKeyType.Attribute;
                    }

                    configs.Add(connectionConfig);
                }
            }

            // 如果没有读取到任何配置，抛出异常
            if (configs.Count == 0)
            {
                throw new Exception("数据库配置未找到，请检查 appsettings.json 中的 dbConfigs 配置");
            }

            // 读取配置
            var showDbLog = _configuration.GetValue<bool>("ShowDbLog", false);
            var aopLogEnabled = _configuration.GetValue<bool>("Logging:AopLog", true);

            // 创建支持多租户的 SqlSugarScope（按照官方Demo）
            // 注意：AOP一定要设置在你操作语句之前，多库情况下使用 GetConnectionScope
            var scope = new SqlSugarScope(configs, db =>
            {
                // 确保映射信息已初始化（通过访问主连接的 EntityMaintenance）
                // 这可以避免在使用 GetConnectionScope 时出现空引用异常
                try
                {
                    _ = db.EntityMaintenance;
                }
                catch
                {
                    // 忽略初始化错误，让后续操作自然失败并抛出更明确的错误
                }

                // AOP 配置：为每个数据库连接配置事件
                foreach (var config in configs)
                {
                    // 多库情况下使用 GetConnectionScope（按照官方文档）
                    // 注意：GetConnectionScope 可能会在连接失败时抛出异常，需要捕获
                    ISqlSugarClient? connection = null;
                    try
                    {
                        connection = db.GetConnectionScope(config.ConfigId);
                    }
                    catch (Exception ex)
                    {
                        // 连接失败时记录警告，但不阻止应用程序启动
                        // 后续使用该连接时会再次尝试连接并抛出更明确的错误
                        Serilog.Log.Warning(ex, "数据库连接初始化失败 ConfigId: {ConfigId}，将在首次使用时重试。错误: {Message}",
                            config.ConfigId, ex.Message);
                        continue; // 跳过此连接的AOP配置
                    }

                    if (connection != null)
                    {
                        // 确保每个连接的映射信息已初始化
                        try
                        {
                            _ = connection.EntityMaintenance;
                        }
                        catch
                        {
                            // 忽略初始化错误，让后续操作自然失败并抛出更明确的错误
                        }
                        // SQL执行前事件
                        connection.Aop.OnLogExecuting = (sql, pars) =>
                        {
                            if (showDbLog)
                            {
                                // 获取原生SQL（推荐方式，性能OK）
                                var nativeSql = UtilMethods.GetNativeSql(sql, pars);

                                // 输出到控制台
                                Console.WriteLine($"[DB Log] ConfigId: {config.ConfigId}");
                                Console.WriteLine($"SQL: {nativeSql}");
                                if (pars != null && pars.Length > 0)
                                {
                                    Console.WriteLine($"Parameters: {string.Join(", ", pars.Select(p => $"{p.ParameterName}={p.Value}"))}");
                                }
                                Console.WriteLine();

                                // 使用 Serilog 记录
                                Serilog.Log.Information("[DB Log] ConfigId: {ConfigId}, SQL: {Sql}, Parameters: {Parameters}",
                                    config.ConfigId, nativeSql, pars != null ? string.Join(", ", pars.Select(p => $"{p.ParameterName}={p.Value}")) : "");
                            }
                        };

                        // SQL执行完事件（可以输出执行时间）
                        connection.Aop.OnLogExecuted = (sql, pars) =>
                        {
                            if (showDbLog)
                            {
                                var executionTime = connection.Ado.SqlExecutionTime;
                                Serilog.Log.Information("[DB Log] ConfigId: {ConfigId}, ExecutionTime: {ExecutionTime}ms",
                                    config.ConfigId, executionTime);
                            }
                        };

                        // SQL报错事件
                        connection.Aop.OnError = (exp) =>
                        {
                            // OnError 事件的参数是 Exception 类型
                            Serilog.Log.Error(exp,
                                "[DB Error] ConfigId: {ConfigId}, Exception: {Exception}",
                                config.ConfigId, exp.Message);
                        };

                        // 差异日志事件（数据变更监听）
                        if (aopLogEnabled)
                        {
                            connection.Aop.OnDiffLogEvent = (diffLogEvent) =>
                            {
                                try
                                {
                                    // 获取表名（从AfterData或BeforeData的第一条记录中获取）
                                    string tableName = string.Empty;
                                    if (diffLogEvent.AfterData != null && diffLogEvent.AfterData.Count > 0)
                                    {
                                        tableName = diffLogEvent.AfterData[0].TableName ?? string.Empty;
                                    }
                                    else if (diffLogEvent.BeforeData != null && diffLogEvent.BeforeData.Count > 0)
                                    {
                                        tableName = diffLogEvent.BeforeData[0].TableName ?? string.Empty;
                                    }

                                    // 使用统一的排除配置判断是否应该排除此表
                                    if (TaktLoggingExclusions.ShouldExcludeAopLogTable(tableName))
                                    {
                                        return;
                                    }

                                    // 排除种子数据操作，不记录差异日志
                                    if (TaktLoggingExclusions.IsSeedingData())
                                    {
                                        return;
                                    }

                                    // 获取当前用户名
                                    var userName = TaktUserContext.CurrentUser?.UserName ?? "Takt365";

                                    // 获取操作类型（从DiffType枚举获取）
                                    var operType = diffLogEvent.DiffType.ToString().ToUpperInvariant();

                                    // 获取主键ID（从AfterData或BeforeData中提取）
                                    long? primaryKeyId = null;
                                    try
                                    {
                                        // 尝试从AfterData获取主键（INSERT/UPDATE）
                                        if (diffLogEvent.AfterData != null && diffLogEvent.AfterData.Count > 0)
                                        {
                                            var afterColumns = diffLogEvent.AfterData[0].Columns;
                                            var idColumn = afterColumns?.FirstOrDefault(c =>
                                                c.ColumnName.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                                                c.ColumnName.Equals("id", StringComparison.OrdinalIgnoreCase));
                                            if (idColumn != null && idColumn.Value != null)
                                            {
                                                if (long.TryParse(idColumn.Value.ToString(), out var id))
                                                {
                                                    primaryKeyId = id;
                                                }
                                            }
                                        }
                                        // 如果AfterData没有，尝试从BeforeData获取（DELETE）
                                        if (primaryKeyId == null && diffLogEvent.BeforeData != null && diffLogEvent.BeforeData.Count > 0)
                                        {
                                            var beforeColumns = diffLogEvent.BeforeData[0].Columns;
                                            var idColumn = beforeColumns?.FirstOrDefault(c =>
                                                c.ColumnName.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                                                c.ColumnName.Equals("id", StringComparison.OrdinalIgnoreCase));
                                            if (idColumn != null && idColumn.Value != null)
                                            {
                                                if (long.TryParse(idColumn.Value.ToString(), out var id))
                                                {
                                                    primaryKeyId = id;
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        // 忽略解析错误
                                    }

                                    // 序列化修改前后的数据
                                    string? beforeData = null;
                                    string? afterData = null;
                                    string? diffData = null;

                                    if (diffLogEvent.BeforeData != null && diffLogEvent.BeforeData.Count > 0)
                                    {
                                        beforeData = JsonConvert.SerializeObject(diffLogEvent.BeforeData);
                                    }

                                    if (diffLogEvent.AfterData != null && diffLogEvent.AfterData.Count > 0)
                                    {
                                        afterData = JsonConvert.SerializeObject(diffLogEvent.AfterData);
                                    }

                                    // 生成差异数据（对比BeforeData和AfterData的列值）
                                    if (diffLogEvent.BeforeData != null && diffLogEvent.BeforeData.Count > 0 &&
                                        diffLogEvent.AfterData != null && diffLogEvent.AfterData.Count > 0)
                                    {
                                        var diffList = new List<object>();
                                        var beforeColumns = diffLogEvent.BeforeData[0].Columns;
                                        var afterColumns = diffLogEvent.AfterData[0].Columns;

                                        if (beforeColumns != null && afterColumns != null)
                                        {
                                            foreach (var beforeCol in beforeColumns)
                                            {
                                                var afterCol = afterColumns.FirstOrDefault(a =>
                                                    a.ColumnName == beforeCol.ColumnName);
                                                if (afterCol != null && !Equals(beforeCol.Value, afterCol.Value))
                                                {
                                                    diffList.Add(new
                                                    {
                                                        ColumnName = beforeCol.ColumnName,
                                                        BeforeValue = beforeCol.Value,
                                                        AfterValue = afterCol.Value
                                                    });
                                                }
                                            }
                                        }

                                        if (diffList.Count > 0)
                                        {
                                            diffData = JsonConvert.SerializeObject(diffList);
                                        }
                                    }

                                    // 获取SQL语句
                                    var sqlStatement = diffLogEvent.Sql;

                                    // 获取执行耗时（毫秒，如果没有则使用0）
                                    var costTime = 0; // SqlSugar的DiffLogModel可能不包含ElapsedMilliseconds，使用0

                                    // 异步保存差异日志（不阻塞SQL执行）
                                    // 注意：这里不能直接使用 c.Resolve，因为事件回调在运行时执行，需要使用全局服务提供者
                                    // 使用静态服务提供者或通过其他方式获取服务
                                    _ = Task.Run(async () =>
                                    {
                                        try
                                        {
                                            var serviceProvider = TaktServiceProvider.ServiceProvider;
                                            if (serviceProvider == null)
                                            {
                                                Serilog.Log.Warning("无法获取 ServiceProvider，跳过差异日志记录");
                                                return;
                                            }

                                            // 后台线程必须使用独立作用域，否则会与请求线程共享/争用连接，导致 ConfigId 5 等出现「连接未关闭/正在连接」
                                            using (var scope = serviceProvider.CreateScope())
                                            {
                                                var aopLogService = scope.ServiceProvider.GetService<ITaktAopLogService>();
                                                if (aopLogService == null)
                                                {
                                                    Serilog.Log.Warning("无法获取 ITaktAopLogService，跳过差异日志记录");
                                                    return;
                                                }

                                                var createDto = new TaktCreateAopLogDto
                                                {
                                                    UserName = userName,
                                                    OperType = operType,
                                                    TableName = tableName,
                                                    PrimaryKeyId = primaryKeyId,
                                                    BeforeData = beforeData,
                                                    AfterData = afterData,
                                                    DiffData = diffData,
                                                    SqlStatement = sqlStatement,
                                                    OperTime = DateTime.Now,
                                                    CostTime = costTime
                                                };

                                                await aopLogService.CreateAsync(createDto);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Serilog.Log.Error(ex, "保存差异日志失败: TableName: {TableName}, OperType: {OperType}",
                                                tableName, operType);
                                        }
                                    });
                                }
                                catch (Exception ex)
                                {
                                    Serilog.Log.Error(ex, "处理差异日志事件失败");
                                }
                            };
                        }

                        // 可以修改SQL和参数的事件（可选）
                        // connection.Aop.OnExecutingChangeSql = (sql, pars) =>
                        // {
                        //     // 可以在这里修改SQL或参数
                        //     return new KeyValuePair<string, SugarParameter[]>(sql, pars);
                        // };
                    }
                }
            });

            return scope;
        }).As<ISqlSugarClient>().SingleInstance();

        // 注册数据库上下文
        builder.RegisterType<TaktSqlSugarDbContext>().AsSelf().InstancePerLifetimeScope();

        // 使用扩展方法注册所有种子数据提供者
        builder.AddTaktSeeds();

        // 注册数据库初始化器（需要注入种子数据提供者集合和服务提供者）
        // 注意：此注册在 TaktInitializeCollectionExtensions 中直接使用 new 创建实例，此处保留以保持一致性
        builder.Register(c =>
        {
            var client = c.Resolve<ISqlSugarClient>();
            var seedDataProviders = c.Resolve<IEnumerable<ITaktSeedData>>();
            var serviceProvider = c.Resolve<IServiceProvider>();
            var configuration = c.Resolve<IConfiguration>();
            var tenantSection = configuration.GetSection("Tenant");
            var configId = tenantSection["DefaultConfigId"] ?? "0";
            return new TaktDatabaseInitializer(client, seedDataProviders, serviceProvider, configId);
        }).AsSelf().InstancePerLifetimeScope();

        // 使用扩展方法注册所有通用服务（基础设施服务、应用服务）
        builder.AddTaktServices(_configuration);
    }

}