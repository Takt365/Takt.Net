# 敏感词过滤API使用说明

## API基础路径

```
/api/TaktWordFilters
```

## 接口列表

### 1. 检查文本是否包含敏感词

**接口**: `POST /api/TaktWordFilters/check`

**权限**: 允许匿名访问（`[AllowAnonymous]`）

**请求体**:
```json
{
  "text": "这是一段测试文本"
}
```

**响应**:
```json
{
  "code": 200,
  "message": "操作成功",
  "data": {
    "containsIllegalWords": true,
    "illegalWords": ["敏感词1", "敏感词2"]
  },
  "success": true
}
```

**使用示例**:
```javascript
// JavaScript/Axios
const response = await axios.post('/api/TaktWordFilters/check', {
  text: '这是一段包含敏感词的文本'
});

if (response.data.data.containsIllegalWords) {
  console.log('包含敏感词:', response.data.data.illegalWords);
}
```

### 2. 查找文本中的所有敏感词（支持详细信息）

**接口**: `POST /api/TaktWordFilters/find`

**权限**: 允许匿名访问（`[AllowAnonymous]`）

**请求体**:
```json
{
  "text": "这是一段测试文本",
  "includeDetails": true
}
```

**响应**:
```json
{
  "code": 200,
  "message": "操作成功",
  "data": {
    "illegalWords": ["敏感词1", "敏感词2"],
    "illegalWordDetails": [
      {
        "keyword": "敏感词1",
        "start": 5,
        "end": 8
      },
      {
        "keyword": "敏感词2",
        "start": 15,
        "end": 18
      }
    ]
  },
  "success": true
}
```

**使用示例**:
```javascript
const response = await axios.post('/api/TaktWordFilters/find', {
  text: '这是一段包含敏感词的文本',
  includeDetails: true
});

// 获取敏感词列表
const words = response.data.data.illegalWords;

// 获取详细信息（包含位置）
const details = response.data.data.illegalWordDetails;
details.forEach(detail => {
  console.log(`敏感词: ${detail.keyword}, 位置: ${detail.start}-${detail.end}`);
});
```

### 3. 替换文本中的敏感词

**接口**: `POST /api/TaktWordFilters/replace`

**权限**: 允许匿名访问（`[AllowAnonymous]`）

**请求体**:
```json
{
  "text": "这是一段包含敏感词的文本",
  "replaceChar": "*",
  "replaceText": "***"
}
```

**说明**:
- `replaceChar`: 单个字符替换（如 `"*"`）
- `replaceText`: 字符串替换（如 `"***"`）
- 如果两者都提供，优先使用 `replaceText`
- 如果都不提供，默认使用 `"*"` 字符替换

**响应**:
```json
{
  "code": 200,
  "message": "操作成功",
  "data": {
    "originalText": "这是一段包含敏感词的文本",
    "replacedText": "这是一段包含***的文本",
    "replacedCount": 1
  },
  "success": true
}
```

**使用示例**:
```javascript
// 使用字符替换
const response1 = await axios.post('/api/TaktWordFilters/replace', {
  text: '这是一段包含敏感词的文本',
  replaceChar: '*'
});

// 使用字符串替换
const response2 = await axios.post('/api/TaktWordFilters/replace', {
  text: '这是一段包含敏感词的文本',
  replaceText: '***'
});

console.log('替换后:', response2.data.data.replacedText);
```

### 4. 高亮文本中的敏感词

**接口**: `POST /api/TaktWordFilters/highlight`

**权限**: 允许匿名访问（`[AllowAnonymous]`）

**请求体**:
```json
{
  "text": "这是一段包含敏感词的文本",
  "highlightClass": "illegal-word"
}
```

**响应**:
```json
{
  "code": 200,
  "message": "操作成功",
  "data": {
    "originalText": "这是一段包含敏感词的文本",
    "highlightedText": "这是一段包含<span class=\"illegal-word\">敏感词</span>的文本",
    "highlightedCount": 1
  },
  "success": true
}
```

**使用示例**:
```javascript
const response = await axios.post('/api/TaktWordFilters/highlight', {
  text: '这是一段包含敏感词的文本',
  highlightClass: 'illegal-word'
});

// 在Vue中使用 v-html 渲染
// <div v-html="response.data.data.highlightedText"></div>
```

