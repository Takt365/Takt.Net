// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktProductionTeamSeedData.cs
// 创建时间：2026-03-16
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktProductionTeam 实体种子数据，初始化示例生产班组主数据（用于替代原 prod_team_category 字典）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Logistics.Manufacturing.Output;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt 生产班组种子数据
/// </summary>
public class TaktProductionTeamSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（放在部门等组织结构之后，生产相关种子之前）
    /// </summary>
    public int Order => 25;

    /// <summary>
    /// 初始化生产班组种子数据
    /// </summary>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var teamRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktProductionTeam>>();

        int insertCount = 0;
        int updateCount = 0;

        // 说明：
        // 1. 这里初始化原 prod_team_category 中的生产班组列表，按业务顺序排列；
        // 2. PlantCode 使用实际工厂代码 "C100"，生产线与班次信息如后续有精细规则，可再扩展。

        // 按你提供的顺序初始化：
        var (team10, i10, u10) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "10班",  "10班",  null, null, null); insertCount += i10; updateCount += u10;
        var (team11, i11, u11) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "11班",  "11班",  null, null, null); insertCount += i11; updateCount += u11;
        var (team12, i12, u12) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "12班",  "12班",  null, null, null); insertCount += i12; updateCount += u12;
        var (team13, i13, u13) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "13班",  "13班",  null, null, null); insertCount += i13; updateCount += u13;
        var (team14, i14, u14) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "14班",  "14班",  null, null, null); insertCount += i14; updateCount += u14;
        var (team15, i15, u15) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "15班",  "15班",  null, null, null); insertCount += i15; updateCount += u15;

        var (team1smt1, i1smt1, u1smt1) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "1SMT1", "1SMT1", null, null, null); insertCount += i1smt1; updateCount += u1smt1;
        var (team1smt2, i1smt2, u1smt2) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "1SMT2", "1SMT2", null, null, null); insertCount += i1smt2; updateCount += u1smt2;

        var (team1, i1, u1) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "1班",   "1班",   null, null, null); insertCount += i1; updateCount += u1;
        var (team2, i2, u2) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "2班",   "2班",   null, null, null); insertCount += i2; updateCount += u2;

        var (team2a, i2a, u2a) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "2自插A", "2自插A", null, null, null); insertCount += i2a; updateCount += u2a;
        var (team2b, i2b, u2b) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "2自插B", "2自插B", null, null, null); insertCount += i2b; updateCount += u2b;
        var (team2c, i2c, u2c) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "2自插C", "2自插C", null, null, null); insertCount += i2c; updateCount += u2c;
        var (team2d, i2d, u2d) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "2自插D", "2自插D", null, null, null); insertCount += i2d; updateCount += u2d;

        var (team3, i3, u3) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "3班",   "3班",   null, null, null); insertCount += i3; updateCount += u3;
        var (team3a, i3a, u3a) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "3手插A", "3手插A", null, null, null); insertCount += i3a; updateCount += u3a;
        var (team3b, i3b, u3b) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "3手插B", "3手插B", null, null, null); insertCount += i3b; updateCount += u3b;
        var (team3c, i3c, u3c) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "3手插C", "3手插C", null, null, null); insertCount += i3c; updateCount += u3c;

        var (team4, i4, u4) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "4班",   "4班",   null, null, null); insertCount += i4; updateCount += u4;
        var (team4a, i4a, u4a) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "4修正A", "4修正A", null, null, null); insertCount += i4a; updateCount += u4a;
        var (team4b, i4b, u4b) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "4修正B", "4修正B", null, null, null); insertCount += i4b; updateCount += u4b;
        var (team4c, i4c, u4c) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "4修正C", "4修正C", null, null, null); insertCount += i4c; updateCount += u4c;

        var (team5, i5, u5) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "5班",   "5班",   null, null, null); insertCount += i5; updateCount += u5;
        var (team6, i6, u6) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "6班",   "6班",   null, null, null); insertCount += i6; updateCount += u6;
        var (team7, i7, u7) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "7班",   "7班",   null, null, null); insertCount += i7; updateCount += u7;
        var (team8, i8, u8) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "8班",   "8班",   null, null, null); insertCount += i8; updateCount += u8;
        var (team9, i9, u9) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "9班",   "9班",   null, null, null); insertCount += i9; updateCount += u9;

        var (teamIqc, iIqc, uIqc) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "IQC",   "IQC",   null, null, null); insertCount += iIqc; updateCount += uIqc;
        var (teamQa,  iQa,  uQa)  = await CreateOrUpdateTeamAsync(teamRepository, "C100", "QA",    "QA",    null, null, null); insertCount += iQa;  updateCount += uQa;
        var (teamProc, iProc, uProc) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "加工班", "加工班", null, null, null); insertCount += iProc; updateCount += uProc;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新生产班组
    /// </summary>
    private static async Task<(TaktProductionTeam Team, int InsertCount, int UpdateCount)> CreateOrUpdateTeamAsync(
        ITaktRepository<TaktProductionTeam> teamRepository,
        string plantCode,
        string teamCode,
        string teamName,
        string? productionLine,
        string? productionLineName,
        int? shiftNo)
    {
        var team = await teamRepository.GetAsync(t => t.PlantCode == plantCode && t.TeamCode == teamCode);
        if (team == null)
        {
            team = new TaktProductionTeam
            {
                PlantCode = plantCode,
                TeamCode = teamCode,
                TeamName = teamName,
                ProductionLine = productionLine,
                ProductionLineName = productionLineName,
                ShiftNo = shiftNo,
                Status = 0,
                IsDeleted = 0
            };
            team = await teamRepository.CreateAsync(team);
            return (team, 1, 0);
        }

        team.TeamName = teamName;
        team.ProductionLine = productionLine;
        team.ProductionLineName = productionLineName;
        team.ShiftNo = shiftNo;
        team.Status = 0;
        await teamRepository.UpdateAsync(team);
        return (team, 0, 1);
    }
}

