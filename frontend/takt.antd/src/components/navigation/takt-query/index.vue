<template>
  <a-input-search
    v-model:value="query"
    :placeholder="$t('common.form.placeholder.searchMenu')"
    class="takt-query"
    @search="handleSearch"
    @change="handleChange"
    allow-clear
  >
    <template #prefix>
      <RiSearchLine />
    </template>
  </a-input-search>
  <a-dropdown
    v-model:open="dropdownVisible"
    :trigger="['click']"
    placement="bottomLeft"
    class="takt-query-dropdown"
  >
    <template #overlay>
      <a-menu v-if="searchResults.length > 0">
        <a-menu-item
          v-for="item in searchResults"
          :key="item.path"
          @click="handleSelect(item.path)"
        >
          <div class="search-result-item">
            <span class="result-title">{{ item.title }}</span>
            <span class="result-path">{{ item.path }}</span>
          </div>
        </a-menu-item>
      </a-menu>
      <div v-else class="no-results">
        {{ $t('common.msg.noSearchResult') }}
      </div>
    </template>
  </a-dropdown>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useRouter } from 'vue-router'
import { useMenuStore } from '@/stores/identity/menu'
import { RiSearchLine } from '@remixicon/vue'

interface SearchResult {
  title: string
  path: string
}

const router = useRouter()
const menuStore = useMenuStore()

const query = ref('')
const dropdownVisible = ref(false)

const searchResults = computed(() => {
  if (!query.value.trim()) {
    return []
  }
  
  const results: SearchResult[] = []
  const searchText = query.value.toLowerCase()
  
  const searchInMenu = (items: any[]) => {
    items.forEach(item => {
      const title = item.title || item.label || ''
      const path = item.key || ''
      
      if (title.toLowerCase().includes(searchText) || path.toLowerCase().includes(searchText)) {
        results.push({
          title,
          path
        })
      }
      
      if (item.children && item.children.length > 0) {
        searchInMenu(item.children)
      }
    })
  }
  
  searchInMenu(menuStore.menuItems)
  
  return results.slice(0, 10) // 限制最多显示10个结果
})

watch(searchResults, (results) => {
  dropdownVisible.value = results.length > 0 && query.value.trim() !== ''
})

const handleSearch = (value: string) => {
  if (searchResults.value.length > 0) {
    handleSelect(searchResults.value[0].path)
  }
}

const handleChange = () => {
  // 输入变化时自动显示下拉
}

const handleSelect = (path: string) => {
  router.push(path)
  query.value = ''
  dropdownVisible.value = false
}
</script>

<style scoped lang="less">
.takt-query {
  width: 300px;

  :deep(svg) {
    font-size: 16px !important;
    width: 16px !important;
    height: 16px !important;
    display: inline-flex !important;
    align-items: center !important;
    justify-content: center !important;
    vertical-align: middle !important;
    color: var(--ant-color-text);
    fill: currentColor;
  }
}

.takt-query-dropdown {
  .search-result-item {
    display: flex;
    flex-direction: column;
    
    .result-title {
      font-weight: 500;
    }
    
    .result-path {
      font-size: 12px;
      color: rgba(0, 0, 0, 0.45);
      margin-top: 2px;
    }
  }
  
  .no-results {
    padding: 12px;
    text-align: center;
    color: rgba(0, 0, 0, 0.45);
  }
}
</style>
