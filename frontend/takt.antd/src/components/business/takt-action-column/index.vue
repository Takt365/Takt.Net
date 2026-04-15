<template>
  <!-- 此文件仅用于样式定义，函数实现在 index.ts 中 -->
</template>

<script setup lang="ts">
// 函数和接口已移至 index.ts
</script>

<style scoped lang="less">
.takt-action-column {
  padding-left: 4px;
  padding-right: 4px;
  
  :deep(.ant-space) {
    flex-wrap: nowrap;
    gap: 4px;
  }
  
  // 确保"更多"按钮和下拉菜单可见
  :deep(.ant-dropdown),
  :deep(.takt-action-column-dropdown) {
    z-index: 1060 !important;
  }
  
  // 确保操作列按钮的层级和宽度
  :deep(.ant-btn) {
    position: relative;
    z-index: 1;
    width: 24px;
    min-width: 24px;
    padding: 0;
    display: inline-flex;
    align-items: center;
    justify-content: center;
  }
  
  // "更多"按钮确保可见
  :deep(.takt-button-more) {
    position: relative;
    z-index: 2;
  }
}
</style>

<style lang="less">
@import '../assets/styles/color-base.less';

// 全局样式：确保操作列下拉菜单在所有表格元素上方
// 注意：Ant Design 的 Dropdown 会生成多层 DOM，需要覆盖所有层级
.takt-action-column-dropdown {
  z-index: 1060 !important;
  
  // 确保下拉菜单容器完全没有背景
  background-color: transparent !important;
  background: transparent !important;
  box-shadow: none !important;
  padding: 0 !important;
  
  // 覆盖 Ant Design 的下拉菜单默认样式（第一层）
  .ant-dropdown-menu {
    background-color: transparent !important;
    background: transparent !important;
    box-shadow: none !important;
    padding: 0 !important;
    border: none !important;
    margin: 0 !important;
  }
  
  // 覆盖可能存在的 wrapper 层
  .ant-dropdown-menu-wrapper,
  .ant-dropdown-menu-container {
    background-color: transparent !important;
    background: transparent !important;
    box-shadow: none !important;
    padding: 0 !important;
  }
}

