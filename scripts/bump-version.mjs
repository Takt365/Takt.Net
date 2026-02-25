#!/usr/bin/env node
/**
 * 统一版本号自增并同步到前端、后端
 * 格式：0.1.年.月+序号，如 0.1.2026.02001, 0.1.2026.02002
 * 年月取当前日期，序号按月自增。
 */

import fs from 'fs';
import path from 'path';
import { fileURLToPath } from 'url';

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const rootDir = path.resolve(__dirname, '..');
const versionPath = path.join(rootDir, 'version.json');
const backendPropsPath = path.join(rootDir, 'backend', 'src', 'Directory.Build.props');
const frontendPkgPath = path.join(rootDir, 'frontend', 'takt.antd', 'package.json');

function loadVersion() {
  if (!fs.existsSync(versionPath)) {
    const now = new Date();
    return { year: now.getFullYear(), month: now.getMonth() + 1, sequence: 1 };
  }
  const raw = fs.readFileSync(versionPath, 'utf8');
  return JSON.parse(raw);
}

/** 月份规范为 1~12，格式化为 01~12 */
function normalizeMonth(m) {
  const n = Math.max(1, Math.min(12, Number(m) || 1));
  return n;
}

function formatVersion(v) {
  const month = normalizeMonth(v.month);
  const monthSeq = String(month).padStart(2, '0') + String(v.sequence).padStart(3, '0');
  return `0.1.${v.year}.${monthSeq}`;
}

function bumpVersion() {
  const now = new Date();
  const currentYear = now.getFullYear();
  const currentMonth = normalizeMonth(now.getMonth() + 1);

  let data = loadVersion();
  data.month = normalizeMonth(data.month);
  if (data.year === currentYear && data.month === currentMonth) {
    data.sequence += 1;
  } else {
    data = { year: currentYear, month: currentMonth, sequence: 1 };
  }

  fs.writeFileSync(versionPath, JSON.stringify(data, null, 2) + '\n', 'utf8');
  return data;
}

function syncBackend(data) {
  const version = formatVersion(data);
  let content = fs.readFileSync(backendPropsPath, 'utf8');
  content = content.replace(/<Version>[\d.]*<\/Version>/, `<Version>${version}</Version>`);
  content = content.replace(/<AssemblyVersion>[\d.]*<\/AssemblyVersion>/, `<AssemblyVersion>${version}</AssemblyVersion>`);
  content = content.replace(/<FileVersion>[\d.]*<\/FileVersion>/, `<FileVersion>${version}</FileVersion>`);
  if (content.includes('<AssemblyInformationalVersion>')) {
    content = content.replace(/<AssemblyInformationalVersion>[\d.]*<\/AssemblyInformationalVersion>/, `<AssemblyInformationalVersion>${version}</AssemblyInformationalVersion>`);
  } else {
    content = content.replace(/<FileVersion>[\d.]*<\/FileVersion>/, `<FileVersion>${version}</FileVersion>\n    <AssemblyInformationalVersion>${version}</AssemblyInformationalVersion>`);
  }
  fs.writeFileSync(backendPropsPath, content, 'utf8');
}

function syncFrontend(data) {
  const version = formatVersion(data);
  const pkg = JSON.parse(fs.readFileSync(frontendPkgPath, 'utf8'));
  pkg.version = version;
  fs.writeFileSync(frontendPkgPath, JSON.stringify(pkg, null, 2) + '\n', 'utf8');
}

const isInit = process.argv.includes('--init') || process.argv.includes('-i');
if (isInit) {
  const data = loadVersion();
  console.log('当前版本（未自增）:', formatVersion(data));
  syncBackend(data);
  syncFrontend(data);
  console.log('已同步到 backend/src/Directory.Build.props 与 frontend/takt.antd/package.json');
} else {
  const data = bumpVersion();
  const version = formatVersion(data);
  console.log('新版本:', version);
  syncBackend(data);
  syncFrontend(data);
  console.log('已自增并同步到前端、后端');
}