### 5. 获取敏感词统计信息

**接口**: `GET /api/TaktWordFilters/stats`

**权限**: 需要认证（`routine:wordfilter:stats`）

**响应**:
```json
{
  "code": 200,
  "message": "操作成功",
  "data": {
    "totalCount": 500,
    "isInitialized": true
  },
  "success": true
}
```

**使用示例**:
```javascript
const response = await axios.get('/api/TaktWordFilters/stats');
console.log('敏感词总数:', response.data.data.totalCount);
```

### 6. 获取所有敏感词列表

**接口**: `GET /api/TaktWordFilters/words`

**权限**: 需要认证（`routine:wordfilter:list`）

**响应**:
```json
{
  "code": 200,
  "message": "操作成功",
  "data": [
    "敏感词1",
    "敏感词2",
    "敏感词3"
  ],
  "success": true
}
```

**使用示例**:
```javascript
const response = await axios.get('/api/TaktWordFilters/words');
const words = response.data.data;
console.log('敏感词列表:', words);
```

### 7. 添加敏感词

**接口**: `POST /api/TaktWordFilters/words`

**权限**: 需要认证（`routine:wordfilter:add`）

**请求体**:
```json
{
  "words": ["新敏感词1", "新敏感词2", "新敏感词3"]
}
```

**响应**:
```json
{
  "code": 200,
  "message": "成功添加 3 个敏感词",
  "data": {
    "addedCount": 3,
    "totalCount": 503
  },
  "success": true
}
```

**使用示例**:
```javascript
const response = await axios.post('/api/TaktWordFilters/words', {
  words: ['新敏感词1', '新敏感词2']
});

console.log('新增数量:', response.data.data.addedCount);
console.log('总数量:', response.data.data.totalCount);
```

### 8. 移除敏感词

**接口**: `DELETE /api/TaktWordFilters/words`

**权限**: 需要认证（`routine:wordfilter:remove`）

**请求体**:
```json
{
  "words": ["敏感词1", "敏感词2"]
}
```

**响应**:
```json
{
  "code": 200,
  "message": "成功移除 2 个敏感词",
  "data": {
    "removedCount": 2,
    "remainingCount": 501
  },
  "success": true
}
```

**使用示例**:
```javascript
const response = await axios.delete('/api/TaktWordFilters/words', {
  data: {
    words: ['敏感词1', '敏感词2']
  }
});

console.log('移除数量:', response.data.data.removedCount);
console.log('剩余数量:', response.data.data.remainingCount);
```

### 9. 清空敏感词库

**接口**: `DELETE /api/TaktWordFilters/clear`

**权限**: 需要认证（`routine:wordfilter:clear`）

**响应**:
```json
{
  "code": 200,
  "message": "敏感词库已清空",
  "data": null,
  "success": true
}
```

**使用示例**:
```javascript
const response = await axios.delete('/api/TaktWordFilters/clear');
console.log(response.data.message);
```

## 前端集成示例

### Vue 3 + TypeScript 示例

```typescript
// api/wordFilter.ts
import axios from 'axios';

export interface CheckTextRequest {
  text: string;
}

export interface CheckTextResponse {
  containsIllegalWords: boolean;
  illegalWords: string[];
}

export const wordFilterApi = {
  // 检查文本
  async checkText(text: string): Promise<CheckTextResponse> {
    const response = await axios.post('/api/TaktWordFilters/check', { text });
    return response.data.data;
  },

  // 替换敏感词
  async replaceWords(text: string, replaceText: string = '***'): Promise<string> {
    const response = await axios.post('/api/TaktWordFilters/replace', {
      text,
      replaceText
    });
    return response.data.data.replacedText;
  },

  // 高亮敏感词
  async highlightWords(text: string, highlightClass: string = 'illegal-word'): Promise<string> {
    const response = await axios.post('/api/TaktWordFilters/highlight', {
      text,
      highlightClass
    });
    return response.data.data.highlightedText;
  }
};
```

### 在表单验证中使用

