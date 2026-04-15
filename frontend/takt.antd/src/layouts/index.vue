<template>
  <TaktWatermark>
    <SideLayout v-if="settingSafe.layout === 'side'" />
    <TopLayout v-else-if="settingSafe.layout === 'top'" />
    <MixLayout v-else-if="settingSafe.layout === 'mix'" />
    <SideLayout v-else />
  </TaktWatermark>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { storeToRefs } from 'pinia'
import { defaultSetting, useSettingStore } from '@/stores/setting'
import TaktWatermark from '@/components/navigation/takt-watermark/index.vue'
import SideLayout from './side/index.vue'
import TopLayout from './top/index.vue'
import MixLayout from './mix/index.vue'

const { setting } = storeToRefs(useSettingStore())
const settingSafe = computed(() => setting.value ?? defaultSetting)
</script>
