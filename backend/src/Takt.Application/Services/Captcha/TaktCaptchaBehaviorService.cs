// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Captcha
// 文件名称：TaktCaptchaBehaviorService.cs
// 创建时间：2025-01-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt行为验证码服务实现，基于用户行为特征进行验证
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Collections.Concurrent;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Takt.Application.Services;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Captcha;

/// <summary>
/// 行为验证码数据（存储在内存中）
/// </summary>
internal class BehaviorCaptchaData
{
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 行为数据
    /// </summary>
    public Dictionary<string, object> BehaviorData { get; set; } = new();
}

/// <summary>
/// Takt行为验证码服务实现
/// </summary>
public class TaktCaptchaBehaviorService : TaktServiceBase, ITaktCaptchaService
{
    private readonly IConfiguration _configuration;
    private readonly TaktCaptchaBehaviorOptions _options;
    private readonly int _expirationMinutes;
    private readonly ConcurrentDictionary<string, BehaviorCaptchaData> _captchaStore = new();
    private readonly Random _random = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCaptchaBehaviorService(
        IConfiguration configuration,
        Microsoft.Extensions.Options.IOptions<Takt.Shared.Models.TaktCaptchaOptions> captchaOptions)
    {
        _configuration = configuration;

        var captchaSection = _configuration.GetSection("Captcha:Behavior");
        _options = new TaktCaptchaBehaviorOptions();
        captchaSection.Bind(_options);

        // 从配置选项读取统一的过期时间
        _expirationMinutes = captchaOptions.Value.ExpirationMinutes;

        // 启动清理过期数据的定时任务
        _ = Task.Run(async () => await CleanupExpiredDataAsync());
    }

    /// <summary>
    /// 生成验证码
    /// </summary>
    public Task<CaptchaGenerateResult> GenerateAsync()
    {
        var startTime = DateTime.UtcNow;
        try
        {
            TaktLogger.Debug("[Captcha Behavior] 开始生成验证码");
            
            // 生成验证码ID
            var captchaId = GenerateCaptchaId();
            var step1Time = (DateTime.UtcNow - startTime).TotalMilliseconds;
            TaktLogger.Debug("[Captcha Behavior] 步骤1-生成ID完成: 耗时={Time}ms", step1Time);

            // 生成目标位置（百分比）
            var targetPosition = _random.Next(60, 91); // 60% 到 90% 之间
            var step2Time = (DateTime.UtcNow - startTime).TotalMilliseconds;
            TaktLogger.Debug("[Captcha Behavior] 步骤2-生成目标位置完成: TargetPosition={TargetPos}%, 总耗时={Time}ms",
                targetPosition, step2Time);

            // 存储验证码数据
            _captchaStore.TryAdd(captchaId, new BehaviorCaptchaData
            {
                CreatedAt = DateTime.UtcNow,
                BehaviorData = new Dictionary<string, object>
                {
                    { "targetPosition", targetPosition }
                }
            });
            var totalTime = (DateTime.UtcNow - startTime).TotalMilliseconds;
            TaktLogger.Information("[Captcha Behavior] 验证码生成完成: CaptchaId={CaptchaId}, TargetPosition={TargetPos}%, 总耗时={Time}ms",
                captchaId, targetPosition, totalTime);

            return Task.FromResult(new CaptchaGenerateResult
            {
                CaptchaId = captchaId,
                Type = "Behavior",
                BackgroundImage = string.Empty, // 行为验证码不需要图片
                TargetPosition = targetPosition
            });
        }
        catch (Exception ex)
        {
            var totalTime = (DateTime.UtcNow - startTime).TotalMilliseconds;
            TaktLogger.Error(ex, "[Captcha Behavior] 生成行为验证码失败，耗时={Time}ms", totalTime);
            throw;
        }
    }