```vue
<template>
  <el-form :model="form" :rules="rules" ref="formRef">
    <el-form-item label="用户名" prop="userName">
      <el-input 
        v-model="form.userName" 
        @blur="validateUserName"
        placeholder="请输入用户名"
      />
      <div v-if="illegalWords.length > 0" class="error-tip">
        包含敏感词: {{ illegalWords.join('、') }}
      </div>
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { wordFilterApi } from '@/api/wordFilter';

const form = ref({ userName: '' });
const illegalWords = ref<string[]>([]);

const validateUserName = async () => {
  if (!form.value.userName) return;
  
  const result = await wordFilterApi.checkText(form.value.userName);
  if (result.containsIllegalWords) {
    illegalWords.value = result.illegalWords;
    // 可以在这里阻止提交或显示错误提示
  } else {
    illegalWords.value = [];
  }
};
</script>
```

### 实时输入验证

```vue
<template>
  <el-input 
    v-model="inputText" 
    @input="handleInput"
    placeholder="输入内容，敏感词会自动替换"
  />
  <div v-html="displayText"></div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';
import { wordFilterApi } from '@/api/wordFilter';

const inputText = ref('');
const displayText = ref('');

// 防抖处理
let timer: NodeJS.Timeout;
const handleInput = () => {
  clearTimeout(timer);
  timer = setTimeout(async () => {
    if (inputText.value) {
      // 实时高亮敏感词
      displayText.value = await wordFilterApi.highlightWords(inputText.value);
    } else {
      displayText.value = '';
    }
  }, 300);
};
</script>

<style scoped>
:deep(.illegal-word) {
  background-color: #ffebee;
  color: #c62828;
  padding: 2px 4px;
  border-radius: 2px;
}
</style>
```

## 权限说明

### 匿名访问接口（无需登录）
- `POST /api/TaktWordFilters/check` - 检查敏感词
- `POST /api/TaktWordFilters/find` - 查找敏感词
- `POST /api/TaktWordFilters/replace` - 替换敏感词
- `POST /api/TaktWordFilters/highlight` - 高亮敏感词

### 需要认证的接口（需要登录）
- `GET /api/TaktWordFilters/stats` - 获取统计信息
- `GET /api/TaktWordFilters/words` - 获取敏感词列表
- `POST /api/TaktWordFilters/words` - 添加敏感词
- `DELETE /api/TaktWordFilters/words` - 移除敏感词
- `DELETE /api/TaktWordFilters/clear` - 清空敏感词库

## 错误处理

所有接口统一返回 `TaktApiResult<T>` 格式：

```json
{
  "code": 400,
  "message": "错误信息",
  "data": null,
  "success": false
}
```

**常见错误码**:
- `200` - 成功
- `400` - 参数错误
- `401` - 未授权
- `403` - 无权限
- `500` - 服务器错误

## 注意事项

1. **性能考虑**: 敏感词检查是同步操作，对于大量文本建议分批处理
2. **实时验证**: 前端可以使用防抖（debounce）来减少API调用频率
3. **缓存策略**: 可以考虑在前端缓存敏感词列表，减少服务器请求
4. **安全性**: 添加/删除敏感词需要管理员权限，普通用户只能查询和检查
5. **数据持久化**: 添加的敏感词只在内存中，重启后需要重新加载文件

## 完整示例

### React + TypeScript 示例

```typescript
import axios from 'axios';

// 检查文本是否包含敏感词
export const checkSensitiveWords = async (text: string) => {
  try {
    const response = await axios.post('/api/TaktWordFilters/check', { text });
    return response.data.data;
  } catch (error) {
    console.error('检查敏感词失败:', error);
    return { containsIllegalWords: false, illegalWords: [] };
  }
};

// 替换敏感词
export const replaceSensitiveWords = async (text: string) => {
  try {
    const response = await axios.post('/api/TaktWordFilters/replace', {
      text,
      replaceText: '***'
    });
    return response.data.data.replacedText;
  } catch (error) {
    console.error('替换敏感词失败:', error);
    return text;
  }
};

// 使用示例
const MyComponent = () => {
  const [input, setInput] = useState('');
  const [filtered, setFiltered] = useState('');

  const handleChange = async (value: string) => {
    setInput(value);
    const replaced = await replaceSensitiveWords(value);
    setFiltered(replaced);
  };

  return (
    <div>
      <input value={input} onChange={(e) => handleChange(e.target.value)} />
      <div>{filtered}</div>
    </div>
  );
};
```
