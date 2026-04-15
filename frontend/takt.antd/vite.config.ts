import { defineConfig, loadEnv } from 'vite'
import vue from '@vitejs/plugin-vue'
import { resolve, dirname } from 'node:path'
import { fileURLToPath } from 'node:url'
import { readFileSync } from 'node:fs'
import mkcert from 'vite-plugin-mkcert'
import { VitePWA } from 'vite-plugin-pwa'
import AutoImport from 'unplugin-auto-import/vite'
import Components from 'unplugin-vue-components/vite'
import { AntDesignVueResolver } from 'unplugin-vue-components/resolvers'
import { vitePluginLogger } from './src/utils/logger'
import dayjs from 'dayjs'

const __filename = fileURLToPath(import.meta.url)
const __dirname = dirname(__filename)

const pkg = JSON.parse(readFileSync(resolve(__dirname, 'package.json'), 'utf-8')) as { name?: string; version?: string; dependencies?: Record<string, string>; devDependencies?: Record<string, string> }
const { dependencies = {}, devDependencies = {}, name = '', version = '' } = pkg
const __APP_INFO__ = {
  pkg: { dependencies, devDependencies, name, version },
  lastBuildTime: dayjs().format('YYYY-MM-DD HH:mm:ss')
}

/** 虚拟模块：应用 import 'virtual:app-info' 即得到 package 信息 JSON 字符串 */
const VIRTUAL_ID = '\0virtual:app-info'
function appInfoPlugin() {
  const payload = JSON.stringify(__APP_INFO__)
  return {
    name: 'app-info',
    resolveId(id: string) {
      return id === 'virtual:app-info' ? VIRTUAL_ID : null
    },
    load(id: string) {
      if (id !== VIRTUAL_ID) return null
      return `export default ${JSON.stringify(payload)}`
    }
  }
}

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => {
  // 加载环境变量
  const env = loadEnv(mode, process.cwd(), '')
  
  return {
    plugins: [
      appInfoPlugin(),
      vue(),
      // 启用 HTTPS（使用 vite-plugin-mkcert 生成受信任的本地证书）
      // vite-plugin-mkcert 会自动安装证书到系统信任存储，无需手动接受
      ...(env.VITE_DEV_SERVER_HTTPS === 'true' ? [mkcert()] : []),
      // PWA 插件配置
      VitePWA({
        registerType: 'autoUpdate',
        includeAssets: ['favicon.ico'],
        manifest: {
          name: 'Takt Digital Factory (TDF)',
          short_name: 'Takt DF',
          description: '节拍数字工厂 · Takt Digital Factory',
          theme_color: '#1890ff',
          background_color: '#ffffff',
          display: 'standalone',
          orientation: 'portrait',
          start_url: '/',
          scope: '/',
          icons: [
            {
              src: '/src/assets/images/takt.svg',
              sizes: '192x192',
              type: 'image/svg+xml',
              purpose: 'any'
            },
            {
              src: '/src/assets/images/takt.svg',
              sizes: '512x512',
              type: 'image/svg+xml',
              purpose: 'any maskable'
            }
          ]
        },
        workbox: {
          globPatterns: ['**/*.{js,css,html,ico,png,svg,woff,woff2}'],
          runtimeCaching: [
            {
              urlPattern: /^https:\/\/.*\.(?:png|jpg|jpeg|svg|gif|webp)$/i,
              handler: 'CacheFirst',
              options: {
                cacheName: 'images-cache',
                expiration: {
                  maxEntries: 50,
                  maxAgeSeconds: 60 * 60 * 24 * 30 // 30 days
                }
              }
            },
            {
              urlPattern: /\/api\/.*/i,
              handler: 'NetworkFirst',
              options: {
                cacheName: 'api-cache',
                networkTimeoutSeconds: 10,
                cacheableResponse: {
                  statuses: [0, 200]
                }
              }
            }
          ]
        },
        devOptions: {
          enabled: false, // 开发环境禁用 PWA，避免干扰
          type: 'module'
        }
      }),
      // API 请求日志插件（开发服务器终端输出）
      vitePluginLogger(),
      // 自动导入 Vue API（ref, reactive, computed, watch 等）
      AutoImport({
        imports: [
          'vue',
          'vue-router',
          'pinia',
          {
            'ant-design-vue': [
              'message',
              'notification',
              'Modal',
              'confirm'
            ]
          },
          {
            '@/utils/logger': [
              'logger'
            ]
          }
        ],
        // 自动扫描 src/components 目录下的文件中的导出函数
        dirs: ['src/components'],
        // 自动导入 Vue 相关类型
        vueTemplate: true,
        // 生成类型声明文件
        dts: true,
        // 生成 ESLint 配置
        eslintrc: {
          enabled: true,
          filepath: './.eslintrc-auto-import.json',
          globalsPropValue: true
        }
      }),
      // 自动导入组件（Vue 组件和 Ant Design Vue 组件）
      Components({
        resolvers: [
          AntDesignVueResolver({
            importStyle: false // css in js，样式由 Ant Design Vue 自动处理
          })
        ],
        // 自动导入 src/components 目录下的组件
        dirs: ['src/components'],
        // 包含的文件类型
        extensions: ['vue', 'tsx'],
        // 生成类型声明文件
        dts: true
      })
    ],
    resolve: {
      alias: {
        '@': resolve(__dirname, 'src'),
        '@api': resolve(__dirname, 'src/api'),
        '@components': resolve(__dirname, 'src/components'),
        '@views': resolve(__dirname, 'src/views'),
        '@stores': resolve(__dirname, 'src/stores'),
        '@utils': resolve(__dirname, 'src/utils'),
        '@types': resolve(__dirname, 'src/types'),
        '@assets': resolve(__dirname, 'src/assets'),
        '@layouts': resolve(__dirname, 'src/layouts'),
        '@router': resolve(__dirname, 'src/router'),
        '@locales': resolve(__dirname, 'src/locales'),
        '@permission': resolve(__dirname, 'src/permission')
      }
    },
    server: {
      port: Number(env.VITE_DEV_SERVER_PORT) || 60081,
      host: env.VITE_DEV_SERVER_HOST || 'localhost',
      // 注意：HTTPS 由 basicSsl 插件自动处理，不需要在这里配置 https
      strictPort: false, // 如果端口被占用，自动尝试下一个可用端口
      // HMR WebSocket 配置：使用 vite-mkcert 后，证书受信任，WebSocket 连接正常
      // 注意：HMR WebSocket 使用与开发服务器相同的协议和端口
      hmr: env.VITE_DEV_SERVER_HTTPS === 'true' ? {
        protocol: 'wss',
        host: env.VITE_DEV_SERVER_HOST || 'localhost',
        port: Number(env.VITE_DEV_SERVER_PORT) || 60081,
        clientPort: Number(env.VITE_DEV_SERVER_PORT) || 60081
      } : undefined,
      // Vite 开发服务器默认支持 history fallback（所有非 API 请求重定向到 index.html）
      // 这确保了刷新页面时不会出现 404 错误
      proxy: {
        // 统一使用 /api 前缀的路由
        // 前端请求 /api/xxx，Vite 代理默认保留路径，转发 /api/xxx 到后端
        // 后端路由是 api/xxx，所以直接转发即可（不需要 rewrite）
        '/api': {
          target: env.VITE_API_TARGET || 'https://localhost:60071',
          changeOrigin: true,
          secure: false // 允许自签名证书
        },
        // SignalR Hub 代理配置
        // 前端请求 /hubs/xxx，Vite 代理转发到后端的 /hubs/xxx
        '/hubs': {
          target: env.VITE_API_TARGET || 'https://localhost:60071',
          changeOrigin: true,
          secure: false, // 允许自签名证书
          ws: true // 启用 WebSocket 代理（SignalR 需要）
        }
      }
    },
    preview: {
      port: Number(env.VITE_DEV_SERVER_PORT) || 60081,
      host: env.VITE_DEV_SERVER_HOST || 'localhost',
      // Vite preview 模式默认支持 history fallback（所有非静态资源请求重定向到 index.html）
      // 这确保了预览构建产物时刷新页面不会出现 404 错误
      // 
      // 注意：生产环境部署时，需要在 Web 服务器（Nginx/Apache/IIS）中配置 history fallback
      // 参考项目根目录下的配置文件：
      // - nginx.conf.example (Nginx)
      // - .htaccess.example (Apache)
      // - web.config.example (IIS)
    },
    build: {
      target: 'es2015',
      outDir: 'dist',
      assetsDir: 'assets',
      sourcemap: env.VITE_BUILD_SOURCEMAP === 'true',
      chunkSizeWarningLimit: 1000,
      minify: env.VITE_BUILD_COMPRESS !== 'false' ? 'terser' : false,
      // 可以根据依赖信息进行优化配置
      rollupOptions: {
        output: {
          // 可以根据依赖自动配置 chunk 分割策略
          manualChunks: (id) => {
            // 将 node_modules 中的依赖分离
            if (id.includes('node_modules')) {
              // 将 Vue 相关依赖单独打包
              if (id.includes('vue') || id.includes('vue-router') || id.includes('pinia')) {
                return 'vue-vendor'
              }
              // 将 Ant Design Vue 相关依赖单独打包
              if (id.includes('ant-design-vue') || id.includes('@ant-design')) {
                return 'antd-vendor'
              }
              // 其他第三方依赖
              return 'vendor'
            }
          }
        }
      }
    }
  }
})