    /// <summary>
    /// 验证验证码
    /// </summary>
    public Task<CaptchaVerifyResult> VerifyAsync(CaptchaVerifyRequest request)
    {
        try
        {
            TaktLogger.Debug("[Captcha Behavior] 开始验证: CaptchaId={CaptchaId}", request.CaptchaId);
            
            if (string.IsNullOrEmpty(request.CaptchaId))
            {
                TaktLogger.Warning("[Captcha Behavior] 验证码ID为空");
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaIdRequired"
                });
            }

            // 获取验证码数据
            if (!_captchaStore.TryGetValue(request.CaptchaId, out var captchaData))
            {
                TaktLogger.Warning("[Captcha Behavior] 验证码不存在: CaptchaId={CaptchaId}", request.CaptchaId);
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaNotFoundOrExpired"
                });
            }

            // 检查是否过期
            var expirationTime = captchaData.CreatedAt.AddMinutes(_expirationMinutes);
            if (DateTime.UtcNow > expirationTime)
            {
                _captchaStore.TryRemove(request.CaptchaId, out _);
                TaktLogger.Warning("[Captcha Behavior] 验证码已过期: CaptchaId={CaptchaId}, CreatedAt={CreatedAt}, ExpirationTime={ExpirationTime}",
                    request.CaptchaId, captchaData.CreatedAt, expirationTime);
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaExpired"
                });
            }

            // 防机器人：生成到验证的最短间隔。过短视为自动化脚本
            var elapsedSeconds = (DateTime.UtcNow - captchaData.CreatedAt).TotalSeconds;
            TaktLogger.Debug("[Captcha Behavior] 时间检查: Elapsed={Elapsed:F2}s, MinComplete={Min}s",
                elapsedSeconds, _options.MinCompleteSeconds);
            
            if (elapsedSeconds < _options.MinCompleteSeconds)
            {
                // 完成过快时不删除验证码，允许用户重试（可能是误操作）
                TaktLogger.Warning("[Captcha Behavior] 完成过快，疑似机器人: CaptchaId={CaptchaId}, Elapsed={Elapsed:F2}s < {Min}s",
                    request.CaptchaId, elapsedSeconds, _options.MinCompleteSeconds);
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaTooFastRetry"
                });
            }

            // 解析用户输入
            if (request.UserInput == null)
            {
                TaktLogger.Warning("[Captcha Behavior] 用户输入为空: CaptchaId={CaptchaId}", request.CaptchaId);
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaUserInputEmpty"
                });
            }

            // 记录用户输入的详细信息
            TaktLogger.Debug("[Captcha Behavior] 用户输入数据: CaptchaId={CaptchaId}, UserInput={UserInputJson}",
                request.CaptchaId, JsonConvert.SerializeObject(request.UserInput));

            // 强制行为数据校验
            var validationFail = ValidateBehaviorInput(request.UserInput, out var failMessage);
            if (validationFail)
            {
                // 数据校验失败时不删除验证码，允许用户重试
                // 只有验证通过或严重错误（如过期）时才删除
                TaktLogger.Warning("[Captcha Behavior] 行为数据校验失败: CaptchaId={CaptchaId}, Message={Message}",
                    request.CaptchaId, failMessage);
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = failMessage
                });
            }

            // 计算行为分数
            var score = CalculateBehaviorScore(request.UserInput, captchaData);
            
            TaktLogger.Debug("[Captcha Behavior] 行为分数计算完成: CaptchaId={CaptchaId}, Score={Score:F2}, Threshold={Threshold}",
                request.CaptchaId, score, _options.ScoreThreshold);

            // 判断是否通过验证
            var success = score >= _options.ScoreThreshold;

            // 只有验证成功时才删除验证码数据（一次性使用）
            // 验证失败时保留验证码，允许用户重试
            if (success)
            {
                _captchaStore.TryRemove(request.CaptchaId, out _);
                TaktLogger.Debug("[Captcha Behavior] 验证成功，已删除验证码数据: CaptchaId={CaptchaId}", request.CaptchaId);
            }
            else
            {
                TaktLogger.Debug("[Captcha Behavior] 验证失败，保留验证码数据允许重试: CaptchaId={CaptchaId}, Score={Score:F2}", 
                    request.CaptchaId, score);
            }

            TaktLogger.Information("[Captcha Behavior] 验证完成: CaptchaId={CaptchaId}, Success={Success}, Score={Score:F2}, Threshold={Threshold}, Message={Message}",
                request.CaptchaId, success, score, _options.ScoreThreshold, success ? "验证成功" : "验证失败，行为特征不符合要求");

            return Task.FromResult(new CaptchaVerifyResult
            {
                Success = success,
                Message = success ? "validation.captchaVerifySuccess" : "validation.captchaVerifyBehaviorMismatch",
                Score = score
            });
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Behavior] 验证行为验证码失败: CaptchaId={CaptchaId}", request.CaptchaId);
            return Task.FromResult(new CaptchaVerifyResult
            {
                Success = false,
                Message = "validation.captchaVerifyProcessError"
            });
        }
    }

    /// <summary>
    /// 生成验证码ID
    /// </summary>
    private string GenerateCaptchaId()
    {
        var bytes = new byte[16];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
    }

    /// <summary>
    /// 校验行为数据是否满足强制要求（RequireTimeSpent、RequireTrajectory、最短时长/点数）。
    /// 返回 true 表示校验失败，message 为失败原因。
    /// </summary>
    private bool ValidateBehaviorInput(object userInput, out string message)
    {
        message = string.Empty;
        if (userInput is not JObject jo)
        {
            message = "validation.captchaBehaviorDataInvalid";
            TaktLogger.Debug("[Captcha Behavior] 用户输入不是JObject格式");
            return true;
        }

        if (_options.RequireTimeSpent)
        {
            var tsToken = jo["timeSpent"];
            if (tsToken == null || (tsToken.Type != JTokenType.Float && tsToken.Type != JTokenType.Integer))
            {
                message = "validation.captchaNeedTimeSpent";
                TaktLogger.Debug("[Captcha Behavior] 缺少timeSpent字段或格式错误");
                return true;
            }
            double timeSpent;
            try
            {
                timeSpent = tsToken.ToObject<double>();
                TaktLogger.Debug("[Captcha Behavior] timeSpent={TimeSpent}s, MinRequired={Min}s",
                    timeSpent, _options.MinTimeSpentSeconds);
            }
            catch
            {
                message = "validation.captchaTimeSpentInvalid";
                TaktLogger.Debug("[Captcha Behavior] timeSpent转换失败");
                return true;
            }
            if (timeSpent < _options.MinTimeSpentSeconds)
            {
                message = "validation.captchaBehaviorTooFast";
                TaktLogger.Debug("[Captcha Behavior] timeSpent过短: {TimeSpent}s < {Min}s",
                    timeSpent, _options.MinTimeSpentSeconds);
                return true;
            }
        }

        if (_options.RequireTrajectory)
        {
            var trToken = jo["mouseTrajectory"];
            if (trToken == null || trToken.Type != JTokenType.Array)
            {
                message = "validation.captchaMouseTrajectoryRequired";
                TaktLogger.Debug("[Captcha Behavior] 缺少mouseTrajectory字段或不是数组");
                return true;
            }
            var count = trToken.Children().Count();
            TaktLogger.Debug("[Captcha Behavior] 轨迹点数: {Count}, MinRequired={Min}",
                count, _options.MinTrajectoryPoints);
            if (count < _options.MinTrajectoryPoints)
            {
                message = "validation.captchaTrajectoryInsufficient";
                TaktLogger.Debug("[Captcha Behavior] 轨迹点数不足: {Count} < {Min}",
                    count, _options.MinTrajectoryPoints);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 计算行为分数
    /// </summary>
    private double CalculateBehaviorScore(object userInput, BehaviorCaptchaData captchaData)
    {
        try
        {
            double score = 0.0;
            var factors = 0;

            // 解析用户输入
            var userInputDict = new Dictionary<string, object>();
            if (userInput is JObject jo)
            {
                foreach (var prop in jo.Properties())
                {
                    userInputDict[prop.Name] = prop.Value?.ToString() ?? string.Empty;
                }
            }
            else if (userInput is Dictionary<string, object> dict)
            {
                userInputDict = dict;
            }

            // 1. 检查位置准确性（如果提供了位置信息）
            if (userInputDict.TryGetValue("position", out var positionObj) &&
                captchaData.BehaviorData.TryGetValue("targetPosition", out var targetPositionObj))
            {
                if (int.TryParse(positionObj?.ToString(), out var userPosition) &&
                    int.TryParse(targetPositionObj?.ToString(), out var targetPosition))
                {
                    var positionDiff = Math.Abs(userPosition - targetPosition);
                    var positionScore = Math.Max(0, 1.0 - (positionDiff / 50.0)); // 50% 误差范围内线性递减
                    score += positionScore * 0.4; // 位置准确性占40%权重
                    factors++;
                    
                    TaktLogger.Debug("[Captcha Behavior] 位置评分: UserPosition={UserPos}%, TargetPosition={TargetPos}%, Diff={Diff}%, PositionScore={PosScore:F2}, Weight=40%",
                        userPosition, targetPosition, positionDiff, positionScore);
                }
            }

            // 2. 检查鼠标移动轨迹（如果提供了轨迹信息）
            if (userInputDict.TryGetValue("mouseTrajectory", out var trajectoryObj))
            {
                var trajectoryScore = AnalyzeMouseTrajectory(trajectoryObj);
                score += trajectoryScore * 0.3; // 鼠标轨迹占30%权重
                factors++;
                
                TaktLogger.Debug("[Captcha Behavior] 轨迹评分: TrajectoryScore={TrajScore:F2}, Weight=30%",
                    trajectoryScore);
            }

            // 3. 检查时间特征（如果提供了时间信息）
            if (userInputDict.TryGetValue("timeSpent", out var timeSpentObj))
            {
                if (double.TryParse(timeSpentObj?.ToString(), out var timeSpent))
                {
                    if (timeSpent < _options.MinTimeSpentSeconds)
                    {
                        // 时长过短，该项不计分（防机器人）
                        timeSpent = 0;
                    }
                    // 正常人类操作时间通常在1-5秒之间
                    var timeScore = timeSpent >= 1.0 && timeSpent <= 5.0 ? 1.0 :
                                   timeSpent < 1.0 ? timeSpent :
                                   Math.Max(0, 1.0 - (timeSpent - 5.0) / 10.0);
                    score += timeScore * 0.2; // 时间特征占20%权重
                    factors++;
                    
                    TaktLogger.Debug("[Captcha Behavior] 时间评分: TimeSpent={TimeSpent}s, TimeScore={TimeScore:F2}, Weight=20%",
                        timeSpent, timeScore);
                }
            }

            // 4. 检查其他行为特征（如果启用了机器学习）
            if (_options.EnableMachineLearning && userInputDict.Count > 0)
            {
                var mlScore = AnalyzeWithMachineLearning(userInputDict);
                score += mlScore * 0.1; // 机器学习分析占10%权重
                factors++;
            }

            // 如果没有提供任何行为数据，返回0分
            if (factors == 0)
            {
                return 0.0;
            }

            // 归一化分数
            return Math.Min(1.0, score);
        }
        catch (Exception ex)
        {
            TaktLogger.Warning(ex, "[Captcha Behavior] 计算行为分数时发生错误");
            return 0.0;
        }
    }

    /// <summary>
    /// 分析鼠标轨迹
    /// </summary>
    private double AnalyzeMouseTrajectory(object trajectoryObj)
    {
        try
        {
            // 简单的轨迹分析：检查轨迹是否平滑（非机器人特征）
            // 实际应用中可以使用更复杂的算法
            if (trajectoryObj is JArray jArray)
            {
                var points = new List<(double x, double y)>();
                foreach (var item in jArray.Children())
                {
                    if (item is JObject pointObj)
                    {
                        var xToken = pointObj["x"];
                        var yToken = pointObj["y"];
                        var x = xToken != null && (xToken.Type == JTokenType.Float || xToken.Type == JTokenType.Integer) ? xToken.ToObject<double>() : 0;
                        var y = yToken != null && (yToken.Type == JTokenType.Float || yToken.Type == JTokenType.Integer) ? yToken.ToObject<double>() : 0;
                        points.Add((x, y));
                    }
                }

                if (points.Count < 2)
                    return 0.0;
                if (points.Count < _options.MinTrajectoryPoints)
                {
                    // 点数不足，视为无效轨迹（机器人或异常）
                    return 0.0;
                }

                // 计算轨迹的平滑度（相邻点之间的角度变化）
                var smoothness = CalculateTrajectorySmoothness(points);
                return smoothness;
            }
        }
        catch (Exception ex)
        {
            TaktLogger.Warning(ex, "[Captcha Behavior] 分析鼠标轨迹时发生错误");
        }

        return 0.5; // 默认中等分数
    }

    /// <summary>
    /// 计算轨迹平滑度
    /// </summary>
    private double CalculateTrajectorySmoothness(List<(double x, double y)> points)
    {
        if (points.Count < 3)
        {
            return 0.5;
        }

        var angleChanges = new List<double>();
        for (int i = 1; i < points.Count - 1; i++)
        {
            var dx1 = points[i].x - points[i - 1].x;
            var dy1 = points[i].y - points[i - 1].y;
            var dx2 = points[i + 1].x - points[i].x;
            var dy2 = points[i + 1].y - points[i].y;

            var angle1 = Math.Atan2(dy1, dx1);
            var angle2 = Math.Atan2(dy2, dx2);
            var angleDiff = Math.Abs(angle2 - angle1);
            if (angleDiff > Math.PI)
            {
                angleDiff = 2 * Math.PI - angleDiff;
            }
            angleChanges.Add(angleDiff);
        }

        // 平滑的轨迹角度变化应该较小
        var avgAngleChange = angleChanges.Average();
        var smoothness = Math.Max(0, 1.0 - (avgAngleChange / Math.PI));
        return smoothness;
    }

    /// <summary>
    /// 使用机器学习分析（简化版本）
    /// </summary>
    private double AnalyzeWithMachineLearning(Dictionary<string, object> behaviorData)
    {
        // 这里可以实现更复杂的机器学习算法
        // 目前返回一个基于数据完整性的简单分数
        var dataCompleteness = behaviorData.Count / 10.0; // 假设10个特征为完整
        return Math.Min(1.0, dataCompleteness);
    }

    /// <summary>
    /// 清理过期数据
    /// </summary>
    private async Task CleanupExpiredDataAsync()
    {
        while (true)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMinutes(5)); // 每5分钟清理一次

                var expiredKeys = new List<string>();
                var expirationTime = DateTime.UtcNow.AddMinutes(-_expirationMinutes);

                foreach (var kvp in _captchaStore)
                {
                    if (kvp.Value.CreatedAt < expirationTime)
                    {
                        expiredKeys.Add(kvp.Key);
                    }
                }

                foreach (var key in expiredKeys)
                {
                    _captchaStore.TryRemove(key, out _);
                }

                if (expiredKeys.Count > 0)
                {
                    TaktLogger.Debug("[Captcha Behavior] 清理了 {Count} 个过期的行为验证码数据", expiredKeys.Count);
                }
            }
            catch (Exception ex)
            {
                TaktLogger.Error(ex, "[Captcha Behavior] 清理过期数据时发生错误");
            }
        }
    }
}
