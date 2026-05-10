@echo off
chcp 65001 >nul
REM ========================================
REM Takt 代码生成工具便捷脚本
REM ========================================

echo ========================================
echo   Takt 代码生成工具
echo ========================================
echo.

REM 检查是否为全量生成
set GENERATE_ALL=0
if "%1"=="--all" (
    set GENERATE_ALL=1
    shift
)

REM 如果不是全量生成，必须指定实体名称
if "%GENERATE_ALL%"=="0" (
    if "%1"=="" (
        echo ❌ 错误：必须指定实体名称或使用 --all 参数
        echo.
        echo 使用方法：
        echo   generate TaktEmployee                     生成单个实体的所有代码
        echo   generate TaktEmployee --dry-run           预览模式
        echo   generate --all                            全量生成所有实体（谨慎）
        echo.
        echo 生成顺序：
        echo   1. DTO（从实体自动生成）
        echo   2. 服务接口（从DTO）
        echo   3. 服务实现（从接口）
        echo   4. 控制器（从接口）
        echo   5. 验证器（从实体）
        echo   6. 前端API（从控制器）
        echo   7. 实体翻译种子数据（从实体）
        echo.
        exit /b 1
    )
    set ENTITY=%1
    shift
    echo 🎯 目标实体: %ENTITY%
) else (
    echo 🔍 全量生成所有实体（谨慎操作）
)
echo.

REM 构建参数：如果是全量生成，使用 --all，否则使用 --entity=
if "%GENERATE_ALL%"=="1" (
    set PARAM=--all
) else (
    set PARAM=--entity=%ENTITY%
)

REM 传递额外参数（如 --dry-run）
set EXTRA_PARAMS=%*

echo [1/7] 生成DTO（从实体）...
node generate_dto.cjs %PARAM% %EXTRA_PARAMS%
if %ERRORLEVEL% NEQ 0 (
    echo ❌ DTO生成失败
    exit /b 1
)
echo.

echo [2/7] 生成服务接口（从DTO）...
node generate_service_interfaces.cjs %PARAM% %EXTRA_PARAMS%
if %ERRORLEVEL% NEQ 0 (
    echo ❌ 服务接口生成失败
    exit /b 1
)
echo.

echo [3/7] 生成服务实现（从接口）...
node generate_service_implementations.cjs %PARAM% %EXTRA_PARAMS%
if %ERRORLEVEL% NEQ 0 (
    echo ❌ 服务实现生成失败
    exit /b 1
)
echo.

echo [4/7] 生成控制器（从接口）...
node generate_controllers.cjs %PARAM% %EXTRA_PARAMS%
if %ERRORLEVEL% NEQ 0 (
    echo ❌ 控制器生成失败
    exit /b 1
)
echo.

echo [5/7] 生成验证器（从实体）...
node generate_validators.cjs %PARAM% %EXTRA_PARAMS%
if %ERRORLEVEL% NEQ 0 (
    echo ❌ 验证器生成失败
    exit /b 1
)
echo.

echo [6/7] 生成前端API（从控制器）...
REM 全量生成时前端API也由脚本自动扫描，无需指定控制器
if "%GENERATE_ALL%"=="1" (
    node generate_frontend_api.cjs --all %EXTRA_PARAMS%
) else (
    REM 实体名已经有Takt前缀，直接加s即可：TaktEmployee -> TaktEmployees
    node generate_frontend_api.cjs --entity=%ENTITY% %EXTRA_PARAMS%
)
if %ERRORLEVEL% NEQ 0 (
    echo ❌ 前端API生成失败
    exit /b 1
)
echo.

echo [7/7] 生成实体翻译种子数据（从实体）...
node generate_entities_seed_data.cjs %PARAM% %EXTRA_PARAMS%
if %ERRORLEVEL% NEQ 0 (
    echo ❌ 实体翻译种子数据生成失败
    exit /b 1
)
echo.

echo ========================================
if "%GENERATE_ALL%"=="1" (
    echo   ✅ 全量生成完成！
) else (
    echo   ✅ 实体 %ENTITY% 生成完成！
)
echo ========================================