// 操作列下拉菜单样式：为菜单项应用按钮颜色
.takt-action-column-dropdown-menu {
  // 确保整个菜单容器也没有背景
  &.ant-menu {
    background-color: transparent !important;
    background: transparent !important;
    box-shadow: none !important;
    padding: 0 !important;
    border: none !important;
  }
  
  // 使用通用选择器为所有按钮类的菜单项应用颜色
  [class^="takt-button-"].ant-menu-item {
    // 默认状态：图标和文本使用按钮的主题色，完全透明背景
    color: inherit !important;
    background-color: transparent !important;
    background: transparent !important;
    
    // 悬停状态：保持完全透明背景，只显示主题色文本和图标
    &:hover:not(.ant-menu-item-disabled),
    &:hover:not(.ant-menu-item-disabled):not(.ant-menu-item-selected) {
      background-color: transparent !important;
      background: transparent !important;
      color: inherit !important;
      
      // 图标保持主题色
      i,
      svg,
      .anticon {
        color: inherit !important;
      }
    }
    
    // 激活状态：保持完全透明背景
    &.ant-menu-item-selected,
    &.ant-menu-item-selected:hover,
    &:active:not(.ant-menu-item-disabled) {
      background-color: transparent !important;
      background: transparent !important;
      color: inherit !important;
      
      i,
      svg,
      .anticon {
        color: inherit !important;
      }
    }
  }
  
  // 覆盖所有 Ant Design 菜单项的默认背景样式
  .ant-menu-item {
    background-color: transparent !important;
    background: transparent !important;
    box-shadow: none !important;
    border: none !important;
    margin: 0 !important;
    padding: 4px 8px !important;
    
    // 移除所有可能的伪元素背景
    &::before,
    &::after {
      display: none !important;
      background: transparent !important;
      content: none !important;
    }
    
    // 覆盖所有状态
    &,
    &:hover,
    &:focus,
    &:active,
    &.ant-menu-item-selected,
    &.ant-menu-item-selected:hover,
    &.ant-menu-item-active,
    &:not(.ant-menu-item-disabled):hover,
    &:not(.ant-menu-item-disabled):active {
      background-color: transparent !important;
      background: transparent !important;
      box-shadow: none !important;
      border: none !important;
      
      &::before,
      &::after {
        display: none !important;
        background: transparent !important;
        content: none !important;
      }
    }
    
    // 覆盖 Ant Design 4.x 可能的内部元素
    .ant-menu-title-content {
      background-color: transparent !important;
      background: transparent !important;
    }
  }
  
  // 移除菜单的分隔线
  .ant-menu-item-divider {
    display: none !important;
  }
  
  // 为每个具体的按钮类设置颜色
  .takt-button-query.ant-menu-item { color: @qun-qing; }
  .takt-button-create.ant-menu-item { color: @bao-lan; }
  .takt-button-update.ant-menu-item { color: @cong-qian; }
  .takt-button-delete.ant-menu-item { color: @shan-dan; }
  .takt-button-detail.ant-menu-item { color: @shi-qing; }
  .takt-button-preview.ant-menu-item { color: @wei-lan; }
  .takt-button-print.ant-menu-item { color: @cong-qian; }
  .takt-button-import.ant-menu-item { color: @qing-lian; }
  .takt-button-export.ant-menu-item { color: @terigaki; }
  .takt-button-template.ant-menu-item { color: @qie-lan; }
  .takt-button-approve.ant-menu-item { color: @zhu-sha; }
  .takt-button-revoke.ant-menu-item { color: @fei-hong; }
  .takt-button-copy.ant-menu-item { color: @jian-de; }
  .takt-button-clone.ant-menu-item { color: @cang-cang; }
  .takt-button-suspend.ant-menu-item { color: @zhu-biao; }
  .takt-button-resume.ant-menu-item { color: #52c41a; }
  .takt-button-submit.ant-menu-item { color: @zhu-sha; }
  .takt-button-withdraw.ant-menu-item { color: @shi-liu-hong; }
  .takt-button-transfer.ant-menu-item { color: @fei-se; }
  .takt-button-delegate.ant-menu-item { color: @tao-hong; }
  .takt-button-return.ant-menu-item { color: @hai-tang-hong; }
  .takt-button-urge.ant-menu-item { color: @ying-tao-se; }
  .takt-button-addsign.ant-menu-item { color: @tuo-yan; }
  .takt-button-subsign.ant-menu-item { color: @yin-hong; }
  .takt-button-progress.ant-menu-item { color: @dian-qing; }
  .takt-button-history.ant-menu-item { color: @hua-qing; }
  .takt-button-publish.ant-menu-item { color: @zang-qing; }
  .takt-button-disable.ant-menu-item { color: @dai; }
  .takt-button-enable.ant-menu-item { color: #52c41a; }
  .takt-button-version.ant-menu-item { color: @zi-tan; }
  .takt-button-design.ant-menu-item { color: @gan-qing; }
  .takt-button-config.ant-menu-item { color: @zi-tang; }
  .takt-button-validate.ant-menu-item { color: @qing-lian; }
  .takt-button-start.ant-menu-item { color: @qun-qing-2; }
  .takt-button-terminate.ant-menu-item { color: @jiang-zi; }
  .takt-button-field.ant-menu-item { color: @xue-qing; }
  .takt-button-permission.ant-menu-item { color: @ding-xiang-se; }
  .takt-button-datasource.ant-menu-item { color: @ou-se; }
  .takt-button-theme.ant-menu-item { color: @ou-he-se; }
  .takt-button-data.ant-menu-item { color: @huo-hong; }
  .takt-button-archive.ant-menu-item { color: @yan-zhi; }
  .takt-button-clean.ant-menu-item { color: @zhu-hong; }
  .takt-button-draft.ant-menu-item { color: @dan; }
  .takt-button-deletedraft.ant-menu-item { color: @tong; }
  .takt-button-send.ant-menu-item { color: @tuo-hong; }
  .takt-button-forward.ant-menu-item { color: @yan; }
  .takt-button-reply.ant-menu-item { color: @qian-se; }
  .takt-button-read.ant-menu-item { color: @wan; }
  .takt-button-unread.ant-menu-item { color: @tan; }
  .takt-button-circulate.ant-menu-item { color: @yan-hong; }
  .takt-button-sign.ant-menu-item { color: @zao-hong; }
  .takt-button-confirm.ant-menu-item { color: @yin-hong-2; }
  .takt-button-like.ant-menu-item { color: @he-chi; }
  .takt-button-unlike.ant-menu-item { color: @yin-zhu; }
  .takt-button-favorite.ant-menu-item { color: @chi; }
  .takt-button-unfavorite.ant-menu-item { color: @li-se; }
  .takt-button-share.ant-menu-item { color: @xuan-se; }
  .takt-button-unshare.ant-menu-item { color: @song-hua-se; }
  .takt-button-comment.ant-menu-item { color: @liu-huang; }
  .takt-button-uncomment.ant-menu-item { color: @nen-lv; }
  .takt-button-flagging.ant-menu-item { color: @liu-lv; }
  .takt-button-unflagging.ant-menu-item { color: @cong-huang; }
  .takt-button-follow.ant-menu-item { color: @cong-lv; }
  .takt-button-unfollow.ant-menu-item { color: @dou-lv; }
  .takt-button-upload.ant-menu-item { color: @dou-qing; }
  .takt-button-download.ant-menu-item { color: @you-lv; }
  .takt-button-destroy.ant-menu-item { color: @cong-qian; }
  .takt-button-run.ant-menu-item { color: @qing-cong; }
  .takt-button-stop.ant-menu-item { color: @shi-lv; }
  .takt-button-restart.ant-menu-item { color: @song-bai-lv; }
  .takt-button-refresh.ant-menu-item { color: @song-hua-lv; }
  .takt-button-reset.ant-menu-item { color: @lan-hui; }
  .takt-button-empty.ant-menu-item { color: @nadeshiko; }
  .takt-button-authorize.ant-menu-item { color: @koubai; }
  .takt-button-allocate.ant-menu-item { color: @suou; }
  .takt-button-allocate-user-role.ant-menu-item { color: @cong-qian; }
  .takt-button-allocate-role-menu.ant-menu-item { color: @qing-cong; }
  .takt-button-allocate-dept-role.ant-menu-item { color: @shi-lv; }
  .takt-button-allocate-dept-user.ant-menu-item { color: @song-bai-lv; }
  .takt-button-allocate-post-user.ant-menu-item { color: @song-hua-lv; }
  .takt-button-allocate-tenant-user.ant-menu-item { color: @lv-chen; }
  .takt-button-resetpwd.ant-menu-item { color: @taikou; }
  .takt-button-changepwd.ant-menu-item { color: @hitotsu-tsuki-zome; }
  .takt-button-truncate.ant-menu-item { color: @kuwa-zome; }
  .takt-button-calculate.ant-menu-item { color: @ichigo; }
  .takt-button-fullscreen.ant-menu-item { color: @jian-de; }
  .takt-button-expand.ant-menu-item { color: @cang-cang; }
}

// 全局样式：确保覆盖所有可能的 Dropdown 层级
// 专门针对 Dropdown 的弹出层容器
.ant-dropdown.takt-action-column-dropdown,
.ant-dropdown.takt-action-column-dropdown .ant-dropdown-menu,
.ant-dropdown.takt-action-column-dropdown .ant-dropdown-menu-container,
.ant-dropdown.takt-action-column-dropdown .ant-dropdown-menu-wrapper {
  background-color: transparent !important;
  background: transparent !important;
  box-shadow: none !important;
  padding: 0 !important;
  border: none !important;
}

// 使用更高优先级的选择器覆盖所有可能的层级
body .takt-action-column-dropdown {
  background-color: transparent !important;
  background: transparent !important;
  
  .ant-dropdown-menu,
  .ant-dropdown-menu-container,
  .ant-dropdown-menu-wrapper,
  .ant-menu {
    background-color: transparent !important;
    background: transparent !important;
    box-shadow: none !important;
  }
}
</style>


